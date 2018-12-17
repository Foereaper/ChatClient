using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Client.Authentication;
//@todo: Add 60 second 'timeout' since ping/pong is implemented

namespace Client.World.Network
{
    public partial class WorldSocket : GameSocket
    {
        WorldServerInfo ServerInfo;

        private long transferred;
        public long Transferred => transferred;

        private long sent;
        public long Sent => sent;

        private long received;
        public long Received => received;

        public override string LastOutOpcodeName => LastOutOpcode?.ToString();

        public WorldCommand? LastOutOpcode
        {
            get;
            protected set;
        }
        public override DateTime LastOutOpcodeTime => _lastOutOpcodeTime;
        protected DateTime _lastOutOpcodeTime;
        public override string LastInOpcodeName => LastInOpcode?.ToString();

        public WorldCommand? LastInOpcode
        {
            get;
            protected set;
        }
        public override DateTime LastInOpcodeTime => _lastInOpcodeTime;
        protected DateTime _lastInOpcodeTime;

        BatchQueue<InPacket> packetsQueue = new BatchQueue<InPacket>();

        public WorldSocket(IGame program, WorldServerInfo serverInfo)
        {
            Game = program;
            ServerInfo = serverInfo;
        }

        #region Handler registration

        Dictionary<WorldCommand, PacketHandler> PacketHandlers;

        public override void InitHandlers()
        {
            PacketHandlers = new Dictionary<WorldCommand, PacketHandler>();

            RegisterHandlersFrom(this);
            RegisterHandlersFrom(Game);
        }

        void RegisterHandlersFrom(object obj)
        {
            // create binding flags to discover all non-static methods
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var method in obj.GetType().GetMethods(flags))
            {
                if (!method.TryGetAttributes(false, out IEnumerable<PacketHandlerAttribute> attributes))
                    continue;

                var handler = (PacketHandler)Delegate.CreateDelegate(typeof(PacketHandler), obj, method);

                foreach (var attribute in attributes)
                {
                    Game.UI.LogDebug($"Registered '{obj.GetType().Name}.{method.Name}' to '{attribute.Command}'");
                    PacketHandlers[attribute.Command] = handler;
                }
            }
        }

        #endregion

        #region Asynchronous Reading

        int Index;
        int Remaining;
        
        private void ReadAsync(EventHandler<SocketAsyncEventArgs> callback, object state = null)
        {
            if (Disposing)
                return;

            SocketAsyncState = state;
            SocketArgs.SetBuffer(ReceiveData, Index, Remaining);
            SocketCallback = callback;
            connection.Client.ReceiveAsync(SocketArgs);
        }

        /// <summary>
        /// Determines how large the incoming header will be by
        /// inspecting the first byte, then initiates reading the header.
        /// </summary>
        private void ReadSizeCallback(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var bytesRead = e.BytesTransferred;
                if (bytesRead == 0)
                {
                    // TODO: world server disconnect
                    Game.Reconnect();
                    return;
                }

                Interlocked.Increment(ref transferred);
                Interlocked.Increment(ref received);

                authenticationCrypto.Decrypt(ReceiveData, 0, 1);
                if ((ReceiveData[0] & 0x80) != 0)
                {
                    // need to resize the buffer
                    var temp = ReceiveData[0];
                    ReserveData(5);
                    ReceiveData[0] = (byte)((0x7f & temp));

                    Remaining = 4;
                }
                else
                    Remaining = 3;

                Index = 1;
                ReadAsync(ReadHeaderCallback);
            }
            // these exceptions can happen as race condition on shutdown
            catch(ObjectDisposedException ex)
            {
                Game.UI.LogException(ex);
            }
            catch(NullReferenceException ex)
            {
                Game.UI.LogException(ex);
            }
            catch(InvalidOperationException ex)
            {
                Game.UI.LogException(ex);
            }
            catch(SocketException /*ex*/)
            {
                Game.Reconnect();
            }
        }

        /// <summary>
        /// Reads the rest of the incoming header.
        /// </summary>
        private void ReadHeaderCallback(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var bytesRead = e.BytesTransferred;
                if (bytesRead == 0)
                {
                    // TODO: world server disconnect
                    Game.Reconnect();
                    return;
                }

                Interlocked.Add(ref transferred, bytesRead);
                Interlocked.Add(ref received, bytesRead);

                if (bytesRead == Remaining)
                {
                    // finished reading header
                    // the first byte was decrypted already, so skip it
                    authenticationCrypto.Decrypt(ReceiveData, 1, ReceiveDataLength - 1);
                    var header = new ServerHeader(ReceiveData, ReceiveDataLength);

                    Game.UI.LogDebug(header.ToString());
                    if (header.InputDataLength > 5 || header.InputDataLength < 4)
                        Game.UI.LogException($"Header.InputDataLength invalid: {header.InputDataLength}");

                    if (header.Size > 0)
                    {
                        // read the packet payload
                        Index = 0;
                        Remaining = header.Size;
                        ReserveData(header.Size);
                        ReadAsync(ReadPayloadCallback, header);
                    }
                    else
                    {
                        // the packet is just a header, start next packet
                        QueuePacket(new InPacket(header));
                        Start();
                    }
                }
                else
                {
                    // more header to read
                    Index += bytesRead;
                    Remaining -= bytesRead;
                    ReadAsync(ReadHeaderCallback);
                }
            }
            // these exceptions can happen as race condition on shutdown
            catch (ObjectDisposedException ex)
            {
                Game.UI.LogException(ex);
            }
            catch (NullReferenceException ex)
            {
                Game.UI.LogException(ex);
            }
            catch (SocketException ex)
            {
                Game.UI.LogException(ex);
            }
        }

        /// <summary>
        /// Reads the payload data.
        /// </summary>
        private void ReadPayloadCallback(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                var bytesRead = e.BytesTransferred;
                if (bytesRead == 0)
                {
                    // TODO: world server disconnect
                    Game.Reconnect();
                    return;
                }

                Interlocked.Add(ref transferred, bytesRead);
                Interlocked.Add(ref received, bytesRead);

                if (bytesRead == Remaining)
                {
                    // get header and packet, handle it
                    var header = (ServerHeader)SocketAsyncState;
                    QueuePacket(new InPacket(header, ReceiveData, ReceiveDataLength));

                    // start new asynchronous read
                    Start();
                }
                else
                {
                    // more payload to read
                    Index += bytesRead;
                    Remaining -= bytesRead;
                    ReadAsync(ReadPayloadCallback, SocketAsyncState);
                }
            }
            catch(NullReferenceException ex)
            {
                Game.UI.LogException(ex);
            }
            catch(SocketException ex)
            {
                Game.UI.LogException(ex);
                Game.Reconnect();
            }
        }

        #endregion

        public void HandlePackets()
        {
            foreach (var packet in packetsQueue.BatchDequeue())
                HandlePacket(packet);
        }

        private void HandlePacket(InPacket packet)
        {
            try
            {
                LastInOpcode = packet.Header.Command;
                _lastInOpcodeTime = DateTime.Now;
                if (PacketHandlers.TryGetValue(packet.Header.Command, out var handler))
                {
                    Game.UI.LogDebug($"Received {packet.Header.Command}");
                    handler(packet);
                }
                else
                    return;

                Game.HandleTriggerInput(TriggerActionType.Opcode, packet);
            }
            catch(Exception ex)
            {
                Game.UI.LogException(ex);
            }
            finally
            {
                packet.Dispose();
            }
        }

        private void QueuePacket(InPacket packet)
        {
            packetsQueue.Enqueue(packet);
        }

        #region GameSocket Members

        public override void Start()
        {
            ReserveData(4, true);
            Index = 0;
            Remaining = 1;
            ReadAsync(ReadSizeCallback);
        }

        public override bool Connect()
        {
            try
            {
                if (connection != null)
                    connection.Close();
                connection = new TcpClient(ServerInfo.Address, ServerInfo.Port);
            }
            catch (SocketException /*ex*/)
            {
                return false;
            }

            return true;
        }

        #endregion

        public void Send(OutPacket packet)
        {
            LastOutOpcode = packet.Header.Command;
            _lastOutOpcodeTime = DateTime.Now;
            var data = packet.Finalize(authenticationCrypto);

            try
            {
                connection.Client.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch(ObjectDisposedException ex)
            {
                Game.UI.LogException(ex);
            }
            catch(EndOfStreamException ex)
            {
                Game.UI.LogException(ex);
            }

            Interlocked.Add(ref transferred, data.Length);
            Interlocked.Add(ref sent, data.Length);
        }
    }
}

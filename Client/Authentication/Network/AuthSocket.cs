using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Client.Crypto;
using HashAlgorithm = Client.Crypto.HashAlgorithm;

namespace Client.Authentication.Network
{
    public class AuthSocket : GameSocket
    {
        public BigInteger Key { get; private set; }
        byte[] m2;

        NetworkStream stream;

        private string Username;
        private byte[] PasswordHash;

        private string Hostname;
        private int Port;
        //int failedAuthentications;
        //const int MAX_FAILED_AUTENTICATIONS = 10;

        Dictionary<AuthCommand, CommandHandler> Handlers;

        public override string LastOutOpcodeName => LastOutOpcode?.ToString();

        public AuthCommand? LastOutOpcode
        {
            get;
            protected set;
        }
        public override DateTime LastOutOpcodeTime => _lastOutOpcodeTime;
        protected DateTime _lastOutOpcodeTime;
        public override string LastInOpcodeName => LastInOpcode?.ToString();

        public AuthCommand? LastInOpcode
        {
            get;
            protected set;
        }
        public override DateTime LastInOpcodeTime => _lastInOpcodeTime;
        protected DateTime _lastInOpcodeTime;

        public AuthSocket(IGame program, string hostname, int port, string username, string password)
        {
            Game = program;

            Username = username.ToUpper();
            Hostname = hostname;
            Port = port;
            
            var authstring = $"{Username}:{password}";

            PasswordHash = HashAlgorithm.SHA1.Hash(Encoding.ASCII.GetBytes(authstring.ToUpper()));

            ReserveData(1);
        }

        ~AuthSocket()
        {
            Dispose();
        }

        void SendLogonChallenge()
        {
            Game.UI.LogDebug("Sending logon challenge");

            var challenge = new ClientAuthChallenge
            {
                username = Username,
                IP = BitConverter.ToUInt32((connection.Client.LocalEndPoint as IPEndPoint).Address.GetAddressBytes(), 0)
            };

            Send(challenge);
            ReadCommand();
        }

        void Send(ISendable sendable)
        {
            LastOutOpcode = sendable.Command;
            _lastOutOpcodeTime = DateTime.Now;
            sendable.Send(stream);
        }

        void Send(byte[] buffer)
        {
            LastOutOpcode = (AuthCommand)buffer[0];
            _lastOutOpcodeTime = DateTime.Now;
            stream.Write(buffer, 0, buffer.Length);
        }

        #region Handlers

        public override void InitHandlers()
        {
            Handlers = new Dictionary<AuthCommand, CommandHandler>
            {
                [AuthCommand.LOGON_CHALLENGE] = HandleRealmLogonChallenge,
                [AuthCommand.LOGON_PROOF] = HandleRealmLogonProof,
                [AuthCommand.REALM_LIST] = HandleRealmList
            };

        }

        private void HandleRealmLogonChallenge()
        {
            var challenge = new ServerAuthChallenge(new BinaryReader(connection.GetStream()));

            switch (challenge.error)
            {
                case AuthResult.SUCCESS:
                {
                    Game.UI.LogDebug("Received logon challenge");

                    BigInteger A, a;
                    var k = new BigInteger(3);

                    #region Receive and initialize

                    var B = challenge.B.ToBigInteger();
                    var g = challenge.g.ToBigInteger();
                    var N = challenge.N.ToBigInteger();
                    var salt = challenge.salt.ToBigInteger();
                    var unk1 = challenge.unk3.ToBigInteger();

                    #endregion

                    #region Hash password

                    var x = HashAlgorithm.SHA1.Hash(challenge.salt, PasswordHash).ToBigInteger();

                    #endregion

                    #region Create random key pair

                    var rand = RandomNumberGenerator.Create();

                    do
                    {
                        var randBytes = new byte[19];
                        rand.GetBytes(randBytes);
                        a = randBytes.ToBigInteger();

                        A = g.ModPow(a, N);
                    } while (A.ModPow(1, N) == 0);

                    #endregion

                    #region Compute session key

                    var u = HashAlgorithm.SHA1.Hash(A.ToCleanByteArray(), B.ToCleanByteArray()).ToBigInteger();

                    // compute session key
                    var S = ((B + k * (N - g.ModPow(x, N))) % N).ModPow(a + (u * x), N);
                    var sData = S.ToCleanByteArray();
                    if (sData.Length < 32)
                    {
                        var tmpBuffer = new byte[32];
                        Buffer.BlockCopy(sData, 0, tmpBuffer, 32 - sData.Length, sData.Length);
                        sData = tmpBuffer;
                    }
                    var keyData = new byte[40];
                    var temp = new byte[16];

                    // take every even indices byte, hash, store in even indices
                    for (var i = 0; i < 16; ++i)
                        temp[i] = sData[i * 2];
                    var keyHash = HashAlgorithm.SHA1.Hash(temp);
                    for (var i = 0; i < 20; ++i)
                        keyData[i * 2] = keyHash[i];

                    // do the same for odd indices
                    for (var i = 0; i < 16; ++i)
                        temp[i] = sData[i * 2 + 1];
                    keyHash = HashAlgorithm.SHA1.Hash(temp);
                    for (var i = 0; i < 20; ++i)
                        keyData[i * 2 + 1] = keyHash[i];

                    Key = keyData.ToBigInteger();

                    #endregion

                    #region Generate crypto proof

                    // XOR the hashes of N and g together
                    var gNHash = new byte[20];

                    var nHash = HashAlgorithm.SHA1.Hash(N.ToCleanByteArray());
                    for (var i = 0; i < 20; ++i)
                        gNHash[i] = nHash[i];

                    var gHash = HashAlgorithm.SHA1.Hash(g.ToCleanByteArray());
                    for (var i = 0; i < 20; ++i)
                        gNHash[i] ^= gHash[i];

                    // hash username
                    var userHash = HashAlgorithm.SHA1.Hash(Encoding.ASCII.GetBytes(Username));

                    // our proof
                    var m1Hash = HashAlgorithm.SHA1.Hash
                    (
                        gNHash,
                        userHash,
                        challenge.salt,
                        A.ToCleanByteArray(),
                        B.ToCleanByteArray(),
                        Key.ToCleanByteArray()
                    );

                    // expected proof for server
                    m2 = HashAlgorithm.SHA1.Hash(A.ToCleanByteArray(), m1Hash, keyData);

                    #endregion

                    #region Send proof

                    var proof = new ClientAuthProof
                    {
                        A = A.ToCleanByteArray(),
                        M1 = m1Hash,
                        crc = new byte[20]
                    };

                     Send(proof);

                    #endregion

                    break;
                }
                case AuthResult.NO_MATCH:
                    Game.UI.AuthError("Unknown Username/Password combination.");
                    //Game.UI.LogLine("Unknown account name", LogLevel.Error);
                    break;
                case AuthResult.ACCOUNT_BANNED:
                    Game.UI.AuthError("Your account is banned.");
                    break;
                default:
                    Game.UI.AuthError("Login failed.");
                    break;
            }

            // get next command
            ReadCommand();
        }

        void HandleRealmLogonProof()
        {
            var proof = new ServerAuthProof(new BinaryReader(connection.GetStream()));

            switch (proof.error)
            {
                case AuthResult.SUCCESS:
                    break;
                case AuthResult.NO_MATCH:
                    Game.UI.AuthError("Unknown Username/Password combination.");
                    //Game.UI.LogLine("Unknown account name", LogLevel.Error);
                    break;
                case AuthResult.ACCOUNT_BANNED:
                    Game.UI.AuthError("Your account is banned.");
                    break;
                default:
                    Game.UI.AuthError("Login failed.");
                    break;
            }

            if (proof.error != AuthResult.SUCCESS)
            {
                //Game.Reconnect();
                return;
            }

            var equal = true;
            equal = m2 != null && m2.Length == 20;
            for (var i = 0; i < m2.Length && equal; ++i)
                if (!(equal = m2[i] == proof.M2[i]))
                    break;

            if (!equal)
            {
                SendLogonChallenge();
                return;
            }

            Send(new byte[] { (byte)AuthCommand.REALM_LIST, 0x0, 0x0, 0x0, 0x0 });

            // get next command
            ReadCommand();
        }

        private void HandleRealmList()
        {
            //connection.
            var reader = new BinaryReader(connection.GetStream());

            uint size = reader.ReadUInt16();
            var realmList = new WorldServerList(reader);
            Game.UI.PresentRealmList(realmList);
        }

        #endregion

        #region GameSocket Members

        public override void Start()
        {
            ReadCommand();
        }

        private void ReadCommand()
        {
            try
            {
                connection.Client.BeginReceive
                (
                    ReceiveData, 0, 1,    // buffer and buffer bounds
                    SocketFlags.None,    // flags for the read
                    ReadCallback,    // callback to handle completion
                    null                // state object
                );
            }
            catch (Exception /*ex*/)
            {
                // ignored
            }
        }

        protected void ReadCallback(IAsyncResult result)
        {
            try
            {
                var size = connection.Client.EndReceive(result);

                if (size == 0)
                {
                    Game.Exit();
                }

                var command = (AuthCommand)ReceiveData[0];
                LastInOpcode = command;
                _lastInOpcodeTime = DateTime.Now;

                if (Handlers.TryGetValue(command, out var handler))
                    handler();
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
                Game.Reconnect();
            }
            catch(EndOfStreamException)
            {
                Game.Reconnect();
            }
        }

        public override bool Connect()
        {
            try
            {
                connection = new TcpClient(Hostname, Port);
                stream = connection.GetStream();
                SendLogonChallenge();
            }
            catch (SocketException /*ex*/)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}

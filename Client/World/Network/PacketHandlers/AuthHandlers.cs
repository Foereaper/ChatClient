using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Client.Crypto;
using HashAlgorithm = Client.Crypto.HashAlgorithm;

namespace Client.World.Network
{
    public partial class WorldSocket
    {
        [PacketHandler(WorldCommand.ServerAuthChallenge)]
        protected void HandleServerAuthChallenge(InPacket packet)
        {
            var one = packet.ReadUInt32();
            var seed = packet.ReadUInt32();

            var seed1 = packet.ReadBytes(16).ToBigInteger();
            var seed2 = packet.ReadBytes(16).ToBigInteger();

            var rand = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rand.GetBytes(bytes);
            var ourSeed = bytes.ToBigInteger();

            uint zero = 0;

            var authResponse = HashAlgorithm.SHA1.Hash
            (
                Encoding.ASCII.GetBytes(Game.Username.ToUpper()),
                BitConverter.GetBytes(zero),
                BitConverter.GetBytes((uint)ourSeed),
                BitConverter.GetBytes(seed),
                Game.Key.ToCleanByteArray()
            );

            var response = new OutPacket(WorldCommand.ClientAuthSession);
            response.Write((uint)12340);        // client build
            response.Write(zero);
            response.Write(Game.Username.ToUpper().ToCString());
            response.Write(zero);
            response.Write((uint)ourSeed);
            response.Write(zero);
            response.Write(zero);
            response.Write(ServerInfo.ID);
            response.Write((ulong)zero);
            response.Write(authResponse);
            response.Write(zero);            // length of addon data

            Send(response);

            // TODO: don't fully initialize here, auth may fail
            // instead, initialize in HandleServerAuthResponse when auth succeeds
            // will require special logic in network code to correctly decrypt/parse packet header
            authenticationCrypto.Initialize(Game.Key.ToCleanByteArray());
        }

        [PacketHandler(WorldCommand.ServerAuthResponse)]
        protected void HandleServerAuthResponse(InPacket packet)
        {
            var detail = (CommandDetail)packet.ReadByte();

            var billingTimeRemaining = packet.ReadUInt32();
            var billingFlags = packet.ReadByte();
            var billingTimeRested = packet.ReadUInt32();
            var expansion = packet.ReadByte();

            if (detail == CommandDetail.AUTH_OK)
            {
                var request = new OutPacket(WorldCommand.CMSG_CHAR_ENUM);
                Send(request);
            }
            else
            {
                Game.UI.Exit();
            }
        }

        [PacketHandler(WorldCommand.SMSG_CHAR_ENUM)]
        protected void HandleCharEnum(InPacket packet)
        {
            var count = packet.ReadByte();

            if (count == 0)
            {
                Game.NoCharactersFound();
            }
            else
            {
                var characters = new Character[count];
                for (byte i = 0; i < count; ++i)
                    characters[i] = new Character(packet);

                Game.UI.PresentCharacterList(characters);
            }
        }
    }
}
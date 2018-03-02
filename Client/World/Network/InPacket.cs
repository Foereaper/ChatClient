﻿using Client.World.Definitions;
using System;
using System.IO;
using System.IO.Compression;


namespace Client.World.Network
{
    public class InPacket : BinaryReader, Packet
    {
        public Header Header { get; private set; }

        internal InPacket(Header header)
            : this(header, new byte[] { }, 0)
        {

        }

        internal InPacket(Header header, byte[] buffer, int bufferLength)
            : base(new MemoryStream(buffer, 0, bufferLength, false, false))
        {
            Header = header;
            
            string hh = header.ToString();

            if (!hh.Contains("SMSG_MONSTER_MOVE"))
            {
                AutomatedGame.DbgLogging = AutomatedGame.DbgLogging + hh + "\r\n";
            } 

            

        }

        public ulong ReadPackedGuid()
        {
            var mask = ReadByte();

            if (mask == 0)
                return (ulong)0;

            ulong res = 0;

            var i = 0;
            while (i < 8)
            {
                if ((mask & 1 << i) != 0)
                    res += (ulong)ReadByte() << (i * 8);

                i++;
            }

            return res;
        }

        public DateTime ReadPackedTime()
        {
            var packedDate = ReadInt32();
            var minute = packedDate & 0x3F;
            var hour = (packedDate >> 6) & 0x1F;
            // var weekDay = (packedDate >> 11) & 7;
            var day = (packedDate >> 14) & 0x3F;
            var month = (packedDate >> 20) & 0xF;
            var year = (packedDate >> 24) & 0x1F;
            // var something2 = (packedDate >> 29) & 3; always 0

            return new DateTime(2000, 1, 1).AddYears(year).AddMonths(month).AddDays(day).AddHours(hour).AddMinutes(minute);
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(ReadSingle(), ReadSingle(), ReadSingle());
        }

        public byte[] ReadToEnd()
        {
            var length = (int)(BaseStream.Length - BaseStream.Position);
            return ReadBytes(length);
        }

        public override string ToString()
        {
            return Header.Command.ToString();
        }

        public InPacket Inflate()
        {
            uint uncompressedSize = ReadUInt32();
            //Skip first 2 bytes used by zlib only
            ReadBytes(2);

            using (DeflateStream decompressedStream = new DeflateStream(BaseStream, CompressionMode.Decompress))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    decompressedStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    return new InPacket(Header, memoryStream.GetBuffer(), (int)memoryStream.Length);
                }
            }
        }
    }
}

﻿using System;
using Client.World.Entities;

namespace Client.World
{
    public static class Utility
    {
        public static bool IsType(this UInt64 guid, HighGuid highGuidType)
        {
            return ((guid & 0xF0F0000000000000) >> 52) == (UInt64)highGuidType;
        }

        public static bool IsPlayer(this UInt64 guid)
        {
            return IsType(guid, HighGuid.Player);
        }

        public static bool IsPet(this UInt64 guid)
        {
            return IsType(guid, HighGuid.Pet);
        }
    }
}

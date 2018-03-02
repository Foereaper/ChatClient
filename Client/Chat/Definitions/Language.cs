﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Chat.Definitions
{
    public enum Language : uint
    {
        Universal = 0,
        Orcish = 1,
        Darnassian = 2,
        Taurahe = 3,
        Dwarvish = 6,
        Common = 7,
        Demonic = 8,
        Titan = 9,
        Thalassian = 10,
        Draconic = 11,
        Kalimag = 12,
        Gnomish = 13,
        Troll = 14,
        Gutterspeak = 33,
        Draenei = 35,
        Zombie = 36,
        GnomishBinary = 37,
        GoblinBinary = 38,
        Addon = uint.MaxValue
    }
}

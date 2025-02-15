﻿using Client.World.Definitions;

namespace Client.World.Entities
{
    public class Player : Unit
    {
        public bool IsGhost => HasFlag(PlayerFlags.PLAYER_FLAGS_GHOST);

        public bool IsAlive => this[UnitField.UNIT_FIELD_HEALTH] > 0 && !IsGhost;

        public bool HasFlag(PlayerFlags flag)
        {
            return (this[PlayerField.PLAYER_FLAGS] & (uint)flag) != 0;
        }

        public Position CorpsePosition
        {
            get;
            set;
        }
    }
}

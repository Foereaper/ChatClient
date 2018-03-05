namespace Client.World.Entities
{
    public class Unit : WorldObject
    {
        public float Speed
        {
            get;
            private set;
        }

        public Unit()
        {
            Speed = 7.0f;
        }
    }
}

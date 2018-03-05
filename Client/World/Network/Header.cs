namespace Client.World.Network
{
    public interface Header
    {
        WorldCommand Command { get; }
        int Size { get; }
    }
}

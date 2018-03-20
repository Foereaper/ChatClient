using System.Numerics;
using System.Threading.Tasks;
using Client.Authentication;
using Client.Chat;
using Client.UI;
using Client.World.Network;

namespace Client
{
    public interface IGame
    {
        BigInteger Key { get; }
        string Username { get; }

        IGameUI UI { get; }

        GameWorld World { get; }

        void ConnectTo(WorldServerInfo server);

        void Start();

        void Reconnect();

        void NoCharactersFound();

        void InvalidCredentials();

        Task Exit();

        void SendPacket(OutPacket packet);

        void PresentChatMessage(ChatMessage message);

        void HandleTriggerInput(TriggerActionType type, params object[] inputs);

        //void Log(string message, LogLevel level = LogLevel.Info);
    }
}

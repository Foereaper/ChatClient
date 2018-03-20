using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Client.Authentication;
using Client.Chat;
using Client.World;

namespace Client.UI
{
    public abstract class IGameUI
    {
        public abstract IGame Game { get; set; }

        public abstract LogLevel LogLevel { get; set; }

        public abstract void Update();
        public abstract Task Exit();

        #region Packet handler presenters

        public abstract void PresentRealmList(WorldServerList realmList);
        public abstract void PresentCharacterList(Character[] characterList);

        public abstract void PresentChatMessage(ChatMessage message);

        #endregion

        #region UI Output

        public abstract void Log(string message, LogLevel level = LogLevel.Info);
        public abstract void LogLine(string message, LogLevel level = LogLevel.Info);
        public abstract void AuthError(string message);
        public abstract void NewMessage(string message);
        public abstract void UpdateGroupList(string message);
        public abstract void UpdateWhoList(string message);
        public abstract void UpdateRoster(string message);
        public abstract void UpdateFriendList(string message);
        public abstract void UpdateDefaultChannelList(string message);
        public abstract void UpdateCustomChannelList(string message);
        public abstract void UpdateTicketList(string message);
        [Conditional("DEBUG")]
        public abstract void LogDebug(string message);
        public abstract void LogException(string message);

        public abstract void LogException(Exception ex);

        #endregion

        #region UI Input

        public abstract string ReadLine();
        public abstract int Read();
        public abstract ConsoleKeyInfo ReadKey();

        #endregion
    }
}

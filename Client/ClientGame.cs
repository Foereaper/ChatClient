using System;
using Client;
using Client.UI;

namespace BotFarm
{
    class ClientInit : AutomatedGame
    {
        public bool SettingUp
        {
            get;
            set;
        }

        public ClientInit(string hostname, int port, string username, string password, int realmId, int character)
            : base(hostname, port, username, password, realmId, character) {}



        public override void Start()
        {
            base.Start();
        }

        #region Logging
        public override void Log(string message, LogLevel level = LogLevel.Info)
        {
            SessionInit.Instance.Log(Username + " - " + message, level);
        }

        public override void AuthError(string message)
        {
            //BotFactory.Instance.Log(Username + " - " + message, level);
            SessionInit.Instance.Log(message);
        }

        public override void LogLine(string message, LogLevel level = LogLevel.Info)
        {
            //BotFactory.Instance.Log(Username + " - " + message, level);
            SessionInit.Instance.Log(message, level);
        }

        public override void LogException(string message)
        {
            SessionInit.Instance.Log(Username + " - " + message, LogLevel.Error);
        }

        public override void LogException(Exception ex)
        {
            SessionInit.Instance.Log(string.Format(Username + " - {0} {1}", ex.Message, ex.StackTrace), LogLevel.Error);
        }
        #endregion
    }
}

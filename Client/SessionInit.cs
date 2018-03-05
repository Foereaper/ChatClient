using Client;
using Client.UI;
using System.Diagnostics;

namespace BotFarm
{
    class SessionInit //, AutomatedGame
    {
        public static SessionInit Instance
        {
            get;
            private set;
        }

        public static string setLogonserver
        {
            set
            {
                Logonserver = value;
            }
            get
            {
                return Logonserver;
            }
        }

        public static string setUsername
        {
            set
            {
                Username = value;
            }
            get
            {
                return Username;
            }
        }

        public static string setPassword
        {
            set
            {
                Password = value;
            }
            get
            {
                return Password;
            }
        }

        private static string Logonserver;
        private static string Username;
        private static string Password;
        public static string Logging;
        
        public AutomatedGame factoryGame;

        public SessionInit()
        {
            
            Instance = this;

            factoryGame = new AutomatedGame(Logonserver,
                                            3724,
                                            Username,
                                            Password,
                                            0,
                                            0);
            factoryGame.Start();
        }

        [Conditional("DEBUG")]
        public void LogDebug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
#if !DEBUG_LOG
            if (level > LogLevel.Debug)
#endif
            {
                Logging = Logging + message + "/n";
                //Console.WriteLine(message);
                //logger.WriteLine(message);
            }
        }
    }
}

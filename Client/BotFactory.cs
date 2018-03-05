using Client;
using Client.UI;
using Client.World.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotFarm
{
    class BotFactory : IDisposable //, AutomatedGame
    {
        public static BotFactory Instance
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

        public static void DisconnectBot()
        {
        }

        //public static void DoWhisperChat()
        //{
        //   BotFactory.Instance.factoryGame.DoWhisperChat("dsg", "fgs");
        //}


        private static string Logonserver;
        private static string Username;
        private static string Password;
        public static string Logging;

        List<BotGame> bots = new List<BotGame>();
        public AutomatedGame factoryGame;
        const string botsInfosPath = "botsinfos.xml";
        const string logPath = "botfactory.log";
        //TextWriter logger;

        public BotFactory()
        {
            
            Instance = this;
            /*
            logger = TextWriter.Synchronized(new StreamWriter(logPath));
            logger.WriteLine("Starting BotFactory");

            if (!File.Exists(botsInfosPath))
                botInfos = new List<BotInfo>();
            else using (StreamReader sr = new StreamReader(botsInfosPath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<BotInfo>));
                    botInfos = (List<BotInfo>)serializer.Deserialize(sr);
                }
                catch(InvalidOperationException)
                {
                    botInfos = new List<BotInfo>();
                }
            }

            */
            //Logonserver;
            //Username;
            //Password;

            factoryGame = new AutomatedGame(Logonserver,
                                            3724,
                                            Username,
                                            Password,
                                            0,
                                            0);
            factoryGame.Start();
        }

        public bool IsBot(WorldObject obj)
        {
            if (factoryGame.Player.GUID == obj.GUID)
                return true;
            return bots.FirstOrDefault(bot => bot.Player.GUID == obj.GUID) != null;
        }

        public void SetupFactory()
        {
            while(!factoryGame.LoggedIn)
            {
                 //Log("Waiting for account to login");
                 Thread.Sleep(1000);
            }

            /*
            base.Start();

            ScheduleAction(() =>
            {
                if (LoggedIn)
                    SendPacket(new OutPacket(WorldCommand.CMSG_KEEP_ALIVE));
            }, DateTime.Now.AddSeconds(15), new TimeSpan(0, 0, 30)); */

            /*
            Log("Setting up bot factory with " + botCount + " bots");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            */

            /*
            int createdBots = 0;
            List<BotInfo> infos;
            if (Settings.Default.RandomBots)
                infos = botInfos.TakeRandom(botCount).ToList();
            else
                infos = botInfos.Take(botCount).ToList();
            Parallel.ForEach<BotInfo>(infos, info =>
            {
                var bot = LoadBot(info);
                lock(bots)
                    bots.Add(bot);
                Interlocked.Increment(ref createdBots);
            });*/

            /*
            Parallel.For(createdBots, botCount, index =>
            {
                try
                {
                    var bot = CreateBot();
                    lock (bots)
                    {
                        bots.Add(bot);
                        if (bots.Count % 100 == 0)
                            SaveBotInfos();
                    }
                }
                catch(Exception ex)
                {
                    Log("Error creating new bot: " + ex.Message + "\n" + ex.StackTrace, LogLevel.Error);
                }
            });*/

            /*
            watch.Stop();
            Log("Finished setting up bot factory with " + botCount + " bots in " + watch.Elapsed);

            SaveBotInfos();*/

            /*
            for (; ; )
            {
                string line = Console.ReadLine();
                if (line == null)
                    return;
                string[] lineSplit = line.Split(' ');
                switch(lineSplit[0])
                {
                    case "/w":
                        string wisperdata = Console.ReadLine();
                        string[] str = wisperdata.Split(' ');
                        BotFactory.Instance.factoryGame.DoWhisperChat(str[1], str[0]);
                        break;
                    case "/s":
                        BotFactory.Instance.factoryGame.DoSayChat("dingding");
                        break;
                    case "/g":
                        string test = "yo whatsup";
                        BotFactory.Instance.factoryGame.DoGuildChat(test);
                        //BotFactory.Instance.factoryGame.DoTextEmote(TextEmote.Yawn);
                        break;
                    case "quit":
                    case "exit":
                    case "close":
                    case "shutdown":
                        return;
                    case "info":
                        Log("dumping chat;");
                        //Client World gameworld LastWhisperers QueuedChatMessages
                        //Gameworld.LastWhisperers;

                        Log(BotFactory.Instance.factoryGame.LastReceivedPacket.ToString());
                        BotFactory.Instance.factoryGame.DoWhisperChat("pauper hond", "Biotech");
                        break;
                    case "infos":
                    case "stats":
                    case "statistics":
                        DisplayStatistics(lineSplit.Length > 1 ? lineSplit[1] : "");
                        break;
                }
            }*/
        }

        void DisplayStatistics(string botname)
        {
            if (String.IsNullOrEmpty(botname))
            {
                foreach (var bot in bots)
                    DisplayStatistics(bot);

                // Display stats about all bots
                Console.WriteLine(bots.Where(bot => bot.Running).Count() + " bots are active");
                Console.WriteLine(bots.Where(bot => bot.Connected).Count() + " bots are connected");
                Console.WriteLine(bots.Where(bot => bot.LoggedIn).Count() + " bots are ingame");
            }
            else
            {
                // Display stats about a single bot
                var bot = bots.SingleOrDefault(b => b.Username.Equals(botname, StringComparison.InvariantCultureIgnoreCase));
                if (bot == null)
                    Console.WriteLine("Bot with username '" + botname + "' not found");
                else
                    DisplayStatistics(bot);
            }
        }

        void DisplayStatistics(BotGame bot)
        {
            Console.WriteLine("Bot username: " + bot.Username);
            Console.WriteLine("\tRunning: " + bot.Running);
            Console.WriteLine("\tConnected: " + bot.Connected);
            Console.WriteLine("\tLogged In: " + bot.LoggedIn);
            Console.WriteLine("\tPosition: " + bot.Player.GetPosition());
            //if (bot.GroupLeaderGuid == 0)
            //    Console.WriteLine("\tGroup Leader: " + "Not in group");
            //else if (!bot.World.PlayerNameLookup.ContainsKey(bot.GroupLeaderGuid))
            //    Console.WriteLine("\tGroup Leader: " + "Not found");
            //else
            //    Console.WriteLine("\tGroup Leader: " + bot.World.PlayerNameLookup[bot.GroupLeaderGuid]);
            Console.WriteLine("\tLast Received Packet: " + bot.LastReceivedPacket);
            Console.WriteLine("\tLast Received Packet Time: " + bot.LastReceivedPacketTime.ToLongTimeString());
            Console.WriteLine("\tLast Sent Packet: " + bot.LastSentPacket);
            Console.WriteLine("\tLast Sent Packet Time: " + bot.LastSentPacketTime.ToLongTimeString());
            Console.WriteLine("\tLast Update() call: " + bot.LastUpdate.ToLongTimeString());
            Console.WriteLine("\tSchedule Actions: " + bot.ScheduledActionsCount);
        }

        public void Dispose()
        {
            Log("Shutting down BotFactory");
            Log("This might take at least 20 seconds to allow all bots to properly logout");

            List<Task> botsDisposing = new List<Task>(bots.Count);
            foreach (var bot in bots)
                botsDisposing.Add(bot.Dispose());

            Task.WaitAll(botsDisposing.ToArray(), new TimeSpan(0, 2, 0));

            factoryGame.Dispose().Wait();

            //logger.Dispose();
            //logger = null;
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

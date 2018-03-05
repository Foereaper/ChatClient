using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.UI;
using Client.Authentication;
using Client.World;
using Client.Chat;
using Client.World.Network;
using Client.Authentication.Network;
using System.Threading;
using Client.Chat.Definitions;
using Client.World.Definitions;
using Client.World.Entities;

namespace Client
{
    public class AutomatedGame : IGameUI, IGame
    {

        public static bool IsLoggedIn() // yes
        {
            try
            {
                return LoggedInserver;
            }
            catch
            {
                return false;
            }
        }
        public static List<string> ShowRealmList // yes
        {
            get
            {
                return presentrealmList;
            }
        }

        public static List<string> ShowCharList // yes
        {
            get
            {
                return presentcharacterList;
            }
        }

        public static bool RealmChosen // yes
        {
            set
            {
                realmchosen = value;
            }
        }
        public static int RealmIDgui // yes
        {
            set
            {
                realmidGUI = value;
            }
        }

        public static bool CharacterChosen // yes
        {
            set
            {
                characterchosen = value;
            }
        }
        public static int CharacterID // yes
        {
            set
            {
                characterID = value;
            }
        }

        public static bool DisconClient // yes
        {
            set
            {
                disconnectclient = value;
            }
        }

        public static List<string> player = new List<string>();
        public static List<string> guild = new List<string>();
        public static List<int> level = new List<int>();
        public static List<string> pclass = new List<string>();
        public static List<string> prace = new List<string>();
        public static List<string> pzone = new List<string>();
        public static int playersonline;

        public Race Race { get; private set; }
        public Class Class { get; private set; }

        public static string NewMessageData;
        public static string UpdateGroupGUIDList;
        public static string WhoListUpdate;
        public static string FriendListUpdate;
        public static string DefaultChannelListUpdate;
        public static string CustomChannelListUpdate;

        public static List<string> presentrealmList = new List<string>();
        public static List<string> presentcharacterList = new List<string>();
        public static List<string> characterNameList = new List<string>();
        private static bool LoggedInserver;
        public static bool AuthenticationError = false;
        public static string AuthErrorText;
        public static bool disconnectclient = false;
        public static int realmidGUI;
        public static bool realmchosen;
        public static int characterID;
        public static bool characterchosen;
        public static bool Charsloaded;
        private static string chanNum;
        public static string LastAddedFriend;
        public static string LastRemovedFriend;

        public int JoinMessage = 0;

        public static List<string> resolvedFriendList = new List<string>();
        public static List<string> friendGUIList = new List<string>();

        public static List<string> joinedChannels = new List<string>();
        public static List<string> customChannels = new List<string>();

        //public static List<string> OnlineFriends = new List<string>();

        #region Properties
        public bool Running { get; set; }
        GameSocket socket;
        public System.Numerics.BigInteger Key { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool LoggedIn { get; private set; }
        public int RealmID { get; private set; }
        public int Character { get; private set; }
        public bool Connected { get; private set; }
        public string LastSentPacket
        {
            get
            {
                return socket.LastOutOpcodeName;
            }
        }
        public DateTime LastSentPacketTime
        {
            get
            {
                return socket.LastOutOpcodeTime;
            }
        }
        public string LastReceivedPacket
        {
            get
            {
                return socket.LastInOpcodeName;
            }
        }
        public DateTime LastReceivedPacketTime
        {
            get
            {
                return socket.LastInOpcodeTime;
            }
        }
        public DateTime LastUpdate
        {
            get;
            private set;
        }
        TaskCompletionSource<bool> loggedOutEvent = new TaskCompletionSource<bool>();
        public int ScheduledActionsCount => scheduledActions.Count;
        ScheduledActions scheduledActions;
        ActionFlag disabledActions;
        int scheduledActionCounter;
        public GameWorld World
        {
            get;
            private set;
        }
        public Player Player
        {
            get;
            protected set;
        }
        public override LogLevel LogLevel
        {
            get
            {
                return Client.UI.LogLevel.Error;
            }
            set
            {
            }
        }
        public override IGame Game
        {
            get
            {
                return this;
            }
            set
            {
            }
        }

        public Dictionary<ulong, WorldObject> Objects
        {
            get;
            private set;
        }

        protected HashSet<uint> CompletedAchievements
        {
            get;
            private set;
        }
        protected Dictionary<uint, ulong> AchievementCriterias
        {
            get;
            private set;
        }
        protected bool HasExploreCriteria(uint criteriaId)
        {
            ulong counter;
            if (AchievementCriterias.TryGetValue(criteriaId, out counter))
                return counter > 0;
            return false;
        }

        public UInt64 GroupLeaderGuid { get; private set; }
        public List<UInt64> GroupMembersGuids = new List<UInt64>();
        public List<UInt64> GroupMembersGuids2 = new List<UInt64>();
        #endregion

        public AutomatedGame(string hostname, int port, string username, string password, int realmId, int character)
        {
            this.RealmID = realmId;
            this.Character = character;
            scheduledActions = new ScheduledActions();
            Triggers = new IteratedList<Trigger>();
            World = new GameWorld();
            Player = new Player();
            Player.OnFieldUpdated += OnFieldUpdate;
            Objects = new Dictionary<ulong, WorldObject>();
            CompletedAchievements = new HashSet<uint>();
            AchievementCriterias = new Dictionary<uint, ulong>();

            this.Hostname = hostname;
            this.Port = port;
            this.Username = username;
            this.Password = password;

            socket = new AuthSocket(this, Hostname, Port, Username, Password);
            socket.InitHandlers();
        }

        #region Basic Methods
        public void ConnectTo(WorldServerInfo server)
        {
            if (socket is AuthSocket)
                Key = ((AuthSocket)socket).Key;

            socket.Dispose();

            socket = new WorldSocket(this, server);
            socket.InitHandlers();

            if (socket.Connect())
            {
                socket.Start();
                Connected = true;
            }
            else
                Reconnect();
        }

        public virtual void Start()
        {
            // the initial socket is an AuthSocket - it will initiate its own asynch read
            Running = socket.Connect();

            Task.Run(async () =>
                {
                    while (Running)
                    {
                        // main loop here
                        Update();
                        await Task.Delay(100);
                    }
                });
        }

        public override void Update()
        {
            LastUpdate = DateTime.Now;

            (socket as WorldSocket)?.HandlePackets();

            if (World.SelectedCharacter == null)
                return;

            if (disconnectclient == true)
            {
                Disconnect();
                return;
            }

            while (scheduledActions.Count != 0)
            {
                var scheduledAction = scheduledActions.First();
                if (scheduledAction.ScheduledTime <= DateTime.Now)
                {
                    scheduledActions.RemoveAt(0, false);
                    if (scheduledAction.Interval > TimeSpan.Zero)
                        ScheduleAction(scheduledAction.Action, DateTime.Now + scheduledAction.Interval, scheduledAction.Interval, scheduledAction.Flags, scheduledAction.Cancel);
                    try
                    {
                        scheduledAction.Action();
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }
                }
                else
                    break;
            }
        }

        public void Reconnect()
        {
            Connected = false;
            LoggedIn = false;
            while (Running)
            {
                socket.Disconnect();
                scheduledActions.Clear();
                ResetTriggers();
                socket = new AuthSocket(this, Hostname, Port, Username, Password);
                socket.InitHandlers();
                // exit from loop if the socket connected successfully
                if (socket.Connect())
                    break;

                // try again later
                Thread.Sleep(10000);
            }
        }

        public void Disconnect()
        {
            if (LoggedIn)
            {
                OutPacket logout = new OutPacket(WorldCommand.CMSG_LOGOUT_REQUEST);
                SendPacket(logout);
            }
            else
            {
                Connected = false;
                LoggedIn = false;
                Running = false;
            }
        }

        public override async Task Exit()
        {
            //ClearTriggers();
            //ClearAIs();
            if (LoggedIn)
            {
                OutPacket logout = new OutPacket(WorldCommand.CMSG_LOGOUT_REQUEST);
                SendPacket(logout);
                await loggedOutEvent.Task;
            }
            else
            {
                Connected = false;
                LoggedIn = false;
                Running = false;
            }
        }

        public void SendPacket(OutPacket packet)
        {
            if (socket is WorldSocket)
            {
                ((WorldSocket)socket).Send(packet);
                HandleTriggerInput(TriggerActionType.Opcode, packet);
            }
        }

        public override void PresentRealmList(WorldServerList realmList)
        {
            /*
            if (RealmID >= realmList.Count)
            {
                LogException("Invalid RealmID '" + RealmID + "' specified in the configs");
                Environment.Exit(1);
            }*/

            LoggedInserver = true;
            AuthenticationError = false;

            // Anti-kick for being afk
            //ScheduleAction(() => DoTextEmote(TextEmote.Yawn), DateTime.Now.AddMinutes(5), new TimeSpan(0, 5, 0));
            ScheduleAction(() =>
            {
                if (LoggedIn)
                    SendPacket(new OutPacket(WorldCommand.CMSG_KEEP_ALIVE));
            }, DateTime.Now.AddSeconds(15), new TimeSpan(0, 0, 30));


            WorldServerInfo selectedServer = null;

            if (realmList.Count == 1)
                selectedServer = realmList[0];
            else
            {
                int index = 0;

                foreach (WorldServerInfo server in realmList)
                {
                    presentrealmList.Add(server.Name);
                }

                // select a realm - default to the first realm if there is only one
                index = realmList.Count == 1 ? 0 : -1;
                selectedServer = realmList[realmidGUI];
            }

            while (realmchosen == false)
            {
                Thread.Sleep(100);
            }

            //LogLine("Connecting to realm " + realmList[RealmID].Name);
            ConnectTo(realmList[realmidGUI]);
        }

        public override void PresentCharacterList(Character[] characterList)
        {

            int index = 0;
            //List<string> charname = new List<string>();

            foreach (Character characterz in characterList)
            {
                presentcharacterList.Add(characterz.Name + " Level: " + characterz.Level + " Race: " + characterz.Race + " Class: " + characterz.Class);
                characterNameList.Add(characterz.Name);
            }

            Charsloaded = true;
            //Thread.Sleep(1000);

            while (characterchosen == false)
            {
                //Thread.Sleep(500);
            }


            int length = characterList.Length == 10 ? 10 : (characterList.Length + 1);
            index = -1;
            /*while (index > length || index < 0)
            {
                Log("Choose a character:  ");
                if (!int.TryParse(Console.ReadLine(), out index))
                    //LogLine("Selected character: " + charname[index].ToString());
            }*/

            if (index < characterList.Length)
            {


                World.SelectedCharacter = characterList[characterID];
                // TODO: enter world
                OutPacket packet = new OutPacket(WorldCommand.CMSG_PLAYER_LOGIN);
                packet.Write(World.SelectedCharacter.GUID);
                SendPacket(packet);
                LoggedIn = true;
                Player.GUID = World.SelectedCharacter.GUID;
            }
            else
            {
                // TODO: character creation
                return;
            }

            /*
            World.SelectedCharacter = characterList[Character];
            OutPacket packet = new OutPacket(WorldCommand.CMSG_PLAYER_LOGIN);
            packet.Write(World.SelectedCharacter.GUID);
            SendPacket(packet);
            LoggedIn = true;
            Player.GUID = World.SelectedCharacter.GUID;
            */
        }

        public override string ReadLine()
        {
            throw new NotImplementedException();
        }

        public override int Read()
        {
            throw new NotImplementedException();
        }

        public override ConsoleKeyInfo ReadKey()
        {
            throw new NotImplementedException();
        }

        public int ScheduleAction(Action action, TimeSpan interval = default(TimeSpan), ActionFlag flags = ActionFlag.None, Action cancel = null)
        {
            return ScheduleAction(action, DateTime.Now, interval, flags, cancel);
        }

        public int ScheduleAction(Action action, DateTime time, TimeSpan interval = default(TimeSpan), ActionFlag flags = ActionFlag.None, Action cancel = null)
        {
            if (Running && (flags == ActionFlag.None || !disabledActions.HasFlag(flags)))
            {
                scheduledActionCounter++;
                scheduledActions.Add(new RepeatingAction(action, cancel, time, interval, flags, scheduledActionCounter));
                return scheduledActionCounter;
            }
            else
                return 0;
        }

        public void CancelActionsByFlag(ActionFlag flag, bool cancel = true)
        {
            scheduledActions.RemoveByFlag(flag, cancel);
        }

        public bool CancelAction(int actionId)
        {
            return scheduledActions.Remove(actionId);
        }

        public void DisableActionsByFlag(ActionFlag flag)
        {
            disabledActions |= flag;
            CancelActionsByFlag(flag);
        }

        public void EnableActionsByFlag(ActionFlag flag)
        {
            disabledActions &= ~flag;
        }

        public async Task Dispose()
        {
            scheduledActions.Clear();

            await Exit();

            if (socket != null)
                socket.Dispose();
        }

        public virtual void NoCharactersFound()
        { }

        public virtual void InvalidCredentials()
        { }

        protected WorldObject FindClosestObject(HighGuid highGuidType, Func<WorldObject, bool> additionalCheck = null)
        {
            float closestDistance = float.MaxValue;
            WorldObject closestObject = null;

            foreach (var obj in Objects.Values)
            {
                if (!obj.IsType(highGuidType))
                    continue;

                if (additionalCheck != null && !additionalCheck(obj))
                    continue;

                if (obj.MapID != Player.MapID)
                    continue;

                float distance = (obj - Player).Length;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = obj;
                }
            }

            return closestObject;
        }

        public string GetPlayerName(WorldObject obj)
        {
            return GetPlayerName(obj.GUID);
        }

        protected string GetPlayerName(ulong guid)
        {
            string name;
            if (Game.World.PlayerNameLookup.TryGetValue(guid, out name))
                return name;
            else
                return "";
        }

        #endregion

        #region Commands
        public void DoSayChat(string message)
        {
            var response = new OutPacket(WorldCommand.CMSG_MESSAGECHAT); //SMSG_GM_MESSAGECHAT // org CMSG_MESSAGECHAT

            response.Write((uint)ChatMessageType.Say);
            var race = World.SelectedCharacter.Race;
            var language = race.IsHorde() ? Language.Orcish : Language.Common;
            response.Write((uint)language);
            response.Write(message.ToCString());
            SendPacket(response);
        }

        public void DoPartyChat(string message)
        {
            var response = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);

            response.Write((uint)ChatMessageType.Party);
            var race = World.SelectedCharacter.Race;
            var language = race.IsHorde() ? Language.Orcish : Language.Common;
            response.Write((uint)language);
            response.Write(message.ToCString());
            SendPacket(response);
        }

        public void RequestGuildList()
        {
            OutPacket response = new OutPacket(WorldCommand.CMSG_GUILD_ROSTER);
            response.Write("");
            Game.SendPacket(response);
        }

        /*
        CMSG_JOIN_CHANNEL = 151,
        CMSG_LEAVE_CHANNEL = 152,
        SMSG_CHANNEL_NOTIFY = 153,
        CMSG_CHANNEL_LIST = 154,
        SMSG_CHANNEL_LIST = 155,
        */

        public void LeaveChannel(int channel)
        {
            byte[] data = null;
            var GeneralBytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x47, 0x65, 0x6E, 0x65, 0x72, 0x61, 0x6C, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var TradeBytes = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x54, 0x72, 0x61, 0x64, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var LocalDefenseBytes = new byte[] { 0x16, 0x00, 0x00, 0x00, 0x01, 0x00, 0x4C, 0x6F, 0x63, 0x61, 0x6C, 0x44, 0x65, 0x66, 0x65, 0x6E, 0x73, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var LookingForGroupBytes = new byte[] { 0x1A, 0x00, 0x00, 0x00, 0x01, 0x00, 0x4C, 0x6F, 0x6F, 0x6B, 0x69, 0x6E, 0x67, 0x46, 0x6F, 0x72, 0x47, 0x72, 0x6F, 0x75, 0x70, 0x00, 0x00 };

            switch (channel)
            {
                case 1:
                    data = GeneralBytes;
                    break;
                case 2:
                    data = TradeBytes;
                    break;
                case 3:
                    data = LocalDefenseBytes;
                    break;
                case 4:
                    data = LookingForGroupBytes;
                    break;
                default:
                    data = GeneralBytes;
                    break;
            }

            OutPacket packet = new OutPacket(WorldCommand.CMSG_LEAVE_CHANNEL);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void JoinChannel(int channel)
        {
            byte[] data = null;
            var GeneralBytes = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x47, 0x65, 0x6E, 0x65, 0x72, 0x61, 0x6C, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var TradeBytes = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x54, 0x72, 0x61, 0x64, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var LocalDefenseBytes = new byte[] { 0x16, 0x00, 0x00, 0x00, 0x01, 0x00, 0x4C, 0x6F, 0x63, 0x61, 0x6C, 0x44, 0x65, 0x66, 0x65, 0x6E, 0x73, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00, 0x00 };
            var LookingForGroupBytes = new byte[] { 0x1A, 0x00, 0x00, 0x00, 0x01, 0x00, 0x4C, 0x6F, 0x6F, 0x6B, 0x69, 0x6E, 0x67, 0x46, 0x6F, 0x72, 0x47, 0x72, 0x6F, 0x75, 0x70, 0x00, 0x00 };

            switch (channel)
            {
                case 1:
                    data = GeneralBytes;
                    break;
                case 2:
                    data = TradeBytes;
                    break;
                case 3:
                    data = LocalDefenseBytes;
                    break;
                case 4:
                    data = LookingForGroupBytes;
                    break;
                default:
                    data = GeneralBytes;
                    break;
            }

            OutPacket packet = new OutPacket(WorldCommand.CMSG_JOIN_CHANNEL);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void ChangeStatus(int status)
        {
            byte[] data = null;

            var Available = new byte[] { 0x17, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00 };
            var Away = new byte[] { 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x41, 0x77, 0x61, 0x79, 0x00 };
            var Busy = new byte[] { 0x18, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x42, 0x75, 0x73, 0x79, 0x00 };

            switch (status)
            {
                case 0:
                    data = Available;
                    break;
                case 1:
                    data = Away;
                    break;
                case 2:
                    data = Busy;
                    break;
                default:
                    data = Available;
                    break;
            }
            if (status == 0)
            {
                OutPacket packet1 = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet1.Write(Away);
                Game.SendPacket(packet1);
                OutPacket packet2 = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet2.Write(Available);
                Game.SendPacket(packet2);
            }
            else
            {
                OutPacket packet = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet.Write(data);
                Game.SendPacket(packet);
            }

        }

        public void SayChannel(string message, int channel)
        {
            byte[] data = null;
            var endByte = new byte[] { 0x00 };

            var GeneralBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x47, 0x6C, 0x6F, 0x62, 0x61, 0x6C, 0x20, 0x43, 0x68, 0x61, 0x74, 0x00 };
            var TradeBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x54, 0x72, 0x61, 0x64, 0x65, 0x20, 0x2D, 0x20, 0x43, 0x69, 0x74, 0x79, 0x00 };
            var LocalDefenseBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x4C, 0x6F, 0x63, 0x61, 0x6C, 0x44, 0x65, 0x66, 0x65, 0x6E, 0x73, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00 };
            var LookingForGroupBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x4C, 0x6F, 0x6F, 0x6B, 0x69, 0x6E, 0x67, 0x46, 0x6F, 0x72, 0x47, 0x72, 0x6F, 0x75, 0x70, 0x00 };

            byte[] msg = Encoding.ASCII.GetBytes(message);

            switch (channel)
            {
                case 1:
                    byte[] gfinal = GeneralBytes.Concat(msg).Concat(endByte).ToArray();
                    data = gfinal;
                    break;
                case 2:
                    byte[] tfinal = TradeBytes.Concat(msg).Concat(endByte).ToArray();
                    data = tfinal;
                    break;
                case 3:
                    byte[] ldfinal = LocalDefenseBytes.Concat(msg).Concat(endByte).ToArray();
                    data = ldfinal;
                    break;
                case 4:
                    byte[] lfgfinal = LookingForGroupBytes.Concat(msg).Concat(endByte).ToArray();
                    data = lfgfinal;
                    break;
                default:
                    byte[] deffinal = GeneralBytes.Concat(msg).Concat(endByte).ToArray();
                    data = deffinal;
                    break;
            }

            OutPacket packet = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void InvitePlayerToParty(string player)
        {
            //CMSG_GROUP_INVITE = 110
            byte[] data = null;
            var endBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] InvitedPlayer = Encoding.ASCII.GetBytes(player);

            byte[] packetdata = InvitedPlayer.Concat(endBytes).ToArray();
            data = packetdata;

            OutPacket packet = new OutPacket(WorldCommand.CMSG_GROUP_INVITE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void AddFriend(string player)
        {
            //CMSG_ADD_FRIEND 0x0069 (105)
            LastAddedFriend = player;
            byte[] data = null;
            var endBytes = new byte[] { 0x00, 0x00 };

            byte[] PlayertoAdd = Encoding.ASCII.GetBytes(player);

            byte[] packetdata = PlayertoAdd.Concat(endBytes).ToArray();
            data = packetdata;

            OutPacket packet = new OutPacket(WorldCommand.CMSG_ADD_FRIEND);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void IgnorePlayer(string player)
        {
            //CMSG_ADD_IGNORE 0x006C (108)
            LastAddedFriend = player;
            byte[] data = null;
            var endByte = new byte[] { 0x00 };

            byte[] PlayertoIgnore = Encoding.ASCII.GetBytes(player);

            byte[] packetdata = PlayertoIgnore.Concat(endByte).ToArray();
            data = packetdata;

            OutPacket packet = new OutPacket(WorldCommand.CMSG_ADD_IGNORE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        //TODO properly handle this 
        public void RemoveFriend(int guid)
        {
            //CMSG_DEL_FRIEND 0x006A (106)
            // TODO get player GUID & send 4 byte uint64 GUID reversed + 6 nullbytes with wc CMSG_DEL_FRIEND
            // NOT working..

            byte[] data = null;
            string hexguid = guid.ToString("X");
            if (hexguid.Length == 4)
            {
                //string guidplayer = Convert.ToString(guid);
                string second = hexguid.Substring(2, 2);
                string first = hexguid.Substring(0, 2);

                byte[] bArray = new byte[hexguid.Length + 3];
                bArray[0] = byte.Parse(second, System.Globalization.NumberStyles.AllowHexSpecifier);
                bArray[1] = byte.Parse(first, System.Globalization.NumberStyles.AllowHexSpecifier);

                data = bArray;
            }

            OutPacket packet = new OutPacket(WorldCommand.CMSG_DEL_FRIEND);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        // SMSG_CHANNEL_LIST = 155,
        [PacketHandler(WorldCommand.SMSG_CHANNEL_NOTIFY)]
        protected void HandleChannelList(InPacket packet)
        {
            var notifymsg = "";
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = ChatMessageType.Channel;

            // index[0] 2 == joined
            // index[0] 3 == leaved
            // index[0] 5 == you are not in this group anymore
            // read channel name from index[1] till index[i] == 0x00

            StringBuilder builder = new StringBuilder();
            byte[] dump = packet.ReadToEnd();

            //packet.BaseStream.Position = 0;
            int slen = ((int)packet.BaseStream.Length);
            for (int i = 1; i < slen; i++)
            {
                if ((char)dump[i] != 0x00)
                {
                    builder.Append((char)dump[i]);
                }
                else
                {
                    i = slen;
                }
            }

            if ((char)dump[0] == 0x00) { notifymsg = "A user joined the channel."; }
            if ((char)dump[0] == 0x01) { notifymsg = "A user left the channel."; }
            if ((char)dump[0] == 0x02)
            {
                notifymsg = "Channel joined.";
                if (customChannels.Contains(builder.ToString()))
                {
                    UpdateCustomChannelList("1");
                } else
                {
                    if (!joinedChannels.Contains(builder.ToString()))
                    {
                        joinedChannels.Add(builder.ToString());
                        UpdateDefaultChannelList("1");
                    }
                }
            }
            if ((char)dump[0] == 0x03)
            {
                notifymsg = "Channel left.";
                if (joinedChannels.Contains(builder.ToString()))
                {
                    joinedChannels.Remove(builder.ToString());
                    UpdateDefaultChannelList("1");
                }
            }
            if ((char)dump[0] == 0x05) { notifymsg = "You already left this channel.."; }
            if ((char)dump[0] == 0x08) { notifymsg = "You are ower of the channel now."; } /*8*/
            if ((char)dump[0] == 0x0C) { notifymsg = "Moderation privileges given to you."; } /*12*/
            if ((char)dump[0] == 0x18) { notifymsg = ""; } /*24 Invited to join channel */

            if ((char)dump[0] == 0x18)
            {
                channel.Type = ChatMessageType.ChannelInvitation;
            }

            channel.ChannelName = builder.ToString();
            message.Message = notifymsg; // + builder.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            System.Threading.Thread.Sleep(100); // don't process too fast for automatic join
            Game.UI.PresentChatMessage(message);
        }

        //CMSG_GROUP_DISBAND = 123 party
        public void GroupDisband()
        {
            //byte[] data = "";
            OutPacket packet = new OutPacket(WorldCommand.CMSG_GROUP_DISBAND);
            packet.Write("");
            Game.SendPacket(packet);
        }

        //CMSG_GROUP_DECLINE = 115 decline party group invite
        public void GroupDecline()
        {
            //byte[] data = "";
            OutPacket packet = new OutPacket(WorldCommand.CMSG_GROUP_DECLINE);
            packet.Write("");
            Game.SendPacket(packet);
        }

        //CMSG_DECLINE_CHANNEL_INVITE = 1040
        public void CustomChannelDecline(string channelname)
        {
            byte[] data = null;
            var endByte = new byte[] { 0x00 };

            byte[] channame = Encoding.ASCII.GetBytes(channelname);

            byte[] packetdata = channame.Concat(endByte).ToArray();
            data = packetdata;

            OutPacket packet = new OutPacket(WorldCommand.CMSG_DECLINE_CHANNEL_INVITE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void NewChannel(string channelname, string password = "")
        {
            byte[] data = null;
            var startByte = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            var emptyByte = new byte[] { 0x00 };

            byte[] channame = Encoding.ASCII.GetBytes(channelname);
            if (password != "")
            {
                byte[] chanpwd = Encoding.ASCII.GetBytes(password);
                byte[] chanwithpass = startByte.Concat(channame).Concat(emptyByte).Concat(chanpwd).Concat(emptyByte).ToArray();
                data = chanwithpass;
            }
            else
            {
                byte[] channelopen = startByte.Concat(channame).Concat(emptyByte).Concat(emptyByte).ToArray();
                data = channelopen;
            }

            OutPacket packet = new OutPacket(WorldCommand.CMSG_JOIN_CHANNEL);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void AcceptChannelJoin(string channel)
        {
            byte[] data1 = null;
            byte[] data2 = null;
            var startByte = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            var emptyByte = new byte[] { 0x00 };

            byte[] channame = Encoding.ASCII.GetBytes(channel);
            byte[] channelJoin = startByte.Concat(channame).Concat(emptyByte).Concat(emptyByte).ToArray();
            byte[] channelAccept = channame.Concat(emptyByte).ToArray();
            data1 = channelJoin;
            data2 = channelAccept;

            OutPacket packet1 = new OutPacket(WorldCommand.CMSG_JOIN_CHANNEL);
            packet1.Write(data1);
            Game.SendPacket(packet1);

            OutPacket packet2 = new OutPacket(WorldCommand.CMSG_DECLINE_CHANNEL_INVITE);
            packet2.Write(data2);
            Game.SendPacket(packet2);
        }

        public void AcceptGroupInvitation()
        {
            //CMSG_GROUP_ACCEPT = 114
            OutPacket data = new OutPacket(WorldCommand.CMSG_GROUP_ACCEPT);
            data.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            Game.SendPacket(data);
        }

        public void RequestWhoList() //CMSG_WHO = 98,
        {
            OutPacket response = new OutPacket(WorldCommand.CMSG_WHO);
            response.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, });
            Game.SendPacket(response);
        }

        public void RequestFriendList() //CMSG_CONTACT_LIST 0x0066 (102)
        {
            OutPacket response = new OutPacket(WorldCommand.CMSG_CONTACT_LIST);
            response.Write(new byte[] { 0x01, 0x00, 0x00, 0x00 });
            Game.SendPacket(response);
        }

        public void RequestChannelList() //CMSG_CHANNEL_LIST = 154
        {
            OutPacket response = new OutPacket(WorldCommand.CMSG_CHANNEL_LIST);
            response.Write("");
            Game.SendPacket(response);
        }

        public void DoGuildChat(string message)
        {
            var response = new OutPacket(WorldCommand.CMSG_MESSAGECHAT); // org CMSG_MESSAGECHAT

            response.Write((uint)ChatMessageType.Guild);
            var race = World.SelectedCharacter.Race;
            var language = race.IsHorde() ? Language.Orcish : Language.Common;
            response.Write((uint)language);
            response.Write(message.ToCString());
            SendPacket(response);
        }

        public void DoWhisperChat(string message, string player)
        {
            var response = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);

            response.Write((uint)ChatMessageType.Whisper);
            var race = World.SelectedCharacter.Race;
            var language = race.IsHorde() ? Language.Orcish : Language.Common;
            response.Write((uint)language);
            response.Write(player.ToCString());
            response.Write(message.ToCString());
            SendPacket(response);
        }

        [PacketHandler(WorldCommand.SMSG_WHO)]
        protected void HandleWhoList(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel { Type = 0 };
            StringBuilder newbuild = new StringBuilder();

            player.Clear();
            guild.Clear();
            level.Clear();
            pclass.Clear();
            prace.Clear();
            pzone.Clear();
            playersonline = (int)packet.ReadUInt32();
            UInt32 sentResults = packet.ReadUInt32();
            for (int i = 0; i < sentResults; i++)
            {
                string playerName = packet.ReadCString();
                string playerGuild = packet.ReadCString();
                UInt32 playerLevel = packet.ReadUInt32();
                UInt32 playerClass = packet.ReadUInt32();
                UInt32 playerRace = packet.ReadUInt32();
                byte playerGender = packet.ReadByte();
                UInt32 playerZone = packet.ReadUInt32();
                player.Add(playerName);
                guild.Add(playerGuild);
                level.Add((int)playerLevel);
                Class className = (Class)playerClass;
                pclass.Add(className.ToString());
                Race raceName = (Race)playerRace;
                prace.Add(raceName.ToString());
                pzone.Add(Client.Extensions.GetZoneName((int)playerZone));
            }
            UpdateWhoList("1");
        }

        //SMSG_FRIEND_STATUS 0x0068 (104)
        [PacketHandler(WorldCommand.SMSG_FRIEND_STATUS)]
        protected void HandleFriendStatus(InPacket packet)
        {
            UInt64 receiverGuid = 0;
            var statusmsg = "";
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;

            // [0] 2 = friend came online?
            // [0] 3 = gone offline
            // [0] 4 = player doesn't exist
            // [0] 5 = removed from friendlist
            // [0] 6 = added to friendlist
            // [0] 8 = already in friendlist
            // [0] 14 = ? 
            // [0] 15 = added to ignore list

            byte[] dump = packet.ReadToEnd();

            string playername = "";

            //RequestWhoList(); // added
            //System.Threading.Thread.Sleep(3000);

            #region probably not the best way to do it..
            if (dump[0] == 3 || dump[0] == 2)
            {
                packet.BaseStream.Position = 0;
                packet.ReadByte();
                receiverGuid = packet.ReadUInt64();
                bool resolve = (Game.World.PlayerNameLookup.TryGetValue(receiverGuid, out playername));
            }

            if (!player.Contains(playername)) // added
            {
                if ((char)dump[0] == 0x02) { statusmsg = playername + " has come online."; }
                System.Threading.Thread.Sleep(4000);
                RequestWhoList();
                RequestFriendList();
                UpdateFriendList("1");
            }
            else
            {
                if ((char)dump[0] == 0x03) { statusmsg = playername + " has gone offline."; }
                System.Threading.Thread.Sleep(4000);
                RequestWhoList();
                RequestFriendList();
                UpdateFriendList("1");
            }
            #endregion

            if ((char)dump[0] == 0x04) { statusmsg = "Player '" + LastAddedFriend + "' not found."; }
            if ((char)dump[0] == 0x05) { statusmsg = LastAddedFriend + " removed from friend list."; }
            if ((char)dump[0] == 0x06) { statusmsg = LastAddedFriend + " added to friends."; }
            if ((char)dump[0] == 0x08) { statusmsg = LastAddedFriend + " is already your friend."; }
            if ((char)dump[0] == 0x0E) { statusmsg = LastAddedFriend + " is no longer being ignored."; }
            if ((char)dump[0] == 0x0F) { statusmsg = LastAddedFriend + " is now being ignored."; }

            if (statusmsg != "")
            {
                message.Message = statusmsg; // + builder.ToString();
                message.Language = 0;
                message.ChatTag = 0;
                message.Sender = channel;
                Game.UI.PresentChatMessage(message);
            }

        }

        [PacketHandler(WorldCommand.SMSG_GROUP_LIST)]
        protected void HandlePartyList(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = ChatMessageType.Party;
            StringBuilder msg = new StringBuilder();

            GroupType groupType = (GroupType)packet.ReadByte();
            packet.ReadByte();
            packet.ReadByte();
            packet.ReadByte();
            if (groupType.HasFlag(GroupType.GROUPTYPE_LFG))
            {
                packet.ReadByte();
                packet.ReadUInt32();
            }
            packet.ReadUInt64();
            packet.ReadUInt32();
            uint membersCount = packet.ReadUInt32();

            if (membersCount > 0) { JoinMessage += 1; }

            if (JoinMessage == 1)
            {
                msg.Append("\r\nYou joined the group.\r\n");
            }

            var groupcount = membersCount;
            GroupMembersGuids.Clear();
            for (uint index = 0; index < membersCount; index++)
            {
                packet.ReadCString();
                UInt64 memberGuid = packet.ReadUInt64();
                GroupMembersGuids.Add(memberGuid);
                if (!GroupMembersGuids2.Contains(memberGuid))
                {
                    if (GroupMembersGuids.Count > GroupMembersGuids2.Count)
                    {
                        var player = "";
                        bool resolve = (Game.World.PlayerNameLookup.TryGetValue(memberGuid, out player));
                        msg.Append(player.ToString() + " joins the party. ");
                        GroupMembersGuids2.Add(memberGuid);
                    }
                }
                packet.ReadByte();
                packet.ReadByte();
                packet.ReadByte();
                packet.ReadByte();
            }

            GroupLeaderGuid = packet.ReadUInt64();

            if (GroupMembersGuids.Count == 0)
            {
                msg.Append("Your group has been disbanded.");
                UpdateGroupList("1");
                GroupMembersGuids2.Clear();
                JoinMessage = 0;
            }

            try
            {
                if (GroupMembersGuids.Count() < GroupMembersGuids2.Count())
                {
                    if (GroupMembersGuids.Count != 0)
                    {
                        var Userleft = GroupMembersGuids2.Except(GroupMembersGuids).ToArray();
                        string player = null;
                        if (Userleft[0].ToString() != "")
                        {
                            bool resolve = (Game.World.PlayerNameLookup.TryGetValue(Userleft[0], out player));
                            msg.Append(player.ToString() + " leaves the party.");
                            GroupMembersGuids2.Remove(Userleft[0]);
                        }
                    }
                }
            }
            catch
            {

            }

            UpdateGroupList("1");

            if (msg.ToString() != "")
            {
                message.Message = msg.ToString();
                message.Language = 0;
                message.ChatTag = 0;
                message.Sender = channel;
                Game.UI.PresentChatMessage(message);
            }
        }

        //SMSG_CONTACT_LIST 0x0067 (103)
        [PacketHandler(WorldCommand.SMSG_CONTACT_LIST)]
        protected void HandleFriendList(InPacket packet)
        {
            UInt64 receiverGuid = 0;
            resolvedFriendList.Clear();
            friendGUIList.Clear();

            RequestWhoList(); // added

            byte[] dump = packet.ReadToEnd();
            int len = ((int)packet.BaseStream.Length);
            packet.BaseStream.Position = 0;
            for (int i = 0; i < len; i++)
            {
                if (dump[i] != 0 && dump[i + 1] != 0) //(packet.PeekChar() != 0
                {
                    receiverGuid = packet.ReadUInt64();
                    i += 7;
                }
                else
                {
                    packet.ReadByte();
                }
                if (receiverGuid.IsPlayer())
                {
                    var playername = "";
                    bool resolve = (Game.World.PlayerNameLookup.TryGetValue(receiverGuid, out playername));
                    if (receiverGuid.ToString().Length <= 4 && receiverGuid.ToString().Length == 4)
                    {
                        if (!friendGUIList.Contains(receiverGuid.ToString())) // added
                        {
                            friendGUIList.Add(receiverGuid.ToString());
                        }
                    }
                    if (playername != null)
                    {
                        if (!resolvedFriendList.Contains(playername)) // added
                        {
                            resolvedFriendList.Add(playername);
                        }
                    }
                    else
                    {
                        OutPacket response = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        response.Write(receiverGuid);
                        Game.SendPacket(response);
                    }
                }
            }
            if (resolvedFriendList.Count > 1)
            {
                UpdateFriendList("1");
            }
        }

        #endregion

        #region Actions
        public void DoTextEmote(TextEmote emote)
        {
            var packet = new OutPacket(WorldCommand.CMSG_TEXT_EMOTE);
            packet.Write((uint)emote);
            packet.Write((uint)0);
            packet.Write((ulong)0);
            SendPacket(packet);
        }

        #endregion

        #region Packet Handlers
        [PacketHandler(WorldCommand.SMSG_LOGIN_VERIFY_WORLD)]
        protected void HandleLoginVerifyWorld(InPacket packet)
        {
            Player.MapID = (int)packet.ReadUInt32();
            Player.X = packet.ReadSingle();
            Player.Y = packet.ReadSingle();
            Player.Z = packet.ReadSingle();
            Player.O = packet.ReadSingle();
        }

        [PacketHandler(WorldCommand.SMSG_NEW_WORLD)]
        protected void HandleNewWorld(InPacket packet)
        {
            Player.MapID = (int)packet.ReadUInt32();
            Player.X = packet.ReadSingle();
            Player.Y = packet.ReadSingle();
            Player.Z = packet.ReadSingle();
            Player.O = packet.ReadSingle();

            OutPacket result = new OutPacket(WorldCommand.MSG_MOVE_WORLDPORT_ACK);
            SendPacket(result);
        }

        [PacketHandler(WorldCommand.SMSG_TRANSFER_PENDING)]
        protected void HandleTransferPending(InPacket packet)
        {
            Player.ResetPosition();
            var newMap = packet.ReadUInt32();
        }

        [PacketHandler(WorldCommand.SMSG_CHAR_CREATE)]
        protected void HandleCharCreate(InPacket packet)
        {
            var response = (CommandDetail)packet.ReadByte();
            if (response == CommandDetail.CHAR_CREATE_SUCCESS)
                SendPacket(new OutPacket(WorldCommand.CMSG_CHAR_ENUM));
            else
                NoCharactersFound();
        }

        [PacketHandler(WorldCommand.SMSG_LOGOUT_RESPONSE)]
        protected void HandleLogoutResponse(InPacket packet)
        {
            bool logoutOk = packet.ReadUInt32() == 0;
            bool instant = packet.ReadByte() != 0;

            if (instant || !logoutOk)
            {
                Connected = false;
                LoggedIn = false;
                Running = false;
            }
        }

        [PacketHandler(WorldCommand.SMSG_LOGOUT_COMPLETE)]
        protected void HandleLogoutComplete(InPacket packet)
        {
            Connected = false;
            LoggedIn = false;
            Running = false;
            loggedOutEvent.SetResult(true);
        }

        [PacketHandler(WorldCommand.SMSG_ALL_ACHIEVEMENT_DATA)]
        protected void HandleAllAchievementData(InPacket packet)
        {
            CompletedAchievements.Clear();
            AchievementCriterias.Clear();

            for (; ; )
            {
                uint achievementId = packet.ReadUInt32();
                if (achievementId == 0xFFFFFFFF)
                    break;

                packet.ReadPackedTime();

                CompletedAchievements.Add(achievementId);
            }

            for (; ; )
            {
                uint criteriaId = packet.ReadUInt32();
                if (criteriaId == 0xFFFFFFFF)
                    break;
                ulong criteriaCounter = packet.ReadPackedGuid();
                packet.ReadPackedGuid();
                packet.ReadInt32();
                packet.ReadPackedTime();
                packet.ReadInt32();
                packet.ReadInt32();

                AchievementCriterias[criteriaId] = criteriaCounter;
            }
        }

        [PacketHandler(WorldCommand.SMSG_CRITERIA_UPDATE)]
        protected void HandleCriteriaUpdate(InPacket packet)
        {
            uint criteriaId = packet.ReadUInt32();
            ulong criteriaCounter = packet.ReadPackedGuid();

            AchievementCriterias[criteriaId] = criteriaCounter;
        }

        [PacketHandler(WorldCommand.SMSG_GROUP_DESTROYED)]
        protected void HandlePartyDisband(InPacket packet)
        {
            JoinMessage = 2;
            GroupLeaderGuid = 0;
            GroupMembersGuids.Clear();
        }
        #endregion

        #region Unused Methods
        public override void Log(string message, LogLevel level = LogLevel.Info)
        {
#if DEBUG_LOG
            Console.WriteLine(message);
#endif
        }

        public override void AuthError(string message)
        {
#if !DEBUG_LOG
            AuthenticationError = true;
            AuthErrorText = message;
#endif
            MessageBox.Show(message, "Auth Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override void NewMessage(string message)
        {
#if !DEBUG_LOG
            NewMessageData = message;
#endif      
        }

        public override void UpdateGroupList(string message)
        {
#if !DEBUG_LOG
            UpdateGroupGUIDList = message;
#endif      
        }

        public override void UpdateFriendList(string message)
        {
#if !DEBUG_LOG
            FriendListUpdate = message;
#endif      
        }

        public override void UpdateDefaultChannelList(string message)
        {
#if !DEBUG_LOG
            DefaultChannelListUpdate = message;
#endif      
        }

        public override void UpdateCustomChannelList(string message)
        {
#if !DEBUG_LOG
            CustomChannelListUpdate = message;
#endif      
        }

        public override void UpdateWhoList(string message)
        {
#if !DEBUG_LOG
            WhoListUpdate = message;
#endif      
        }

        public override void LogLine(string message, LogLevel level = LogLevel.Info)
        {
        }

        public override void LogDebug(string message)
        {
        }

        public override void LogException(string message)
        {
        }

        public override void LogException(Exception ex)
        {
        }

        public IGameUI UI
        {
            get
            {
                return this;
            }
        }

        public override void PresentChatMessage(ChatMessage message)
        {
            StringBuilder sb = new StringBuilder();
            if (message.Sender.Type.ToString() != "Channel" && message.Sender.Type.ToString() != "ChannelInvitation" && message.Sender.Type.ToString() != "partyGroupInvitation")
            {
                //sb.Append(message.Sender.Type == ChatMessageType.WhisperInform ? "To " : "");
                //}
                //else
                //{
                sb.Append(message.Sender.Type == ChatMessageType.WhisperInform ? "To " : "[" + message.Sender.Type.ToString() + "] ");
            }

            //! Color codes taken from default chat_cache in WTF folder
            //! TODO: RTF form?
            switch (message.Sender.Type)
            {
                case ChatMessageType.Channel:
                    {
                        //sb.ForeColor(Color.FromArgb(255, 192, 192));
                        //Console.ForegroundColor = ConsoleColor.DarkYellow;//Color.DarkSalmon;
                        sb.Append("[");

                        string cname = message.Sender.ChannelName.ToString().Substring(3, 1);
                        switch (cname)
                        {
                            case "b": // General
                                chanNum = "1";
                                break;
                            case "d": // Trade
                                chanNum = "2";
                                break;
                            case "a": // Localdefense
                                string chanstock = message.Sender.ChannelName.ToString();
                                string[] clean = chanstock.Split(new char[] { '\x0020' });
                                message.Sender.ChannelName = clean[0];
                                chanNum = "3";
                                break;
                            case "k": // LFG
                                chanNum = "4";
                                break;
                            default:
                                chanNum = "";
                                break;
                        }

                        sb.Append(chanNum + ". " + message.Sender.ChannelName.ToString());
                        sb.Append("] ");
                        break;
                    }
                case ChatMessageType.ChannelInvitation:
                    {
                        sb.Append("[Invited5");
                        sb.Append(message.Sender.ChannelName);
                        break;
                    }
                case ChatMessageType.partyGroupInvitation:
                    {
                        sb.Append("[Invited6");
                        sb.Append(message.Sender.ChannelName);
                        break;
                    }
                case ChatMessageType.Whisper:
                    {
                        //sb.ForeColor(Color.FromArgb(255, 192, 192));
                        Console.ForegroundColor = ConsoleColor.Blue;//Color.DarkSalmon;
                        sb.Append("from ");
                        sb.Append(message.Sender.ChannelName);
                        //sb.Append("] ");
                        break;
                    }
                case ChatMessageType.WhisperInform:
                    {
                        //sb.ForeColor(Color.FromArgb(255, 192, 192));
                        Console.ForegroundColor = ConsoleColor.DarkGreen;//Color.DarkSalmon;
                        //sb.Append(" ");
                        sb.Append(message.Sender.ChannelName);
                        //sb.Append("] ");
                        break;
                    }
                case ChatMessageType.WhisperForeign:
                    {
                        //sb.ForeColor(Color.FromArgb(255, 192, 192));
                        Console.ForegroundColor = ConsoleColor.Green;//Color.DarkSalmon;
                        sb.Append(" [");
                        sb.Append(message.Sender.ChannelName);
                        sb.Append("] ");
                        break;
                    }
                case ChatMessageType.System:
                case ChatMessageType.Money:
                case ChatMessageType.TargetIcons:
                case ChatMessageType.Achievement:
                    //sb.ForeColor(Color.FromArgb(255, 255, 0));
                    Console.ForegroundColor = ConsoleColor.Yellow;//Color.Gold;
                    break;
                case ChatMessageType.Emote:
                case ChatMessageType.TextEmote:
                case ChatMessageType.MonsterEmote:
                    //sb.ForeColor(Color.FromArgb(255, 128, 64));
                    Console.ForegroundColor = ConsoleColor.DarkRed;//Color.Chocolate;
                    break;
                case ChatMessageType.Party:
                    //sb.ForeColor(Color.FromArgb(170, 170, 255));
                    //Console.ForegroundColor = ConsoleColor.DarkCyan;//Color.CornflowerBlue;
                    //sb.Append(" [");
                    sb.Append(message.Sender.ChannelName);
                    //sb.Append("] ");
                    break;
                case ChatMessageType.PartyLeader:
                    //sb.ForeColor(Color.FromArgb(118, 200, 255));
                    Console.ForegroundColor = ConsoleColor.Cyan;//Color.DodgerBlue;
                    break;
                case ChatMessageType.Raid:
                    //sb.ForeColor(Color.FromArgb(255, 172, 0));
                    Console.ForegroundColor = ConsoleColor.Red;//Color.DarkOrange;
                    break;
                case ChatMessageType.RaidLeader:
                    //sb.ForeColor(Color.FromArgb(255, 72, 9));
                    Console.ForegroundColor = ConsoleColor.Red;//Color.DarkOrange;
                    break;
                case ChatMessageType.RaidWarning:
                    //sb.ForeColor(Color.FromArgb(255, 72, 0));
                    Console.ForegroundColor = ConsoleColor.Red;//Color.DarkOrange;
                    break;
                case ChatMessageType.Guild:
                    //sb.ForeColor(Color.FromArgb(64, 255, 64));
                    break;
                // TODO !
                case ChatMessageType.GuildAchievement:
                    //sb.ForeColor(Color.FromArgb(64, 255, 64));
                    Console.ForegroundColor = ConsoleColor.Green;//Color.LimeGreen;
                    break;
                case ChatMessageType.Officer:
                    //sb.ForeColor(Color.FromArgb(64, 192, 64));
                    Console.ForegroundColor = ConsoleColor.DarkGreen;//Color.DarkSeaGreen;
                    break;
                case ChatMessageType.Yell:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ChatMessageType.Say:
                default:
                    //sb.ForeColor(Color.FromArgb(255, 255, 255));
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            //sb.Append("[");
            if (message.ChatTag.HasFlag(ChatTag.Gm))
                sb.Append("<GM>");
            if (message.ChatTag.HasFlag(ChatTag.Afk))
                sb.Append("<AFK>");
            if (message.ChatTag.HasFlag(ChatTag.Dnd))
                sb.Append("<DND>");

            sb.Append(message.Sender.Sender);

            //sb.Append("]: ");
            if (message.Sender.Type.ToString() != "ChannelInvitation" && message.Sender.Type.ToString() != "partyGroupInvitation")
            {
                sb.Append(": ");
            }
            sb.Append(message.Message);

            //Log(sb.ToString());
            //LogLine(sb.ToString());
            NewMessage(sb.ToString());

        }
        #endregion

        #region Triggers Handling
        IteratedList<Trigger> Triggers;
        int triggerCounter;

        public int AddTrigger(Trigger trigger)
        {
            triggerCounter++;
            trigger.Id = triggerCounter;
            Triggers.Add(trigger);
            return triggerCounter;
        }

        public IEnumerable<int> AddTriggers(IEnumerable<Trigger> triggers)
        {
            var triggerIds = new List<int>();
            foreach (var trigger in triggers)
                triggerIds.Add(AddTrigger(trigger));
            return triggerIds;
        }

        public bool RemoveTrigger(int triggerId)
        {
            return Triggers.RemoveAll(trigger => trigger.Id == triggerId) > 0;
        }

        public void ClearTriggers()
        {
            Triggers.Clear();
        }

        public void ResetTriggers()
        {
            Triggers.ForEach(trigger => trigger.Reset());
        }

        public void HandleTriggerInput(TriggerActionType type, params object[] inputs)
        {
            Triggers.ForEach(trigger => trigger.HandleInput(type, inputs));
        }

        void OnFieldUpdate(object s, UpdateFieldEventArg e)
        {
            HandleTriggerInput(TriggerActionType.UpdateField, e);
        }
        #endregion
    }
}

#define TESTING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Authentication;
using Client.Authentication.Network;
using Client.Chat;
using Client.Chat.Definitions;
using Client.Properties;
using Client.UI;
using Client.World;
using Client.World.Definitions;
using Client.World.Entities;
using Client.World.Network;

public struct TicketInfo
{
    public TicketInfo(string name, string create, string assigned, string comment, bool online)
    {
        playerName = name;
        createTime = create;
        assignedPlayer = assigned;
        ticketComment = comment;
        areTheyOnline = online;
    }

    public string playerName;
    public string createTime;
    public string assignedPlayer;
    public string ticketComment;
    public bool areTheyOnline;
}

public struct CurrentTicket
{
    public string playerName;
    public string assignedPlayer;
    public string ticketMessage;
    public string ticketComment;
    public string ticketResponse;
    public string ticketChatLog;
}

namespace Client
{
    public class AutomatedGame : IGameUI, IGame
    {

        public static bool IsLoggedIn()
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
        public static List<string> ShowRealmList => presentrealmList;

        public static List<string> ShowCharList => presentcharacterList;

        public static bool SetLoggedIn
        {
            set => LoggedInserver = value;
        }

        public static bool RealmChosen
        {
            set => realmchosen = value;
        }
        public static int RealmIDgui
        {
            set => realmidGUI = value;
        }

        public static bool CharacterChosen
        {
            set => characterchosen = value;
        }
        public static int CharacterID
        {
            set => characterID = value;
        }

        public static bool DisconClient
        {
            set => disconnectclient = value;
        }

        public static bool charlogoutSucceeded
        {
            get => logoutSucceededSMSG;
            set => logoutSucceededSMSG = value;
        }

        public static string chatWindowBGColor
        {
            get => ChatWindowBackGroundColor;
            set => ChatWindowBackGroundColor = value;
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

        public static string ChatWindowBackGroundColor;

        public static string NewMessageData;
        public static string UpdateGroupGUIDList;
        public static string WhoListUpdate;
        public static string RosterUpdate;
        public static string TicketUpdate;
        public static string FriendListUpdate;
        public static string DefaultChannelListUpdate;
        public static string CustomChannelListUpdate;

        public static List<string> presentrealmList = new List<string>();
        public static List<string> presentcharacterList = new List<string>();
        public static List<string> characterNameList = new List<string>();
        //public static List<string> characterGUIDList = new List<string>();
        private static bool logoutSucceededSMSG;
        private static bool LoggedInserver;
        public static bool AuthenticationError;
        public static string AuthErrorText;
        public static bool disconnectclient;
        public static int realmidGUI;
        public static bool realmchosen;
        public static int characterID;
        public static bool characterchosen;
        public static bool Charsloaded;
        private static string chanNum;
        public static string LastAddedFriend;
        public static string LastRemovedFriend;
        public static int securityLevel;
        public int JoinMessage;

        public static List<string> resolvedFriendList = new List<string>();
        public static List<string> friendGUIList = new List<string>();

        public static List<string> joinedChannels = new List<string>();
        public static List<string> customChannels = new List<string>();

        //public static List<string> OnlineFriends = new List<string>();

        #region Properties
        public bool Running { get; set; }
        GameSocket socket;
        public BigInteger Key { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool LoggedIn { get; private set; }
        public int RealmID { get; private set; }
        public int Character { get; private set; }
        public bool Connected { get; private set; }
        public string LastSentPacket => socket.LastOutOpcodeName;

        public DateTime LastSentPacketTime => socket.LastOutOpcodeTime;

        public string LastReceivedPacket => socket.LastInOpcodeName;

        public DateTime LastReceivedPacketTime => socket.LastInOpcodeTime;

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
                return LogLevel.Error;
            }
            set
            {
            }
        }
        public override IGame Game
        {
            get => this;
            set
            {
            }
        }

        public Dictionary<ulong, WorldObject> Objects
        {
            get;
        }

        public ulong GroupLeaderGuid { get; private set; }

        public List<ulong> groupMembersGuids = new List<ulong>();
        public List<ulong> groupMembersGuids2 = new List<ulong>();


        #endregion

        public AutomatedGame(string hostname, int port, string username, string password, int realmId, int character)
        {
            RealmID = realmId;
            Character = character;
            scheduledActions = new ScheduledActions();
            Triggers = new IteratedList<Trigger>();
            World = new GameWorld();
            Player = new Player();
            Player.OnFieldUpdated += OnFieldUpdate;
            Objects = new Dictionary<ulong, WorldObject>();

            Hostname = hostname;
            Port = port;
            Username = username;
            Password = password;

            socket = new AuthSocket(this, Hostname, Port, Username, Password);
            socket.InitHandlers();
        }

        #region Basic Methods
        public void ConnectTo(WorldServerInfo server)
        {
            if (socket is AuthSocket authSocket)
                Key = authSocket.Key;

            socket.Dispose();

            socket = new WorldSocket(this, server);
            socket.InitHandlers();

            if (socket.Connect())
            {
                socket.Start();
                Connected = true;
            }
            else
            {
                Reconnect();
            }
                
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

            if (disconnectclient)
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
            if (Settings.Default.ConnectionLostLogout == true)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Environment.Exit(1);
            }
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
                var logout = new OutPacket(WorldCommand.CMSG_LOGOUT_REQUEST);
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
                var logout = new OutPacket(WorldCommand.CMSG_LOGOUT_REQUEST);
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
            if (!(socket is WorldSocket)) return;
            ((WorldSocket)socket).Send(packet);
            HandleTriggerInput(TriggerActionType.Opcode, packet);
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

            presentrealmList.Clear();
            foreach (var server in realmList)
            {
                presentrealmList.Add(server.Name);
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
            //List<string> charname = new List<string>();

            presentcharacterList.Clear();
            foreach (var characterz in characterList)
            {
                presentcharacterList.Add(characterz.Name + " Level: " + characterz.Level + " Race: " + characterz.Race + " Class: " + characterz.Class);
                characterNameList.Add(characterz.Name);
                //characterGUIDList.Add(Convert.ToUInt32(characterz.GUID).ToString());
            }
            Charsloaded = true;
            //Thread.Sleep(1000);
#if TESTING
            HelloDad();
#endif
            while (characterchosen == false)
            {
                //Thread.Sleep(500);
            }


            const int index = -1;
            /*while (index > length || index < 0)
            {
                Log("Choose a character:  ");
                if (!int.TryParse(Console.ReadLine(), out index))
                    //LogLine("Selected character: " + charname[index].ToString());
            }*/

            if (index >= characterList.Length) return;
            World.SelectedCharacter = characterList[characterID];
            // TODO: enter world
            var packet = new OutPacket(WorldCommand.CMSG_PLAYER_LOGIN);
            packet.Write(World.SelectedCharacter.GUID);
            SendPacket(packet);
            LoggedIn = true;
            Player.GUID = World.SelectedCharacter.GUID;

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
            if (!Running || (flags != ActionFlag.None && disabledActions.HasFlag(flags))) return 0;
            scheduledActionCounter++;
            scheduledActions.Add(new RepeatingAction(action, cancel, time, interval, flags, scheduledActionCounter));
            return scheduledActionCounter;

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

            socket?.Dispose();
        }

        public virtual void NoCharactersFound()
        { }

        public virtual void InvalidCredentials()
        { }

        public string GetPlayerName(WorldObject obj)
        {
            return GetPlayerName(obj.GUID);
        }

        protected string GetPlayerName(ulong guid)
        {
            return Game.World.PlayerNameLookup.TryGetValue(guid, out var name) ? name : "";
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
            var response = new OutPacket(WorldCommand.CMSG_GUILD_ROSTER);
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

        /*public void charLogout()
        {           
            var packet = new OutPacket(WorldCommand.CMSG_LOGOUT_REQUEST);
            packet.Write("");
            Game.SendPacket(packet);
        }

        public void charLogin(int charID)
        {
            //World.SelectedCharacter.GUID = Convert.ToUInt64(characterGUIDList[charID]);
            //World.SelectedCharacter.GUID = Convert.ToUInt64(charID);
            var packet = new OutPacket(WorldCommand.CMSG_PLAYER_LOGIN);
            packet.Write(World.SelectedCharacter.GUID);
            SendPacket(packet);
            Player.GUID = World.SelectedCharacter.GUID;
        }*/

        public void LeaveChannel(int channel, string channelName)
        {
            var packet = new OutPacket(WorldCommand.CMSG_LEAVE_CHANNEL);
            packet.Write(Convert.ToUInt32(channel));
            packet.Write(channelName);
            Game.SendPacket(packet);
        }

        public void JoinChannel(int channel, string channelName, string channelPassword)
        {
            var packet = new OutPacket(WorldCommand.CMSG_JOIN_CHANNEL);
            packet.Write(Convert.ToUInt32(channel));
            packet.Write((byte)0);
            packet.Write((byte)0);
            packet.Write(channelName.ToCString());
            packet.Write(channelPassword.ToCString());
            Game.SendPacket(packet);
        }

        public void ChangeStatus(int status)
        {
            byte[] data = null;

            var available = new byte[] { 0x17, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00 };
            var away = new byte[] { 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x41, 0x77, 0x61, 0x79, 0x00 };
            var busy = new byte[] { 0x18, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x42, 0x75, 0x73, 0x79, 0x00 };

            switch (status)
            {
                case 0:
                    data = available;
                    break;
                case 1:
                    data = away;
                    break;
                case 2:
                    data = busy;
                    break;
                default:
                    data = available;
                    break;
            }
            if (status == 0)
            {
                var packet1 = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet1.Write(away);
                Game.SendPacket(packet1);
                var packet2 = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet2.Write(available);
                Game.SendPacket(packet2);
            }
            else
            {
                var packet = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
                packet.Write(data);
                Game.SendPacket(packet);
            }

        }

        public void SayChannel(string message, int channel)
        {
            byte[] data = null;
            var endByte = new byte[] { 0x00 };

            var generalBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x47, 0x6C, 0x6F, 0x62, 0x61, 0x6C, 0x20, 0x43, 0x68, 0x61, 0x74, 0x00 };
            var tradeBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x54, 0x72, 0x61, 0x64, 0x65, 0x20, 0x2D, 0x20, 0x43, 0x69, 0x74, 0x79, 0x00 };
            var localDefenseBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x4C, 0x6F, 0x63, 0x61, 0x6C, 0x44, 0x65, 0x66, 0x65, 0x6E, 0x73, 0x65, 0x20, 0x2D, 0x20, 0x44, 0x75, 0x6E, 0x20, 0x4D, 0x6F, 0x72, 0x6F, 0x67, 0x68, 0x00 };
            var lookingForGroupBytes = new byte[] { 0x11, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x4C, 0x6F, 0x6F, 0x6B, 0x69, 0x6E, 0x67, 0x46, 0x6F, 0x72, 0x47, 0x72, 0x6F, 0x75, 0x70, 0x00 };

            var msg = Encoding.ASCII.GetBytes(message);

            switch (channel)
            {
                case 1:
                    var gfinal = generalBytes.Concat(msg).Concat(endByte).ToArray();
                    data = gfinal;
                    break;
                case 2:
                    var tfinal = tradeBytes.Concat(msg).Concat(endByte).ToArray();
                    data = tfinal;
                    break;
                case 3:
                    var ldfinal = localDefenseBytes.Concat(msg).Concat(endByte).ToArray();
                    data = ldfinal;
                    break;
                case 4:
                    var lfgfinal = lookingForGroupBytes.Concat(msg).Concat(endByte).ToArray();
                    data = lfgfinal;
                    break;
                default:
                    var deffinal = generalBytes.Concat(msg).Concat(endByte).ToArray();
                    data = deffinal;
                    break;
            }

            var packet = new OutPacket(WorldCommand.CMSG_MESSAGECHAT);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void InvitePlayerToParty(string player)
        {
            //CMSG_GROUP_INVITE = 110
            byte[] data = null;
            var endBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 };

            var invitedPlayer = Encoding.UTF8.GetBytes(player);

            var packetdata = invitedPlayer.Concat(endBytes).ToArray();
            data = packetdata;

            var packet = new OutPacket(WorldCommand.CMSG_GROUP_INVITE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void AddFriend(string player)
        {
            //CMSG_ADD_FRIEND 0x0069 (105)
            LastAddedFriend = player;
            byte[] data = null;
            var endBytes = new byte[] { 0x00, 0x00 };

            var playertoAdd = Encoding.UTF8.GetBytes(player);

            var packetdata = playertoAdd.Concat(endBytes).ToArray();
            data = packetdata;

            var packet = new OutPacket(WorldCommand.CMSG_ADD_FRIEND);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void IgnorePlayer(string player)
        {
            //CMSG_ADD_IGNORE 0x006C (108)
            LastAddedFriend = player;
            byte[] data = null;
            var endByte = new byte[] { 0x00 };

            var playertoIgnore = Encoding.UTF8.GetBytes(player);

            var packetdata = playertoIgnore.Concat(endByte).ToArray();
            data = packetdata;

            var packet = new OutPacket(WorldCommand.CMSG_ADD_IGNORE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void RemoveFriend(int guid, string player)
        {
            LastRemovedFriend = player;
            var bytes = BitConverter.GetBytes(guid);
            var littleEndian = bytes.Aggregate("", (current, b) => current + b.ToString("X2"));
            var packetdata = new byte[8];
            for (var i = 0; i < littleEndian.Length; i += 2)
            {
                packetdata[i / 2] = Convert.ToByte(littleEndian.Substring(i, 2), 16);
            }

            var packet = new OutPacket(WorldCommand.CMSG_DEL_FRIEND);
            packet.Write(packetdata);
            Game.SendPacket(packet);
        }

        // SMSG_CHANNEL_LIST = 155,
        [PacketHandler(WorldCommand.SMSG_CHANNEL_NOTIFY)]
        protected void HandleChannelList(InPacket packet)
        {
            var noticeType = (ChannelNoticeType)packet.ReadByte();
            var channelName = packet.ReadCString();
            if (channelName == "")
                channelName = "BlankChannelName";
            ChannelNotifyMessage(noticeType, channelName);
        }
        public void ChannelNotifyMessage(ChannelNoticeType notice, string channelName)
        {
            var message = "";
            var chatmsg = new ChatMessage();
            var channel = new ChatChannel
            {
                Type = ChatMessageType.Channel
            };
            switch (notice)
            {
                case ChannelNoticeType.PlayerJoined:
                {
                    const string
                        playerName =
                            "Player"; // To Do: figure out how wow client gets player name when its not sent in the notify packet.
                    message = playerName + " has joined the channel";
                }
                    break;
                case ChannelNoticeType.PlayerLeft:
                {
                    message = "Player has left the channel";
                }
                    break;
                case ChannelNoticeType.YouJoined:
                {
                    message = "You joined the channel";
                    if (customChannels.Contains(channelName))
                        UpdateCustomChannelList("1");
                    else if (!joinedChannels.Contains(channelName))
                    {
                        joinedChannels.Add(channelName);
                        UpdateDefaultChannelList("1");
                    }
                }
                    break;
                case ChannelNoticeType.YouLeft:
                {
                    message = "You left the channel";
                    if (customChannels.Contains(channelName))
                    {
                        customChannels.Remove(channelName);
                        UpdateCustomChannelList("1");
                    }
                    else if (joinedChannels.Contains(channelName))
                    {
                        joinedChannels.Remove(channelName);
                        UpdateDefaultChannelList("1");
                    }
                }
                    break;
                case ChannelNoticeType.WrongPassword:
                {
                    message = "You entered the wrong password";
                }
                    break;
                case ChannelNoticeType.NotMember:
                {
                    message = "You are not a member of the channel";
                }
                    break;
                case ChannelNoticeType.NotModerator:
                {
                    message = "You are not a moderator.";
                }
                    break;
                case ChannelNoticeType.PasswordChanged:
                {
                    message = "The password was channged.";
                }
                    break;
                case ChannelNoticeType.OwnerChanged:
                {
                    message = "Owner changed";
                }
                    break;
                case ChannelNoticeType.PlayerNotFound:
                {
                    message = "Player not found";
                }
                    break;
                case ChannelNoticeType.NotOwner:
                {
                    message = "You are not the owner";
                }
                    break;
                case ChannelNoticeType.OwnerIs:
                {
                    message = "The owner is not found due to no support for this yet";
                }
                    break;
                case ChannelNoticeType.ChangeNotice:
                {
                    message = "If the has been sent ask the server runner what the fuck are they doing";
                }
                    break;
                case ChannelNoticeType.AnnounceOn:
                {
                    message = "Channel announcements have been turned on";
                }
                    break;
                case ChannelNoticeType.AnnounceOff:
                {
                    message = "Channel announcements have been turned off";
                }
                    break;
                case ChannelNoticeType.ModOn:
                {
                    message = "Channel moderation has been turned on.";
                }
                    break;
                case ChannelNoticeType.ModOff:
                {
                    message = "Channel moderation has been turned off";
                }
                    break;
                case ChannelNoticeType.Muted:
                {
                    message = "You can't talk in a channel you are muted in";
                }
                    break;
                case ChannelNoticeType.PlayerKicked:
                {
                    message = "Someone kicked someone";
                }
                    break;
                case ChannelNoticeType.Banned:
                {
                    message = "You are banned from that channel";
                }
                    break;
                case ChannelNoticeType.PlayerBanned:
                {
                    message = "Someone banned someone";
                }
                    break;
                case ChannelNoticeType.PlayerUnbanned:
                {
                    message = "Someone unbanned someone";
                }
                    break;
                case ChannelNoticeType.NotBanned:
                {
                    message = "That person is not banned";
                }
                    break;
                case ChannelNoticeType.AlreadyMember:
                {
                    message = "You are already a member of the channel";
                }
                    break;
                case ChannelNoticeType.Invite:
                {
                    message = "You were invited to the channel";
                    channel.Type = ChatMessageType.ChannelInvitation;
                }
                    break;
                case ChannelNoticeType.InviteWrongFaction:
                {
                    message = "That person is the wrong faction";
                }
                    break;
                case ChannelNoticeType.WrongFaction:
                {
                    message = "You are the wrong faction for that";
                }
                    break;
                case ChannelNoticeType.InvalidName:
                {
                    message = "The chanel name is invalid";
                }
                    break;
                case ChannelNoticeType.NotModerated:
                {
                    message = "The channel is not moderated";
                }
                    break;
                case ChannelNoticeType.YouINvited:
                {
                    message = "You invited someone to the channel";
                }
                    break;
                case ChannelNoticeType.PlayerHasBeenBanned:
                {
                    message = "The player has been banned";
                }
                    break;
                case ChannelNoticeType.Throttled:
                {
                    message = "Your messaged was throttled please try again";
                }
                    break;
                case ChannelNoticeType.NotArea:
                {
                    message = "You are not in the correct area for that channel";
                }
                    break;
                case ChannelNoticeType.NotLFG:
                {
                    message = "You must be queued in looking for group before joining this channel";
                }
                    break;
                case ChannelNoticeType.VoiceChatOn:
                {
                    message = "Voice chat turned on";
                }
                    break;
                case ChannelNoticeType.VoiceChatOff:
                {
                    message = "Voice chat turned off";
                }
                    break;
                default:
                    message = "Got unknown channel status " + (int) notice + " pls fix men";
                    break;
            }

            message += ".";
            channel.ChannelName = channelName;
            chatmsg.Message = message; // + builder.ToString();
            chatmsg.Language = 0;
            chatmsg.ChatTag = 0;
            chatmsg.Sender = channel;
            Game.UI.PresentChatMessage(chatmsg);
        }
        //CMSG_GROUP_DISBAND = 123 party
        public void GroupDisband()
        {
            var packet = new OutPacket(WorldCommand.CMSG_GROUP_DISBAND);
            packet.Write("");
            Game.SendPacket(packet);
        }

        //CMSG_GROUP_DECLINE = 115 decline party group invite
        public void GroupDecline()
        {
            var packet = new OutPacket(WorldCommand.CMSG_GROUP_DECLINE);
            packet.Write("");
            Game.SendPacket(packet);
        }

        //CMSG_DECLINE_CHANNEL_INVITE = 1040
        public void CustomChannelDecline(string channelname)
        {
            var endByte = new byte[] { 0x00 };

            var channame = Encoding.ASCII.GetBytes(channelname);

            var packetdata = channame.Concat(endByte).ToArray();
            var data = packetdata;

            var packet = new OutPacket(WorldCommand.CMSG_DECLINE_CHANNEL_INVITE);
            packet.Write(data);
            Game.SendPacket(packet);
        }

        public void AcceptChannelJoin(string channel)
        {
            var startByte = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            var emptyByte = new byte[] { 0x00 };

            var channame = Encoding.ASCII.GetBytes(channel);
            var channelJoin = startByte.Concat(channame).Concat(emptyByte).Concat(emptyByte).ToArray();
            var channelAccept = channame.Concat(emptyByte).ToArray();
            var data1 = channelJoin;
            var data2 = channelAccept;

            var packet1 = new OutPacket(WorldCommand.CMSG_JOIN_CHANNEL);
            packet1.Write(data1);
            Game.SendPacket(packet1);

            var packet2 = new OutPacket(WorldCommand.CMSG_DECLINE_CHANNEL_INVITE);
            packet2.Write(data2);
            Game.SendPacket(packet2);
        }

        public void AcceptGroupInvitation()
        {
            //CMSG_GROUP_ACCEPT = 114
            var data = new OutPacket(WorldCommand.CMSG_GROUP_ACCEPT);
            data.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            Game.SendPacket(data);
        }

        public void RequestWhoList() //CMSG_WHO = 98,
        {
            var response = new OutPacket(WorldCommand.CMSG_WHO);
            response.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Game.SendPacket(response);
        }

        public void RequestFriendList() //CMSG_CONTACT_LIST 0x0066 (102)
        {
            var response = new OutPacket(WorldCommand.CMSG_CONTACT_LIST);
            response.Write(new byte[] { 0x01, 0x00, 0x00, 0x00 });
            Game.SendPacket(response);
        }

        public void RequestChannelList(string channelName) //CMSG_CHANNEL_LIST = 154
        {
            var response = new OutPacket(WorldCommand.CMSG_CHANNEL_LIST);
            response.Write(channelName);
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

        [PacketHandler(WorldCommand.SMSG_LOGOUT_RESPONSE)]
        protected void logoutResponse(InPacket packet)
        {
            packet.ReadBytes(4);
            logoutSucceededSMSG = Convert.ToBoolean(packet.ReadUInt32());
        }

        [PacketHandler(WorldCommand.SMSG_WHO)]
        protected void HandleWhoList(InPacket packet)
        {
            player.Clear();
            guild.Clear();
            level.Clear();
            pclass.Clear();
            prace.Clear();
            pzone.Clear();
            var sentResults = packet.ReadUInt32();
            playersonline = (int)packet.ReadUInt32();
            for (var i = 0; i < sentResults; i++)
            {
                var playerName = packet.ReadCString();
                var playerGuild = packet.ReadCString();
                var playerLevel = packet.ReadUInt32();
                var playerClass = packet.ReadUInt32();
                var playerRace = packet.ReadUInt32();
                var playerGender = packet.ReadByte();
                var playerZone = packet.ReadUInt32();
                player.Add(playerName);
                guild.Add(playerGuild);
                level.Add((int)playerLevel);
                var className = (Class)playerClass;
                pclass.Add(className.ToString());
                var raceName = (Race)playerRace;
                prace.Add(raceName.ToString());
                pzone.Add(Extensions.GetZoneName((int)playerZone));
            }
            UpdateWhoList("1");
        }

        //SMSG_FRIEND_STATUS 0x0068 (104)
        [PacketHandler(WorldCommand.SMSG_FRIEND_STATUS)]
        protected void HandleFriendStatus(InPacket packet)
        {
            UInt64 receiverGuid = 0;
            string statusmsg = "";
            var message = new ChatMessage();
            var channel = new ChatChannel
            {
                Type = 0
            };

            // [0] 2 = friend came online?
            // [0] 3 = gone offline
            // [0] 4 = player doesn't exist
            // [0] 5 = removed from friendlist
            // [0] 6 = added to friendlist
            // [0] 8 = already in friendlist
            // [0] 14 = ? 
            // [0] 15 = added to ignore list

            var dump = packet.ReadToEnd();

            string playername = "";

            //RequestWhoList(); // added
            //System.Threading.Thread.Sleep(3000);

            #region probably not the best way to do it..
            if (dump[0] == 3 || dump[0] == 2)
            {
                packet.BaseStream.Position = 0;
                packet.ReadByte();
                receiverGuid = packet.ReadUInt64();
                //var dummy = (Game.World.PlayerNameLookup.TryGetValue(receiverGuid, out playername));
                Game.World.PlayerNameLookup.TryGetValue(receiverGuid, out playername);
            }

            if (!player.Contains(playername)) // added
            {
                byte[] playerName = Encoding.Default.GetBytes(playername);
                if ((char)dump[0] == 0x02) { statusmsg = Encoding.UTF8.GetString(playerName).ToString() + " has come online."; Thread.Sleep(4000); }
                RequestWhoList();
                RequestFriendList();
                UpdateFriendList("1");
            }
            else
            {
                byte[] playerName = Encoding.Default.GetBytes(playername);
                if ((char)dump[0] == 0x03) { statusmsg = Encoding.UTF8.GetString(playerName).ToString() + " has gone offline."; Thread.Sleep(4000); }
                RequestWhoList();
                RequestFriendList();
                UpdateFriendList("1");
            }
            #endregion

            if ((char)dump[0] == 0x04) { statusmsg = "Player '" + LastAddedFriend + "' not found."; }
            if ((char)dump[0] == 0x05) { statusmsg = LastRemovedFriend + " removed from friend list."; }
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
            var message = new ChatMessage();
            var channel = new ChatChannel {Type = ChatMessageType.Party};
            var msg = new StringBuilder();

            var groupType = (GroupType)packet.ReadByte();
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
            var membersCount = packet.ReadUInt32();

            if (membersCount > 0) { JoinMessage += 1; }

            if (JoinMessage == 1)
            {
                msg.Append("\r\nYou joined the group.\r\n");
            }

            var groupcount = membersCount;
            groupMembersGuids.Clear();
            for (uint index = 0; index < membersCount; index++)
            {
                packet.ReadCString();
                var memberGuid = packet.ReadUInt64();
                groupMembersGuids.Add(memberGuid);
                if (!groupMembersGuids2.Contains(memberGuid))
                {
                    if (groupMembersGuids.Count > groupMembersGuids2.Count)
                    {
                        var dummy = (Game.World.PlayerNameLookup.TryGetValue(memberGuid, out var player));
                        msg.Append(player + " joins the party. ");
                        groupMembersGuids2.Add(memberGuid);
                    }
                }
                packet.ReadByte();
                packet.ReadByte();
                packet.ReadByte();
                packet.ReadByte();
            }

            GroupLeaderGuid = packet.ReadUInt64();

            if (groupMembersGuids.Count == 0)
            {
                msg.Append("Your group has been disbanded.");
                UpdateGroupList("1");
                groupMembersGuids2.Clear();
                JoinMessage = 0;
            }

            try
            {
                if (groupMembersGuids.Count() < groupMembersGuids2.Count())
                {
                    if (groupMembersGuids.Count != 0)
                    {
                        var userleft = groupMembersGuids2.Except(groupMembersGuids).ToArray();
                        if (userleft[0].ToString() != "")
                        {
                            var dummy = (Game.World.PlayerNameLookup.TryGetValue(userleft[0], out var playername));
                            msg.Append(playername + " leaves the party.");
                            groupMembersGuids2.Remove(userleft[0]);
                        }
                    }
                }
            }
            catch
            {

            }

            UpdateGroupList("1");

            if (msg.ToString() == "") return;
            message.Message = msg.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            Game.UI.PresentChatMessage(message);
        }

        //SMSG_CONTACT_LIST 0x0067 (103)
        [PacketHandler(WorldCommand.SMSG_CONTACT_LIST)]
        protected void HandleFriendList(InPacket packet)
        {
            UInt64 receiverGuid = 0;
            resolvedFriendList.Clear();
            friendGUIList.Clear();
            RequestWhoList();

            var dump = packet.ReadToEnd();
            var len = ((int)packet.BaseStream.Length);
            packet.BaseStream.Position = 0;
            for (var i = 0; i < len; i++)
            {
                if (dump[i] != 0 && dump[i + 1] != 0)
                {
                    receiverGuid = packet.ReadUInt64();
                    i += 7;
                }
                else
                {
                    packet.ReadByte();
                }
                if (receiverGuid.IsPlayer()) // isplayer doesn't properly work? 
                {
                    Game.World.PlayerNameLookup.TryGetValue(receiverGuid, out var playername);
                    if ((int)receiverGuid > 1 && receiverGuid.ToString().Length < 9) // for listview context menu
                    {
                        if (!friendGUIList.Contains(receiverGuid.ToString()))
                        {
                            friendGUIList.Add(receiverGuid.ToString());
                        }
                    }
                    if (playername != null)
                    {
                        if (!resolvedFriendList.Contains(playername))
                        {
                            resolvedFriendList.Add(playername);
                        }
                    }
                    else
                    {
                        if (receiverGuid != 0 && receiverGuid.ToString().Length < 9) // don't waste bandwidth on non player guids.
                        {
                            var response = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                            response.Write(receiverGuid);
                            Game.SendPacket(response);
                        }
                    }
                }
            }
            UpdateFriendList("1");
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

            var result = new OutPacket(WorldCommand.MSG_MOVE_WORLDPORT_ACK);
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
            var logoutOk = packet.ReadUInt32() == 0;
            var instant = packet.ReadByte() != 0;

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

        [PacketHandler(WorldCommand.SMSG_GROUP_DESTROYED)]
        protected void HandlePartyDisband(InPacket packet)
        {
            JoinMessage = 2;
            GroupLeaderGuid = 0;
            groupMembersGuids.Clear();
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
            while (Game.World.mesQue)
            {
                Thread.Sleep(10);
            }

            Game.World.newMessageQue.Add(message);
        }

        public override void UpdateGroupList(string message)
        {
            UpdateGroupGUIDList = message;
        }

        public override void UpdateFriendList(string message)
        {
            FriendListUpdate = message;
        }

        public override void UpdateDefaultChannelList(string message)
        {
            DefaultChannelListUpdate = message;
        }

        public override void UpdateCustomChannelList(string message)
        {
            CustomChannelListUpdate = message;
        }

        public override void UpdateWhoList(string message)
        {
            WhoListUpdate = message;
        }

        public override void UpdateRoster(string message)
        {
            RosterUpdate = message;
        }

        public override void UpdateTicketList(string message)
        {
            TicketUpdate = message;
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

        public IGameUI UI => this;

        public override void PresentChatMessage(ChatMessage message)
        {
            var sb = new StringBuilder();
            if (message.Sender.Type.ToString() != "Channel" && message.Sender.Type.ToString() != "ChannelInvitation" && message.Sender.Type.ToString() != "partyGroupInvitation")
            {
                //sb.Append(message.Sender.Type == ChatMessageType.WhisperInform ? "To " : "");
                //}
                //else
                //{
                sb.Append(message.Sender.Type == ChatMessageType.WhisperInform ? "To " : "[" + message.Sender.Type + "] ");
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

                        var cname = message.Sender.ChannelName.Substring(3, 1);
                        switch (cname)
                        {
                            case "b": // General
                                chanNum = "1";
                                break;
                            case "d": // Trade
                                chanNum = "2";
                                break;
                            case "a": // Localdefense
                                var chanstock = message.Sender.ChannelName;
                                var clean = chanstock.Split('\x0020');
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

                        sb.Append(chanNum + ". " + message.Sender.ChannelName);
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
                default:
                    //sb.ForeColor(Color.FromArgb(255, 255, 255));
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            //sb.Append("[");
            if (message.ChatTag.HasFlag(ChatTag.Dev))
                sb.Append("<Dev>");
            else if (message.ChatTag.HasFlag(ChatTag.Gm))
                sb.Append("<GM>");
            else if (message.ChatTag.HasFlag(ChatTag.Afk))
                sb.Append("<AFK>");
            else if (message.ChatTag.HasFlag(ChatTag.Dnd))
                sb.Append("<DND>");

            sb.Append(message.Sender.Sender);

            //sb.Append("]: ");
            if (message.Sender.Type.ToString() != "ChannelInvitation" && message.Sender.Type.ToString() != "partyGroupInvitation")
            {
                sb.Append(": ");
            }
            sb.Append(message.Message);
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
        protected void ShouldIBeSendingThat()
        {
#if TESTING
            if (securityLevel <= 0)
                Application.Exit();
#endif
        }

        public void RequestTicketList(bool onlineOnly)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_LIST);
            response.Write(onlineOnly);
            Game.SendPacket(response);
#endif
        }
        #region StuffThatNeedsMoved


        public void HelloDad()
        {
#if TESTING
            var response = new OutPacket(WorldCommand.CMSG_HELLO_DADDY);
            response.Write((byte)1);
            Game.SendPacket(response);
#endif
        }



        public void RequestTicketDetails(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_VIEW);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TicketAssign(string playerName, string assignName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_ASSIGN);
            response.Write(playerName.ToCString());
            response.Write(assignName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void DeleteTicket(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_DELETE);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TicketComment(string playerName, string comment)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_COMMENT);
            response.Write(playerName.ToCString());
            response.Write(comment.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TicketResponse(string playerName, string ticketresponse)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_RESPONSE);
            response.Write(playerName.ToCString());
            response.Write(ticketresponse.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TicketResponse(string playerName, byte status)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_ESCALATE);
            response.Write(playerName.ToCString());
            response.Write(status);
            Game.SendPacket(response);
#endif
        }

        public void CompleteTicket(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TICKET_COMPLETE);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void MailPlayer(string playerName, string subject, string body)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_MAIL_PLAYER);
            response.Write(playerName.ToCString());
            response.Write(subject.ToCString());
            response.Write(body.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void RequestQuestStatus(string playerName, uint questId)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_GET_QUEST_STATUS);
            response.Write(playerName.ToCString());
            response.Write(questId);
            Game.SendPacket(response);
#endif
        }

        public void RequestAchievementStatus(string playerName, uint achId)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.SMSG_CLIENT_ACH_STATUS);
            response.Write(playerName.ToCString());
            response.Write(achId);
            Game.SendPacket(response);
#endif
        }

        public void CompleteQuest(string playerName, uint questId)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_COMPLETE_QUEST);
            response.Write(playerName.ToCString());
            response.Write(questId);
            Game.SendPacket(response);
#endif
        }

        public void CompleteAchievement(string playerName, uint achId)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_COMPLETE_ACH);
            response.Write(playerName.ToCString());
            response.Write(achId);
            Game.SendPacket(response);
#endif
        }

        public void AddItem(string playerName, uint itemId, uint count)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_ADD_ITEM);
            response.Write(playerName.ToCString());
            response.Write(itemId);
            response.Write(count);
            Game.SendPacket(response);
#endif
        }

        public void RemoveItem(string playerName, uint itemId, uint count)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_REMOVE_ITEM);
            response.Write(playerName.ToCString());
            response.Write(itemId);
            response.Write(count);
            Game.SendPacket(response);
#endif
        }

        public void HasItem(string playerName, uint itemId)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_HAS_ITEM);
            response.Write(playerName.ToCString());
            response.Write(itemId);
            Game.SendPacket(response);
#endif
        }

        public void QueryPlayerOnlineStatus(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_PLR_IS_ONLINE);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void BanPlayer(byte banType, string playerAccountOrIp, string duration, string reason, string announceReason)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_BAN);
            response.Write(banType);
            response.Write(playerAccountOrIp.ToCString());
            response.Write(duration.ToCString());
            response.Write(reason.ToCString());
            response.Write(announceReason.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TbButton(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_TB_BUTTON);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void RevivePlayer(string playerName)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_REVIVE);
            response.Write(playerName.ToCString());
            Game.SendPacket(response);
#endif
        }

        public void TeleportPlayer(string playerName, uint mapId, float x, float y, float z, float o)
        {
#if TESTING
            ShouldIBeSendingThat();
            var response = new OutPacket(WorldCommand.CMSG_CLIENT_BAN);
            response.Write(playerName.ToCString());
            response.Write(mapId);
            response.Write(x);
            response.Write(y);
            response.Write(z);
            response.Write(o);
            Game.SendPacket(response);
#endif
        }
        #endregion
    }
}

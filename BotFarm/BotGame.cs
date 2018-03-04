using Client;
using Client.UI;
using System;

namespace BotFarm
{
    class BotGame : AutomatedGame
    {
        public bool SettingUp
        {
            get;
            set;
        }

        public BotGame(string hostname, int port, string username, string password, int realmId, int character)
            : base(hostname, port, username, password, realmId, character) {}

        /*public static UInt64 GroupLeaderGuid { get; private set; }
        public static List<UInt64> GroupMembersGuids = new List<UInt64>();
        public List<UInt64> GroupMembersGuids2 = new List<UInt64>();
        public int JoinMessage = 0;

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

            FrmChat frmchat = new FrmChat();
            frmchat.DisplayGroupList();

            message.Message = msg.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            if (msg.ToString() != "")
            {
                Game.UI.PresentChatMessage(message);
            }
        }

        [PacketHandler(WorldCommand.SMSG_GROUP_DESTROYED)]
        protected void HandlePartyDisband(InPacket packet)
        {
            GroupLeaderGuid = 0;
            GroupMembersGuids.Clear();
        }*/

        public override void Start()
        {
            base.Start();

            // Anti-kick for being afk
            //ScheduleAction(() => DoTextEmote(TextEmote.Yawn), DateTime.Now.AddMinutes(5), new TimeSpan(0, 5, 0));
            //ScheduleAction(() =>
            //{
            //    if (LoggedIn)
            //        SendPacket(new OutPacket(WorldCommand.CMSG_KEEP_ALIVE));
            //}, DateTime.Now.AddSeconds(15), new TimeSpan(0, 0, 30));
        }

        public override void InvalidCredentials()
        {
            BotFactory.Instance.RemoveBot(this);

        }

        #region Logging
        public override void Log(string message, LogLevel level = LogLevel.Info)
        {
            BotFactory.Instance.Log(Username + " - " + message, level);
        }

        public override void AuthError(string message)
        {
            //BotFactory.Instance.Log(Username + " - " + message, level);
            BotFactory.Instance.Log(message);
        }

        public override void LogLine(string message, LogLevel level = LogLevel.Info)
        {
            //BotFactory.Instance.Log(Username + " - " + message, level);
            BotFactory.Instance.Log(message, level);
        }

        public override void LogException(string message)
        {
            BotFactory.Instance.Log(Username + " - " + message, LogLevel.Error);
        }

        public override void LogException(Exception ex)
        {
            BotFactory.Instance.Log(string.Format(Username + " - {0} {1}", ex.Message, ex.StackTrace), LogLevel.Error);
        }
        #endregion
    }
}

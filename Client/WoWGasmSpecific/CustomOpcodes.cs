using System;
using Client.Chat;
using Client.Chat.Definitions;
using static Client.Forms.FrmTicket;

namespace Client.World.Network
{
    public partial class WorldSocket
    {
        public void QuickSysMessage(string message)
        {
            var chmessage = new ChatMessage();
            var channel = new ChatChannel
            {
                Type = ChatMessageType.System
            };
            chmessage.Message = message;
            chmessage.Language = 0;
            chmessage.ChatTag = 0;
            chmessage.Sender = channel;
            Game.UI.PresentChatMessage(chmessage);
        }

        [PacketHandler(WorldCommand.SMSG_HELLO_SON)]
        protected void HandleBotAuthReply(InPacket packet) => AutomatedGame.securityLevel = Convert.ToInt32(packet.ReadUInt32());

        [PacketHandler(WorldCommand.SMSG_CLIENT_TICKET_LIST)]
        protected void HandleTicketList(InPacket packet)
        {
            Game.World.ticketList.Clear();
            var ticketCount = packet.ReadUInt32();
            for (var i = 0; i < ticketCount; i++)
            {
                TicketInfo ticket;
                ticket.playerName = packet.ReadCString();
                ticket.createTime = packet.ReadCString();
                ticket.assignedPlayer = packet.ReadCString();
                ticket.ticketComment = packet.ReadCString();
                ticket.areTheyOnline = packet.ReadBoolean();
                Game.World.ticketList.Add(ticket);
            }
            AutomatedGame.TicketUpdate = "1";
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_TICKET_DATA)]
        protected void HandleTicketView(InPacket packet)
        {
            CurrentTicket ticket;
            ticket.playerName = packet.ReadCString();
            ticket.assignedPlayer = packet.ReadCString();
            ticket.ticketMessage = packet.ReadCString();
            ticket.ticketComment = packet.ReadCString();
            ticket.ticketResponse = packet.ReadCString();
            ticket.ticketChatLog = packet.ReadCString();
            Game.World.currentViewedTicket = ticket;
            TicketFrm.DataUpdate();
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_HAS_ITEM)]
        protected void HasItemReply(InPacket packet)
        {
            var playerName = packet.ReadCString();
            var itemId = packet.ReadUInt32();
            var itemName = packet.ReadCString();
            var itemCount = packet.ReadUInt32();
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ONLINE_REPLY)]
        protected void IsPlayerOnlineReply(InPacket packet)
        {
            var playerName = packet.ReadCString();
            var isOnline = packet.ReadByte() == 1 ? "Online" : "Offline";
            QuickSysMessage(playerName + " is " + isOnline + ".");
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_QUEST_STATUS)]
        protected void QuestStatusReply(InPacket packet)
        {
            var playerName = packet.ReadCString();
            var questId = packet.ReadUInt32();
            var questStatus = packet.ReadByte();
            //string questStatusMessage = questStatus.ToString();
            var canCompleteQuest = packet.ReadBoolean();
            QuickSysMessage(playerName + "'s status for quest " + questId + " is " + questStatus + " can be completed status is " + canCompleteQuest + " .");
            //ToDo: Detailed Info
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ACH_STATUS)]
        protected void AchStatusReply(InPacket packet)
        {
            var playerName = packet.ReadCString();
            var achid = packet.ReadUInt32();
            var hasCompletedAch = packet.ReadBoolean();
            QuickSysMessage(playerName + " status for achivement " + achid + " is " + hasCompletedAch + " .");
            //ToDo: Detailed Info
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_COMPLETE_X_REPLY)]
        protected void CompleteXReply(InPacket packet)
        {
            var playerName = packet.ReadCString();
            var questOrAchId = packet.ReadUInt32();
            var questOrAch = packet.ReadByte();
            if (questOrAch == 2)
                QuickSysMessage("You have completed achivement " + Extensions.GetAchName(Convert.ToInt32(questOrAchId)) + " for " + playerName + " .");
            else
                QuickSysMessage("You have completed quest " + questOrAchId + " for " + playerName + " .");
        }
    }
}

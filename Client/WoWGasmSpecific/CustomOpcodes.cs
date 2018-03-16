using System;
using System.Windows;
using Client.Chat;
using Client.Chat.Definitions;
using Client.Forms;
using static Client.Forms.FrmTicket;

namespace Client.World.Network
{
    public partial class WorldSocket
    {
        public void QuickSysMessage(string message)
        {
            ChatMessage chmessage = new ChatMessage();
            ChatChannel channel = new ChatChannel
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
            uint ticketCount = packet.ReadUInt32();
            for (int i = 0; i < ticketCount; i++)
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
            string playerName = packet.ReadCString();
            uint itemId = packet.ReadUInt32();
            string itemName = packet.ReadCString();
            uint itemCount = packet.ReadUInt32();
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ONLINE_REPLY)]
        protected void IsPlayerOnlineReply(InPacket packet)
        {
            string playerName = packet.ReadCString();
            string isOnline = packet.ReadByte() == 1 ? "Online" : "Offline";
            QuickSysMessage(playerName + " is " + isOnline + ".");
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_QUEST_STATUS)]
        protected void QuestStatusReply(InPacket packet)
        {
            string playerName = packet.ReadCString();
            uint questId = packet.ReadUInt32();
            byte questStatus = packet.ReadByte();
            //string questStatusMessage = questStatus.ToString();
            bool canCompleteQuest = packet.ReadBoolean();
            QuickSysMessage(playerName + "'s status for quest " + questId.ToString() + " is " + questStatus.ToString() + " can be completed status is " + canCompleteQuest.ToString() + " .");
            //ToDo: Detailed Info
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ACH_STATUS)]
        protected void AchStatusReply(InPacket packet)
        {
            string playerName = packet.ReadCString();
            uint achid = packet.ReadUInt32();
            bool hasCompletedAch = packet.ReadBoolean();
            QuickSysMessage(playerName + " status for achivement " + achid + " is " + hasCompletedAch.ToString() + " .");
            //ToDo: Detailed Info
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_COMPLETE_X_REPLY)]
        protected void CompleteXReply(InPacket packet)
        {
            string playerName = packet.ReadCString();
            uint questOrAchId = packet.ReadUInt32();
            byte questOrAch = packet.ReadByte();
            if (questOrAch == 2)
                QuickSysMessage("You have completed achivement " + Client.Extensions.GetAchName(Convert.ToInt32(questOrAchId)) + " for " + playerName + " .");
            else
                QuickSysMessage("You have completed quest " + questOrAchId + " for " + playerName + " .");
        }
    }
}

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
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_TICKET_DATA)]
        protected void HandleTicketView(InPacket packet)
        {
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_HAS_ITEM)]
        protected void HasItemReply(InPacket packet)
        {
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ONLINE_REPLY)]
        protected void IsPlayerOnlineReply(InPacket packet)
        {
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_QUEST_STATUS)]
        protected void QuestStatusReply(InPacket packet)
        {
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_ACH_STATUS)]
        protected void AchStatusReply(InPacket packet)
        {
        }

        [PacketHandler(WorldCommand.SMSG_CLIENT_COMPLETE_X_REPLY)]
        protected void CompleteXReply(InPacket packet)
        {
        }
    }
}

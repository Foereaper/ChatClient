using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Chat;
using Client.Chat.Definitions;
using System.Text;
using System.Text.RegularExpressions;
using Client.World.Definitions;


namespace Client.World.Network
{
    public partial class WorldSocket
    {
        public Race Race { get; private set; }
        public Class Class { get; private set; }

        //SMSG_GROUP_LIST = 125
        /*[PacketHandler(WorldCommand.SMSG_GROUP_LIST)]
        protected void HandleGroupList(InPacket packet)
        {
            GroupType groupType = (GroupType)packet.ReadByte();
            packet.ReadByte();
            packet.ReadByte();
            packet.ReadByte();
            if (groupType.HasFlag(GroupType.GROUPTYPE_LFG)) { }

            uint membersCount = packet.ReadUInt32();

            byte[] dump = packet.ReadToEnd();

            #region crap
            //ChatMessage message = new ChatMessage();
            //ChatChannel channel = new ChatChannel();
            //channel.Type = ChatMessageType.System;

            // index[0] 2 == 
            // index[0] 3 == 
            // index[0] 5 == 
            // 

            //StringBuilder Player = new StringBuilder();

            //byte[] dump = packet.ReadToEnd();


            //message.Message = Player.ToString();
            //message.Language = 0;
            //message.ChatTag = 0;
            //message.Sender = channel;
            //Game.UI.PresentChatMessage(message);
            #endregion
        }*/

        //SMSG_GROUP_INVITE = 111
        [PacketHandler(WorldCommand.SMSG_GROUP_INVITE)]
        protected void HandleGroupInvite(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = ChatMessageType.partyGroupInvitation;

            // index[0] 1 == invited to party group

            StringBuilder builder = new StringBuilder();
            byte[] dump = packet.ReadToEnd();

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

            channel.ChannelName = builder.ToString();
            message.Message = "";
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            Game.UI.PresentChatMessage(message);

            //new OutPacket(WorldCommand.CMSG_GROUP_ACCEPT, 4)
        }

        //CMSG_CHANNEL_INVITE = 163
        [PacketHandler(WorldCommand.CMSG_CHANNEL_INVITE)]
        protected void HandleChannelInvite(InPacket packet)
        {
            // 1 == party invite
            byte[] dump = packet.ReadToEnd();
            var tes = "";
            //new OutPacket(WorldCommand.CMSG_GROUP_ACCEPT, 4);
        }

        //SMSG_GROUP_DECLINE = 116
        [PacketHandler(WorldCommand.SMSG_GROUP_DECLINE)]
        protected void handleGroupDecline(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = ChatMessageType.System;

            StringBuilder Player = new StringBuilder();

            byte[] dump = packet.ReadToEnd();

            int slen = ((int)packet.BaseStream.Length);
            for (int i = 0; i < slen; i++)
            {
                if ((char)dump[i] != 0x00)
                {
                    Player.Append((char)dump[i]);
                }
                else
                {
                    i = slen;
                }
            }
            message.Message = Player.ToString() + " declines your group invitation.";
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            //System.Threading.Thread.Sleep(100); // don't process too fast
            Game.UI.PresentChatMessage(message);
        }

        //SMSG_PARTY_COMMAND_RESULT = 127
        [PacketHandler(WorldCommand.SMSG_PARTY_COMMAND_RESULT)]
        protected void HandlePartyCommand(InPacket packet)
        {
            //var notifymsg = "";
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = ChatMessageType.Party;

            // 1 byte after invited username 1 means user doesn't exist
            // 1 byte after invited username 5 means user already in group party

            StringBuilder Player = new StringBuilder();
            StringBuilder ResultCommand = new StringBuilder();
            byte[] dump = packet.ReadToEnd();

            //packet.BaseStream.Position = 0;
            int state = 0;
            int slen = ((int)packet.BaseStream.Length);
            for (int i = 4; i < slen; i++)
            {
                if ((char)dump[i] != 0x00)
                {
                    if(state == 0)
                    {
                        Player.Append((char)dump[i]);
                    }
                    if (state == 1)
                    {
                        ResultCommand.Append((int)dump[i]);
                    }
                }
                else
                {
                    state+=1;
                    if(state == 2)
                    {
                        i = slen;
                    }
                }
            }

            //channel.ChannelName = "";

            switch (ResultCommand.ToString())
            {
                case "":
                    message.Message = "You invited '" + Player.ToString() + "' to join your group.";
                    break;
                case "1":
                    message.Message = "Player '" + Player.ToString() + "' doesn't exist.";
                    break;
                case "5":
                    message.Message = "Player '" + Player.ToString() + "' is already in a group.";
                    break;
                case "7":
                    message.Message = "You are already in this group.";
                    break;
                default:
                    message.Message = "Unknown command in SMSG_PARTY_COMMAND_RESULT handler.";
                    break;
            }

            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            //System.Threading.Thread.Sleep(100); // don't process too fast
            Game.UI.PresentChatMessage(message);

        }

        /* 
        //SMSG_CHANNEL_LIST = 155,
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

            if ((char)dump[0] == 0x00) { notifymsg = "A user joined the channel."; }
            if ((char)dump[0] == 0x01) { notifymsg = "A user left the channel."; }
            if ((char)dump[0] == 0x02) { notifymsg = "Channel joined."; }
            if ((char)dump[0] == 0x03) { notifymsg = "Channel left."; }
            if ((char)dump[0] == 0x05) { notifymsg = "You already left this channel.."; }
            if ((char)dump[0] == 0x08) { notifymsg = "You are ower of the channel now."; } // 8
            if ((char)dump[0] == 0x0C) { notifymsg = "Moderation privileges given to you."; } // 12
            if ((char)dump[0] == 0x18) { notifymsg = ""; } // 24 Invited to join channel

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
        }*/

        [PacketHandler(WorldCommand.SMSG_MOTD)]
        protected void HandleServerMOTD(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;

            StringBuilder builder = new StringBuilder();
            packet.BaseStream.Position = 0;
            int slen = ((int)packet.BaseStream.Length);
            for (int i = 0; i < slen; i++)
            {
                int currentbyte = packet.BaseStream.ReadByte();
                if (currentbyte != 0x00 && currentbyte != 0x01)
                {
                    builder.Append((char)currentbyte);
                }
            }
            #region old crap
            /*result  "\u0001\0\0\0Welcome to x! Enjoy your stay!\0"   string
            result = result.Substring(4);
            result = result.TrimEnd('\0'); fuck trim and substring!*/
            #endregion
            message.Message = builder.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            Game.UI.PresentChatMessage(message);
        }

        /// <summary>
        /// SMSG_GUILD_ROSTER = 138 GET GUILD player list data
        /// Glues together > CMSG_GUILD_MOTD = 145, SMSG_GUILD_EVENT = 146
        /// <summary>
        [PacketHandler(WorldCommand.SMSG_GUILD_ROSTER)]
        protected void HandleGuildMotd(InPacket packet)
        {
            System.Threading.Thread.Sleep(200);
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;

            byte[] dump = packet.ReadToEnd();
            StringBuilder builder = new StringBuilder();
            string debug = System.Text.Encoding.UTF8.GetString(packet.ReadToEnd());

            #region testing crap
            // Guild MOTD 
            //for (int i = 4; dump[i] != 0x07; i++)
            //{
            //    if (dump[i] == 0x0) { builder.Append((char)0x20); i++; }
            //    if (dump[i] != 0x07) { builder.Append((char)dump[i]); }
            //}
            //string debug2 = System.Text.Encoding.UTF8.GetString(dump);
            #endregion

            for (int i = 4; dump[i] != 0xFF; i++)
            {
                if(dump[i] == 0x0 && dump[i+1] == 0x7) //end signature of motd
                {
                    break;
                }
                if (dump[i] == 0x0) 
                {
                    builder.Append((char)0x20); // glue messages together..
                    i++;
                }
                if (dump[i] != 0xFF)
                {
                    builder.Append((char)dump[i]);
                }
            }

            message.Message = builder.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            Game.UI.PresentChatMessage(message);
        }

        /*
        /// <summary>
        /// CMSG_WHO = 98 full who player list data
        /// Warning, this code burns your eyes !!! REFACTORING NEEDED !!
        /// At first I wrote code that handles data, check if in guild or not and read bytes and jump jump jump..
        /// but this code had a flaw/bug so I did this, really bad but whatever..
        /// 
        /// # extra debug information; #
        /// Layout who list ;
        /// 
        /// byte[0] = people found / online
        /// byte[1,2,3] skip == 0x0
        /// byte[4] = people found / online
        /// byte[5,6,7] skip == 0x0
        /// byte[8] = start first username ascii byte
        /// 
        /// keep in mind lvl is in hex and you need cast it back to dec.
        /// 
        /// Not in GUILD
        /// Username 00 00 lvl 00 00 00 Class 00 00 00 Race <Delimiter here>
        ///
        /// In GUILD
        /// Username 00 Guild 00 lvl 00 00 00 Class 00 00 00 Race <Delimiter here>
        ///
        /// Data delimiter 8x bytes 0x00 or 0x??, examples below:
        /// 00 00 00 00 01 00 00 00  after first user
        /// 00 00 00 00 D2 00 00 00  after second user
        /// 00 00 00 00 9B 0D 00 00  after third user
        /// 00 00 00 01 9B 0D 00 00  and so on..
        /// 00 00 00 00 D2 00 00 00
        /// 00 00 00 01 2B 11 00 00
        /// 00 00 00 00 EF 05 00 00
        /// </summary>
        [PacketHandler(WorldCommand.SMSG_WHO)]
        protected void HandleWhoList(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;

            StringBuilder newbuild = new StringBuilder();

            byte[] dump = packet.ReadToEnd();
            for (int i = 0; i < dump.Length; i++)
            {
                if((int)dump[i] >= 32 && (int)dump[i] <= 122)
                {
                    
                    if((int)dump[i] == 32)
                    {
                        newbuild.Append("_");
                    } else
                    {
                        newbuild.Append((char)dump[i]);
                    }
                    
                }
                if((char)dump[i] == 0)
                {
                    newbuild.Append("  ");
                }
                if ((byte)dump[i] >= 1 && (byte)dump[i] <= 64)
                {
                    if((byte)dump[i] != 32)
                    {
                        newbuild.Append((int)dump[i]);
                    }
                }
            }

            StringBuilder builder = new StringBuilder();

            string players = "";
            string[] AllInfos = newbuild.ToString().Split(new[] { "  " }, StringSplitOptions.None); // '\x0020'

            players = players + (int)dump[0] + " People found online:\r\n";

            for(int i = 0; i < AllInfos.Length; i++)
            {
                string current = AllInfos[i];
                if(Regex.Matches(current, @"[a-zA-Z]").Count > 2)
                {
                    players = players + "[] " + AllInfos[i].ToString();
                    if(AllInfos[i+1].ToString() != "")
                    {
                        players = players + " Guild: " + AllInfos[i + 1].ToString().Replace("_", " ");
                        string Level = AllInfos[i + 2].ToString();
                        if(Regex.Matches(Level, @"[a-zA-Z]").Count > 0)
                        {
                            int lvl = (int)System.Convert.ToChar(AllInfos[i + 2].ToString());
                            players = players + " LvL: " + lvl;

                        }
                        else
                        {
                            Level = Regex.Replace(Level, @"[^\d]", "");
                            players = players + " LvL: " + Level.ToString();
                        }
                        Class = (Class)(int)System.Convert.ToDecimal(AllInfos[i + 5].ToString());
                        Race = (Race)(int)System.Convert.ToDecimal(AllInfos[i + 8].ToString());
                        players = players + " Class: " + Class.ToString();
                        players = players + " Race: " + Race.ToString() + "\r\n";
                        i += 3;
                    }
                    else
                    {
                        players = players + " Guild: none";
                        string Level = AllInfos[i + 2].ToString();
                        if (Regex.Matches(Level, @"[a-zA-Z]").Count > 0)
                        {
                            int lvl = (int)System.Convert.ToChar(AllInfos[i + 2].ToString());
                            players = players + " LvL: " + lvl;

                        }
                        else
                        {
                            Level = Regex.Replace(Level, @"[^\d]", "");
                            players = players + " LvL: " + Level.ToString();
                        }
                        Class = (Class)(int)System.Convert.ToDecimal(AllInfos[i + 5].ToString());
                        Race = (Race)(int)System.Convert.ToDecimal(AllInfos[i + 8].ToString());
                        players = players + " Class: " + Class.ToString();
                        players = players + " Race: " + Race.ToString() + "\r\n";
                    }
                }
            }

            message.Message = players.ToString();
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channel;
            Game.UI.PresentChatMessage(message);
        }*/

        [PacketHandler(WorldCommand.SMSG_MESSAGECHAT)]
        protected void HandleMessageChat(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();
            var guid = packet.ReadUInt64();
            var unkInt = packet.ReadInt32();

            switch (type)
            {
                case ChatMessageType.Say:
                    var test0 = "";
                    break;
                case ChatMessageType.Yell:
                case ChatMessageType.Party:
                case ChatMessageType.PartyLeader:
                case ChatMessageType.Raid:
                case ChatMessageType.RaidLeader:
                case ChatMessageType.RaidWarning:
                case ChatMessageType.Guild:
                    {
                        ChatChannel channelg = new ChatChannel();
                        channelg.Type = type;

                        if (type == ChatMessageType.Channel)
                            channelg.ChannelName = packet.ReadCString();

                        //var senderg = packet.ReadUInt64();
                        var senderg = guid;

                        ChatMessage messageg = new ChatMessage();
                        int textLen = packet.ReadInt32();
                        int tLen = unchecked((int)packet.BaseStream.Length);
                        messageg.Message = packet.ReadCString();//packet.ReadCStringFix(packet.ReadBytes(tLen));

                        messageg.Language = lang;
                        messageg.ChatTag = 0;
                        messageg.Sender = channelg;

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        string senderNameg = null;
                        if (type == ChatMessageType.System ||
                            Game.World.PlayerNameLookup.TryGetValue(senderg, out senderNameg))
                        {
                            if (lang.ToString() != "Addon")
                            {
                                messageg.Sender.Sender = senderNameg;
                                Game.UI.PresentChatMessage(messageg);
                                return;
                            }
                        }

                        if (lang.ToString() == "Addon")
                        {
                            return;
                        }

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueueg = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(senderg, out messageQueueg))
                            messageQueueg.Enqueue(messageg);
                        //! or non existent
                        else
                        {
                            messageQueueg = new Queue<ChatMessage>();
                            messageQueueg.Enqueue(messageg);
                            Game.World.QueuedChatMessages.Add(senderg, messageQueueg);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket responseg = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        responseg.Write(senderg);
                        Game.SendPacket(responseg);

                        //! Enqueued chat will be printed when we receive SMSG_NAME_QUERY_RESPONSE

                        break;
                    }
                case ChatMessageType.Officer:
                case ChatMessageType.Emote:
                case ChatMessageType.TextEmote:
                case ChatMessageType.Whisper:
                    {
                        ChatChannel channelw = new ChatChannel();
                        channelw.Type = type;

                        if (type == ChatMessageType.Channel)
                            channelw.ChannelName = packet.ReadCString();

                        //var senderg = packet.ReadUInt64();
                        var senderw = guid;

                        ChatMessage messagew = new ChatMessage();
                        //int textLen = packet.ReadInt32();
                        //int tLen = unchecked((int)packet.BaseStream.Length);
                        messagew.Message = packet.ReadCString();//packet.ReadCStringFix(packet.ReadBytes(tLen));

                        messagew.Language = lang;
                        messagew.ChatTag = 0;
                        messagew.Sender = channelw;

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        string senderNamew = null;
                        if (type == ChatMessageType.System ||
                            Game.World.PlayerNameLookup.TryGetValue(senderw, out senderNamew))
                        {
                            messagew.Sender.Sender = senderNamew;
                            Game.UI.PresentChatMessage(messagew);
                            return;
                        }

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueuew = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(senderw, out messageQueuew))
                            messageQueuew.Enqueue(messagew);
                        //! or non existent
                        else
                        {
                            messageQueuew = new Queue<ChatMessage>();
                            messageQueuew.Enqueue(messagew);
                            Game.World.QueuedChatMessages.Add(senderw, messageQueuew);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket responsew = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        responsew.Write(senderw);
                        Game.SendPacket(responsew);

                        //! Enqueued chat will be printed when we receive SMSG_NAME_QUERY_RESPONSE

                        break;
                    }
                case ChatMessageType.WhisperInform:
                    {
                        ChatChannel channelw = new ChatChannel();
                        channelw.Type = type;

                        if (type == ChatMessageType.Channel)
                            channelw.ChannelName = packet.ReadCString();

                        //var senderg = packet.ReadUInt64();
                        var senderw = guid;

                        ChatMessage messagew = new ChatMessage();
                        //int textLen = packet.ReadInt32();
                        //int tLen = unchecked((int)packet.BaseStream.Length);
                        messagew.Message = packet.ReadCString();//packet.ReadCStringFix(packet.ReadBytes(tLen));

                        messagew.Language = lang;
                        messagew.ChatTag = 0;
                        messagew.Sender = channelw;

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        string senderNamew = null;
                        if (type == ChatMessageType.System ||
                            Game.World.PlayerNameLookup.TryGetValue(senderw, out senderNamew))
                        {
                            messagew.Sender.Sender = senderNamew;
                            Game.UI.PresentChatMessage(messagew);
                            return;
                        }

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueuew = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(senderw, out messageQueuew))
                            messageQueuew.Enqueue(messagew);
                        //! or non existent
                        else
                        {
                            messageQueuew = new Queue<ChatMessage>();
                            messageQueuew.Enqueue(messagew);
                            Game.World.QueuedChatMessages.Add(senderw, messageQueuew);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket responsew = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        responsew.Write(senderw);
                        Game.SendPacket(responsew);

                        //! Enqueued chat will be printed when we receive SMSG_NAME_QUERY_RESPONSE

                        break;
                    }
                case ChatMessageType.System:
                    {
                        ChatChannel channelgc = new ChatChannel();
                        channelgc.Type = type;

                        if (type == ChatMessageType.Channel)
                            channelgc.ChannelName = packet.ReadCString();

                        //var senderg = packet.ReadUInt64();
                        var sendergc = guid;

                        ChatMessage messagegc = new ChatMessage();
                        //int textLen = packet.ReadInt32();
                        //int tLen = unchecked((int)packet.BaseStream.Length);
                        messagegc.Message = packet.ReadCString();//packet.ReadCStringFix(packet.ReadBytes(tLen));

                        messagegc.Language = lang;
                        messagegc.ChatTag = 0;
                        messagegc.Sender = channelgc;
                        break;
                    }
                case ChatMessageType.Channel:
                    {
                        ChatChannel channelChat = new ChatChannel();
                        channelChat.Type = type;

                        var senderChat = guid;

                        ChatMessage messageChannel = new ChatMessage();

                        StringBuilder builder = new StringBuilder();
                        packet.BaseStream.Position = 0;
                        int length = (int)packet.BaseStream.Length;
                        byte[] dump = packet.ReadBytes(length);
                        var ChanName = "";
                        for (int i = 17; i < (length - 2); i++)
                        {
                            if ((char)dump[i] != 0x00)
                            {
                                builder.Append((char)dump[i]);
                            }
                            else
                            {
                                ChanName = builder.ToString();
                                builder.Clear();
                                i += 13;
                                builder.Append((char)dump[i]);
                            }
                        }

                        channelChat.ChannelName = ChanName;
                        messageChannel.Message = builder.ToString();
                        messageChannel.Language = lang;
                        messageChannel.ChatTag = 0;
                        messageChannel.Sender = channelChat;

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        string senderNameChannel = null;
                        if (type == ChatMessageType.System ||
                            Game.World.PlayerNameLookup.TryGetValue(senderChat, out senderNameChannel))
                        {
                            messageChannel.Sender.Sender = senderNameChannel;
                            Game.UI.PresentChatMessage(messageChannel);
                            return;
                        }

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueueChannel = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(senderChat, out messageQueueChannel))
                            messageQueueChannel.Enqueue(messageChannel);
                        //! or non existent
                        else
                        {
                            messageQueueChannel = new Queue<ChatMessage>();
                            messageQueueChannel.Enqueue(messageChannel);
                            Game.World.QueuedChatMessages.Add(senderChat, messageQueueChannel);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket responseChannel = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        responseChannel.Write(senderChat);
                        Game.SendPacket(responseChannel);
                        break;
                    }
                case ChatMessageType.Battleground:
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                case ChatMessageType.BattlegroundLeader:
                case ChatMessageType.Achievement:
                case ChatMessageType.GuildAchievement:
                    {
                        ChatChannel channel = new ChatChannel();
                        channel.Type = type;

                        if (type == ChatMessageType.Channel)
                            channel.ChannelName = packet.ReadCString();

                        var sender = packet.ReadUInt64();

                        ChatMessage message = new ChatMessage();
                        var textLeng = packet.ReadInt32();

                        string clean = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(packet.ReadCString()));

                        message.Message = clean;
                        message.Language = lang;
                        message.ChatTag = 0;
                        message.Sender = channel;

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        string senderName = null;
                        if (type == ChatMessageType.System ||
                            Game.World.PlayerNameLookup.TryGetValue(sender, out senderName))
                        {
                            message.Sender.Sender = senderName;
                            Game.UI.PresentChatMessage(message);
                            return;
                        }

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueue = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(sender, out messageQueue))
                            messageQueue.Enqueue(message);
                        //! or non existent
                        else
                        {
                            messageQueue = new Queue<ChatMessage>();
                            messageQueue.Enqueue(message);
                            Game.World.QueuedChatMessages.Add(sender, messageQueue);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket response = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        response.Write(sender);
                        Game.SendPacket(response);

                        //! Enqueued chat will be printed when we receive SMSG_NAME_QUERY_RESPONSE

                        break;
                    }
                default:
                    return;
            }
        }
        #region old message handler
        /*[PacketHandler(WorldCommand.SMSG_MESSAGECHAT)]
        protected void HandleMessageChat3(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();

            var guid = packet.ReadUInt64();
            var unkInt = packet.ReadInt32();

            string senderName = null;
            Game.World.PlayerNameLookup.TryGetValue(guid, out senderName);

            //GetPlayerName

            //<ulong, Queue<ChatMessage>> QueuedChatMessages

            //ChatMessage test = new ChatMessage();
            //List<string> allPlayers = new List<string>();
            //foreach (KeyValuePair<ulong, Queue < ChatMessage >> entry in Game.World.QueuedChatMessages)
            //{
               // allPlayers.Add(entry.);
            //}


            ChatChannel channel = new ChatChannel();
            channel.Type = type;

            //if (type == ChatMessageType.Channel)
            //    channel.ChannelName = packet.ReadCString();
            //if (type == ChatMessageType.Guild)
            //    channel.ChannelName = packet.ReadCString();


            var sender = packet.ReadUInt64();
            channel.Sender = senderName;

            if (lang.ToString() != "Addon")
            {
                //"T\0\0\0|cffffcc00In need of critical support? Join discord and use the #support channel.|r\0\0"

                var dbg = "";
            }


            ChatMessage message = new ChatMessage();
            var textLen = packet.ReadInt32();

            string result = System.Text.Encoding.UTF8.GetString(packet.ReadToEnd());
            if (result.Contains("T\0\0\0"))
            {
                result = result.Substring(14);
                result = result.TrimEnd('\0');
                result = result.TrimEnd('\0');
                result = result.TrimEnd('r');
                result = result.TrimEnd('|');
                message.Message = result.ToString();
                message.Language = 0; // system
                message.ChatTag = 0;
                channel.Type = 0;
            }
            else
            {
                if(channel.Sender == null)  // message.Sender
                {
                    //"|cffffcc00Have you seen our WoWGasm shop yet? You can vote or donate for great rewards! |r\0\0"	string
                    result = result.Substring(10);
                    result = result.TrimEnd('\0');
                    result = result.TrimEnd('\0');
                    result = result.TrimEnd('r');
                    result = result.TrimEnd('|');
                    message.Message = result.ToString();
                    message.Language = 0;
                    message.ChatTag = 0;
                    channel.Type = 0;
                }
                else
                {
                    //message -- 2
                    result = result.TrimEnd('\0');
                    result = result.TrimEnd('\0');
                    message.Message = result;
                    message.Language = lang;
                    message.ChatTag = ChatTag.None;
                }
            }

            message.Sender = channel;

            if (lang.ToString() == "System" || lang.ToString() == "Universal")
            {
                Game.UI.PresentChatMessage(message);
            }

            if (lang.ToString() != "Addon")
            {
                Game.UI.PresentChatMessage(message);
            }

        }*/
        #endregion

        [PacketHandler(WorldCommand.SMSG_NOTIFICATION)]
        protected void HandleMessageChat5(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();
            var guid = packet.ReadUInt64();
            var unkInt = packet.ReadInt32();

            ChatChannel channel = new ChatChannel();
            channel.Type = type;

            if (type == ChatMessageType.Channel)
                channel.ChannelName = packet.ReadCString();

            var sender = packet.ReadUInt64();

            ChatMessage message = new ChatMessage();
            var textLen = packet.ReadInt32();
            message.Message = packet.ReadCString();
            message.Language = lang;
            message.ChatTag = (ChatTag)packet.ReadByte();
            message.Sender = channel;

            Game.UI.PresentChatMessage(message);
        }

        [PacketHandler(WorldCommand.SMSG_SERVER_MESSAGE)]
        protected void HandleMessageChat6(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();
            var guid = packet.ReadUInt64();
            var unkInt = packet.ReadInt32();

            ChatChannel channel = new ChatChannel();
            channel.Type = type;

            if (type == ChatMessageType.Channel)
                channel.ChannelName = packet.ReadCString();

            var sender = packet.ReadUInt64();

            ChatMessage message = new ChatMessage();
            var textLen = packet.ReadInt32();
            message.Message = packet.ReadCString();
            message.Language = lang;
            message.ChatTag = (ChatTag)packet.ReadByte();
            message.Sender = channel;

            Game.UI.PresentChatMessage(message);
        }

        [PacketHandler(WorldCommand.CMSG_MESSAGECHAT)]
        protected void HandleMessageChat2(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();
            var guid = packet.ReadUInt64();
            var unkInt = packet.ReadInt32();

            ChatChannel channel = new ChatChannel();
            channel.Type = type;

            if (type == ChatMessageType.Channel)
                channel.ChannelName = packet.ReadCString();

            var sender = packet.ReadUInt64();

            ChatMessage message = new ChatMessage();
            var textLen = packet.ReadInt32();
            message.Message = packet.ReadCString();
            message.Language = lang;
            message.ChatTag = (ChatTag)packet.ReadByte();
            message.Sender = channel;

            Game.UI.PresentChatMessage(message);
        }

        [PacketHandler(WorldCommand.SMSG_GM_MESSAGECHAT)] // SMSG_GM_MESSAGECHAT // SMSG_MESSAGECHAT
        protected void HandleGMMessageChat(InPacket packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            var lang = (Language)packet.ReadInt32();
            var guid = packet.ReadUInt64();
            //var unkInt = packet.ReadInt32();
            var sender = packet.ReadUInt64();
           

            switch (type)
            {
                case ChatMessageType.Say:
                case ChatMessageType.Yell:
                case ChatMessageType.Party:
                case ChatMessageType.PartyLeader:
                case ChatMessageType.Raid:
                case ChatMessageType.RaidLeader:
                case ChatMessageType.RaidWarning:
                case ChatMessageType.Guild:
                    
                    ChatMessage messageg = new ChatMessage();
                    //var textLen = packet.ReadInt32();
                    //string result = System.Text.Encoding.UTF8.GetString(packet.ReadBytes(packet.ReadInt32()));
                    long len = packet.BaseStream.Length;
                    // 12                                  13                             2
                    //"\0\0\0\0\0\0\0\0\b\0\0\0Biotech\0\0\0\0\0\0\0\0\0\u0005\0\0\0nice\0\0"
                    //"\0\0\0\0\0\0\0\0\b\0\0\0Biotech\0\0\0\0\0\0\0\0\0\u0005\0\0\0test\0\0"
                    //"\0\0\0\0\0\0\0\0\b\0\0\0Biotech\0\0\0\0\0\0\0\0\0\b\0\0\0xd haha\0\0"
                    ChatChannel channelg = new ChatChannel();
                    channelg.Type = type;

                    string resultg = System.Text.Encoding.UTF8.GetString(packet.ReadBytes(Convert.ToInt32(len)));

                    resultg = resultg.Substring(12);
                    resultg = resultg.TrimEnd('\0');
                    resultg = resultg.TrimEnd('\0');
                    string[] getdata = resultg.Split('\0');
                    var user = getdata[0];
                    var usermessage = resultg.Substring(13 + user.Length);

                    //channelg.ChannelName = user;
                    channelg.Sender = user;

                    messageg.Message = usermessage;
                    messageg.Language = 0;
                    messageg.ChatTag = ChatTag.Gm;
                    messageg.Sender = channelg;

                    Game.UI.PresentChatMessage(messageg);

                    //messageg.Message = resultg;
                    //Console.WriteLine(messageg.Message + "/n");

                    break;
                case ChatMessageType.Officer:
                case ChatMessageType.Emote:
                case ChatMessageType.TextEmote:
                case ChatMessageType.Whisper:



                    break;
                case ChatMessageType.WhisperInform:
                case ChatMessageType.System:
                case ChatMessageType.Channel:
                    {
                        ChatChannel channelChat = new ChatChannel();
                        channelChat.Type = type;

                        var senderChat = guid;

                        ChatMessage messageChannel = new ChatMessage();

                        StringBuilder bSender = new StringBuilder();
                        StringBuilder bChannel = new StringBuilder();
                        StringBuilder bMessage = new StringBuilder();
                        packet.BaseStream.Position = 0;
                        int length = (int)packet.BaseStream.Length;
                        byte[] dump = packet.ReadBytes(length);
                        //string debug = System.Text.Encoding.UTF8.GetString(packet.ReadBytes(Convert.ToInt32(length)));
                        string debug2 = System.Text.Encoding.UTF8.GetString(dump);
                        // "\u0011\0\0\0\0�\0\0\0\0\0\0\0\0\0\0\0\b\0\0\0Biotech\0Trade - City\0�\0\0\0\0\0\0\0\u0015\0\0\0Channel test 123 123\0\0"
                        // 21 start name
                        // einde van channel name +13 = msg

                        var ChanName = "";
                        int index = 0;
                        for (int i = 21; i < (length - 2); i++)
                        {
                            if ((char)dump[i] != 0x00)
                            {
                                if (index == 0) { bSender.Append((char)dump[i]); }
                                if (index == 1) { bChannel.Append((char)dump[i]); }
                                if (index == 2) { bMessage.Append((char)dump[i]); }
                            }
                            else
                            {
                                index += 1;
                                if(index == 2) { i += 12; };
                            }
                        }

                        channelChat.ChannelName = bChannel.ToString();
                        messageChannel.Message = bMessage.ToString();
                        messageChannel.Language = lang;
                        messageChannel.ChatTag = 0;
                        messageChannel.Sender = channelChat;
                        messageChannel.Sender.Sender = bSender.ToString();

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        //string senderNameChannel = null;
                        //if (type == ChatMessageType.System ||
                        //    Game.World.PlayerNameLookup.TryGetValue(senderChat, out senderNameChannel))
                        //{
                        //    messageChannel.Sender.Sender = senderNameChannel;
                            Game.UI.PresentChatMessage(messageChannel);
                            return;
                        //}

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueueChannel = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(senderChat, out messageQueueChannel))
                            messageQueueChannel.Enqueue(messageChannel);
                        //! or non existent
                        else
                        {
                            messageQueueChannel = new Queue<ChatMessage>();
                            messageQueueChannel.Enqueue(messageChannel);
                            Game.World.QueuedChatMessages.Add(senderChat, messageQueueChannel);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket responseChannel = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        responseChannel.Write(senderChat);
                        Game.SendPacket(responseChannel);
                        break;
                    }
                case ChatMessageType.Battleground:
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                case ChatMessageType.BattlegroundLeader:
                case ChatMessageType.Achievement:
                case ChatMessageType.GuildAchievement:
                    {
                        //ChatChannel channel = new ChatChannel();
                        //channel.Type = type;

                        //if (type == ChatMessageType.Channel)
                        //    channel.ChannelName = packet.ReadCString();

                        //var sender = packet.ReadUInt64();

                        /*ChatMessage message = new ChatMessage();
                        var textLen = packet.ReadInt32();
                        message.Message = packet.ReadCString();
                        message.Language = lang;
                        message.ChatTag = (ChatTag)packet.ReadByte();
                        message.Sender = channel;*/

                        //! If we know the name of the sender GUID, use it
                        //! For system messages sender GUID is 0, don't need to do anything fancy
                        //string senderName = null;
                        //if (type == ChatMessageType.System ||
                        //    Game.World.PlayerNameLookup.TryGetValue(sender, out senderName))
                        //{
                            //message.Sender.Sender = senderName;
                            //Game.UI.PresentChatMessage(message);
                        //    return;
                        //}

                        //! If not we place the message in the queue,
                        //! .. either existent
                        Queue<ChatMessage> messageQueue = null;
                        if (Game.World.QueuedChatMessages.TryGetValue(sender, out messageQueue))
                            messageQueue.Enqueue(null);
                        //! or non existent
                        else
                        {
                            messageQueue = new Queue<ChatMessage>();
                            //messageQueue.Enqueue(message);
                            Game.World.QueuedChatMessages.Add(sender, messageQueue);
                        }

                        //! Furthermore we send CMSG_NAME_QUERY to the server to retrieve the name of the sender
                        OutPacket response = new OutPacket(WorldCommand.CMSG_NAME_QUERY);
                        response.Write(sender);
                        Game.SendPacket(response);

                        //! Enqueued chat will be printed when we receive SMSG_NAME_QUERY_RESPONSE

                        break; 
                    }
                default:
                    return;
            }
        }

        //  CMSG_NAME_QUERY = 80,
        //  SMSG_NAME_QUERY_RESPONSE = 81,

        [PacketHandler(WorldCommand.CMSG_NAME_QUERY)]
        protected void HandleCMSGNameQuery(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;
            string result = System.Text.Encoding.UTF8.GetString(packet.ReadToEnd());
        }

        [PacketHandler(WorldCommand.SMSG_NAME_QUERY_RESPONSE)]
        protected void HandleSMSGNameQueryResponse(InPacket packet)
        {
            ChatMessage message = new ChatMessage();
            ChatChannel channel = new ChatChannel();
            channel.Type = 0;
            string result = System.Text.Encoding.UTF8.GetString(packet.ReadToEnd());
        }

        [PacketHandler(WorldCommand.SMSG_CHAT_PLAYER_NOT_FOUND)]
        protected void HandleChatPlayerNotFound(InPacket packet)
        {
            ChatChannel channelgc = new ChatChannel();
            channelgc.Type = 0;
            ChatMessage message = new ChatMessage();
            message.Message = String.Format("Player '{0}' doesn't exist!", packet.ReadCString());
            message.Language = 0;
            message.ChatTag = 0;
            message.Sender = channelgc;
            Game.UI.PresentChatMessage(message);
        }
    }
}
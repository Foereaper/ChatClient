using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Client;

namespace BotFarm
{

    public partial class FrmChat : Form
    {
        public bool LeaderNotMe = false;
        private int _lockColumnIndex = 0;

        public FrmChat()
        {
            InitializeComponent();
            this.listGroup.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listGroup_ColumnWidthChanging);
            cBStatusFlag.Text = "Available";
        }

        private void msgPull_Tick(object sender, EventArgs e)
        {
            //DisplayWhoList()
            string WhoListUpdate = AutomatedGame.WhoListUpdate;
            if (WhoListUpdate != "")
            {
                DisplayWhoList();
                AutomatedGame.WhoListUpdate = "";
                return;
            }
            string GroupListUpdate = AutomatedGame.UpdateGroupGUIDList;
            if (GroupListUpdate != "")
            {
                DisplayGroupList();
                AutomatedGame.UpdateGroupGUIDList = "";
                return;
            }
            //FriendListUpdate
            string FriendListUpdate = AutomatedGame.FriendListUpdate;
            if (FriendListUpdate != "")
            {
                DisplayFriendList();
                AutomatedGame.FriendListUpdate = "";
                return;
            }
            //DefaultChannelListUpdate
            string UpdateDefaultChannelList = AutomatedGame.DefaultChannelListUpdate;
            if (UpdateDefaultChannelList != "")
            {
                DisplayDefaultChannels();
                AutomatedGame.DefaultChannelListUpdate = "";
                return;
            }
            //CustomChannelListUpdate
            string UpdateCustomChannelList = AutomatedGame.CustomChannelListUpdate;
            if (UpdateCustomChannelList != "")
            {
                DisplayCustomChannels();
                AutomatedGame.CustomChannelListUpdate = "";
                return;
            }
            string RosterUpdate = AutomatedGame.RosterUpdate;
            if (RosterUpdate != "")
            {
                DisplayRoster();
                AutomatedGame.RosterUpdate = "";
                return;
            }
            SessionInit.Instance.factoryGame.Game.World.mesQue = true;
            List<string> newMessages = SessionInit.Instance.factoryGame.Game.World.newMessageQue;
            foreach (string messageData in newMessages)
            {
                //"[Invited6"
                string GroupInvite = messageData.Substring(0, 9);
                if (GroupInvite == "[Invited6")
                {
                    Thread HandleInvite = new Thread(new ThreadStart(FrmChat.HandleGroupInvitation));
                    HandleInvite.Start();
                    HandleInvite.Join();
                    continue;
                }
                //"[Invited5"
                string ChannelInvite = messageData.Substring(0, 9);
                if (ChannelInvite == "[Invited5")
                {
                    Thread HandleInvite = new Thread(new ThreadStart(FrmChat.HandleChannelInvitation));
                    HandleInvite.Start();
                    HandleInvite.Join();
                    continue;
                }
                string WhisperSendC = messageData.Substring(0, 2); //To
                if (WhisperSendC == "To")
                {
                    AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.MediumVioletRed);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                string WhisperC = messageData.Substring(0, 9); //[Whisper] 
                if (WhisperC == "[Whisper]")
                {
                    AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.MediumVioletRed);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                string GuildC = messageData.Substring(0, 7); //[Guild] 
                if (GuildC == "[Guild]")
                {
                    AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.Green);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                string SystemC = messageData.Substring(0, 8); //[System] 
                if (SystemC == "[System]")
                {
                    AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.DarkBlue, true);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                if (messageData.Length > 18)
                {
                    string GuildAchievementC = messageData.Substring(0, 18); //[GuildAchievement]
                    if (GuildAchievementC == "[GuildAchievement]")
                    {
                        AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.Green, true);
                        //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                        //ChatWindow.ScrollToCaret();
                        continue;
                    }
                }
                AppendText(ChatWindow, messageData.ToString() + "\r\n", Color.Black, true);
            }
            newMessages.Clear();
            SessionInit.Instance.factoryGame.Game.World.mesQue = false;
        }

        private void AppendText(RichTextBox box, string text, Color color, bool bold = false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            //if (bold)
            //{
            //   box.Font = Font = new Font(box.Font, FontStyle.Bold);
            //}
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }

        private void FrmChat_Load(object sender, EventArgs e)
        {
            lblChar.Text = "Logged in as: " + AutomatedGame.characterNameList[AutomatedGame.characterID].ToString();
            //AutomatedGame.presentcharacterList[AutomatedGame.characterID].ToString();

            textMessage.Focus();
            textMessage.Select();
            this.textMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);

            SessionInit.Instance.factoryGame.JoinChannel(1);
            SessionInit.Instance.factoryGame.JoinChannel(2);
            SessionInit.Instance.factoryGame.JoinChannel(3);
            SessionInit.Instance.factoryGame.JoinChannel(4);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.Hide();
            AutomatedGame.DisconClient = true;
            System.Threading.Thread.Sleep(1000);
            Application.Exit();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MsgSend();
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                MsgSend();
            }
        }

        private void MsgSend()
        {
            /// this terrible code, for testing must obviously be changed/removed on release version.
            try
            {
                string tmp = textMessage.Text;
                if (tmp.Length > 3)
                {
                    if ((tmp.Substring(0, 3) == "/w "))
                    {
                        var tmp1 = tmp.Replace("/w ", "");
                        string[] msg = tmp1.Split(' ');
                        var user = msg[0].ToString();
                        var message = tmp1.Substring(user.Length + 1).ToString();
                        //var message = msg[1].ToString();
                        //AppendText(ChatWindow, "Whisper to" + user + ": " + message + "\r\n", Color.Pink);
                        //ChatWindow.AppendText("Whisper to" + user +": " + message + "\r\n");
                        //ChatWindow.ScrollToCaret();
                        SessionInit.Instance.factoryGame.DoWhisperChat(message, user);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if ((tmp.Substring(0, 3) == "/g "))
                    {
                        //var tmp2 = tmp.Replace("/g ", "");
                        var tmp2 = tmp;
                        tmp2 = tmp2.Substring(3);
                        var message = tmp2.ToString();
                        SessionInit.Instance.factoryGame.DoGuildChat(message);
                        var mychar = AutomatedGame.characterNameList[AutomatedGame.characterID].ToString();
                        //AppendText(ChatWindow, "[Guild] [" + mychar + "]: " + message + "\r\n", Color.Green);
                        //ChatWindow.AppendText("[Guild] [" + mychar + "]: " + message + "\r\n");
                        //ChatWindow.ScrollToCaret();
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if ((tmp.Substring(0, 3) == "/p "))
                    {
                        //var tmp2 = tmp.Replace("/g ", "");
                        var tmp2 = tmp;
                        tmp2 = tmp2.Substring(3);
                        var message = tmp2.ToString();
                        SessionInit.Instance.factoryGame.DoPartyChat(message);
                        //var mychar = AutomatedGame.characterNameList[AutomatedGame.characterID].ToString();
                        //AppendText(ChatWindow, "[Guild] [" + mychar + "]: " + message + "\r\n", Color.Green);
                        //ChatWindow.AppendText("[Guild] [" + mychar + "]: " + message + "\r\n");
                        //ChatWindow.ScrollToCaret();
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if ((tmp.Substring(0, 3) == "/in"))
                    {
                        var tmp2 = tmp;
                        tmp2 = tmp2.Substring(8);
                        var player = tmp2.ToString();
                        SessionInit.Instance.factoryGame.InvitePlayerToParty(player);
                        textMessage.Text = string.Empty;
                        return;
                    }
                }
                if (tmp.Length > 2)
                {
                    if (tmp.Substring(0, 2) == "/j")
                    {
                        SessionInit.Instance.factoryGame.DoSayChat(textMessage.Text);
                        textMessage.Text = string.Empty;
                        return;
                    }
                }
                if (tmp.Length > 1)
                {
                    if (tmp.Substring(0, 2) == "/1")
                    {
                        tmp = tmp.TrimStart('/');
                        tmp = tmp.TrimStart('1');
                        tmp = tmp.TrimStart(' ');
                        SessionInit.Instance.factoryGame.SayChannel(tmp, 1);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if (tmp.Substring(0, 2) == "/2")
                    {
                        tmp = tmp.TrimStart('/');
                        tmp = tmp.TrimStart('2');
                        tmp = tmp.TrimStart(' ');
                        SessionInit.Instance.factoryGame.SayChannel(tmp, 2);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if (tmp.Substring(0, 2) == "/3")
                    {
                        tmp = tmp.TrimStart('/');
                        tmp = tmp.TrimStart('3');
                        tmp = tmp.TrimStart(' ');
                        SessionInit.Instance.factoryGame.SayChannel(tmp, 3);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if (tmp.Substring(0, 2) == "/4")
                    {
                        tmp = tmp.TrimStart('/');
                        tmp = tmp.TrimStart('4');
                        tmp = tmp.TrimStart(' ');
                        SessionInit.Instance.factoryGame.SayChannel(tmp, 4);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if (tmp.Substring(0, 1) != "/")
                    {
                        //AppendText(ChatWindow, "Say: " + textMessage.Text + "\r\n", Color.DarkGray);
                        SessionInit.Instance.factoryGame.DoSayChat(textMessage.Text);
                        textMessage.Text = string.Empty;
                        return;
                    }
                }
                //textMessage.Text = string.Empty;
            }
            catch
            {

            }
        }

        private void FrmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutomatedGame.DisconClient = true;
            this.Hide();
            System.Threading.Thread.Sleep(1000);
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AutomatedGame.DisconClient = true;
            System.Threading.Thread.Sleep(1000);
            Application.Exit();
        }

        private void saveConversationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Credits to jackpoz's work on their project BotFarm." + Environment.NewLine +
                 "Developer Credits: StackerDEV, Foereaper and Terrorblade." + Environment.NewLine +
            "Special thanks to the WCell, Mangos, PseuWoW, and TrinityCore projects.");
        }

        private void btnPlayerRefresh_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.RequestWhoList();
        }

        private void ChatWindow_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //BotFactory.Instance.factoryGame.SayChannel("Channel test 123 123", (int)channelNum.Value);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Commands :\n\nIMPORTANT MESSAGE <> means your INPUT.\n\nFor whisper: /w <username> <message>\nFor Guild: /g <message>\nFor Say: <message>\nFor channels: /1 <message> (/1 means Global, 2 Trade, 3 LocalDefense, 4 LFG)\n", "Thank you for RTFM.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings settings = new FrmSettings();
            settings.Show();
        }

        private void joinAllChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.JoinChannel(1);
            SessionInit.Instance.factoryGame.JoinChannel(2);
            SessionInit.Instance.factoryGame.JoinChannel(3);
            SessionInit.Instance.factoryGame.JoinChannel(4);
        }

        private void createNewChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewChannel frmChannel = new NewChannel();
            frmChannel.Show();
        }

        public static void HandleChannelInvitation()
        {
            string ChannelIvtname = AutomatedGame.NewMessageData;
            ChannelIvtname = ChannelIvtname.Remove(0, 9);
            AutomatedGame.NewMessageData = null;

            DialogResult Accept = MessageBox.Show("You have been invited to join the channel '" + ChannelIvtname.ToString() + "'.", "Do you want to join this channel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            if (Accept == DialogResult.Yes)
            {
                SessionInit.Instance.factoryGame.AcceptChannelJoin(ChannelIvtname);
            }
            else
            {
                SessionInit.Instance.factoryGame.CustomChannelDecline(ChannelIvtname);
            }
            return;
        }

        public static void HandleGroupInvitation()
        {
            string InvitationSender = AutomatedGame.NewMessageData;
            InvitationSender = InvitationSender.Remove(0, 9);
            AutomatedGame.NewMessageData = null;

            DialogResult Accept = MessageBox.Show(InvitationSender.ToString() + " invites you to a group.", "Do you want to join this group?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            if (Accept == DialogResult.Yes)
            {
                SessionInit.Instance.factoryGame.AcceptGroupInvitation();
            }
            else
            {
                SessionInit.Instance.factoryGame.GroupDecline();
            }
            return;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.InvitePlayerToParty("Monsterd");
        }

        public void DisplayGroupList()
        {

            ColumnHeader columnPlayer, columnLeader;
            columnPlayer = new ColumnHeader();
            columnLeader = new ColumnHeader();

            columnPlayer.Text = "Player";
            columnPlayer.TextAlign = HorizontalAlignment.Left;
            columnPlayer.Width = 153;
            columnLeader.TextAlign = HorizontalAlignment.Left;
            columnLeader.Text = "Leader";
            columnLeader.Width = 250;

            listGroup.Columns.Clear();

            listGroup.Columns.Add(columnPlayer);
            listGroup.Columns.Add(columnLeader);
            listGroup.View = View.Details;

            listGroup.Items.Clear();
            btnGroupDisband.Enabled = false;

            List<ulong> memberguids = SessionInit.Instance.factoryGame.GroupMembersGuids;
            ulong leaderguid = SessionInit.Instance.factoryGame.GroupLeaderGuid;

            if (memberguids.Count != 0)
            {
                btnGroupDisband.Enabled = true;
                foreach (ulong guid in memberguids)
                {
                    var player = "";
                    bool resolve = (SessionInit.Instance.factoryGame.Game.World.PlayerNameLookup.TryGetValue(guid, out player));
                    ListViewItem item = new ListViewItem(player);
                    if (guid == leaderguid)
                    {
                        item.SubItems.Add("Yes");
                        LeaderNotMe = true;
                    }
                    listGroup.Items.Add(item);
                }
                if (LeaderNotMe == false)
                {
                    ListViewItem item = new ListViewItem(AutomatedGame.characterNameList[AutomatedGame.characterID].ToString());
                    item.SubItems.Add("Yes");
                    listGroup.Items.Add(item);
                }
                else
                {
                    ListViewItem item = new ListViewItem(AutomatedGame.characterNameList[AutomatedGame.characterID].ToString());
                    item.SubItems.Add("");
                    listGroup.Items.Add(item);
                }
            }
            if (memberguids.Count == 0)
            {
                lblPartyGroupSize.Text = "";
            }
            else
            {
                lblPartyGroupSize.Text = listGroup.Items.Count.ToString();
            }

        }

        private void btnGroupDisband_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.GroupDisband();
        }

        void listGroup_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == _lockColumnIndex)
            {
                //Keep the width not changed.
                e.NewWidth = this.listGroup.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }

        //private void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    BotFactory.Instance.factoryGame.RequestWhoList();
        //}

        public void DisplayWhoList()
        {
            ColumnHeader columnPlayer, columnGuild, columnLvl, columnClass, columnRace, columnZone;
            columnPlayer = new ColumnHeader();
            columnGuild = new ColumnHeader();
            columnLvl = new ColumnHeader();
            columnClass = new ColumnHeader();
            columnRace = new ColumnHeader();
            columnZone = new ColumnHeader();
            columnPlayer.Text = "Player";
            columnPlayer.TextAlign = HorizontalAlignment.Left;
            columnPlayer.Width = 100;
            columnGuild.Text = "Guild";
            columnGuild.TextAlign = HorizontalAlignment.Left;
            columnGuild.Width = 100;
            columnLvl.Text = "LvL";
            columnLvl.TextAlign = HorizontalAlignment.Left;
            columnLvl.Width = 30;
            columnClass.Text = "Class";
            columnClass.TextAlign = HorizontalAlignment.Left;
            columnClass.Width = 80;
            columnRace.Text = "Race";
            columnRace.TextAlign = HorizontalAlignment.Left;
            columnRace.Width = 75;
            columnZone.Text = "Zone";
            columnZone.TextAlign = HorizontalAlignment.Left;
            columnZone.Width = 100;

            listWho.Columns.Clear();

            listWho.Columns.Add(columnPlayer);
            listWho.Columns.Add(columnGuild);
            listWho.Columns.Add(columnLvl);
            listWho.Columns.Add(columnClass);
            listWho.Columns.Add(columnRace);
            listWho.Columns.Add(columnZone);
            //listWho.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //listWho.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            listWho.View = View.Details;
            listWho.Items.Clear();
            refreshWhoList.Visible = false;

            List<string> players = AutomatedGame.player;

            try
            {
                int index = 0;
                foreach (string player in players)
                {
                    ListViewItem item = new ListViewItem(player);
                    item.SubItems.Add(AutomatedGame.guild[index]);
                    item.SubItems.Add(AutomatedGame.level[index].ToString());
                    item.SubItems.Add(AutomatedGame.pclass[index]);
                    item.SubItems.Add(AutomatedGame.prace[index]);
                    item.SubItems.Add(AutomatedGame.pzone[index]);
                    listWho.Items.Add(item);
                    index++;
                }
            }
            catch
            {
                // error handling if we ever need this shit.
            }

            lblplayercount.Text = AutomatedGame.playersonline.ToString();

            refreshWhoList.Visible = true;
        }

        public void DisplayFriendList()
        {
            //listFriends
            ColumnHeader columnPlayer, columnGuild, columnLvl, columnOnline;
            columnPlayer = new ColumnHeader();
            columnGuild = new ColumnHeader();
            columnLvl = new ColumnHeader();
            columnOnline = new ColumnHeader();

            columnPlayer.Text = "Player";
            columnPlayer.TextAlign = HorizontalAlignment.Left;
            columnPlayer.Width = 77;
            columnGuild.Text = "Guild";
            columnGuild.TextAlign = HorizontalAlignment.Left;
            columnGuild.Width = 75;
            columnLvl.Text = "LvL";
            columnLvl.TextAlign = HorizontalAlignment.Left;
            columnLvl.Width = 35;
            columnOnline.Text = "Online";
            columnOnline.TextAlign = HorizontalAlignment.Left;
            columnOnline.Width = 55;

            listFriends.Columns.Clear();

            listFriends.Columns.Add(columnPlayer);
            listFriends.Columns.Add(columnGuild);
            listFriends.Columns.Add(columnLvl);
            listFriends.Columns.Add(columnOnline);

            listFriends.View = View.Details;
            listFriends.Items.Clear();

            List<string> friends = AutomatedGame.resolvedFriendList; // friendGUIList

            foreach (string friend in friends)
            {
                int listindex = AutomatedGame.player.IndexOf(friend);
                ListViewItem item = new ListViewItem(friend);
                if (listindex != -1)
                {
                    item.SubItems.Add(AutomatedGame.guild[listindex]);
                    item.SubItems.Add(AutomatedGame.level[listindex].ToString());
                    item.SubItems.Add("Yes");
                }
                else
                {
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("No");
                }

                listFriends.Items.Add(item);
            }

            lblfriendcount.Text = AutomatedGame.resolvedFriendList.Count.ToString();

        }

        public void DisplayDefaultChannels()
        {
            ColumnHeader columnChannel, columnJoined;
            columnChannel = new ColumnHeader();
            columnJoined = new ColumnHeader();

            columnChannel.Text = "Channel";
            columnChannel.TextAlign = HorizontalAlignment.Left;
            columnChannel.Width = 217;
            columnJoined.Text = "Joined";
            columnJoined.TextAlign = HorizontalAlignment.Left;
            columnJoined.Width = 55;

            listWorld.Columns.Clear();

            listWorld.Columns.Add(columnChannel);
            listWorld.Columns.Add(columnJoined);

            listWorld.View = View.Details;
            listWorld.Items.Clear();

            List<string> defaultchannels = AutomatedGame.joinedChannels;

            foreach (string channel in defaultchannels)
            {
                ListViewItem item = new ListViewItem(channel);
                item.SubItems.Add("Yes");
                listWorld.Items.Add(item);
            }
        }

        public void DisplayCustomChannels()
        {
            ColumnHeader columnChannel, columnJoined;
            columnChannel = new ColumnHeader();
            columnJoined = new ColumnHeader();

            columnChannel.Text = "Custom channel";
            columnChannel.TextAlign = HorizontalAlignment.Left;
            columnChannel.Width = 217;
            columnJoined.Text = "Joined";
            columnJoined.TextAlign = HorizontalAlignment.Left;
            columnJoined.Width = 55;

            listCustom.Columns.Clear();

            listCustom.Columns.Add(columnChannel);
            listCustom.Columns.Add(columnJoined);

            listCustom.View = View.Details;
            listCustom.Items.Clear();

            List<string> customchannels = AutomatedGame.customChannels;

            foreach (string channel in customchannels)
            {
                ListViewItem item = new ListViewItem(channel);
                item.SubItems.Add("Yes");
                listCustom.Items.Add(item);
            }
        }

        public void DisplayRoster()
        {
            ColumnHeader columnPlayer, columnStatus, columnLevel, columnClass, columnZone, columnNote, columnOfficerNote;
            columnPlayer = new ColumnHeader();
            columnStatus = new ColumnHeader();
            columnLevel = new ColumnHeader();
            columnClass = new ColumnHeader();
            columnZone = new ColumnHeader();
            columnNote = new ColumnHeader();
            columnOfficerNote = new ColumnHeader();
            columnPlayer.Text = "Player";
            columnPlayer.TextAlign = HorizontalAlignment.Left;
            columnPlayer.Width = 80;
            columnStatus.Text = "Status";
            columnStatus.TextAlign = HorizontalAlignment.Left;
            columnStatus.Width = 50;
            columnLevel.Text = "Level";
            columnLevel.TextAlign = HorizontalAlignment.Left;
            columnLevel.Width = 50;
            columnClass.Text = "Class";
            columnClass.TextAlign = HorizontalAlignment.Left;
            columnClass.Width = 75;
            columnZone.Text = "Zone";
            columnZone.TextAlign = HorizontalAlignment.Left;
            columnZone.Width = 80;
            columnNote.Text = "Note";
            columnNote.TextAlign = HorizontalAlignment.Left;
            columnNote.Width = 80;
            columnOfficerNote.Text = "O-Note";
            columnOfficerNote.TextAlign = HorizontalAlignment.Left;
            columnOfficerNote.Width = 80;


            listRoster.Columns.Clear();

            listRoster.Columns.Add(columnPlayer);
            listRoster.Columns.Add(columnStatus);
            listRoster.Columns.Add(columnLevel);
            listRoster.Columns.Add(columnClass);
            listRoster.Columns.Add(columnZone);
            listRoster.Columns.Add(columnNote);
            listRoster.Columns.Add(columnOfficerNote);

            listRoster.View = View.Details;
            listRoster.Items.Clear();

            List<string> players = SessionInit.Instance.factoryGame.Game.World.guildPlayer;

            int index = 0;
            foreach (string player in players)
            {
                ListViewItem item = new ListViewItem(player);
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildStatus[index]);
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildLevel[index].ToString());
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildClass[index]);
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildZone[index]);
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildNote[index]);
                item.SubItems.Add(SessionInit.Instance.factoryGame.Game.World.guildOfficerNote[index]);
                listRoster.Items.Add(item);
                index++;
            }
        }

        private void listWho_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point loc = listWho.PointToScreen(e.Location);
                contextMenuWhoList.Show(loc);
            }
        }

        private void refreshWhoList_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.RequestWhoList();
        }

        private void addFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var player = listWho.SelectedItems[0].Text;
                if (player != "")
                {
                    if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                    {
                        SessionInit.Instance.factoryGame.AddFriend(player);
                    }
                    else
                    {
                        MessageBox.Show("You cannot add yourself as friend.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            } catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please don't add friends too quickly!", "Hold on", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void whisperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player != "")
            {
                textMessage.Text = "/w " + player + " ";
                textMessage.Select();
                textMessage.SelectionStart = textMessage.Text.Length;
            }
        }

        private void inviteToPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player != "")
            {
                if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                {
                    SessionInit.Instance.factoryGame.InvitePlayerToParty(player);
                }
                else
                {
                    MessageBox.Show("You cannot invite yourself to a party.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cBStatusFlag_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cBStatusFlag.SelectedIndex;
            if (index != -1)
            {
                SessionInit.Instance.factoryGame.ChangeStatus(index);
            }
        }

        private void ignoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player != "")
            {
                if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                {
                    SessionInit.Instance.factoryGame.IgnorePlayer(player);
                }
                else
                {
                    MessageBox.Show("You cannot ignore yourself.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2) { SessionInit.Instance.factoryGame.RequestWhoList(); }
            if (tabControl1.SelectedIndex == 3) { SessionInit.Instance.factoryGame.RequestFriendList(); }
            if (tabControl1.SelectedIndex == 4) { SessionInit.Instance.factoryGame.RequestGuildList(); }
        }

        private void listGroup_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point loc = listGroup.PointToScreen(e.Location);
                contextMenuGroupList.Show(loc);
            }
        }

        private void whisperToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player != "")
            {
                textMessage.Text = "/w " + player + " ";
                textMessage.Select();
                textMessage.SelectionStart = textMessage.Text.Length;
            }
        }

        private void addFriendToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player != "")
            {
                if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                {
                    SessionInit.Instance.factoryGame.AddFriend(player);
                }
                else
                {
                    MessageBox.Show("You cannot add yourself as friend.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ignoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player != "")
            {
                if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                {
                    SessionInit.Instance.factoryGame.IgnorePlayer(player);
                }
                else
                {
                    MessageBox.Show("You cannot ignore yourself.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void removeFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int index = listFriends.SelectedItems[0].Index;
                var player = listFriends.SelectedItems[0].Text;
                string guid = AutomatedGame.friendGUIList[index];
                SessionInit.Instance.factoryGame.RemoveFriend(Convert.ToInt32(guid), player);
            }
            catch
            {
                MessageBox.Show("You are deleting friends too fast.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayFriendList();
            }
        }

        private void listFriends_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point loc = listFriends.PointToScreen(e.Location);
                contextMenuFriendList.Show(loc);
            }
        }

        private void whisperToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var player = listFriends.SelectedItems[0].Text;
            if (player != "")
            {
                textMessage.Text = "/w " + player + " ";
                textMessage.Select();
                textMessage.SelectionStart = textMessage.Text.Length;
            }
        }

        private void inviteToPartyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listFriends.SelectedItems[0].Text;
            if (player != "")
            {
                if (AutomatedGame.characterNameList[AutomatedGame.characterID].ToString() != player)
                {
                    SessionInit.Instance.factoryGame.InvitePlayerToParty(player);
                }
                else
                {
                    MessageBox.Show("You cannot invite yourself to a party.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void leaveChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // leave channel 1
            SessionInit.Instance.factoryGame.LeaveChannel(1);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // leave channel 2
            SessionInit.Instance.factoryGame.LeaveChannel(2);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // leave channel 3
            SessionInit.Instance.factoryGame.LeaveChannel(3);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // leave channel 4
            SessionInit.Instance.factoryGame.LeaveChannel(4);
        }

        private void leaveAllChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.LeaveChannel(1);
            SessionInit.Instance.factoryGame.LeaveChannel(2);
            SessionInit.Instance.factoryGame.LeaveChannel(3);
            SessionInit.Instance.factoryGame.LeaveChannel(4);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

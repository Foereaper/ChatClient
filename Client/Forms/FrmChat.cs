using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Client;
using Client.Forms;
using Client.Properties;


namespace BotFarm
{
    public partial class FrmChat : Form
    {
        public bool LeaderNotMe;
        private int _lockColumnIndex = 0;
        public DateTime lastChatMessage;
        public DateTime timeNow;

        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public struct CHARFORMAT2
        {
            public int cbSize;
            public int dwMask;
            public int dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szFaceName;
            public short wWeight;
            public short sSpacing;
            public int crBackColor;
            public int lcid;
            public int dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        const int CFE_LINK = 0x20;
        const int CFM_LINK = 0x20;
        const int CFM_LCID = 0x2000000;
        const int CFM_REVAUTHOR = 0x8000;
        const int EM_SETCHARFORMAT = 0x444;
        const int SCF_SELECTION = 0x1;
        const int SCF_WORD = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public FrmChat()
        {
            InitializeComponent();
            listGroup.ColumnWidthChanging += listGroup_ColumnWidthChanging;
            cBStatusFlag.Text = "Available";
        }

        private void msgPull_Tick(object sender, EventArgs e)
        {
            if(AutomatedGame.chatWindowBGColor != null && AutomatedGame.chatWindowBGColor != "")
            {
                string[] BGRGB = AutomatedGame.chatWindowBGColor.Split(':');
                ChatWindow.BackColor = Color.FromArgb(Convert.ToInt32(BGRGB[0]), Convert.ToInt32(BGRGB[1]), Convert.ToInt32(BGRGB[2]));
                AutomatedGame.chatWindowBGColor = string.Empty;
            }
            //DisplayWhoList()
            var whoListUpdate = AutomatedGame.WhoListUpdate;
            if (whoListUpdate != "")
            {
                DisplayWhoList();
                AutomatedGame.WhoListUpdate = "";
                return;
            }
            var groupListUpdate = AutomatedGame.UpdateGroupGUIDList;
            if (groupListUpdate != "")
            {
                DisplayGroupList();
                AutomatedGame.UpdateGroupGUIDList = "";
                return;
            }
            //FriendListUpdate
            var friendListUpdate = AutomatedGame.FriendListUpdate;
            if (friendListUpdate != "")
            {
                DisplayFriendList();
                AutomatedGame.FriendListUpdate = "";
                return;
            }
            //DefaultChannelListUpdate
            var updateDefaultChannelList = AutomatedGame.DefaultChannelListUpdate;
            if (updateDefaultChannelList != "")
            {
                DisplayDefaultChannels();
                AutomatedGame.DefaultChannelListUpdate = "";
                return;
            }
            //CustomChannelListUpdate
            var updateCustomChannelList = AutomatedGame.CustomChannelListUpdate;
            if (updateCustomChannelList != "")
            {
                DisplayCustomChannels();
                AutomatedGame.CustomChannelListUpdate = "";
                return;
            }
            var rosterUpdate = AutomatedGame.RosterUpdate;
            if (rosterUpdate != "")
            {
                DisplayRoster();
                AutomatedGame.RosterUpdate = "";
                return;
            }
            var ticketUpdate = AutomatedGame.TicketUpdate;
            if (ticketUpdate != "")
            {
                DisplayTicketList();
                AutomatedGame.TicketUpdate = "";
                return;
            }
            SessionInit.Instance.factoryGame.Game.World.mesQue = true;
            var newMessages = SessionInit.Instance.factoryGame.Game.World.newMessageQue;
            foreach (var messageData in newMessages)
            {
                
                /*
                ToDo: Something like this to add color support
                if (messageData.Contains("|c"))
                {
                    var index = messageData.IndexOf("|c", StringComparison.Ordinal);
                    index += 2; //we want the FF+data
                    var colorString = messageData.Substring(index, 8);                   
                    messageData.Replace(colorString, ColorConverter.ConvertFromString(colorString);)
                }*/

                byte[] msgData = Encoding.Default.GetBytes(messageData);
                var messageDataUTF8 = Encoding.UTF8.GetString(msgData).ToString();

                //"[Invited6"
                var groupInvite = messageDataUTF8.Substring(0, 9);
                if (groupInvite == "[Invited6")
                {
                    var tInvt = new Thread(() => HandleGroupInvitation(messageDataUTF8));
                    tInvt.Start();
                    continue;
                }
                //"[Invited5"
                var channelInvite = messageDataUTF8.Substring(0, 9);
                if (channelInvite == "[Invited5")
                {
                    var tInvt = new Thread(() => HandleChannelInvitation(messageDataUTF8));
                    tInvt.Start();
                    continue;
                }
                var whisperSendC = messageDataUTF8.Substring(0, 2); //To
                if (whisperSendC == "To")
                {
                    AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.MediumVioletRed);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                var WhisperC = messageDataUTF8.Substring(0, 9); //[Whisper] 
                if (WhisperC == "[Whisper]")
                {
                    //AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.MediumVioletRed);
                    AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.MediumVioletRed);
                    if(Settings.Default.DisableClickableUsernames == false)
                    {
                        clickableUserName(messageDataUTF8.Substring(15, messageDataUTF8.IndexOf(":") - 14), messageDataUTF8);
                    }                   
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                var guildC = messageDataUTF8.Substring(0, 7); //[Guild] 
                if (guildC == "[Guild]")
                {
                    AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.Green);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                var officerC = messageDataUTF8.Substring(0, 9); //[Officer] 
                if (officerC == "[Officer]")
                {
                    AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.Green);
                    continue;
                }
                var systemC = messageDataUTF8.Substring(0, 8); //[System] 
                if (systemC == "[System]")
                {
                    Match m = Regex.Match(messageData, @"(\|)cff");
                    if (m.Success)
                    {
                        systemChannelColor(messageData);
                    }
                    else
                    {
                        AppendText(ChatWindow, messageData + "\r\n", Color.DarkBlue, true);
                    }
                    //AppendText(ChatWindow, messageData + "\r\n", Color.DarkBlue, true);
                    //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                    //ChatWindow.ScrollToCaret();
                    continue;
                }
                if (messageDataUTF8.Length > 18)
                {
                    var guildAchievementC = messageDataUTF8.Substring(0, 18); //[GuildAchievement]
                    if (guildAchievementC == "[GuildAchievement]")
                    {
                        AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.GreenYellow, true);
                        //ChatWindow.AppendText(AutomatedGame.messageDataData.ToString() + "\r\n");
                        //ChatWindow.ScrollToCaret();
                        continue;
                    }
                }
                //"[MonsterSay] Death Knight Initiate: Our enemies will be annihilated!"	string
                var monsterSayC = messageDataUTF8.Substring(0, 12);
                if (monsterSayC == "[MonsterSay]")
                {
                    if(Settings.Default.NPCChat == false)
                    {
                        AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.Black, true);
                    }
                }
                else
                {
                    AppendText(ChatWindow, messageDataUTF8 + "\r\n", Color.Black, true);
                }
                
            }
            newMessages.Clear();
            SessionInit.Instance.factoryGame.Game.World.mesQue = false;
            if (AutomatedGame.securityLevel == 0)
            {
                if (tabControl1.Contains(tabTicket))
                    tabControl1.TabPages.Remove(tabTicket);
            }
            else
            {
                if (!tabControl1.Contains(tabTicket))
                    tabControl1.TabPages.Insert(5, tabTicket);

            }
        }

        private void AppendText(RichTextBox box, string text, Color color, bool bold = false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            //if (bold)
            //{
            //   box.Font = Font = new Font(box.Font, FontStyle.Bold);
            //}
            if(Settings.Default.DisableAllChatColors == true)
            {
                box.SelectionColor = Color.DarkBlue;
            }
            else
            {
                box.SelectionColor = color;
            }          
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }

        private void systemChannelColor(string channelText)
        {
            string colorhex = "";
            string message = "";
            Color textcolor = Color.DarkBlue; // default
            string[] colors = Regex.Split(channelText, @"(\|)cff"); // don't hate me for this.
            for (int i = 0; i < colors.Length; i++)
            {
                colorhex = string.Empty;
                message = string.Empty;
                if (colors[i].Length != 1)
                {
                    if (colors[i] != "[System] : ")
                    {
                        colorhex = colors[i].Substring(0, 6);
                        message = colors[i].Substring(6, colors[i].Length - 6);
                        if(message.Contains("|r") == true)
                        {
                            message = message.Replace("|r", "");
                        }
                        textcolor = System.Drawing.ColorTranslator.FromHtml("#" + colorhex);
                    }
                    else
                    {
                        message = "[System] : ";
                    }
                }
                if (i == colors.Length - 1)
                {
                    if (message.Substring(message.Length - 2, 2) == "|r")
                    {
                        message = message.Remove(message.Length - 2);
                    }
                    if(Settings.Default.DisableSystemChannelColors == true)
                    {
                        AppendText(ChatWindow, message + "\r\n", Color.DarkBlue, true);
                    }
                    else
                    {
                        AppendText(ChatWindow, message + "\r\n", textcolor, true);
                    }                    
                }
                else
                {
                    if (message != "")
                    {
                        if (Settings.Default.DisableSystemChannelColors == true)
                        {
                            AppendText(ChatWindow, message, Color.DarkBlue, true);
                        }
                        else
                        {
                            AppendText(ChatWindow, message, textcolor, true);
                        }                            
                    }
                }
            }
        }

        private void clickableUserName(string username, string fullmsg)
        {
            int selstart = fullmsg.Length - 14;
            int selstartcalculated = ChatWindow.Text.Length - selstart;
            ChatWindow.SelectionStart = selstartcalculated;
            ChatWindow.SelectionLength = username.Length;

            CHARFORMAT2 myFormat = new CHARFORMAT2();
            myFormat.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(myFormat);
            myFormat.dwEffects = CFE_LINK;
            myFormat.dwMask = CFM_REVAUTHOR + CFM_LCID + CFM_LINK;
            myFormat.bRevAuthor = 3;

            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(myFormat));
            Marshal.StructureToPtr(myFormat, lParam, false);
            SendMessage(ChatWindow.Handle, (UInt32)EM_SETCHARFORMAT, (IntPtr)(SCF_SELECTION + SCF_WORD), lParam);
        }

        private void FrmChat_Load(object sender, EventArgs e)
        {
            byte[] currentUser = Encoding.Default.GetBytes(AutomatedGame.characterNameList[AutomatedGame.characterID]);
            lblChar.Text = $"Logged in as: {Encoding.UTF8.GetString(currentUser).ToString()}";
            //lblChar.Text = $"Logged in as: {AutomatedGame.characterNameList[AutomatedGame.characterID]}";
            //AutomatedGame.presentcharacterList[AutomatedGame.characterID].ToString();

            textMessage.Focus();
            textMessage.Select();
            if(Settings.Default.SmgSendEnter == true)
            {
                textMessage.KeyPress += CheckEnter;
            }

            if (Settings.Default.AutoJoinChannel == true)
            {
                SessionInit.Instance.factoryGame.JoinChannel(1, "General", "");
                SessionInit.Instance.factoryGame.JoinChannel(2, "Trade", "");
                SessionInit.Instance.factoryGame.JoinChannel(22, "LocalDefense", "");
                SessionInit.Instance.factoryGame.JoinChannel(26, "LookingForGroup", "");
            }

            if (Settings.Default.ChatWindowBackgroundColor != "")
            {
                string WindowBGColor = Settings.Default.ChatWindowBackgroundColor;
                string[] BGRGB = WindowBGColor.Split(':');
                ChatWindow.BackColor = Color.FromArgb(Convert.ToInt32(BGRGB[0]), Convert.ToInt32(BGRGB[1]), Convert.ToInt32(BGRGB[2]));
            }

            if (Settings.Default.AFKcheck == true)
            {
                int afkminutes = Settings.Default.AFKmins;
                if(Settings.Default.AFKDM == 1)
                {
                    var AfkDM1 = new Thread(() => AfkDetectionM1());
                    AfkDM1.Start();
                }
                if (Settings.Default.AFKDM == 2)
                {
                    var AfkDM2 = new Thread(() => AfkDetectionM2());
                    AfkDM2.Start();
                }
            }
            //create starttime for afk detection m1 thread
            lastChatMessage = DateTime.Now;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(500);
            System.Diagnostics.Process.Start(Application.ExecutablePath, "autologin");
            Environment.Exit(1);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MsgSend();
        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                MsgSend();
            }
        }

        private void MsgSend()
        {
            /// this terrible code, for testing must obviously be changed/removed on release version.
            lastChatMessage = DateTime.Now; //for AFK detection mode 1
            try
            {
                var tmp = textMessage.Text;
                if (tmp.Length > 3)
                {
                    //DoOfficerChat
                    if ((tmp.Substring(0, 3) == "/o "))
                    {
                        //var tmp2 = tmp.Replace("/g ", "");
                        var tmp2 = tmp;
                        tmp2 = tmp2.Substring(3);
                        var message = tmp2;
                        SessionInit.Instance.factoryGame.DoOfficerChat(message);
                        textMessage.Text = string.Empty;
                        return;
                    }
                    if ((tmp.Substring(0, 3) == "/w "))
                    {
                        var tmp1 = tmp.Replace("/w ", "");
                        var msg = tmp1.Split(' ');
                        var user = msg[0];
                        var message = tmp1.Substring(user.Length + 1);
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
                        var message = tmp2;
                        SessionInit.Instance.factoryGame.DoGuildChat(message);
                        var mychar = AutomatedGame.characterNameList[AutomatedGame.characterID];
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
                        var message = tmp2;
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
                        var player = tmp2;
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
                    }
                }
                //textMessage.Text = string.Empty;
            }
            catch
            {
                AppendText(ChatWindow, "[System] : Malformed message." + "\r\n", Color.DarkRed, true);
            }
        }

        private void FrmChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(3000);
            Environment.Exit(1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(3000);
            Environment.Exit(1);
        }

        private void saveConversationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Credits to jackpoz's work on their project BotFarm." + Environment.NewLine +
                 "Developer Credits: StackerDEV, Foereaper and Terrorblade." + Environment.NewLine +
            "Special thanks to the WCell, Mangos, PseuWoW, and TrinityCore projects.", "Information", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void btnPlayerRefresh_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.RequestWhoList();
        }

        private void ChatWindow_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText.Substring(e.LinkText.Length - 1, 1) == ":")
            {
                textMessage.Text = "/w " + e.LinkText.Remove(e.LinkText.Length - 1) + " ";
                textMessage.Select();
                textMessage.SelectionStart = textMessage.Text.Length;
            }
            else
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //BotFactory.Instance.factoryGame.SayChannel("Channel test 123 123", (int)channelNum.Value);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Commands :\n\nIMPORTANT MESSAGE <> means your INPUT." +  Environment.NewLine + Environment.NewLine +
                "For whisper: /w <username> <message>" + Environment.NewLine + 
                "For Guild: /g <message>" + Environment.NewLine + 
                "For Say: <message>" + Environment.NewLine + 
                "For channels: /1 <message> (/1 means Global, 2 Trade, 3 LocalDefense, 4 LFG)" + Environment.NewLine
                , "Thank you for RTFM.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new FrmSettings();
            settings.Show();
        }

        private void joinAllChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.JoinChannel(1, "General", "");
            SessionInit.Instance.factoryGame.JoinChannel(2, "Trade", "");
            SessionInit.Instance.factoryGame.JoinChannel(22, "LocalDefense", "");
            SessionInit.Instance.factoryGame.JoinChannel(26, "LookingForGroup", "");
        }

        private void createNewChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmChannel = new NewChannel();
            frmChannel.Show();
        }

        public delegate void SetCharStatusCallBack(Control control, string text);
        public static void InvokeSetCharStatus(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                SetCharStatusCallBack d = new SetCharStatusCallBack(InvokeSetCharStatus);
                control.Invoke(d, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }

        ///<summary>
        /// Afk detection methode 1 afk if last sent message to server was x minutes ago.
        /// in settings > afk:
        /// 1 == away
        /// 2 == busy
        /// in GUI combobox:
        /// Available == 0
        /// Away == 1
        /// Busy == 2
        ///</summary>
        public void AfkDetectionM1()
        {
            while (true)
            {
                string userstate = null;
                timeNow = DateTime.Now;
                TimeSpan span = timeNow.Subtract(lastChatMessage);
                int mdiff = (int)Math.Round(span.TotalMinutes);
                int afkminutes = Settings.Default.AFKmins;
                cBStatusFlag.Invoke(new Action(() => userstate = cBStatusFlag.Text));
                if (mdiff == afkminutes || mdiff > afkminutes)
                {
                    switch (Settings.Default.AFKstatus)
                    {
                        case 0:
                            if (userstate != "Away")
                            {
                                InvokeSetCharStatus(cBStatusFlag, "Away");
                                SessionInit.Instance.factoryGame.ChangeStatus(1);
                            }
                            break;
                        case 1:
                            if (userstate != "Busy")
                            {
                                InvokeSetCharStatus(cBStatusFlag, "Busy");
                                SessionInit.Instance.factoryGame.ChangeStatus(2);
                            }
                            break;
                    }
                }
                else
                { 
                    if (userstate != "Available")
                    {
                        InvokeSetCharStatus(cBStatusFlag, "Available");
                        SessionInit.Instance.factoryGame.ChangeStatus(0);
                    }
                }
                Thread.Sleep(5000);
            }
        }

        public static void AfkDetectionM2()
        {
            //todo but less important
        }

        public static void HandleChannelInvitation(string invtdat)
        {
            var ChannelIvtname = invtdat;
            ChannelIvtname = ChannelIvtname.Remove(0, 9);
            AutomatedGame.NewMessageData = null;
            if (Settings.Default.IngoreChannelInvite == true)
            {
                if(Settings.Default.IgnoreMode == 1)
                {
                    SessionInit.Instance.factoryGame.CustomChannelDecline(ChannelIvtname);
                }
                if (Settings.Default.IgnoreMode == 2)
                {
                    //ignore ..
                }
            }
            else
            {
                var Accept = MessageBox.Show("You have been invited to join the channel '" + ChannelIvtname + "'.", "Do you want to join this channel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                if (Accept == DialogResult.Yes)
                {
                    SessionInit.Instance.factoryGame.AcceptChannelJoin(ChannelIvtname);
                }
                else
                {
                    SessionInit.Instance.factoryGame.CustomChannelDecline(ChannelIvtname);
                }
            }
        }

        public static void HandleGroupInvitation(string invtdat)
        {
            var InvitationSender = invtdat;
            InvitationSender = InvitationSender.Remove(0, 9);
            byte[] senderName = Encoding.Default.GetBytes(InvitationSender);
            AutomatedGame.NewMessageData = null;
            if(Settings.Default.IgnoreGroupInvite == true)
            {
                if (Settings.Default.IgnoreMode == 1)
                {
                    SessionInit.Instance.factoryGame.GroupDecline();
                }
                if (Settings.Default.IgnoreMode == 2)
                {
                    //ignore ..
                }
            }
            else
            {
                var Accept = MessageBox.Show(Encoding.UTF8.GetString(senderName).ToString() + " invites you to a group.", "Do you want to join this group?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                if (Accept == DialogResult.Yes)
                {
                    SessionInit.Instance.factoryGame.AcceptGroupInvitation();
                }
                else
                {
                    SessionInit.Instance.factoryGame.GroupDecline();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.InvitePlayerToParty("Monsterd");
        }

        public void DisplayGroupList()
        {
            var columnPlayer = new ColumnHeader();
            var columnLeader = new ColumnHeader();

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

            var memberguids = SessionInit.Instance.factoryGame.groupMembersGuids;
            var leaderguid = SessionInit.Instance.factoryGame.GroupLeaderGuid;

            if (memberguids.Count != 0)
            {
                btnGroupDisband.Enabled = true;
                foreach (var guid in memberguids)
                {
                    var resolve = (SessionInit.Instance.factoryGame.Game.World.PlayerNameLookup.TryGetValue(guid, out string player));
                    if (player == null) { player = "Unresolved player"; }
                    byte[] playerName = Encoding.Default.GetBytes(player);
                    var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
                    if (guid == leaderguid)
                    {
                        item.SubItems.Add("Yes");
                        LeaderNotMe = true;
                    }
                    listGroup.Items.Add(item);
                }
                if (LeaderNotMe == false)
                {
                    byte[] playerName = Encoding.Default.GetBytes(AutomatedGame.characterNameList[AutomatedGame.characterID]);
                    var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
                    //var item = new ListViewItem(AutomatedGame.characterNameList[AutomatedGame.characterID]);
                    item.SubItems.Add("Yes");
                    listGroup.Items.Add(item);
                }
                else
                {
                    byte[] playerName = Encoding.Default.GetBytes(AutomatedGame.characterNameList[AutomatedGame.characterID]);
                    var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
                    //var item = new ListViewItem(AutomatedGame.characterNameList[AutomatedGame.characterID]);
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
                e.NewWidth = listGroup.Columns[e.ColumnIndex].Width;
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
            var columnPlayer = new ColumnHeader();
            var columnGuild = new ColumnHeader();
            var columnLvl = new ColumnHeader();
            var columnClass = new ColumnHeader();
            var columnRace = new ColumnHeader();
            var columnZone = new ColumnHeader();
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

            var players = AutomatedGame.player;

            try
            {
                var index = 0;
                foreach (var player in players)
                {
                    byte[] playerName = Encoding.Default.GetBytes(player);
                    var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
                    byte[] playerGuild = Encoding.Default.GetBytes(AutomatedGame.guild[index]);
                    item.SubItems.Add(Encoding.UTF8.GetString(playerGuild).ToString());
                    //item.SubItems.Add(AutomatedGame.guild[index]);
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
            var columnPlayer = new ColumnHeader();
            var columnGuild = new ColumnHeader();
            var columnLvl = new ColumnHeader();
            var columnOnline = new ColumnHeader();

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

            var friends = AutomatedGame.resolvedFriendList; // friendGUIList

            foreach (var friend in friends)
            {
                var listindex = AutomatedGame.player.IndexOf(friend);
                byte[] nameFriend = Encoding.Default.GetBytes(friend);
                var item = new ListViewItem(Encoding.UTF8.GetString(nameFriend).ToString());
                if (listindex != -1)
                {
                    byte[] guildFriend = Encoding.Default.GetBytes(AutomatedGame.guild[listindex]);
                    item.SubItems.Add(Encoding.UTF8.GetString(guildFriend).ToString());
                    //item.SubItems.Add(AutomatedGame.guild[listindex]);
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
            var columnChannel = new ColumnHeader();
            var columnJoined = new ColumnHeader();

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

            var defaultchannels = AutomatedGame.joinedChannels;

            foreach (var channel in defaultchannels)
            {
                var item = new ListViewItem(channel);
                item.SubItems.Add("Yes");
                listWorld.Items.Add(item);
            }
        }

        public void DisplayCustomChannels()
        {
            var columnChannel = new ColumnHeader();
            var columnJoined = new ColumnHeader();

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

            var customchannels = AutomatedGame.customChannels;

            foreach (var channel in customchannels)
            {
                byte[] channelName = Encoding.Default.GetBytes(channel);
                var item = new ListViewItem(Encoding.UTF8.GetString(channelName).ToString());
                item.SubItems.Add("Yes");
                listCustom.Items.Add(item);
            }
        }

        public void DisplayRoster()
        {
            var columnPlayer = new ColumnHeader();
            var columnStatus = new ColumnHeader();
            var columnLevel = new ColumnHeader();
            var columnClass = new ColumnHeader();
            var columnZone = new ColumnHeader();
            var columnNote = new ColumnHeader();
            var columnOfficerNote = new ColumnHeader();
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

            var players = SessionInit.Instance.factoryGame.Game.World.guildPlayer;

            var index = 0;
            foreach (var player in players)
            {
                byte[] playerName = Encoding.Default.GetBytes(player);
                var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
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

        public void DisplayTicketList()
        {
            var columnPlayer = new ColumnHeader();
            var columnCreate = new ColumnHeader();
            var columnAssignedGuy = new ColumnHeader();
            var columnComment = new ColumnHeader();
            var columnOnline = new ColumnHeader();
            columnPlayer.Text = "Player";
            columnPlayer.TextAlign = HorizontalAlignment.Left;
            columnPlayer.Width = 80;
            columnCreate.Text = "Create";
            columnCreate.TextAlign = HorizontalAlignment.Left;
            columnCreate.Width = 50;
            columnAssignedGuy.Text = "Assigned";
            columnAssignedGuy.TextAlign = HorizontalAlignment.Left;
            columnAssignedGuy.Width = 50;
            columnComment.Text = "Comment";
            columnComment.TextAlign = HorizontalAlignment.Left;
            columnComment.Width = 100;
            columnOnline.Text = "Online";
            columnOnline.TextAlign = HorizontalAlignment.Left;
            columnOnline.Width = 50;

            listTicket.Columns.Clear();

            listTicket.Columns.Add(columnPlayer);
            listTicket.Columns.Add(columnCreate);
            listTicket.Columns.Add(columnAssignedGuy);
            listTicket.Columns.Add(columnComment);
            listTicket.Columns.Add(columnOnline);

            listTicket.View = View.Details;
            listTicket.Items.Clear();

            var tickets = SessionInit.Instance.factoryGame.Game.World.ticketList;

            foreach (var ticket in tickets)
            {
                byte[] playerName = Encoding.Default.GetBytes(ticket.playerName);
                var item = new ListViewItem(Encoding.UTF8.GetString(playerName).ToString());
                item.SubItems.Add(ticket.createTime);
                byte[] assignedPlayer = Encoding.Default.GetBytes(ticket.assignedPlayer);
                item.SubItems.Add(Encoding.UTF8.GetString(assignedPlayer).ToString());
                byte[] ticketComment = Encoding.Default.GetBytes(ticket.ticketComment);
                item.SubItems.Add(Encoding.UTF8.GetString(ticketComment).ToString());
                item.SubItems.Add(ticket.areTheyOnline.ToString());
                listTicket.Items.Add(item);
            }
        }

        private void listWho_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var loc = listWho.PointToScreen(e.Location);
                if (AutomatedGame.securityLevel == 0)
                {
                    tbButtonToolStripMenuItem.Visible = false;
                    resurrectToolStripMenuItem.Visible = false;
                }

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
                    if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
                    {
                        SessionInit.Instance.factoryGame.AddFriend(player);
                    }
                    else
                    {
                        MessageBox.Show("You cannot add yourself as friend.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            } catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please don't add friends too quickly!", "Hold on", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void whisperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player == "") return;
            textMessage.Text = "/w " + player + " ";
            textMessage.Select();
            textMessage.SelectionStart = textMessage.Text.Length;
        }

        private void tbButtonToolStripMenuItemItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player == "") return;
            SessionInit.Instance.factoryGame.TbButton(player);
        }

        private void resurrectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player == "") return;
            SessionInit.Instance.factoryGame.RevivePlayer(player);
        }

        private void inviteToPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var player = listWho.SelectedItems[0].Text;
            if (player == "") return;
            if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
            {
                SessionInit.Instance.factoryGame.InvitePlayerToParty(player);
            }
            else
            {
                MessageBox.Show("You cannot invite yourself to a party.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cBStatusFlag_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var index = cBStatusFlag.SelectedIndex;
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
                if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
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
            if (tabControl1.SelectedIndex == 0) { SessionInit.Instance.factoryGame.RequestWhoList(); }
            if (tabControl1.SelectedIndex == 1) { SessionInit.Instance.factoryGame.RequestGuildList(); }
            if (tabControl1.SelectedIndex == 4) { SessionInit.Instance.factoryGame.RequestFriendList(); }
            if (tabControl1.SelectedIndex == 5) { SessionInit.Instance.factoryGame.RequestTicketList(false); }
        }

        private void listGroup_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var loc = listGroup.PointToScreen(e.Location);
            contextMenuGroupList.Show(loc);
        }

        private void whisperToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player == "") return;
            textMessage.Text = "/w " + player + " ";
            textMessage.Select();
            textMessage.SelectionStart = textMessage.Text.Length;
        }

        private void addFriendToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player == "") return;
            if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
            {
                SessionInit.Instance.factoryGame.AddFriend(player);
            }
            else
            {
                MessageBox.Show("You cannot add yourself as friend.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ignoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listGroup.SelectedItems[0].Text;
            if (player == "") return;
            if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
            {
                SessionInit.Instance.factoryGame.IgnorePlayer(player);
            }
            else
            {
                MessageBox.Show("You cannot ignore yourself.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void removeFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var index = listFriends.SelectedItems[0].Index;
                var player = listFriends.SelectedItems[0].Text;
                var guid = AutomatedGame.friendGUIList[index];
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
            if (e.Button != MouseButtons.Right) return;
            var loc = listFriends.PointToScreen(e.Location);
            contextMenuFriendList.Show(loc);
        }

        private void whisperToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var player = listFriends.SelectedItems[0].Text;
            if (player == "") return;
            textMessage.Text = "/w " + player + " ";
            textMessage.Select();
            textMessage.SelectionStart = textMessage.Text.Length;
        }

        private void inviteToPartyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var player = listFriends.SelectedItems[0].Text;
            if (player == "") return;
            if (AutomatedGame.characterNameList[AutomatedGame.characterID] != player)
            {
                SessionInit.Instance.factoryGame.InvitePlayerToParty(player);
            }
            else
            {
                MessageBox.Show("You cannot invite yourself to a party.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void leaveChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // leave channel 1
            SessionInit.Instance.factoryGame.LeaveChannel(1, "");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // leave channel 2
            SessionInit.Instance.factoryGame.LeaveChannel(2, "");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // leave channel 3
            SessionInit.Instance.factoryGame.LeaveChannel(22, "");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // leave channel 4
            SessionInit.Instance.factoryGame.LeaveChannel(26, "");
        }

        private void leaveAllChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.LeaveChannel(1, "");
            SessionInit.Instance.factoryGame.LeaveChannel(2, "");
            SessionInit.Instance.factoryGame.LeaveChannel(22, "");
            SessionInit.Instance.factoryGame.LeaveChannel(26, "");
        }

        private void listTicket_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            try
            {
                var player = listTicket.SelectedItems[0].Text;
                SessionInit.Instance.factoryGame.RequestTicketDetails(player);
                if (!IsFormOpen("Ticket")) // look for form by caption.
                {
                    var frm = new FrmTicket();
                    frm.Show();
                }
                else
                {
                    Application.OpenForms["FrmTicket"]?.Focus();
                }
            }
            catch
            {
                //Ah fuck we broke it.
            }
        }

        private bool IsFormOpen(string formName)
        {
            foreach (Form formLoaded in Application.OpenForms)
            {
                if (formLoaded.Text.IndexOf(formName, StringComparison.Ordinal) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region test
            /*this.Hide();
            AutomatedGame.DisconClient = true;
            AutomatedGame.SetLoggedIn = false;
            FrmLogin frmlogin = new FrmLogin();
            frmlogin.Show();*/
            #endregion
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Environment.Exit(1);
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region test
            /*SessionInit.Instance.factoryGame.charLogout();
            Thread.Sleep(2000);
            AutomatedGame.charlogoutSucceeded = true;
            this.Hide();
            AutomatedGame.DisconClient = true;
            AutomatedGame.SetLoggedIn = false;
            CharacterSelection frmchar = new CharacterSelection();
            frmchar.Show();*/
            #endregion
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(500);
            System.Diagnostics.Process.Start(Application.ExecutablePath, "autologin");
            Environment.Exit(1);
        }
    }
}

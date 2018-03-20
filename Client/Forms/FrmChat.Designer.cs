using System.ComponentModel;
using System.Windows.Forms;

namespace BotFarm
{
    partial class FrmChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChat));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinAllChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveAllChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ChatWindow = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblChar = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.chattimer = new System.Windows.Forms.Timer(this.components);
            this.textMessage = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabWho = new System.Windows.Forms.TabPage();
            this.refreshWhoList = new System.Windows.Forms.PictureBox();
            this.lblplayercount = new System.Windows.Forms.Label();
            this.lblplayersonline = new System.Windows.Forms.Label();
            this.listWho = new System.Windows.Forms.ListView();
            this.tabGuild = new System.Windows.Forms.TabPage();
            this.listRoster = new System.Windows.Forms.ListView();
            this.tabChannel = new System.Windows.Forms.TabPage();
            this.listCustom = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.listWorld = new System.Windows.Forms.ListView();
            this.tabGroup = new System.Windows.Forms.TabPage();
            this.lblPartyGroupSize = new System.Windows.Forms.Label();
            this.lblPartyPlayers = new System.Windows.Forms.Label();
            this.btnGroupDisband = new System.Windows.Forms.Button();
            this.listGroup = new System.Windows.Forms.ListView();
            this.tabFriend = new System.Windows.Forms.TabPage();
            this.lblfriendcount = new System.Windows.Forms.Label();
            this.lblfriends = new System.Windows.Forms.Label();
            this.listFriends = new System.Windows.Forms.ListView();
            this.tabTicket = new System.Windows.Forms.TabPage();
            this.listTicket = new System.Windows.Forms.ListView();
            this.contextMenuWhoList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whisperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteToPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBStatusFlag = new System.Windows.Forms.ComboBox();
            this.lblStatusflag = new System.Windows.Forms.Label();
            this.contextMenuGroupList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whisperToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addFriendToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFriendList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.whisperToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.inviteToPartyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabWho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshWhoList)).BeginInit();
            this.tabGuild.SuspendLayout();
            this.tabChannel.SuspendLayout();
            this.tabGroup.SuspendLayout();
            this.tabFriend.SuspendLayout();
            this.tabTicket.SuspendLayout();
            this.contextMenuWhoList.SuspendLayout();
            this.contextMenuGroupList.SuspendLayout();
            this.contextMenuFriendList.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.channelToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1087, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveConversationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveConversationToolStripMenuItem
            // 
            this.saveConversationToolStripMenuItem.Name = "saveConversationToolStripMenuItem";
            this.saveConversationToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.saveConversationToolStripMenuItem.Text = "Save conversation";
            this.saveConversationToolStripMenuItem.Click += new System.EventHandler(this.saveConversationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinAllChannelsToolStripMenuItem,
            this.createNewChannelToolStripMenuItem,
            this.leaveAllChannelsToolStripMenuItem,
            this.leaveChannelToolStripMenuItem});
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.channelToolStripMenuItem.Text = "Channel";
            // 
            // joinAllChannelsToolStripMenuItem
            // 
            this.joinAllChannelsToolStripMenuItem.Name = "joinAllChannelsToolStripMenuItem";
            this.joinAllChannelsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.joinAllChannelsToolStripMenuItem.Text = "Join all default channels";
            this.joinAllChannelsToolStripMenuItem.Click += new System.EventHandler(this.joinAllChannelsToolStripMenuItem_Click);
            // 
            // createNewChannelToolStripMenuItem
            // 
            this.createNewChannelToolStripMenuItem.Name = "createNewChannelToolStripMenuItem";
            this.createNewChannelToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.createNewChannelToolStripMenuItem.Text = "Create new channel";
            this.createNewChannelToolStripMenuItem.Click += new System.EventHandler(this.createNewChannelToolStripMenuItem_Click);
            // 
            // leaveAllChannelsToolStripMenuItem
            // 
            this.leaveAllChannelsToolStripMenuItem.Name = "leaveAllChannelsToolStripMenuItem";
            this.leaveAllChannelsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.leaveAllChannelsToolStripMenuItem.Text = "Leave all channels";
            this.leaveAllChannelsToolStripMenuItem.Click += new System.EventHandler(this.leaveAllChannelsToolStripMenuItem_Click);
            // 
            // leaveChannelToolStripMenuItem
            // 
            this.leaveChannelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.leaveChannelToolStripMenuItem.Name = "leaveChannelToolStripMenuItem";
            this.leaveChannelToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.leaveChannelToolStripMenuItem.Text = "Leave channel";
            this.leaveChannelToolStripMenuItem.Click += new System.EventHandler(this.leaveChannelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem2.Text = "1. General";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem3.Text = "2. Trade";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem4.Text = "3. LocalDefense";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem5.Text = "4. LookingForGroup";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Settings";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItem1.Text = "About";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ChatWindow
            // 
            this.ChatWindow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ChatWindow.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ChatWindow.Location = new System.Drawing.Point(12, 27);
            this.ChatWindow.Name = "ChatWindow";
            this.ChatWindow.ReadOnly = true;
            this.ChatWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ChatWindow.Size = new System.Drawing.Size(502, 488);
            this.ChatWindow.TabIndex = 1;
            this.ChatWindow.Text = "";
            this.ChatWindow.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ChatWindow_LinkClicked);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(443, 518);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblChar
            // 
            this.lblChar.AutoSize = true;
            this.lblChar.Location = new System.Drawing.Point(544, 36);
            this.lblChar.Name = "lblChar";
            this.lblChar.Size = new System.Drawing.Size(19, 13);
            this.lblChar.TabIndex = 7;
            this.lblChar.Text = "    ";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(520, 29);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(18, 23);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "<";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // chattimer
            // 
            this.chattimer.Enabled = true;
            this.chattimer.Interval = 10;
            this.chattimer.Tick += new System.EventHandler(this.msgPull_Tick);
            // 
            // textMessage
            // 
            this.textMessage.BackColor = System.Drawing.Color.DarkGray;
            this.textMessage.Location = new System.Drawing.Point(94, 521);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(346, 20);
            this.textMessage.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabWho);
            this.tabControl1.Controls.Add(this.tabGuild);
            this.tabControl1.Controls.Add(this.tabChannel);
            this.tabControl1.Controls.Add(this.tabGroup);
            this.tabControl1.Controls.Add(this.tabFriend);
            this.tabControl1.Controls.Add(this.tabTicket);
            this.tabControl1.Location = new System.Drawing.Point(520, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(514, 483);
            this.tabControl1.TabIndex = 20;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabWho
            // 
            this.tabWho.Controls.Add(this.refreshWhoList);
            this.tabWho.Controls.Add(this.lblplayercount);
            this.tabWho.Controls.Add(this.lblplayersonline);
            this.tabWho.Controls.Add(this.listWho);
            this.tabWho.Location = new System.Drawing.Point(4, 22);
            this.tabWho.Name = "tabWho";
            this.tabWho.Size = new System.Drawing.Size(506, 457);
            this.tabWho.TabIndex = 2;
            this.tabWho.Text = "Who";
            this.tabWho.UseVisualStyleBackColor = true;
            // 
            // refreshWhoList
            // 
            this.refreshWhoList.Image = ((System.Drawing.Image)(resources.GetObject("refreshWhoList.Image")));
            this.refreshWhoList.Location = new System.Drawing.Point(476, 4);
            this.refreshWhoList.Name = "refreshWhoList";
            this.refreshWhoList.Size = new System.Drawing.Size(27, 23);
            this.refreshWhoList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.refreshWhoList.TabIndex = 4;
            this.refreshWhoList.TabStop = false;
            this.refreshWhoList.Click += new System.EventHandler(this.refreshWhoList_Click);
            // 
            // lblplayercount
            // 
            this.lblplayercount.Location = new System.Drawing.Point(88, 13);
            this.lblplayercount.Name = "lblplayercount";
            this.lblplayercount.Size = new System.Drawing.Size(49, 14);
            this.lblplayercount.TabIndex = 3;
            // 
            // lblplayersonline
            // 
            this.lblplayersonline.AutoSize = true;
            this.lblplayersonline.Location = new System.Drawing.Point(7, 13);
            this.lblplayersonline.Name = "lblplayersonline";
            this.lblplayersonline.Size = new System.Drawing.Size(78, 13);
            this.lblplayersonline.TabIndex = 2;
            this.lblplayersonline.Text = "Players online :";
            // 
            // listWho
            // 
            this.listWho.FullRowSelect = true;
            this.listWho.GridLines = true;
            this.listWho.Location = new System.Drawing.Point(0, 30);
            this.listWho.Name = "listWho";
            this.listWho.Size = new System.Drawing.Size(506, 434);
            this.listWho.TabIndex = 1;
            this.listWho.UseCompatibleStateImageBehavior = false;
            this.listWho.View = System.Windows.Forms.View.Details;
            this.listWho.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listWho_MouseClick);
            // 
            // tabGuild
            // 
            this.tabGuild.Controls.Add(this.listRoster);
            this.tabGuild.Location = new System.Drawing.Point(4, 22);
            this.tabGuild.Name = "tabGuild";
            this.tabGuild.Padding = new System.Windows.Forms.Padding(3);
            this.tabGuild.Size = new System.Drawing.Size(506, 457);
            this.tabGuild.TabIndex = 4;
            this.tabGuild.Text = "Guild";
            this.tabGuild.UseVisualStyleBackColor = true;
            // 
            // listRoster
            // 
            this.listRoster.FullRowSelect = true;
            this.listRoster.GridLines = true;
            this.listRoster.Location = new System.Drawing.Point(0, 30);
            this.listRoster.Name = "listRoster";
            this.listRoster.Size = new System.Drawing.Size(506, 434);
            this.listRoster.TabIndex = 4;
            this.listRoster.UseCompatibleStateImageBehavior = false;
            this.listRoster.View = System.Windows.Forms.View.Details;
            // 
            // tabChannel
            // 
            this.tabChannel.Controls.Add(this.listCustom);
            this.tabChannel.Controls.Add(this.label2);
            this.tabChannel.Controls.Add(this.listWorld);
            this.tabChannel.Location = new System.Drawing.Point(4, 22);
            this.tabChannel.Name = "tabChannel";
            this.tabChannel.Padding = new System.Windows.Forms.Padding(3);
            this.tabChannel.Size = new System.Drawing.Size(506, 457);
            this.tabChannel.TabIndex = 1;
            this.tabChannel.Text = "Channel";
            this.tabChannel.UseVisualStyleBackColor = true;
            // 
            // listCustom
            // 
            this.listCustom.FullRowSelect = true;
            this.listCustom.GridLines = true;
            this.listCustom.Location = new System.Drawing.Point(3, 115);
            this.listCustom.Name = "listCustom";
            this.listCustom.Size = new System.Drawing.Size(297, 96);
            this.listCustom.TabIndex = 2;
            this.listCustom.UseCompatibleStateImageBehavior = false;
            this.listCustom.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "World";
            // 
            // listWorld
            // 
            this.listWorld.FullRowSelect = true;
            this.listWorld.GridLines = true;
            this.listWorld.Location = new System.Drawing.Point(3, 20);
            this.listWorld.Name = "listWorld";
            this.listWorld.Size = new System.Drawing.Size(297, 97);
            this.listWorld.TabIndex = 0;
            this.listWorld.UseCompatibleStateImageBehavior = false;
            this.listWorld.View = System.Windows.Forms.View.Details;
            // 
            // tabGroup
            // 
            this.tabGroup.Controls.Add(this.lblPartyGroupSize);
            this.tabGroup.Controls.Add(this.lblPartyPlayers);
            this.tabGroup.Controls.Add(this.btnGroupDisband);
            this.tabGroup.Controls.Add(this.listGroup);
            this.tabGroup.Location = new System.Drawing.Point(4, 22);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tabGroup.Size = new System.Drawing.Size(506, 457);
            this.tabGroup.TabIndex = 0;
            this.tabGroup.Text = "Group";
            this.tabGroup.UseVisualStyleBackColor = true;
            // 
            // lblPartyGroupSize
            // 
            this.lblPartyGroupSize.AutoSize = true;
            this.lblPartyGroupSize.Location = new System.Drawing.Point(123, 123);
            this.lblPartyGroupSize.Name = "lblPartyGroupSize";
            this.lblPartyGroupSize.Size = new System.Drawing.Size(0, 13);
            this.lblPartyGroupSize.TabIndex = 3;
            // 
            // lblPartyPlayers
            // 
            this.lblPartyPlayers.AutoSize = true;
            this.lblPartyPlayers.Location = new System.Drawing.Point(3, 396);
            this.lblPartyPlayers.Name = "lblPartyPlayers";
            this.lblPartyPlayers.Size = new System.Drawing.Size(110, 13);
            this.lblPartyPlayers.TabIndex = 2;
            this.lblPartyPlayers.Text = "Total players in party :";
            // 
            // btnGroupDisband
            // 
            this.btnGroupDisband.Enabled = false;
            this.btnGroupDisband.Location = new System.Drawing.Point(157, 428);
            this.btnGroupDisband.Name = "btnGroupDisband";
            this.btnGroupDisband.Size = new System.Drawing.Size(78, 23);
            this.btnGroupDisband.TabIndex = 1;
            this.btnGroupDisband.Text = "Leave party";
            this.btnGroupDisband.UseVisualStyleBackColor = true;
            this.btnGroupDisband.Click += new System.EventHandler(this.btnGroupDisband_Click);
            // 
            // listGroup
            // 
            this.listGroup.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listGroup.FullRowSelect = true;
            this.listGroup.GridLines = true;
            this.listGroup.HoverSelection = true;
            this.listGroup.Location = new System.Drawing.Point(0, 0);
            this.listGroup.MultiSelect = false;
            this.listGroup.Name = "listGroup";
            this.listGroup.Scrollable = false;
            this.listGroup.Size = new System.Drawing.Size(510, 393);
            this.listGroup.TabIndex = 0;
            this.listGroup.UseCompatibleStateImageBehavior = false;
            this.listGroup.View = System.Windows.Forms.View.Details;
            this.listGroup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listGroup_MouseClick);
            // 
            // tabFriend
            // 
            this.tabFriend.Controls.Add(this.lblfriendcount);
            this.tabFriend.Controls.Add(this.lblfriends);
            this.tabFriend.Controls.Add(this.listFriends);
            this.tabFriend.Location = new System.Drawing.Point(4, 22);
            this.tabFriend.Name = "tabFriend";
            this.tabFriend.Padding = new System.Windows.Forms.Padding(3);
            this.tabFriend.Size = new System.Drawing.Size(506, 457);
            this.tabFriend.TabIndex = 3;
            this.tabFriend.Text = "Friends";
            this.tabFriend.UseVisualStyleBackColor = true;
            // 
            // lblfriendcount
            // 
            this.lblfriendcount.Location = new System.Drawing.Point(71, 13);
            this.lblfriendcount.Name = "lblfriendcount";
            this.lblfriendcount.Size = new System.Drawing.Size(24, 16);
            this.lblfriendcount.TabIndex = 2;
            // 
            // lblfriends
            // 
            this.lblfriends.AutoSize = true;
            this.lblfriends.Location = new System.Drawing.Point(7, 14);
            this.lblfriends.Name = "lblfriends";
            this.lblfriends.Size = new System.Drawing.Size(61, 13);
            this.lblfriends.TabIndex = 1;
            this.lblfriends.Text = "My friends :";
            // 
            // listFriends
            // 
            this.listFriends.FullRowSelect = true;
            this.listFriends.GridLines = true;
            this.listFriends.Location = new System.Drawing.Point(0, 30);
            this.listFriends.Name = "listFriends";
            this.listFriends.Size = new System.Drawing.Size(510, 434);
            this.listFriends.TabIndex = 0;
            this.listFriends.UseCompatibleStateImageBehavior = false;
            this.listFriends.View = System.Windows.Forms.View.Details;
            this.listFriends.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listFriends_MouseClick);
            // 
            // tabTicket
            // 
            this.tabTicket.Controls.Add(this.listTicket);
            this.tabTicket.Location = new System.Drawing.Point(4, 22);
            this.tabTicket.Name = "tabTicket";
            this.tabTicket.Padding = new System.Windows.Forms.Padding(3);
            this.tabTicket.Size = new System.Drawing.Size(506, 457);
            this.tabTicket.TabIndex = 5;
            this.tabTicket.Text = "Tickets";
            this.tabTicket.UseVisualStyleBackColor = true;
            // 
            // listTicket
            // 
            this.listTicket.FullRowSelect = true;
            this.listTicket.GridLines = true;
            this.listTicket.Location = new System.Drawing.Point(0, 30);
            this.listTicket.Name = "listTicket";
            this.listTicket.Size = new System.Drawing.Size(506, 434);
            this.listTicket.TabIndex = 5;
            this.listTicket.UseCompatibleStateImageBehavior = false;
            this.listTicket.View = System.Windows.Forms.View.Details;
            this.listTicket.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listTicket_MouseDoubleClick);
            // 
            // contextMenuWhoList
            // 
            this.contextMenuWhoList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whisperToolStripMenuItem,
            this.inviteToPartyToolStripMenuItem,
            this.addFriendToolStripMenuItem,
            this.ignoreToolStripMenuItem});
            this.contextMenuWhoList.Name = "contextMenuWhoList";
            this.contextMenuWhoList.Size = new System.Drawing.Size(148, 92);
            // 
            // whisperToolStripMenuItem
            // 
            this.whisperToolStripMenuItem.Name = "whisperToolStripMenuItem";
            this.whisperToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.whisperToolStripMenuItem.Text = "Whisper";
            this.whisperToolStripMenuItem.Click += new System.EventHandler(this.whisperToolStripMenuItem_Click);
            // 
            // inviteToPartyToolStripMenuItem
            // 
            this.inviteToPartyToolStripMenuItem.Name = "inviteToPartyToolStripMenuItem";
            this.inviteToPartyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.inviteToPartyToolStripMenuItem.Text = "Invite to party";
            this.inviteToPartyToolStripMenuItem.Click += new System.EventHandler(this.inviteToPartyToolStripMenuItem_Click);
            // 
            // addFriendToolStripMenuItem
            // 
            this.addFriendToolStripMenuItem.Name = "addFriendToolStripMenuItem";
            this.addFriendToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addFriendToolStripMenuItem.Text = "Add friend";
            this.addFriendToolStripMenuItem.Click += new System.EventHandler(this.addFriendToolStripMenuItem_Click);
            // 
            // ignoreToolStripMenuItem
            // 
            this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
            this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.ignoreToolStripMenuItem.Text = "Ignore";
            this.ignoreToolStripMenuItem.Click += new System.EventHandler(this.ignoreToolStripMenuItem_Click);
            // 
            // cBStatusFlag
            // 
            this.cBStatusFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBStatusFlag.FormattingEnabled = true;
            this.cBStatusFlag.Items.AddRange(new object[] {
            "Available",
            "Away",
            "Busy"});
            this.cBStatusFlag.Location = new System.Drawing.Point(1018, 36);
            this.cBStatusFlag.Name = "cBStatusFlag";
            this.cBStatusFlag.Size = new System.Drawing.Size(69, 21);
            this.cBStatusFlag.TabIndex = 21;
            this.cBStatusFlag.SelectionChangeCommitted += new System.EventHandler(this.cBStatusFlag_SelectionChangeCommitted);
            // 
            // lblStatusflag
            // 
            this.lblStatusflag.AutoSize = true;
            this.lblStatusflag.Location = new System.Drawing.Point(975, 39);
            this.lblStatusflag.Name = "lblStatusflag";
            this.lblStatusflag.Size = new System.Drawing.Size(37, 13);
            this.lblStatusflag.TabIndex = 22;
            this.lblStatusflag.Text = "Status";
            // 
            // contextMenuGroupList
            // 
            this.contextMenuGroupList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whisperToolStripMenuItem1,
            this.addFriendToolStripMenuItem1,
            this.ignoreToolStripMenuItem1});
            this.contextMenuGroupList.Name = "contextMenuGroupList";
            this.contextMenuGroupList.Size = new System.Drawing.Size(131, 70);
            // 
            // whisperToolStripMenuItem1
            // 
            this.whisperToolStripMenuItem1.Name = "whisperToolStripMenuItem1";
            this.whisperToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.whisperToolStripMenuItem1.Text = "Whisper";
            this.whisperToolStripMenuItem1.Click += new System.EventHandler(this.whisperToolStripMenuItem1_Click);
            // 
            // addFriendToolStripMenuItem1
            // 
            this.addFriendToolStripMenuItem1.Name = "addFriendToolStripMenuItem1";
            this.addFriendToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.addFriendToolStripMenuItem1.Text = "Add friend";
            this.addFriendToolStripMenuItem1.Click += new System.EventHandler(this.addFriendToolStripMenuItem1_Click);
            // 
            // ignoreToolStripMenuItem1
            // 
            this.ignoreToolStripMenuItem1.Name = "ignoreToolStripMenuItem1";
            this.ignoreToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.ignoreToolStripMenuItem1.Text = "Ignore";
            this.ignoreToolStripMenuItem1.Click += new System.EventHandler(this.ignoreToolStripMenuItem1_Click);
            // 
            // contextMenuFriendList
            // 
            this.contextMenuFriendList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whisperToolStripMenuItem2,
            this.inviteToPartyToolStripMenuItem1,
            this.removeFriendToolStripMenuItem});
            this.contextMenuFriendList.Name = "contextMenuFriendList";
            this.contextMenuFriendList.Size = new System.Drawing.Size(152, 70);
            // 
            // whisperToolStripMenuItem2
            // 
            this.whisperToolStripMenuItem2.Name = "whisperToolStripMenuItem2";
            this.whisperToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
            this.whisperToolStripMenuItem2.Text = "Whisper";
            this.whisperToolStripMenuItem2.Click += new System.EventHandler(this.whisperToolStripMenuItem2_Click);
            // 
            // inviteToPartyToolStripMenuItem1
            // 
            this.inviteToPartyToolStripMenuItem1.Name = "inviteToPartyToolStripMenuItem1";
            this.inviteToPartyToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.inviteToPartyToolStripMenuItem1.Text = "Invite to party";
            this.inviteToPartyToolStripMenuItem1.Click += new System.EventHandler(this.inviteToPartyToolStripMenuItem1_Click);
            // 
            // removeFriendToolStripMenuItem
            // 
            this.removeFriendToolStripMenuItem.Name = "removeFriendToolStripMenuItem";
            this.removeFriendToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.removeFriendToolStripMenuItem.Text = "Remove friend";
            this.removeFriendToolStripMenuItem.Click += new System.EventHandler(this.removeFriendToolStripMenuItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Global",
            "Guild",
            "Officer",
            "Party",
            "Raid"});
            this.comboBox1.Location = new System.Drawing.Point(0, 520);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(88, 21);
            this.comboBox1.TabIndex = 23;
            // 
            // FrmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 544);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblStatusflag);
            this.Controls.Add(this.cBStatusFlag);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lblChar);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.ChatWindow);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: WoWGasm Chat <DEBUG BUILD>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChat_FormClosing);
            this.Load += new System.EventHandler(this.FrmChat_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabWho.ResumeLayout(false);
            this.tabWho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshWhoList)).EndInit();
            this.tabGuild.ResumeLayout(false);
            this.tabChannel.ResumeLayout(false);
            this.tabChannel.PerformLayout();
            this.tabGroup.ResumeLayout(false);
            this.tabGroup.PerformLayout();
            this.tabFriend.ResumeLayout(false);
            this.tabFriend.PerformLayout();
            this.tabTicket.ResumeLayout(false);
            this.contextMenuWhoList.ResumeLayout(false);
            this.contextMenuGroupList.ResumeLayout(false);
            this.contextMenuFriendList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveConversationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private RichTextBox ChatWindow;
        private Button btnSend;
        private Label lblChar;
        private Button btnDisconnect;
        private Timer chattimer;
        private TextBox textMessage;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem channelToolStripMenuItem;
        private ToolStripMenuItem joinAllChannelsToolStripMenuItem;
        private ToolStripMenuItem createNewChannelToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabGroup;
        private TabPage tabChannel;
        private TabPage tabWho;
        private ListView listGroup;
        private Label lblPartyGroupSize;
        private Label lblPartyPlayers;
        private Button btnGroupDisband;
        private ListView listWho;
        private ListView listRoster;
        private ListView listTicket;
        private Label lblplayercount;
        private Label lblplayersonline;
        private ContextMenuStrip contextMenuWhoList;
        private ToolStripMenuItem whisperToolStripMenuItem;
        private ToolStripMenuItem inviteToPartyToolStripMenuItem;
        private ToolStripMenuItem addFriendToolStripMenuItem;
        private ToolStripMenuItem ignoreToolStripMenuItem;
        private PictureBox refreshWhoList;
        private TabPage tabFriend;
        private ComboBox cBStatusFlag;
        private Label lblStatusflag;
        private ListView listCustom;
        private Label label2;
        private ListView listWorld;
        private ContextMenuStrip contextMenuGroupList;
        private ToolStripMenuItem whisperToolStripMenuItem1;
        private ToolStripMenuItem addFriendToolStripMenuItem1;
        private ToolStripMenuItem ignoreToolStripMenuItem1;
        private ListView listFriends;
        private Label lblfriendcount;
        private Label lblfriends;
        private ContextMenuStrip contextMenuFriendList;
        private ToolStripMenuItem whisperToolStripMenuItem2;
        private ToolStripMenuItem removeFriendToolStripMenuItem;
        private ToolStripMenuItem inviteToPartyToolStripMenuItem1;
        private ToolStripMenuItem leaveAllChannelsToolStripMenuItem;
        private ToolStripMenuItem leaveChannelToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ComboBox comboBox1;
        private TabPage tabGuild;
        private TabPage tabTicket;
    }
}
using System.ComponentModel;
using System.Windows.Forms;

namespace BotFarm
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.cBAutoJoin = new System.Windows.Forms.CheckBox();
            this.gbChat = new System.Windows.Forms.GroupBox();
            this.btnChangeLoginRealm = new System.Windows.Forms.Button();
            this.btnChatWindowBGC = new System.Windows.Forms.Button();
            this.cbDisableAllColors = new System.Windows.Forms.CheckBox();
            this.cbDisableClickableUsernames = new System.Windows.Forms.CheckBox();
            this.cbDisableChannelColors = new System.Windows.Forms.CheckBox();
            this.cbNpcChat = new System.Windows.Forms.CheckBox();
            this.cbConnectionLost = new System.Windows.Forms.CheckBox();
            this.cbSendWithEnter = new System.Windows.Forms.CheckBox();
            this.gBOther = new System.Windows.Forms.GroupBox();
            this.rAfkDectM2 = new System.Windows.Forms.RadioButton();
            this.lblAfkDection = new System.Windows.Forms.Label();
            this.rAfkDectM1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numAfkMins = new System.Windows.Forms.NumericUpDown();
            this.cbAfk = new System.Windows.Forms.CheckBox();
            this.cboxAfkStatus = new System.Windows.Forms.ComboBox();
            this.gBInvites = new System.Windows.Forms.GroupBox();
            this.rbIgnoreM2 = new System.Windows.Forms.RadioButton();
            this.rbIgnoreM1 = new System.Windows.Forms.RadioButton();
            this.cbIgnoreChannelInvite = new System.Windows.Forms.CheckBox();
            this.cbIgnoreGroupInvite = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            this.ChatWindowPreview = new System.Windows.Forms.RichTextBox();
            this.lbExampletxt = new System.Windows.Forms.Label();
            this.btnRestoreDefault = new System.Windows.Forms.Button();
            this.gbChat.SuspendLayout();
            this.gBOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfkMins)).BeginInit();
            this.gBInvites.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBAutoJoin
            // 
            this.cBAutoJoin.AutoSize = true;
            this.cBAutoJoin.Location = new System.Drawing.Point(7, 19);
            this.cBAutoJoin.Name = "cBAutoJoin";
            this.cBAutoJoin.Size = new System.Drawing.Size(161, 17);
            this.cBAutoJoin.TabIndex = 0;
            this.cBAutoJoin.Text = "Auto join all default channels";
            this.cBAutoJoin.UseVisualStyleBackColor = true;
            // 
            // gbChat
            // 
            this.gbChat.Controls.Add(this.btnChangeLoginRealm);
            this.gbChat.Controls.Add(this.btnChatWindowBGC);
            this.gbChat.Controls.Add(this.cbDisableAllColors);
            this.gbChat.Controls.Add(this.cbDisableClickableUsernames);
            this.gbChat.Controls.Add(this.cbDisableChannelColors);
            this.gbChat.Controls.Add(this.cbNpcChat);
            this.gbChat.Controls.Add(this.cbConnectionLost);
            this.gbChat.Controls.Add(this.cbSendWithEnter);
            this.gbChat.Controls.Add(this.cBAutoJoin);
            this.gbChat.Location = new System.Drawing.Point(6, 6);
            this.gbChat.Name = "gbChat";
            this.gbChat.Size = new System.Drawing.Size(211, 248);
            this.gbChat.TabIndex = 1;
            this.gbChat.TabStop = false;
            this.gbChat.Text = "Chat";
            // 
            // btnChangeLoginRealm
            // 
            this.btnChangeLoginRealm.Location = new System.Drawing.Point(130, 192);
            this.btnChangeLoginRealm.Name = "btnChangeLoginRealm";
            this.btnChangeLoginRealm.Size = new System.Drawing.Size(75, 37);
            this.btnChangeLoginRealm.TabIndex = 9;
            this.btnChangeLoginRealm.Text = "Change login realm";
            this.btnChangeLoginRealm.UseVisualStyleBackColor = true;
            this.btnChangeLoginRealm.Click += new System.EventHandler(this.btnChangeLoginRealm_Click);
            // 
            // btnChatWindowBGC
            // 
            this.btnChatWindowBGC.Location = new System.Drawing.Point(7, 192);
            this.btnChatWindowBGC.Name = "btnChatWindowBGC";
            this.btnChatWindowBGC.Size = new System.Drawing.Size(122, 37);
            this.btnChatWindowBGC.TabIndex = 8;
            this.btnChatWindowBGC.Text = "Change chat window background color";
            this.btnChatWindowBGC.UseVisualStyleBackColor = true;
            this.btnChatWindowBGC.Click += new System.EventHandler(this.btnChatWindowBGC_Click);
            // 
            // cbDisableAllColors
            // 
            this.cbDisableAllColors.AutoSize = true;
            this.cbDisableAllColors.Location = new System.Drawing.Point(6, 160);
            this.cbDisableAllColors.Name = "cbDisableAllColors";
            this.cbDisableAllColors.Size = new System.Drawing.Size(200, 17);
            this.cbDisableAllColors.TabIndex = 7;
            this.cbDisableAllColors.Text = "Disable all colors (default is darkblue)";
            this.cbDisableAllColors.UseVisualStyleBackColor = true;
            // 
            // cbDisableClickableUsernames
            // 
            this.cbDisableClickableUsernames.AutoSize = true;
            this.cbDisableClickableUsernames.Location = new System.Drawing.Point(6, 136);
            this.cbDisableClickableUsernames.Name = "cbDisableClickableUsernames";
            this.cbDisableClickableUsernames.Size = new System.Drawing.Size(182, 17);
            this.cbDisableClickableUsernames.TabIndex = 6;
            this.cbDisableClickableUsernames.Text = "Disable clickable usernames (/w)";
            this.cbDisableClickableUsernames.UseVisualStyleBackColor = true;
            // 
            // cbDisableChannelColors
            // 
            this.cbDisableChannelColors.AutoSize = true;
            this.cbDisableChannelColors.Location = new System.Drawing.Point(7, 112);
            this.cbDisableChannelColors.Name = "cbDisableChannelColors";
            this.cbDisableChannelColors.Size = new System.Drawing.Size(168, 17);
            this.cbDisableChannelColors.TabIndex = 5;
            this.cbDisableChannelColors.Text = "Disable system channel colors";
            this.cbDisableChannelColors.UseVisualStyleBackColor = true;
            // 
            // cbNpcChat
            // 
            this.cbNpcChat.AutoSize = true;
            this.cbNpcChat.Location = new System.Drawing.Point(7, 89);
            this.cbNpcChat.Name = "cbNpcChat";
            this.cbNpcChat.Size = new System.Drawing.Size(110, 17);
            this.cbNpcChat.TabIndex = 3;
            this.cbNpcChat.Text = "Disable NPC chat";
            this.cbNpcChat.UseVisualStyleBackColor = true;
            // 
            // cbConnectionLost
            // 
            this.cbConnectionLost.AutoSize = true;
            this.cbConnectionLost.Location = new System.Drawing.Point(7, 66);
            this.cbConnectionLost.Name = "cbConnectionLost";
            this.cbConnectionLost.Size = new System.Drawing.Size(158, 17);
            this.cbConnectionLost.TabIndex = 2;
            this.cbConnectionLost.Text = "Logout after lost connection";
            this.cbConnectionLost.UseVisualStyleBackColor = true;
            // 
            // cbSendWithEnter
            // 
            this.cbSendWithEnter.AutoSize = true;
            this.cbSendWithEnter.Location = new System.Drawing.Point(7, 43);
            this.cbSendWithEnter.Name = "cbSendWithEnter";
            this.cbSendWithEnter.Size = new System.Drawing.Size(155, 17);
            this.cbSendWithEnter.TabIndex = 1;
            this.cbSendWithEnter.Text = "Use enter to send message";
            this.cbSendWithEnter.UseVisualStyleBackColor = true;
            // 
            // gBOther
            // 
            this.gBOther.Controls.Add(this.rAfkDectM2);
            this.gBOther.Controls.Add(this.lblAfkDection);
            this.gBOther.Controls.Add(this.rAfkDectM1);
            this.gBOther.Controls.Add(this.label1);
            this.gBOther.Controls.Add(this.numAfkMins);
            this.gBOther.Controls.Add(this.cbAfk);
            this.gBOther.Controls.Add(this.cboxAfkStatus);
            this.gBOther.Location = new System.Drawing.Point(223, 6);
            this.gBOther.Name = "gBOther";
            this.gBOther.Size = new System.Drawing.Size(207, 153);
            this.gBOther.TabIndex = 2;
            this.gBOther.TabStop = false;
            this.gBOther.Text = "Away from keyboard";
            // 
            // rAfkDectM2
            // 
            this.rAfkDectM2.AutoSize = true;
            this.rAfkDectM2.Location = new System.Drawing.Point(7, 125);
            this.rAfkDectM2.Name = "rAfkDectM2";
            this.rAfkDectM2.Size = new System.Drawing.Size(196, 17);
            this.rAfkDectM2.TabIndex = 6;
            this.rAfkDectM2.Text = "No XY mouse pos change detection";
            this.rAfkDectM2.UseVisualStyleBackColor = true;
            // 
            // lblAfkDection
            // 
            this.lblAfkDection.AutoSize = true;
            this.lblAfkDection.Location = new System.Drawing.Point(7, 81);
            this.lblAfkDection.Name = "lblAfkDection";
            this.lblAfkDection.Size = new System.Drawing.Size(162, 13);
            this.lblAfkDection.TabIndex = 5;
            this.lblAfkDection.Text = "AFK detection engine based on :";
            // 
            // rAfkDectM1
            // 
            this.rAfkDectM1.AutoSize = true;
            this.rAfkDectM1.Checked = true;
            this.rAfkDectM1.Location = new System.Drawing.Point(7, 101);
            this.rAfkDectM1.Name = "rAfkDectM1";
            this.rAfkDectM1.Size = new System.Drawing.Size(181, 17);
            this.rAfkDectM1.TabIndex = 4;
            this.rAfkDectM1.TabStop = true;
            this.rAfkDectM1.Text = "Last sent chat message to server";
            this.rAfkDectM1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "After being afk for minutes";
            // 
            // numAfkMins
            // 
            this.numAfkMins.Location = new System.Drawing.Point(149, 47);
            this.numAfkMins.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAfkMins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAfkMins.Name = "numAfkMins";
            this.numAfkMins.Size = new System.Drawing.Size(47, 20);
            this.numAfkMins.TabIndex = 2;
            this.numAfkMins.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // cbAfk
            // 
            this.cbAfk.AutoSize = true;
            this.cbAfk.Location = new System.Drawing.Point(7, 23);
            this.cbAfk.Name = "cbAfk";
            this.cbAfk.Size = new System.Drawing.Size(122, 17);
            this.cbAfk.TabIndex = 1;
            this.cbAfk.Text = "Change my status to";
            this.cbAfk.UseVisualStyleBackColor = true;
            this.cbAfk.CheckedChanged += new System.EventHandler(this.cbAfk_CheckedChanged);
            // 
            // cboxAfkStatus
            // 
            this.cboxAfkStatus.DisplayMember = "ter";
            this.cboxAfkStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxAfkStatus.FormattingEnabled = true;
            this.cboxAfkStatus.Items.AddRange(new object[] {
            "Away",
            "Busy"});
            this.cboxAfkStatus.Location = new System.Drawing.Point(132, 20);
            this.cboxAfkStatus.Name = "cboxAfkStatus";
            this.cboxAfkStatus.Size = new System.Drawing.Size(64, 21);
            this.cboxAfkStatus.TabIndex = 0;
            // 
            // gBInvites
            // 
            this.gBInvites.Controls.Add(this.rbIgnoreM2);
            this.gBInvites.Controls.Add(this.rbIgnoreM1);
            this.gBInvites.Controls.Add(this.cbIgnoreChannelInvite);
            this.gBInvites.Controls.Add(this.cbIgnoreGroupInvite);
            this.gBInvites.Location = new System.Drawing.Point(223, 165);
            this.gBInvites.Name = "gBInvites";
            this.gBInvites.Size = new System.Drawing.Size(207, 89);
            this.gBInvites.TabIndex = 3;
            this.gBInvites.TabStop = false;
            this.gBInvites.Text = "Invites";
            // 
            // rbIgnoreM2
            // 
            this.rbIgnoreM2.AutoSize = true;
            this.rbIgnoreM2.Location = new System.Drawing.Point(112, 64);
            this.rbIgnoreM2.Name = "rbIgnoreM2";
            this.rbIgnoreM2.Size = new System.Drawing.Size(76, 17);
            this.rbIgnoreM2.TabIndex = 3;
            this.rbIgnoreM2.Text = "Just ignore";
            this.rbIgnoreM2.UseVisualStyleBackColor = true;
            // 
            // rbIgnoreM1
            // 
            this.rbIgnoreM1.AutoSize = true;
            this.rbIgnoreM1.Checked = true;
            this.rbIgnoreM1.Location = new System.Drawing.Point(13, 64);
            this.rbIgnoreM1.Name = "rbIgnoreM1";
            this.rbIgnoreM1.Size = new System.Drawing.Size(94, 17);
            this.rbIgnoreM1.TabIndex = 2;
            this.rbIgnoreM1.TabStop = true;
            this.rbIgnoreM1.Text = "Decline invites";
            this.rbIgnoreM1.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreChannelInvite
            // 
            this.cbIgnoreChannelInvite.AutoSize = true;
            this.cbIgnoreChannelInvite.Location = new System.Drawing.Point(7, 44);
            this.cbIgnoreChannelInvite.Name = "cbIgnoreChannelInvite";
            this.cbIgnoreChannelInvite.Size = new System.Drawing.Size(130, 17);
            this.cbIgnoreChannelInvite.TabIndex = 1;
            this.cbIgnoreChannelInvite.Text = "Ignore channel invites";
            this.cbIgnoreChannelInvite.UseVisualStyleBackColor = true;
            this.cbIgnoreChannelInvite.CheckedChanged += new System.EventHandler(this.cbIgnoreChannelInvite_CheckedChanged);
            // 
            // cbIgnoreGroupInvite
            // 
            this.cbIgnoreGroupInvite.AutoSize = true;
            this.cbIgnoreGroupInvite.Location = new System.Drawing.Point(7, 20);
            this.cbIgnoreGroupInvite.Name = "cbIgnoreGroupInvite";
            this.cbIgnoreGroupInvite.Size = new System.Drawing.Size(115, 17);
            this.cbIgnoreGroupInvite.TabIndex = 0;
            this.cbIgnoreGroupInvite.Text = "Ignore party invites";
            this.cbIgnoreGroupInvite.UseVisualStyleBackColor = true;
            this.cbIgnoreGroupInvite.CheckedChanged += new System.EventHandler(this.cbIgnoreGroupInvite_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(424, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Okay";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colorGrid
            // 
            this.colorGrid.AutoAddColors = false;
            this.colorGrid.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.colorGrid.Columns = 40;
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.Both;
            this.colorGrid.Location = new System.Drawing.Point(446, 6);
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Padding = new System.Windows.Forms.Padding(0);
            this.colorGrid.SelectedCellStyle = Cyotek.Windows.Forms.ColorGridSelectedCellStyle.Standard;
            this.colorGrid.ShowCustomColors = false;
            this.colorGrid.Size = new System.Drawing.Size(480, 48);
            this.colorGrid.Spacing = new System.Drawing.Size(0, 0);
            this.colorGrid.TabIndex = 21;
            this.colorGrid.ColorChanged += new System.EventHandler(this.colorGrid_ColorChanged);
            // 
            // colorEditor
            // 
            this.colorEditor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorEditor.Location = new System.Drawing.Point(448, 49);
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Size = new System.Drawing.Size(162, 228);
            this.colorEditor.TabIndex = 20;
            this.colorEditor.ColorChanged += new System.EventHandler(this.colorEditor_ColorChanged);
            // 
            // colorWheel
            // 
            this.colorWheel.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorWheel.Location = new System.Drawing.Point(616, 55);
            this.colorWheel.Name = "colorWheel";
            this.colorWheel.Size = new System.Drawing.Size(105, 101);
            this.colorWheel.TabIndex = 19;
            this.colorWheel.ColorChanged += new System.EventHandler(this.colorWheel_ColorChanged);
            // 
            // ChatWindowPreview
            // 
            this.ChatWindowPreview.Location = new System.Drawing.Point(616, 155);
            this.ChatWindowPreview.Name = "ChatWindowPreview";
            this.ChatWindowPreview.Size = new System.Drawing.Size(310, 130);
            this.ChatWindowPreview.TabIndex = 22;
            this.ChatWindowPreview.Text = "";
            // 
            // lbExampletxt
            // 
            this.lbExampletxt.AutoSize = true;
            this.lbExampletxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExampletxt.Location = new System.Drawing.Point(723, 135);
            this.lbExampletxt.Name = "lbExampletxt";
            this.lbExampletxt.Size = new System.Drawing.Size(194, 13);
            this.lbExampletxt.TabIndex = 23;
            this.lbExampletxt.Text = "Chat background color example :";
            // 
            // btnRestoreDefault
            // 
            this.btnRestoreDefault.Location = new System.Drawing.Point(781, 82);
            this.btnRestoreDefault.Name = "btnRestoreDefault";
            this.btnRestoreDefault.Size = new System.Drawing.Size(107, 23);
            this.btnRestoreDefault.TabIndex = 24;
            this.btnRestoreDefault.Text = "Restore default";
            this.btnRestoreDefault.UseVisualStyleBackColor = true;
            this.btnRestoreDefault.Click += new System.EventHandler(this.btnRestoreDefault_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 291);
            this.Controls.Add(this.btnRestoreDefault);
            this.Controls.Add(this.lbExampletxt);
            this.Controls.Add(this.ChatWindowPreview);
            this.Controls.Add(this.colorEditor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gBInvites);
            this.Controls.Add(this.gBOther);
            this.Controls.Add(this.gbChat);
            this.Controls.Add(this.colorGrid);
            this.Controls.Add(this.colorWheel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.gbChat.ResumeLayout(false);
            this.gbChat.PerformLayout();
            this.gBOther.ResumeLayout(false);
            this.gBOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAfkMins)).EndInit();
            this.gBInvites.ResumeLayout(false);
            this.gBInvites.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cBAutoJoin;
        private GroupBox gbChat;
        private CheckBox cbSendWithEnter;
        private GroupBox gBOther;
        private Label label1;
        private NumericUpDown numAfkMins;
        private CheckBox cbAfk;
        private ComboBox cboxAfkStatus;
        private GroupBox gBInvites;
        private CheckBox cbIgnoreChannelInvite;
        private CheckBox cbIgnoreGroupInvite;
        private Button btnSave;
        private RadioButton rAfkDectM2;
        private Label lblAfkDection;
        private RadioButton rAfkDectM1;
        private RadioButton rbIgnoreM2;
        private RadioButton rbIgnoreM1;
        private CheckBox cbConnectionLost;
        private CheckBox cbNpcChat;
        private Button btnChatWindowBGC;
        private CheckBox cbDisableAllColors;
        private CheckBox cbDisableClickableUsernames;
        private CheckBox cbDisableChannelColors;
        private Cyotek.Windows.Forms.ColorGrid colorGrid;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ColorWheel colorWheel;
        private RichTextBox ChatWindowPreview;
        private Label lbExampletxt;
        private Button btnRestoreDefault;
        private Button btnChangeLoginRealm;
    }
}
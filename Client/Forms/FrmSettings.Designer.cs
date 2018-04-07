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
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.gBOther = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numAfkMins = new System.Windows.Forms.NumericUpDown();
            this.cbAfk = new System.Windows.Forms.CheckBox();
            this.cboxAfkStatus = new System.Windows.Forms.ComboBox();
            this.gBInvites = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
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
            this.gbChat.Controls.Add(this.checkBox3);
            this.gbChat.Controls.Add(this.cBAutoJoin);
            this.gbChat.Location = new System.Drawing.Point(6, 6);
            this.gbChat.Name = "gbChat";
            this.gbChat.Size = new System.Drawing.Size(180, 79);
            this.gbChat.TabIndex = 1;
            this.gbChat.TabStop = false;
            this.gbChat.Text = "Chat";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(7, 43);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(155, 17);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Use enter to send message";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // gBOther
            // 
            this.gBOther.Controls.Add(this.label1);
            this.gBOther.Controls.Add(this.numAfkMins);
            this.gBOther.Controls.Add(this.cbAfk);
            this.gBOther.Controls.Add(this.cboxAfkStatus);
            this.gBOther.Location = new System.Drawing.Point(193, 6);
            this.gBOther.Name = "gBOther";
            this.gBOther.Size = new System.Drawing.Size(207, 170);
            this.gBOther.TabIndex = 2;
            this.gBOther.TabStop = false;
            this.gBOther.Text = "Other";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "After being afk for minutes :";
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
            this.gBInvites.Controls.Add(this.checkBox2);
            this.gBInvites.Controls.Add(this.checkBox1);
            this.gBInvites.Location = new System.Drawing.Point(6, 87);
            this.gBInvites.Name = "gBInvites";
            this.gBInvites.Size = new System.Drawing.Size(181, 89);
            this.gBInvites.TabIndex = 3;
            this.gBInvites.TabStop = false;
            this.gBInvites.Text = "Invites";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(7, 44);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(130, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Ignore channel invites";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Ignore party invites";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(394, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Okay";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 211);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gBInvites);
            this.Controls.Add(this.gBOther);
            this.Controls.Add(this.gbChat);
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

        }

        #endregion

        private CheckBox cBAutoJoin;
        private GroupBox gbChat;
        private CheckBox checkBox3;
        private GroupBox gBOther;
        private Label label1;
        private NumericUpDown numAfkMins;
        private CheckBox cbAfk;
        private ComboBox cboxAfkStatus;
        private GroupBox gBInvites;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button btnSave;
    }
}
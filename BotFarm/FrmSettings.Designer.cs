namespace BotFarm
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.gBOther = new System.Windows.Forms.GroupBox();
            this.gBInvites = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbChat.SuspendLayout();
            this.gBOther.SuspendLayout();
            this.gBInvites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            // gBOther
            // 
            this.gBOther.Controls.Add(this.label1);
            this.gBOther.Controls.Add(this.numericUpDown1);
            this.gBOther.Controls.Add(this.checkBox4);
            this.gBOther.Controls.Add(this.comboBox1);
            this.gBOther.Location = new System.Drawing.Point(193, 6);
            this.gBOther.Name = "gBOther";
            this.gBOther.Size = new System.Drawing.Size(207, 170);
            this.gBOther.TabIndex = 2;
            this.gBOther.TabStop = false;
            this.gBOther.Text = "Other";
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Away",
            "Busy"});
            this.comboBox1.Location = new System.Drawing.Point(132, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(7, 23);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(122, 17);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Text = "Change my status to";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(149, 47);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(394, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Okay";
            this.btnSave.UseVisualStyleBackColor = true;
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
            this.gBInvites.ResumeLayout(false);
            this.gBInvites.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cBAutoJoin;
        private System.Windows.Forms.GroupBox gbChat;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.GroupBox gBOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox gBInvites;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnSave;
    }
}
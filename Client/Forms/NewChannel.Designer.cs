namespace BotFarm
{
    partial class NewChannel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewChannel));
            this.lblChannelname = new System.Windows.Forms.Label();
            this.lblChannelpassword = new System.Windows.Forms.Label();
            this.ChannelName = new System.Windows.Forms.TextBox();
            this.ChannelPassword = new System.Windows.Forms.TextBox();
            this.btnNewChannel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChannelname
            // 
            this.lblChannelname.AutoSize = true;
            this.lblChannelname.Location = new System.Drawing.Point(12, 19);
            this.lblChannelname.Name = "lblChannelname";
            this.lblChannelname.Size = new System.Drawing.Size(77, 13);
            this.lblChannelname.TabIndex = 0;
            this.lblChannelname.Text = "Channel Name";
            // 
            // lblChannelpassword
            // 
            this.lblChannelpassword.AutoSize = true;
            this.lblChannelpassword.Location = new System.Drawing.Point(12, 61);
            this.lblChannelpassword.Name = "lblChannelpassword";
            this.lblChannelpassword.Size = new System.Drawing.Size(99, 13);
            this.lblChannelpassword.TabIndex = 1;
            this.lblChannelpassword.Text = "Password (optional)";
            // 
            // ChannelName
            // 
            this.ChannelName.Location = new System.Drawing.Point(15, 36);
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.Size = new System.Drawing.Size(168, 20);
            this.ChannelName.TabIndex = 2;
            // 
            // ChannelPassword
            // 
            this.ChannelPassword.Location = new System.Drawing.Point(15, 78);
            this.ChannelPassword.Name = "ChannelPassword";
            this.ChannelPassword.Size = new System.Drawing.Size(168, 20);
            this.ChannelPassword.TabIndex = 3;
            // 
            // btnNewChannel
            // 
            this.btnNewChannel.Location = new System.Drawing.Point(15, 113);
            this.btnNewChannel.Name = "btnNewChannel";
            this.btnNewChannel.Size = new System.Drawing.Size(85, 23);
            this.btnNewChannel.TabIndex = 4;
            this.btnNewChannel.Text = "Okay";
            this.btnNewChannel.UseVisualStyleBackColor = true;
            this.btnNewChannel.Click += new System.EventHandler(this.btnNewChannel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(99, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NewChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 148);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNewChannel);
            this.Controls.Add(this.ChannelPassword);
            this.Controls.Add(this.ChannelName);
            this.Controls.Add(this.lblChannelpassword);
            this.Controls.Add(this.lblChannelname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Channel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChannelname;
        private System.Windows.Forms.Label lblChannelpassword;
        private System.Windows.Forms.TextBox ChannelName;
        private System.Windows.Forms.TextBox ChannelPassword;
        private System.Windows.Forms.Button btnNewChannel;
        private System.Windows.Forms.Button btnCancel;
    }
}
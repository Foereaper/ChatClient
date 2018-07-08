namespace Client.Forms
{
    partial class LoginRealmSelection
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
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.btnSaveRealm = new System.Windows.Forms.Button();
            this.cbRealms = new System.Windows.Forms.ComboBox();
            this.lblAvailableRealms = new System.Windows.Forms.Label();
            this.lblCurrentLoginRealm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Location = new System.Drawing.Point(12, 99);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(98, 23);
            this.btnRestoreDefaults.TabIndex = 1;
            this.btnRestoreDefaults.Text = "Restore defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // btnSaveRealm
            // 
            this.btnSaveRealm.Location = new System.Drawing.Point(137, 99);
            this.btnSaveRealm.Name = "btnSaveRealm";
            this.btnSaveRealm.Size = new System.Drawing.Size(121, 23);
            this.btnSaveRealm.TabIndex = 2;
            this.btnSaveRealm.Text = "Save selected realm";
            this.btnSaveRealm.UseVisualStyleBackColor = true;
            this.btnSaveRealm.Click += new System.EventHandler(this.btnSaveRealm_Click);
            // 
            // cbRealms
            // 
            this.cbRealms.FormattingEnabled = true;
            this.cbRealms.Location = new System.Drawing.Point(12, 38);
            this.cbRealms.Name = "cbRealms";
            this.cbRealms.Size = new System.Drawing.Size(246, 21);
            this.cbRealms.TabIndex = 3;
            // 
            // lblAvailableRealms
            // 
            this.lblAvailableRealms.AutoSize = true;
            this.lblAvailableRealms.Location = new System.Drawing.Point(12, 22);
            this.lblAvailableRealms.Name = "lblAvailableRealms";
            this.lblAvailableRealms.Size = new System.Drawing.Size(86, 13);
            this.lblAvailableRealms.TabIndex = 4;
            this.lblAvailableRealms.Text = "Available realms:";
            // 
            // lblCurrentLoginRealm
            // 
            this.lblCurrentLoginRealm.AutoSize = true;
            this.lblCurrentLoginRealm.Location = new System.Drawing.Point(12, 72);
            this.lblCurrentLoginRealm.Name = "lblCurrentLoginRealm";
            this.lblCurrentLoginRealm.Size = new System.Drawing.Size(80, 13);
            this.lblCurrentLoginRealm.TabIndex = 5;
            this.lblCurrentLoginRealm.Text = "Currently set to:";
            // 
            // LoginRealmSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 131);
            this.Controls.Add(this.lblCurrentLoginRealm);
            this.Controls.Add(this.lblAvailableRealms);
            this.Controls.Add(this.cbRealms);
            this.Controls.Add(this.btnSaveRealm);
            this.Controls.Add(this.btnRestoreDefaults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginRealmSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select default realm for login";
            this.Load += new System.EventHandler(this.LoginRealmSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.Button btnSaveRealm;
        private System.Windows.Forms.ComboBox cbRealms;
        private System.Windows.Forms.Label lblAvailableRealms;
        private System.Windows.Forms.Label lblCurrentLoginRealm;
    }
}
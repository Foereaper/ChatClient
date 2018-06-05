using System.ComponentModel;
using System.Windows.Forms;

namespace BotFarm
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.lblAcc = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.autherror = new System.Windows.Forms.Label();
            this.authcheck = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.loginSave = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(48, 175);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(112, 20);
            this.username.TabIndex = 1;
            this.username.Text = global::Client.Properties.Settings.Default.Username;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(48, 221);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(112, 20);
            this.password.TabIndex = 2;
            this.password.Text = global::Client.Properties.Settings.Default.Password;
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.Location = new System.Drawing.Point(46, 159);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(53, 13);
            this.lblAcc.TabIndex = 4;
            this.lblAcc.Text = "Account :";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(46, 205);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(59, 13);
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "Password :";
            // 
            // BtnLogin
            // 
            this.BtnLogin.Location = new System.Drawing.Point(178, 175);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(90, 72);
            this.BtnLogin.TabIndex = 6;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // autherror
            // 
            this.autherror.AutoSize = true;
            this.autherror.ForeColor = System.Drawing.Color.Red;
            this.autherror.Location = new System.Drawing.Point(12, 252);
            this.autherror.Name = "autherror";
            this.autherror.Size = new System.Drawing.Size(0, 13);
            this.autherror.TabIndex = 9;
            // 
            // authcheck
            // 
            this.authcheck.Enabled = true;
            this.authcheck.Tick += new System.EventHandler(this.authcheck_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Copy debug log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Debug shizzle :";
            // 
            // Logo
            // 
            this.Logo.Image = global::Client.Properties.Resources.logo_new;
            this.Logo.Location = new System.Drawing.Point(20, 12);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(260, 93);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 3;
            this.Logo.TabStop = false;
            // 
            // loginSave
            // 
            this.loginSave.AutoSize = true;
            this.loginSave.Location = new System.Drawing.Point(182, 248);
            this.loginSave.Name = "loginSave";
            this.loginSave.Size = new System.Drawing.Size(86, 17);
            this.loginSave.TabIndex = 12;
            this.loginSave.Text = "Save Login?";
            this.loginSave.UseVisualStyleBackColor = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 274);
            this.Controls.Add(this.loginSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.autherror);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblAcc);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:: WoWGasm Chat Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox username;
        private TextBox password;
        private PictureBox Logo;
        private Label lblAcc;
        private Label lblPass;
        private Button BtnLogin;
        private Label autherror;
        private Timer authcheck;
        private Button button1;
        private Label label1;
        private CheckBox loginSave;
    }
}
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
            this.lblAcc = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.autherror = new System.Windows.Forms.Label();
            this.authcheck = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loginSave = new System.Windows.Forms.CheckBox();
            this.lblLogonserver = new System.Windows.Forms.Label();
            this.logonserver = new System.Windows.Forms.TextBox();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.password = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(26)))));
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username.ForeColor = System.Drawing.Color.White;
            this.username.Location = new System.Drawing.Point(34, 147);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(202, 13);
            this.username.TabIndex = 1;
            this.username.Text = global::Client.Properties.Settings.Default.Username;
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.BackColor = System.Drawing.Color.Transparent;
            this.lblAcc.ForeColor = System.Drawing.Color.White;
            this.lblAcc.Location = new System.Drawing.Point(21, 125);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(81, 13);
            this.lblAcc.TabIndex = 4;
            this.lblAcc.Text = "Account Name:";
            this.lblAcc.Click += new System.EventHandler(this.lblAcc_Click);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.BackColor = System.Drawing.Color.Transparent;
            this.lblPass.ForeColor = System.Drawing.Color.White;
            this.lblPass.Location = new System.Drawing.Point(21, 178);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(56, 13);
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "Password:";
            // 
            // BtnLogin
            // 
            this.BtnLogin.BackColor = System.Drawing.Color.Transparent;
            this.BtnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLogin.BackgroundImage")));
            this.BtnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLogin.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnLogin.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogin.Image")));
            this.BtnLogin.Location = new System.Drawing.Point(24, 272);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(214, 34);
            this.BtnLogin.TabIndex = 6;
            this.BtnLogin.Text = "Sign in";
            this.BtnLogin.UseVisualStyleBackColor = false;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // autherror
            // 
            this.autherror.AutoSize = true;
            this.autherror.ForeColor = System.Drawing.Color.Red;
            this.autherror.Location = new System.Drawing.Point(16, 252);
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
            // loginSave
            // 
            this.loginSave.AutoSize = true;
            this.loginSave.BackColor = System.Drawing.Color.Transparent;
            this.loginSave.Cursor = System.Windows.Forms.Cursors.Default;
            this.loginSave.ForeColor = System.Drawing.Color.White;
            this.loginSave.Location = new System.Drawing.Point(23, 234);
            this.loginSave.Name = "loginSave";
            this.loginSave.Size = new System.Drawing.Size(147, 17);
            this.loginSave.TabIndex = 12;
            this.loginSave.Text = "Remember my credentials";
            this.loginSave.UseVisualStyleBackColor = false;
            this.loginSave.CheckedChanged += new System.EventHandler(this.loginSave_CheckedChanged);
            // 
            // lblLogonserver
            // 
            this.lblLogonserver.AutoSize = true;
            this.lblLogonserver.Location = new System.Drawing.Point(59, 358);
            this.lblLogonserver.Name = "lblLogonserver";
            this.lblLogonserver.Size = new System.Drawing.Size(75, 13);
            this.lblLogonserver.TabIndex = 7;
            this.lblLogonserver.Text = "Logon server :";
            this.lblLogonserver.Visible = false;
            this.lblLogonserver.Click += new System.EventHandler(this.lblLogonserver_Click);
            // 
            // logonserver
            // 
            this.logonserver.Location = new System.Drawing.Point(62, 386);
            this.logonserver.Name = "logonserver";
            this.logonserver.Size = new System.Drawing.Size(217, 20);
            this.logonserver.TabIndex = 0;
            this.logonserver.Text = "logon.wowgasm-reloaded.org";
            this.logonserver.Visible = false;
            this.logonserver.TextChanged += new System.EventHandler(this.logonserver_TextChanged);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Location = new System.Drawing.Point(158, 400);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(100, 100);
            this.printPreviewControl1.TabIndex = 13;
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
            this.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Logo.Image = global::Client.Properties.Resources.logo_new;
            this.Logo.Location = new System.Drawing.Point(1, 6);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(260, 93);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 3;
            this.Logo.TabStop = false;
            this.Logo.Click += new System.EventHandler(this.Logo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Client.Properties.Resources.loginbox1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(21, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 30);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::Client.Properties.Resources.loginbox1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(21, 194);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(232, 30);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(26)))));
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.ForeColor = System.Drawing.Color.White;
            this.password.Location = new System.Drawing.Point(36, 199);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(202, 13);
            this.password.TabIndex = 2;
            this.password.Text = global::Client.Properties.Settings.Default.Password;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Client.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(262, 322);
            this.Controls.Add(this.username);
            this.Controls.Add(this.printPreviewControl1);
            this.Controls.Add(this.loginSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.autherror);
            this.Controls.Add(this.lblLogonserver);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.lblAcc);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.password);
            this.Controls.Add(this.logonserver);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogin_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox username;
        private PictureBox Logo;
        private Label lblAcc;
        private Label lblPass;
        private Button BtnLogin;
        private Label autherror;
        private Timer authcheck;
        private Button button1;
        private Label label1;
        private CheckBox loginSave;
        private Label lblLogonserver;
        private TextBox logonserver;
        private PrintPreviewControl printPreviewControl1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox password;
    }
}
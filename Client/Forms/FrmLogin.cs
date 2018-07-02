﻿using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Client;
using Client.Properties;

namespace BotFarm
{
    public partial class FrmLogin : Form
    {
        
        public FrmLogin(bool autologin)
        {
            InitializeComponent();
            if(autologin == true)
            {
                DoLogin();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            BtnLogin.Enabled = true;
            password.KeyPress += CheckEnter;
        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DoLogin();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(1000);
            Environment.Exit(1);
        }

        private void authcheck_Tick(object sender, EventArgs e)
        {
            if (AutomatedGame.IsLoggedIn())
            {
                Hide();
                authcheck.Enabled = false;
                var realmselection = new RealmSelection();
                realmselection.Show();   
            }
            if (AutomatedGame.AuthenticationError)
            {
                autherror.Text = AutomatedGame.AuthErrorText;
            }
        }

        /// <summary> 
        /// Check if login service/server is responding
        /// </summary>
        public bool KnockServerPort(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }
                    client.EndConnect(result);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void DoLogin()
        {
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please enter a username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BtnLogin.Enabled = false;
            password.Enabled = false;
            var timeout = TimeSpan.FromSeconds(10);
            if (KnockServerPort(logonserver.Text, 3724, timeout))
            {
                SessionInit.setLogonserver = logonserver.Text;
                SessionInit.setUsername = username.Text;
                SessionInit.setPassword = password.Text;
                if (loginSave.Checked)
                {
                    Settings.Default.Username = username.Text;
                    Settings.Default.Password = password.Text;
                    Settings.Default.Save();
                }
                var start = new SessionInit();
                GC.KeepAlive(start);
            }
            else
            {
                MessageBox.Show("The logon server is not responding.\nWrong logon address or server is offline.", "we hit a wall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            BtnLogin.Enabled = true;
            password.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void lblAcc_Click(object sender, EventArgs e)
        {

        }

        private void Logo_Click(object sender, EventArgs e)
        {

        }

        private void logonserver_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblLogonserver_Click(object sender, EventArgs e)
        {

        }

        private void loginSave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

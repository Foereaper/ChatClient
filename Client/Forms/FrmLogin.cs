using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Client;
using Client.Properties;

namespace BotFarm
{
    public partial class FrmLogin : Form
    {
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;

        private static int count;
        
        public FrmLogin(bool autologin)
        {
            InitializeComponent();
            // Optimalize window for heavy graphics display (reduce flickering).
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            if (autologin == true)
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
                authcheck.Enabled = false;
                Hide();
                NextWindow();
            }
            if (AutomatedGame.AuthenticationError)
            {
                autherror.Text = AutomatedGame.AuthErrorText;
            }
        }

        private void NextWindow()
        {
            //CharacterSelection charselection = new CharacterSelection();
            RealmSelection realm = new RealmSelection();
            try
            {
                if(Settings.Default.DefaultLoginRealm == "")
                {
                    realm.Show();
                }
                /*else
                {
                    if (charselection.WindowState != System.Windows.Forms.FormWindowState.Normal)
                    {
                        charselection.Show();
                    }
                }*/
            }
            catch
            {
                realm.Show();
            }
            //var realmselection = new RealmSelection();
            //realmselection.Show();
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

        #region movableWindow & mutex
        protected override void WndProc(ref Message m)
        {
            //movablewindow
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            //disable double click to fullscreen the launcher
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
            //mutex
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }
        #endregion
    }
}

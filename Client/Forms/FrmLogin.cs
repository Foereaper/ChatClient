using System;
using System.Net.Sockets;
using System.Windows.Forms;
using Client;


namespace BotFarm
{
    public partial class FrmLogin : Form
    {
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            BtnLogin.Enabled = true;
            this.password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnter);
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DoLogin();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutomatedGame.DisconClient = true;
            this.Hide();
            System.Threading.Thread.Sleep(1000);
            Application.Exit();
        }

        private void authcheck_Tick(object sender, EventArgs e)
        {
            if (AutomatedGame.IsLoggedIn() == true)
            {
                this.Hide();
                authcheck.Enabled = false;
                RealmSelection realmselection = new RealmSelection();
                realmselection.Show();   
            }
            if (AutomatedGame.AuthenticationError == true)
            {
                autherror.Text = AutomatedGame.AuthErrorText.ToString();
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
            BtnLogin.Enabled = false;
            password.Enabled = false;
            TimeSpan timeout = TimeSpan.FromSeconds(10);
            if (KnockServerPort(logonserver.Text, 3724, timeout) == true)
            {
                BotFactory.setLogonserver = logonserver.Text;
                BotFactory.setUsername = username.Text;
                BotFactory.setPassword = password.Text;
                BotFactory start = new BotFactory();
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
            //Clipboard.SetText(dbgLog.Text);
        }
    }
}

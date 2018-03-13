using System;
using System.Windows.Forms;
using Client;

namespace BotFarm
{
    public partial class NewChannel : Form
    {
        public NewChannel()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ChannelName.Text = "";
            ChannelPassword.Text = "";
            this.Hide();
        }

        private void btnNewChannel_Click(object sender, EventArgs e)
        {
            var input = ChannelName.Text;
            bool cnametest = true;//Regex.IsMatch(input, @"^[a-zA-Z0-9~!@#$%^&*()[]_+{}:;|<>?,./\-]+$"); //@"^[a-zA-Z0-9~!@#$%^&*()_+{}:;|<>?,./]+$"
            if (cnametest == true && AutomatedGame.customChannels.Count < 6)
            {
                if (!AutomatedGame.customChannels.Contains(ChannelName.Text))
                {
                    AutomatedGame.customChannels.Add(ChannelName.Text);
                }

                MakeChannel(ChannelName.Text, ChannelPassword.Text);

                ChannelName.Text = "";
                ChannelPassword.Text = "";
                this.Hide();
            }
            else
            {
                if(cnametest == false)
                {
                    MessageBox.Show("Channel name invalid!", "we hit a wall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if(AutomatedGame.customChannels.Count < 6)
                {
                    MessageBox.Show("Maximum custom channels is 6.\nDelete one before creating a new one.", "we hit a wall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void MakeChannel(string channel, string password)
        {
            SessionInit.Instance.factoryGame.JoinChannel(0, channel, password);
        }
    }
}

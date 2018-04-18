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
            Hide();
        }

        private void btnNewChannel_Click(object sender, EventArgs e)
        {
            var input = ChannelName.Text;
            if (AutomatedGame.customChannels.Count < 6)
            {
                MakeChannel(ChannelName.Text, ChannelPassword.Text);

                ChannelName.Text = "";
                ChannelPassword.Text = "";
                Hide();
            }
            else
            {
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

using System;
using System.Windows.Forms;
using Client.Properties;

namespace BotFarm
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            /*try
            {
                Settings.Default.AFKcheck = cbAfk.Checked;
                Settings.Default.AFKstatus = cboxAfkStatus.SelectedIndex;
                Settings.Default.AFKmins = Convert.ToInt32(numAfkMins.Value);
                Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("There is an issue with your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            try
            {
                // AFK
                cbAfk.Checked = Settings.Default.AFKcheck;
                cboxAfkStatus.SelectedIndex = Settings.Default.AFKstatus;
                numAfkMins.Value = Settings.Default.AFKmins;
                if (Settings.Default.AFKDM == 1)
                {
                    rAfkDectM1.Checked = true;
                }
                if (Settings.Default.AFKDM == 2)
                {
                    rAfkDectM2.Checked = true;
                }
                // CHAT
                cBAutoJoin.Checked = Settings.Default.AutoJoinChannel;
                cbSendWithEnter.Checked = Settings.Default.SmgSendEnter;
                // INVITE
                cbIgnoreGroupInvite.Checked = Settings.Default.IgnoreGroupInvite;
                cbIgnoreChannelInvite.Checked = Settings.Default.IngoreChannelInvite;
            }
            catch
            {
                MessageBox.Show("There is an issue with your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                // AFK
                Settings.Default.AFKcheck = cbAfk.Checked;
                Settings.Default.AFKstatus = cboxAfkStatus.SelectedIndex;
                Settings.Default.AFKmins = Convert.ToInt32(numAfkMins.Value);
                if (rAfkDectM1.Checked == true)
                {
                    Settings.Default.AFKDM = 1;
                }
                if (rAfkDectM2.Checked == true)
                {
                    Settings.Default.AFKDM = 2;
                }

                // CHAT
                Settings.Default.AutoJoinChannel = cBAutoJoin.Checked;
                Settings.Default.SmgSendEnter = cbSendWithEnter.Checked;
                // INVITE
                Settings.Default.IgnoreGroupInvite = cbIgnoreGroupInvite.Checked;
                Settings.Default.IngoreChannelInvite = cbIgnoreChannelInvite.Checked;


                /*FrmChat frmc = new FrmChat();
                if (cbSendWithEnter.Checked == true)
                {
                    frmc.ChatCheckEnter(true);
                }
                if (cbSendWithEnter.Checked == false)
                {
                    frmc.ChatCheckEnter(false);
                }*/


                Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Error while saving to your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
        }

    }
}

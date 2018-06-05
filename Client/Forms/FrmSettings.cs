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
            try
            {
                // AFK
                cbAfk.Checked = Settings.Default.AFKcheck;
                if (Settings.Default.AFKcheck == false)
                {
                    cboxAfkStatus.Enabled = false;
                    numAfkMins.Enabled = false;
                    numAfkMins.Enabled = false;
                    rAfkDectM1.Enabled = false;
                    rAfkDectM2.Enabled = false;
                }
                else
                {
                    cboxAfkStatus.Enabled = true;
                    numAfkMins.Enabled = true;
                    numAfkMins.Enabled = true;
                    rAfkDectM1.Enabled = true;
                    rAfkDectM2.Enabled = true;
                }
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
                if(cbIgnoreGroupInvite.Checked == false && cbIgnoreChannelInvite.Checked == false)
                {
                    rbIgnoreM1.Enabled = false;
                    rbIgnoreM2.Enabled = false;
                }

                if(Settings.Default.IgnoreMode == 1)
                {
                    rbIgnoreM1.Checked = true;
                    rbIgnoreM2.Checked = false;
                }
                if(Settings.Default.IgnoreMode == 2)
                {
                    rbIgnoreM1.Checked = false;
                    rbIgnoreM2.Checked = true;
                }
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
                //let end user know we need a restart for the applied settings to work
                if (Settings.Default.AFKcheck != cbAfk.Checked || Settings.Default.SmgSendEnter != cbSendWithEnter.Checked)
                {
                    MessageBox.Show("These setting(s) require a client restart in order for them" + Environment.NewLine +
                        "to properly work.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                if(Settings.Default.IgnoreGroupInvite == false && Settings.Default.IngoreChannelInvite == false)
                {
                    rbIgnoreM1.Enabled = false;
                    rbIgnoreM2.Enabled = false;
                }
                else
                {
                    if (rbIgnoreM1.Checked == true)
                    {
                        Settings.Default.IgnoreMode = 1;
                    }
                    if (rbIgnoreM2.Checked == true)
                    {
                        Settings.Default.IgnoreMode = 2;
                    }
                }
                Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("Error while saving to your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
        }

        private void cbAfk_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAfk.Checked == true)
            {
                cboxAfkStatus.Enabled = true;
                numAfkMins.Enabled = true;
                numAfkMins.Enabled = true;
                rAfkDectM1.Enabled = true;
                rAfkDectM2.Enabled = true;
            }
            else
            {
                cboxAfkStatus.Enabled = false;
                numAfkMins.Enabled = false;
                numAfkMins.Enabled = false;
                rAfkDectM1.Enabled = false;
                rAfkDectM2.Enabled = false;
            }
        }

        private void cbIgnoreGroupInvite_CheckedChanged(object sender, EventArgs e)
        {
            if(cbIgnoreGroupInvite.Checked == true || cbIgnoreChannelInvite.Checked == true)
            {
                rbIgnoreM1.Enabled = true;
                rbIgnoreM2.Enabled = true;
            }
            else
            {
                rbIgnoreM1.Enabled = false;
                rbIgnoreM2.Enabled = false;
            }
        }

        private void cbIgnoreChannelInvite_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIgnoreChannelInvite.Checked == true || cbIgnoreGroupInvite.Checked == true)
            {
                rbIgnoreM1.Enabled = true;
                rbIgnoreM2.Enabled = true;
            }
            else
            {
                rbIgnoreM1.Enabled = false;
                rbIgnoreM2.Enabled = false;
            }
        }
    }
}

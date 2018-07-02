using System;
using System.Drawing;
using System.Windows.Forms;
using Client.Properties;
using Client.Forms;
using Client;

namespace BotFarm
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            try
            {
                InitializeComponent();
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
                cbConnectionLost.Checked = Settings.Default.ConnectionLostLogout;
                cbNpcChat.Checked = Settings.Default.NPCChat;
                cbDisableChannelColors.Checked = Settings.Default.DisableSystemChannelColors;
                cbDisableClickableUsernames.Checked = Settings.Default.DisableClickableUsernames;
                cbDisableAllColors.Checked = Settings.Default.DisableAllChatColors;
                if(Settings.Default.ChatWindowBackgroundColor != "")
                {
                    string WindowBGColor = Settings.Default.ChatWindowBackgroundColor;
                    string[] BGRGB = WindowBGColor.Split(':');
                    ChatWindowPreview.BackColor = Color.FromArgb(Convert.ToInt32(BGRGB[0]), Convert.ToInt32(BGRGB[1]), Convert.ToInt32(BGRGB[2]));
                    colorEditor.Color = Color.FromArgb(Convert.ToInt32(BGRGB[0]), Convert.ToInt32(BGRGB[1]), Convert.ToInt32(BGRGB[2]));
                    colorWheel.Color = Color.FromArgb(Convert.ToInt32(BGRGB[0]), Convert.ToInt32(BGRGB[1]), Convert.ToInt32(BGRGB[2]));
                }
                // INVITE
                cbIgnoreGroupInvite.Checked = Settings.Default.IgnoreGroupInvite;
                cbIgnoreChannelInvite.Checked = Settings.Default.IngoreChannelInvite;
                if (cbIgnoreGroupInvite.Checked == false && cbIgnoreChannelInvite.Checked == false)
                {
                    rbIgnoreM1.Enabled = false;
                    rbIgnoreM2.Enabled = false;
                }

                if (Settings.Default.IgnoreMode == 1)
                {
                    rbIgnoreM1.Checked = true;
                    rbIgnoreM2.Checked = false;
                }
                if (Settings.Default.IgnoreMode == 2)
                {
                    rbIgnoreM1.Checked = false;
                    rbIgnoreM2.Checked = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error while reading the config or loading a reference.\n\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Width = 453;
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
                Settings.Default.ConnectionLostLogout = cbConnectionLost.Checked;
                Settings.Default.NPCChat = cbNpcChat.Checked;
                Settings.Default.DisableSystemChannelColors = cbDisableChannelColors.Checked;
                Settings.Default.DisableClickableUsernames = cbDisableClickableUsernames.Checked;
                Settings.Default.DisableAllChatColors = cbDisableAllColors.Checked;
                if (colorEditor.Color != Color.FromArgb(unchecked((int)0xFF000000)))
                {
                    string R = Convert.ToInt32(colorEditor.Color.R).ToString();
                    string G = Convert.ToInt32(colorEditor.Color.G).ToString();
                    string B = Convert.ToInt32(colorEditor.Color.B).ToString();
                    Settings.Default.ChatWindowBackgroundColor = R + ":" + G + ":" + B;
                    AutomatedGame.chatWindowBGColor = R + ":" + G + ":" + B;
                }
                // INVITE
                Settings.Default.IgnoreGroupInvite = cbIgnoreGroupInvite.Checked;
                Settings.Default.IngoreChannelInvite = cbIgnoreChannelInvite.Checked;
                if (Settings.Default.IgnoreGroupInvite == false && Settings.Default.IngoreChannelInvite == false)
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
            if (cbAfk.Checked == true)
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
            if (cbIgnoreGroupInvite.Checked == true || cbIgnoreChannelInvite.Checked == true)
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

        private void btnChatWindowBGC_Click(object sender, EventArgs e)
        {
            if (this.Width != 948)
            {
                this.Width = 948;
            }
            else if (this.Width == 948)
            {
                this.Width = 453;
            }
            //render chat window example;
            ChatWindowPreview.Text = "";
            AppendText(ChatWindowPreview, "[System] : We have chat client authentication." + "\r\n", Color.DarkBlue, true);
            AppendText(ChatWindowPreview, "[System] : Welcome to WoWGasm Reloaded! Enjoy your stay!" + "\r\n", Color.DarkBlue, true);
            AppendText(ChatWindowPreview, "[System] : ", Color.DarkBlue, true);
            AppendText(ChatWindowPreview, "First character? Be sure to use the ", Color.LightBlue, true);
            AppendText(ChatWindowPreview, "WELCOME-2-GASM ", Color.Yellow, true);
            AppendText(ChatWindowPreview, "redeem code before reaching level 10! Look for Landro Longshot at every starting area or faction city! ", Color.LightBlue, true);
            AppendText(ChatWindowPreview, "This code can only be redeemed once!" + "\r\n", Color.Red, true);
        }

        private void AppendText(RichTextBox box, string text, Color color, bool bold = false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.ScrollToCaret();
        }

        private void colorWheel_ColorChanged(object sender, EventArgs e)
        {
            try
            {
                colorEditor.Color = colorWheel.Color;
                ChatWindowPreview.BackColor = colorWheel.Color;
                string R = Convert.ToInt32(colorEditor.Color.R).ToString();
                string G = Convert.ToInt32(colorEditor.Color.G).ToString();
                string B = Convert.ToInt32(colorEditor.Color.B).ToString();
                AutomatedGame.chatWindowBGColor = R + ":" + G + ":" + B;
            }
            catch
            {

            }
        }

        private void colorGrid_ColorChanged(object sender, EventArgs e)
        {
            colorEditor.Color = colorGrid.Color;
            colorWheel.Color = colorGrid.Color;
        }

        private void btnRestoreDefault_Click(object sender, EventArgs e)
        {
            ChatWindowPreview.BackColor = Color.WhiteSmoke;
            colorEditor.Color = Color.FromArgb(unchecked((int)0xFFF5F5F5));
            colorWheel.Color = Color.FromArgb(unchecked((int)0xFFF5F5F5));
            string R = Convert.ToInt32(colorEditor.Color.R).ToString();
            string G = Convert.ToInt32(colorEditor.Color.G).ToString();
            string B = Convert.ToInt32(colorEditor.Color.B).ToString();
            AutomatedGame.chatWindowBGColor = R + ":" + G + ":" + B;
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            try
            {
                colorWheel.Color = colorEditor.Color;
                ChatWindowPreview.BackColor = colorEditor.Color;
                string R = Convert.ToInt32(colorEditor.Color.R).ToString();
                string G = Convert.ToInt32(colorEditor.Color.G).ToString();
                string B = Convert.ToInt32(colorEditor.Color.B).ToString();
                AutomatedGame.chatWindowBGColor = R + ":" + G + ":" + B;
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Transparent colors are not supported.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

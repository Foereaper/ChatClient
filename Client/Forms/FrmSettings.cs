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
                Settings.Default.AFKcheck = cbAfk.Checked;
                Settings.Default.AFKstatus = cboxAfkStatus.SelectedIndex;
                Settings.Default.AFKmins = Convert.ToInt32(numAfkMins.Value);
                Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show("There is an issue with your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Settings.Default.Save();
        }
    }
}

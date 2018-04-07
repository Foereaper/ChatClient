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

            Settings.Default.AFKcheck = cbAfk.Checked;
            Settings.Default.AFKstatus = Convert.ToByte(cboxAfkStatus.SelectedIndex);
            Settings.Default.AFKmins = Convert.ToInt32(numAfkMins.Value);
            Settings.Default.Save();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Settings.Default.Save();
        }
    }
}

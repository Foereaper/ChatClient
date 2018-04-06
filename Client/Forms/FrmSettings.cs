using System;
using System.Configuration;
using System.Windows.Forms;

namespace BotFarm
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            cbAfk.Checked = Convert.ToBoolean(config.AppSettings.Settings["AFKcheck"].Value);
            cboxAfkStatus.SelectedIndex = Convert.ToInt32(config.AppSettings.Settings["AFKstatus"].Value);
            numAfkMins.Value = Convert.ToDecimal(config.AppSettings.Settings["AFKmins"].Value);
            config.AppSettings.Settings.Add("AFKstatus", cboxAfkStatus.SelectedIndex.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("AFKcheck");
            config.AppSettings.Settings.Remove("AFKstatus");
            config.AppSettings.Settings.Remove("AFKmins");
            config.AppSettings.Settings.Add("AFKcheck", cbAfk.Checked.ToString());
            config.AppSettings.Settings.Add("AFKstatus", cboxAfkStatus.SelectedIndex.ToString());
            config.AppSettings.Settings.Add("AFKmins", numAfkMins.Value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}

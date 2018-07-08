using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;
using Client.Properties;

namespace Client.Forms
{
    public partial class LoginRealmSelection : Form
    {
        public LoginRealmSelection()
        {
            InitializeComponent();
        }

        private void LoginRealmSelection_Load(object sender, EventArgs e)
        {
            cbRealms.Items.Clear();
            foreach (var realmname in AutomatedGame.presentrealmList)
            {
                cbRealms.Items.Add(realmname);
            }
            try
            {
                if(Settings.Default.DefaultLoginRealm != "")
                {
                    lblCurrentLoginRealm.Text = "Currently set to: " + Settings.Default.DefaultLoginRealm;
                }
                else
                {
                    lblCurrentLoginRealm.Text = "Currently set to: default (selected when logged in)"; 
                }
            }
            catch
            {
                MessageBox.Show("There was an error while reading the config.\n\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.DefaultLoginRealm = "";
                Settings.Default.Save();
                if (Settings.Default.DefaultLoginRealm != "")
                {
                    lblCurrentLoginRealm.Text = "Currently set to: " + Settings.Default.DefaultLoginRealm;
                }
                else
                {
                    lblCurrentLoginRealm.Text = "Currently set to: default (selected when logged in)";
                }
            }
            catch
            {
                MessageBox.Show("Error while saving to your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            try
            {
                if (AutomatedGame.presentrealmList.Contains(cbRealms.Text))
                {
                    Settings.Default.DefaultLoginRealm = cbRealms.Text;
                    Settings.Default.Save();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The realm you're trying to save doesn't exist.", "Non existent", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Error while saving to your settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Threading;
using System.Windows.Forms;
using Client.Properties;
using Client;
using Client.Forms;

namespace BotFarm
{
    public partial class RealmSelection : Form
    {
        public RealmSelection()
        {
            InitializeComponent();
            try
            {
                if (Settings.Default.DefaultLoginRealm != "")
                {
                    if (AutomatedGame.presentrealmList.Contains(Settings.Default.DefaultLoginRealm.ToString()))
                    {
                        int index = AutomatedGame.presentrealmList.IndexOf(Settings.Default.DefaultLoginRealm.ToString());
                        AutomatedGame.realmidGUI = index;
                        AutomatedGame.realmchosen = true;
                        Hide();
                        var characterselection = new CharacterSelection();
                        characterselection.Show();
                    }
                }
            }
            catch { }
        }

        private void lb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lb1.SelectedIndex != -1)
            {
                AutomatedGame.realmidGUI = lb1.SelectedIndex;
                AutomatedGame.realmchosen = true;
                Hide();
                var characterselection = new CharacterSelection();
                characterselection.Show();
            }
        }

        private void RealmSelection_Load(object sender, EventArgs e)
        {
            lb1.Items.Clear();
            foreach (var realmname in AutomatedGame.presentrealmList)
            {
                lb1.Items.Add(realmname);
            }         
        }

        private void RealmSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            AutomatedGame.DisconClient = true;
            Thread.Sleep(3000);
            Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Environment.Exit(1);
        }
    }
}

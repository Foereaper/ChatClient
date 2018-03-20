using System;
using System.Threading;
using System.Windows.Forms;
using Client;

namespace BotFarm
{
    public partial class RealmSelection : Form
    {
        public RealmSelection()
        {
            InitializeComponent();
        }

        private void lb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutomatedGame.realmidGUI = lb1.SelectedIndex;
            AutomatedGame.realmchosen = true;
            Hide();
            var characterselection = new CharacterSelection();
            characterselection.Show();
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
            AutomatedGame.DisconClient = true;
            Hide();
            Thread.Sleep(1000);
            Application.Exit();
        }
    }
}

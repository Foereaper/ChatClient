using System;
using System.Windows.Forms;
using Client;

namespace BotFarm
{
    public partial class CharacterSelection : Form
    {
        public CharacterSelection()
        {
            InitializeComponent();
        }

        private void lb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lb1.Items.Count == 1 || lb1.Items.Count > 1)
            {
                if (lb1.SelectedIndex == -1)
                {
                    return;
                }
                charfound.Enabled = false;
                AutomatedGame.characterID = lb1.SelectedIndex;
                AutomatedGame.characterchosen = true;
                this.Hide();
                FrmChat frmchat = new FrmChat();
                frmchat.Show();
            }
            else
            {
                MessageBox.Show("This account does not seem to have any characters.", "we hit a wall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AutomatedGame.DisconClient = true;
                this.Hide();
                System.Threading.Thread.Sleep(1000);
                Application.Exit();
            }
        }

        private void CharacterSelection_Load(object sender, EventArgs e)
        {
            charfound.Enabled = true;
            /*
            //lb1.Items.Clear();
            while (!AutomatedGame.Charsloaded)
            {
                //System.Threading.Thread.Sleep(100);
                foreach (string charactername in AutomatedGame.presentcharacterList)
                {
                    lb1.Items.Add(charactername.ToString()); //charactername.ToString()
                }
                //System.Threading.Thread.Sleep(100);
            }*/
        }

        private void charfound_Tick(object sender, EventArgs e)
        {
            if(AutomatedGame.Charsloaded == false)
            {
                //System.Threading.Thread.Sleep(100);
            }
            else
            {
                charfound.Enabled = false;
                foreach (string charactername in AutomatedGame.presentcharacterList)
                {
                    lb1.Items.Add(charactername.ToString()); //charactername.ToString()
                }
            }
        }

        private void CharacterSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutomatedGame.DisconClient = true;
            this.Hide();
            System.Threading.Thread.Sleep(1000);
            Application.Exit();
        }
    }
}

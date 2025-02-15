﻿using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Client;
using Client.World;

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
                /*if(AutomatedGame.charlogoutSucceeded == true)
                {
                    SessionInit.Instance.factoryGame.charLogin(lb1.SelectedIndex);
                    charfound.Enabled = false;
                    AutomatedGame.characterID = lb1.SelectedIndex;
                    AutomatedGame.characterchosen = true;
                    Hide();
                    var frmchat = new FrmChat();
                    frmchat.Show();
                }
                else
                {*/
                    charfound.Enabled = false;
                    AutomatedGame.characterID = lb1.SelectedIndex;
                    AutomatedGame.characterchosen = true;
                    Hide();
                    var frmchat = new FrmChat();
                    frmchat.Show();
                //}
            }
            else
            {
                MessageBox.Show("This account does not seem to have any characters.", "we hit a wall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AutomatedGame.DisconClient = true;
                Hide();
                Thread.Sleep(1000);
                Environment.Exit(1);
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
                foreach (var charactername in AutomatedGame.presentcharacterList)
                {
                    byte[] bytes = Encoding.Default.GetBytes(charactername);
                    lb1.Items.Add(Encoding.UTF8.GetString(bytes).ToString());
                }
            }
        }

        private void CharacterSelection_FormClosing(object sender, FormClosingEventArgs e)
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

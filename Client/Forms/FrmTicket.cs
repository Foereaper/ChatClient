using BotFarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class FrmTicket : Form
    {
        public FrmTicket()
        {
            InitializeComponent();
        }

        private void FrmTicket_Load(object sender, EventArgs e)
        {
            CurrentTicket ticket = SessionInit.Instance.factoryGame.Game.World.currentViewedTicket;
            ticketMessage.Text = ticket.ticketMessage;
            ticketComment.Text = ticket.ticketComment;
            assignName.Text = ticket.assignedPlayer;
            ticketResponse.Text = ticket.ticketResponse;
            chatLog.Text = ticket.ticketChatLog;
        }

        private void completeTicketButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteTicketButton_Click(object sender, EventArgs e)
        {

        }

        private void completeAchButton_Click(object sender, EventArgs e)
        {

        }

        private void completeQuestButton_Click(object sender, EventArgs e)
        {

        }

        private void checkItemButton_Click(object sender, EventArgs e)
        {

        }

        private void responseButton_Click(object sender, EventArgs e)
        {

        }

        private void commentButton_Click(object sender, EventArgs e)
        {

        }

        private void sendMailButton_Click(object sender, EventArgs e)
        {

        }

        private void assignButton_Click(object sender, EventArgs e)
        {

        }

        private void addItemButton_Click(object sender, EventArgs e)
        {

        }

        private void removeItemButton_Click(object sender, EventArgs e)
        {

        }

        private void checkQuestBotton_Click(object sender, EventArgs e)
        {

        }

        private void checkAchButton_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;
using BotFarm;

namespace Client.Forms
{
    
    public partial class FrmTicket : Form
    {
        public static FrmTicket frm = new FrmTicket();

        public FrmTicket()
        {
            InitializeComponent();
        }

        public static string ticketMessageData;
        public string ticketMessageProp
        {
            set => ticketMessageData = value;
            get => ticketMessageData;
        }

        public static string ticketCommentData;
        public string ticketCommentProp
        {
            set => ticketCommentData = value;
            get => ticketCommentData;
        }

        public static string assignNameData;
        public string assignNameProp
        {
            set => assignNameData = value;
            get => assignNameData;
        }

        public static string ticketResponseData;
        public string ticketResponseProp
        {
            set => ticketResponseData = value;
            get => ticketResponseData;
        }

        public static string chatLogData;
        public string chatLogProp
        {
            set => chatLogData = value;
            get => chatLogData;
        }

        public static bool PullProps;
        public bool PullProp
        {
            set => PullProps = value;
            get => PullProps;
        }

        public static class TicketFrm
        {
            public static void DataUpdate()
            {
                GenerateTicketData();
            }
        }

        private static void GenerateTicketData()
        {
            var ticket = SessionInit.Instance.factoryGame.Game.World.currentViewedTicket;
            frm.ticketMessageProp = ticket.ticketMessage;
            frm.ticketCommentProp = ticket.ticketComment;
            frm.assignNameProp = ticket.assignedPlayer;
            frm.ticketResponseProp = ticket.ticketResponse;
            frm.chatLogProp = ticket.ticketChatLog;
            frm.PullProp = true;
        }

        private void TimerCheckPull_Tick(object sender, EventArgs e)
        {
            if (PullProp)
            {
                ticketMessage.Text = ticketMessageData;
                ticketComment.Text = ticketCommentData;
                assignName.Text = assignNameData;
                ticketResponse.Text = ticketResponseData;
                chatLog.Text = chatLogData;
                PullProp = false;
                TimerCheckPull.Enabled = false;
            }
        }

        private void FrmTicket_Shown(object sender, EventArgs e)
        {
            TimerCheckPull.Enabled = true;
        }

        private void FrmTicket_Load(object sender, EventArgs e)
        {
            TimerCheckPull.Enabled = true;
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

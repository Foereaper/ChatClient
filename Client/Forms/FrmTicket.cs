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

        public static string ticketNameData;
        public string ticketNameProp
        {
            set => ticketNameData = value;
            get => ticketNameData;
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
            }
        }

        private static void GenerateTicketData()
        {
        }

        private void TimerCheckPull_Tick(object sender, EventArgs e)
        {
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
            SessionInit.Instance.factoryGame.CompleteTicket(ticketNameData);
        }

        private void deleteTicketButton_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.DeleteTicket(ticketNameData);
        }

        private void completeAchButton_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(completeAchButton.Text))
                SessionInit.Instance.factoryGame.CompleteAchievement(ticketNameData, Convert.ToUInt32(completeAchButton.Text));
        }

        private void completeQuestButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(completeQuestId.Text))
                SessionInit.Instance.factoryGame.CompleteQuest(ticketNameData, Convert.ToUInt32(completeQuestId.Text));
        }

        private void checkItemButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(checkItemId.Text))
                SessionInit.Instance.factoryGame.HasItem(ticketNameData, Convert.ToUInt32(checkItemId.Text));
        }

        private void responseButton_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(ticketResponse.Text))
                SessionInit.Instance.factoryGame.TicketResponse(ticketNameData, ticketResponse.Text);
        }

        private void commentButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ticketComment.Text))
                SessionInit.Instance.factoryGame.TicketComment(ticketNameData, ticketComment.Text);

        }

        private void sendMailButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mailSubject.Text) || string.IsNullOrWhiteSpace(mailBody.Text))
                return;
            SessionInit.Instance.factoryGame.MailPlayer(ticketNameData, mailSubject.Text, mailBody.Text);
        }

        private void assignButton_Click(object sender, EventArgs e)
        {
            SessionInit.Instance.factoryGame.TicketAssign(ticketNameData, string.IsNullOrWhiteSpace(assignName.Text) ? "UNASSIGN" : assignName.Text);
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addItemEntry.Text) || string.IsNullOrWhiteSpace(addItemCount.Text))
                return;

            SessionInit.Instance.factoryGame.AddItem(ticketNameData, Convert.ToUInt32(addItemEntry.Text), Convert.ToUInt32(addItemCount.Text));
        }

        private void removeItemButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(removeItemId.Text) || string.IsNullOrWhiteSpace(removeItemCount.Text))
                return;

            SessionInit.Instance.factoryGame.RemoveItem(ticketNameData, Convert.ToUInt32(removeItemId.Text), Convert.ToUInt32(removeItemCount.Text));

        }

        private void checkQuestBotton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(checkQuestId.Text))
                SessionInit.Instance.factoryGame.RequestQuestStatus(ticketNameData,Convert.ToUInt32(checkQuestId.Text));
        }

        private void checkAchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(checkAchId.Text))
                SessionInit.Instance.factoryGame.RequestAchievementStatus(ticketNameData, Convert.ToUInt32(checkAchId.Text));

        }

    }
}

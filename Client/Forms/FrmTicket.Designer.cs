namespace Client.Forms
{
    partial class FrmTicket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTicket));
            this.ticketMessage = new System.Windows.Forms.RichTextBox();
            this.assignName = new System.Windows.Forms.TextBox();
            this.ticketComment = new System.Windows.Forms.RichTextBox();
            this.ticketResponse = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.assignButton = new System.Windows.Forms.Button();
            this.commentButton = new System.Windows.Forms.Button();
            this.responseButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.addItemEntry = new System.Windows.Forms.TextBox();
            this.addItemCount = new System.Windows.Forms.TextBox();
            this.removeItemId = new System.Windows.Forms.TextBox();
            this.removeItemCount = new System.Windows.Forms.TextBox();
            this.checkQuestId = new System.Windows.Forms.TextBox();
            this.checkAchId = new System.Windows.Forms.TextBox();
            this.checkItemId = new System.Windows.Forms.TextBox();
            this.completeQuestId = new System.Windows.Forms.TextBox();
            this.completeAchId = new System.Windows.Forms.TextBox();
            this.mailSubject = new System.Windows.Forms.TextBox();
            this.mailBody = new System.Windows.Forms.RichTextBox();
            this.addItemButton = new System.Windows.Forms.Button();
            this.removeItemButton = new System.Windows.Forms.Button();
            this.checkQuestBotton = new System.Windows.Forms.Button();
            this.checkAchButton = new System.Windows.Forms.Button();
            this.checkItemButton = new System.Windows.Forms.Button();
            this.completeQuestButton = new System.Windows.Forms.Button();
            this.completeAchButton = new System.Windows.Forms.Button();
            this.sendMailButton = new System.Windows.Forms.Button();
            this.completeTicketButton = new System.Windows.Forms.Button();
            this.deleteTicketButton = new System.Windows.Forms.Button();
            this.chatLog = new System.Windows.Forms.RichTextBox();
            this.TimerCheckPull = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ticketMessage
            // 
            this.ticketMessage.Location = new System.Drawing.Point(12, 34);
            this.ticketMessage.Name = "ticketMessage";
            this.ticketMessage.Size = new System.Drawing.Size(268, 117);
            this.ticketMessage.TabIndex = 0;
            this.ticketMessage.Text = "";
            // 
            // assignName
            // 
            this.assignName.Location = new System.Drawing.Point(12, 168);
            this.assignName.Name = "assignName";
            this.assignName.Size = new System.Drawing.Size(100, 20);
            this.assignName.TabIndex = 1;
            // 
            // ticketComment
            // 
            this.ticketComment.Location = new System.Drawing.Point(286, 34);
            this.ticketComment.Name = "ticketComment";
            this.ticketComment.Size = new System.Drawing.Size(123, 117);
            this.ticketComment.TabIndex = 2;
            this.ticketComment.Text = "";
            // 
            // ticketResponse
            // 
            this.ticketResponse.Location = new System.Drawing.Point(286, 168);
            this.ticketResponse.Name = "ticketResponse";
            this.ticketResponse.Size = new System.Drawing.Size(123, 122);
            this.ticketResponse.TabIndex = 3;
            this.ticketResponse.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Assignee";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Comment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Response";
            // 
            // assignButton
            // 
            this.assignButton.Location = new System.Drawing.Point(118, 165);
            this.assignButton.Name = "assignButton";
            this.assignButton.Size = new System.Drawing.Size(75, 23);
            this.assignButton.TabIndex = 8;
            this.assignButton.Text = "Apply";
            this.assignButton.UseVisualStyleBackColor = true;
            this.assignButton.Click += new System.EventHandler(this.assignButton_Click);
            // 
            // commentButton
            // 
            this.commentButton.Location = new System.Drawing.Point(415, 32);
            this.commentButton.Name = "commentButton";
            this.commentButton.Size = new System.Drawing.Size(75, 23);
            this.commentButton.TabIndex = 9;
            this.commentButton.Text = "Apply";
            this.commentButton.UseVisualStyleBackColor = true;
            this.commentButton.Click += new System.EventHandler(this.commentButton_Click);
            // 
            // responseButton
            // 
            this.responseButton.Location = new System.Drawing.Point(415, 185);
            this.responseButton.Name = "responseButton";
            this.responseButton.Size = new System.Drawing.Size(75, 23);
            this.responseButton.TabIndex = 10;
            this.responseButton.Text = "Apply";
            this.responseButton.UseVisualStyleBackColor = true;
            this.responseButton.Click += new System.EventHandler(this.responseButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Check Quest";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 339);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Complete Quest";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Check Achievment";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Complete Achievement";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Check Item";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Escalate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(615, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Mail";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 235);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Add Item";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 274);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Remove Item";
            // 
            // addItemEntry
            // 
            this.addItemEntry.Location = new System.Drawing.Point(11, 251);
            this.addItemEntry.Name = "addItemEntry";
            this.addItemEntry.Size = new System.Drawing.Size(100, 20);
            this.addItemEntry.TabIndex = 20;
            // 
            // addItemCount
            // 
            this.addItemCount.Location = new System.Drawing.Point(117, 251);
            this.addItemCount.Name = "addItemCount";
            this.addItemCount.Size = new System.Drawing.Size(100, 20);
            this.addItemCount.TabIndex = 21;
            // 
            // removeItemId
            // 
            this.removeItemId.Location = new System.Drawing.Point(11, 290);
            this.removeItemId.Name = "removeItemId";
            this.removeItemId.Size = new System.Drawing.Size(100, 20);
            this.removeItemId.TabIndex = 22;
            // 
            // removeItemCount
            // 
            this.removeItemCount.Location = new System.Drawing.Point(117, 290);
            this.removeItemCount.Name = "removeItemCount";
            this.removeItemCount.Size = new System.Drawing.Size(100, 20);
            this.removeItemCount.TabIndex = 23;
            // 
            // checkQuestId
            // 
            this.checkQuestId.Location = new System.Drawing.Point(11, 329);
            this.checkQuestId.Name = "checkQuestId";
            this.checkQuestId.Size = new System.Drawing.Size(100, 20);
            this.checkQuestId.TabIndex = 24;
            // 
            // checkAchId
            // 
            this.checkAchId.Location = new System.Drawing.Point(12, 368);
            this.checkAchId.Name = "checkAchId";
            this.checkAchId.Size = new System.Drawing.Size(100, 20);
            this.checkAchId.TabIndex = 25;
            // 
            // checkItemId
            // 
            this.checkItemId.Location = new System.Drawing.Point(299, 316);
            this.checkItemId.Name = "checkItemId";
            this.checkItemId.Size = new System.Drawing.Size(100, 20);
            this.checkItemId.TabIndex = 26;
            // 
            // completeQuestId
            // 
            this.completeQuestId.Location = new System.Drawing.Point(299, 355);
            this.completeQuestId.Name = "completeQuestId";
            this.completeQuestId.Size = new System.Drawing.Size(100, 20);
            this.completeQuestId.TabIndex = 27;
            // 
            // completeAchId
            // 
            this.completeAchId.Location = new System.Drawing.Point(299, 394);
            this.completeAchId.Name = "completeAchId";
            this.completeAchId.Size = new System.Drawing.Size(100, 20);
            this.completeAchId.TabIndex = 28;
            // 
            // mailSubject
            // 
            this.mailSubject.Location = new System.Drawing.Point(618, 34);
            this.mailSubject.Name = "mailSubject";
            this.mailSubject.Size = new System.Drawing.Size(100, 20);
            this.mailSubject.TabIndex = 29;
            // 
            // mailBody
            // 
            this.mailBody.Location = new System.Drawing.Point(618, 60);
            this.mailBody.Name = "mailBody";
            this.mailBody.Size = new System.Drawing.Size(100, 96);
            this.mailBody.TabIndex = 30;
            this.mailBody.Text = "";
            // 
            // addItemButton
            // 
            this.addItemButton.Location = new System.Drawing.Point(223, 248);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(57, 23);
            this.addItemButton.TabIndex = 31;
            this.addItemButton.Text = "Apply";
            this.addItemButton.UseVisualStyleBackColor = true;
            this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            // 
            // removeItemButton
            // 
            this.removeItemButton.Location = new System.Drawing.Point(223, 287);
            this.removeItemButton.Name = "removeItemButton";
            this.removeItemButton.Size = new System.Drawing.Size(57, 23);
            this.removeItemButton.TabIndex = 32;
            this.removeItemButton.Text = "Apply";
            this.removeItemButton.UseVisualStyleBackColor = true;
            this.removeItemButton.Click += new System.EventHandler(this.removeItemButton_Click);
            // 
            // checkQuestBotton
            // 
            this.checkQuestBotton.Location = new System.Drawing.Point(117, 326);
            this.checkQuestBotton.Name = "checkQuestBotton";
            this.checkQuestBotton.Size = new System.Drawing.Size(75, 23);
            this.checkQuestBotton.TabIndex = 33;
            this.checkQuestBotton.Text = "Apply";
            this.checkQuestBotton.UseVisualStyleBackColor = true;
            this.checkQuestBotton.Click += new System.EventHandler(this.checkQuestBotton_Click);
            // 
            // checkAchButton
            // 
            this.checkAchButton.Location = new System.Drawing.Point(118, 365);
            this.checkAchButton.Name = "checkAchButton";
            this.checkAchButton.Size = new System.Drawing.Size(75, 23);
            this.checkAchButton.TabIndex = 34;
            this.checkAchButton.Text = "Apply";
            this.checkAchButton.UseVisualStyleBackColor = true;
            this.checkAchButton.Click += new System.EventHandler(this.checkAchButton_Click);
            // 
            // checkItemButton
            // 
            this.checkItemButton.Location = new System.Drawing.Point(405, 316);
            this.checkItemButton.Name = "checkItemButton";
            this.checkItemButton.Size = new System.Drawing.Size(75, 23);
            this.checkItemButton.TabIndex = 35;
            this.checkItemButton.Text = "Apply";
            this.checkItemButton.UseVisualStyleBackColor = true;
            this.checkItemButton.Click += new System.EventHandler(this.checkItemButton_Click);
            // 
            // completeQuestButton
            // 
            this.completeQuestButton.Location = new System.Drawing.Point(405, 353);
            this.completeQuestButton.Name = "completeQuestButton";
            this.completeQuestButton.Size = new System.Drawing.Size(75, 23);
            this.completeQuestButton.TabIndex = 36;
            this.completeQuestButton.Text = "Apply";
            this.completeQuestButton.UseVisualStyleBackColor = true;
            this.completeQuestButton.Click += new System.EventHandler(this.completeQuestButton_Click);
            // 
            // completeAchButton
            // 
            this.completeAchButton.Location = new System.Drawing.Point(405, 394);
            this.completeAchButton.Name = "completeAchButton";
            this.completeAchButton.Size = new System.Drawing.Size(75, 23);
            this.completeAchButton.TabIndex = 37;
            this.completeAchButton.Text = "Apply";
            this.completeAchButton.UseVisualStyleBackColor = true;
            this.completeAchButton.Click += new System.EventHandler(this.completeAchButton_Click);
            // 
            // sendMailButton
            // 
            this.sendMailButton.Location = new System.Drawing.Point(628, 162);
            this.sendMailButton.Name = "sendMailButton";
            this.sendMailButton.Size = new System.Drawing.Size(75, 23);
            this.sendMailButton.TabIndex = 38;
            this.sendMailButton.Text = "Send";
            this.sendMailButton.UseVisualStyleBackColor = true;
            this.sendMailButton.Click += new System.EventHandler(this.sendMailButton_Click);
            // 
            // completeTicketButton
            // 
            this.completeTicketButton.Location = new System.Drawing.Point(563, 415);
            this.completeTicketButton.Name = "completeTicketButton";
            this.completeTicketButton.Size = new System.Drawing.Size(75, 23);
            this.completeTicketButton.TabIndex = 39;
            this.completeTicketButton.Text = "Complete";
            this.completeTicketButton.UseVisualStyleBackColor = true;
            this.completeTicketButton.Click += new System.EventHandler(this.completeTicketButton_Click);
            // 
            // deleteTicketButton
            // 
            this.deleteTicketButton.Location = new System.Drawing.Point(696, 415);
            this.deleteTicketButton.Name = "deleteTicketButton";
            this.deleteTicketButton.Size = new System.Drawing.Size(75, 23);
            this.deleteTicketButton.TabIndex = 40;
            this.deleteTicketButton.Text = "Delete";
            this.deleteTicketButton.UseVisualStyleBackColor = true;
            this.deleteTicketButton.Click += new System.EventHandler(this.deleteTicketButton_Click);
            // 
            // chatLog
            // 
            this.chatLog.Enabled = false;
            this.chatLog.Location = new System.Drawing.Point(548, 204);
            this.chatLog.Name = "chatLog";
            this.chatLog.Size = new System.Drawing.Size(240, 205);
            this.chatLog.TabIndex = 41;
            this.chatLog.Text = "";
            // 
            // TimerCheckPull
            // 
            this.TimerCheckPull.Interval = 10;
            this.TimerCheckPull.Tick += new System.EventHandler(this.TimerCheckPull_Tick);
            // 
            // FrmTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chatLog);
            this.Controls.Add(this.deleteTicketButton);
            this.Controls.Add(this.completeTicketButton);
            this.Controls.Add(this.sendMailButton);
            this.Controls.Add(this.completeAchButton);
            this.Controls.Add(this.completeQuestButton);
            this.Controls.Add(this.checkItemButton);
            this.Controls.Add(this.checkAchButton);
            this.Controls.Add(this.checkQuestBotton);
            this.Controls.Add(this.removeItemButton);
            this.Controls.Add(this.addItemButton);
            this.Controls.Add(this.mailBody);
            this.Controls.Add(this.mailSubject);
            this.Controls.Add(this.completeAchId);
            this.Controls.Add(this.completeQuestId);
            this.Controls.Add(this.checkItemId);
            this.Controls.Add(this.checkAchId);
            this.Controls.Add(this.checkQuestId);
            this.Controls.Add(this.removeItemCount);
            this.Controls.Add(this.removeItemId);
            this.Controls.Add(this.addItemCount);
            this.Controls.Add(this.addItemEntry);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.responseButton);
            this.Controls.Add(this.commentButton);
            this.Controls.Add(this.assignButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ticketResponse);
            this.Controls.Add(this.ticketComment);
            this.Controls.Add(this.assignName);
            this.Controls.Add(this.ticketMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTicket";
            this.Text = "Ticket";
            this.Load += new System.EventHandler(this.FrmTicket_Load);
            this.Shown += new System.EventHandler(this.FrmTicket_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ticketMessage;
        private System.Windows.Forms.TextBox assignName;
        private System.Windows.Forms.RichTextBox ticketComment;
        private System.Windows.Forms.RichTextBox ticketResponse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button assignButton;
        private System.Windows.Forms.Button commentButton;
        private System.Windows.Forms.Button responseButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox addItemEntry;
        private System.Windows.Forms.TextBox addItemCount;
        private System.Windows.Forms.TextBox removeItemId;
        private System.Windows.Forms.TextBox removeItemCount;
        private System.Windows.Forms.TextBox checkQuestId;
        private System.Windows.Forms.TextBox checkAchId;
        private System.Windows.Forms.TextBox checkItemId;
        private System.Windows.Forms.TextBox completeQuestId;
        private System.Windows.Forms.TextBox completeAchId;
        private System.Windows.Forms.TextBox mailSubject;
        private System.Windows.Forms.RichTextBox mailBody;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.Button removeItemButton;
        private System.Windows.Forms.Button checkQuestBotton;
        private System.Windows.Forms.Button checkAchButton;
        private System.Windows.Forms.Button checkItemButton;
        private System.Windows.Forms.Button completeQuestButton;
        private System.Windows.Forms.Button completeAchButton;
        private System.Windows.Forms.Button sendMailButton;
        private System.Windows.Forms.Button completeTicketButton;
        private System.Windows.Forms.Button deleteTicketButton;
        private System.Windows.Forms.RichTextBox chatLog;
        private System.Windows.Forms.Timer TimerCheckPull;
    }
}
using System.ComponentModel;
using System.Windows.Forms;

namespace BotFarm
{
    partial class CharacterSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterSelection));
            this.lb1 = new System.Windows.Forms.ListBox();
            this.charfound = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb1
            // 
            this.lb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.FormattingEnabled = true;
            this.lb1.Location = new System.Drawing.Point(10, 25);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(259, 147);
            this.lb1.TabIndex = 0;
            this.lb1.SelectedIndexChanged += new System.EventHandler(this.lb1_SelectedIndexChanged);
            // 
            // charfound
            // 
            this.charfound.Enabled = true;
            this.charfound.Tick += new System.EventHandler(this.charfound_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 19);
            this.button1.TabIndex = 2;
            this.button1.Text = "<-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CharacterSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 180);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CharacterSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a character";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CharacterSelection_FormClosing);
            this.Load += new System.EventHandler(this.CharacterSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lb1;
        private Timer charfound;
        private Button button1;
    }
}
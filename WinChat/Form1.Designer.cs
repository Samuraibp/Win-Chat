namespace WinChat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            buttonConnect = new Button();
            groupBox2 = new GroupBox();
            textBoxName = new MaskedTextBox();
            groupBox3 = new GroupBox();
            richTextBoxChat = new RichTextBox();
            Chat = new Label();
            groupBox4 = new GroupBox();
            listBoxUsers = new ListBox();
            label1 = new Label();
            textBoxMessage = new TextBox();
            buttonSend = new Button();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonConnect);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Location = new Point(148, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(347, 58);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(219, 18);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(122, 29);
            buttonConnect.TabIndex = 2;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(3, 57);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(179, 163);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "groupBox2";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(15, 22);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(181, 23);
            textBoxName.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(richTextBoxChat);
            groupBox3.Controls.Add(Chat);
            groupBox3.Location = new Point(148, 84);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(213, 170);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            // 
            // richTextBoxChat
            // 
            richTextBoxChat.Location = new Point(15, 59);
            richTextBoxChat.Name = "richTextBoxChat";
            richTextBoxChat.Size = new Size(181, 98);
            richTextBoxChat.TabIndex = 1;
            richTextBoxChat.Text = "";
            // 
            // Chat
            // 
            Chat.AutoSize = true;
            Chat.Location = new Point(82, 25);
            Chat.Name = "Chat";
            Chat.Size = new Size(32, 15);
            Chat.TabIndex = 0;
            Chat.Text = "Chat";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(listBoxUsers);
            groupBox4.Controls.Add(label1);
            groupBox4.Location = new Point(367, 84);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(128, 170);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            // 
            // listBoxUsers
            // 
            listBoxUsers.FormattingEnabled = true;
            listBoxUsers.Location = new Point(12, 59);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(99, 94);
            listBoxUsers.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 19);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "Users: ";
            // 
            // textBoxMessage
            // 
            textBoxMessage.Location = new Point(148, 264);
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.Size = new Size(347, 23);
            textBoxMessage.TabIndex = 3;
            // 
            // buttonSend
            // 
            buttonSend.Location = new Point(230, 301);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(222, 35);
            buttonSend.TabIndex = 5;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 351);
            Controls.Add(buttonSend);
            Controls.Add(textBoxMessage);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private MaskedTextBox textBoxName;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private RichTextBox richTextBoxChat;
        private Label Chat;
        private ListBox listBoxUsers;
        private TextBox textBoxMessage;
        private Button buttonSend;
        private Button buttonConnect;
    }
}

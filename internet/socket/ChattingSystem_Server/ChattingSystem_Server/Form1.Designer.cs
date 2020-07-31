namespace ChattingSystem_Server
{
    partial class ServerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Stop_button = new System.Windows.Forms.Button();
            this.Start_button = new System.Windows.Forms.Button();
            this.Port_textBox = new System.Windows.Forms.TextBox();
            this.LocalIpAddress_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ReceivedData_TextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sendData_textBox = new System.Windows.Forms.TextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Stop_button);
            this.groupBox1.Controls.Add(this.Start_button);
            this.groupBox1.Controls.Add(this.Port_textBox);
            this.groupBox1.Controls.Add(this.LocalIpAddress_textBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(801, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // Stop_button
            // 
            this.Stop_button.Location = new System.Drawing.Point(687, 18);
            this.Stop_button.Name = "Stop_button";
            this.Stop_button.Size = new System.Drawing.Size(94, 34);
            this.Stop_button.TabIndex = 5;
            this.Stop_button.Text = "Stop";
            this.Stop_button.UseVisualStyleBackColor = true;
            this.Stop_button.Click += new System.EventHandler(this.Stop_button_Click);
            // 
            // Start_button
            // 
            this.Start_button.Location = new System.Drawing.Point(587, 17);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(94, 34);
            this.Start_button.TabIndex = 4;
            this.Start_button.Text = "Start";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // Port_textBox
            // 
            this.Port_textBox.Location = new System.Drawing.Point(425, 24);
            this.Port_textBox.Name = "Port_textBox";
            this.Port_textBox.Size = new System.Drawing.Size(100, 25);
            this.Port_textBox.TabIndex = 3;
            // 
            // LocalIpAddress_textBox
            // 
            this.LocalIpAddress_textBox.Location = new System.Drawing.Point(146, 24);
            this.LocalIpAddress_textBox.Name = "LocalIpAddress_textBox";
            this.LocalIpAddress_textBox.Size = new System.Drawing.Size(191, 25);
            this.LocalIpAddress_textBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local IP Address : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReceivedData_TextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(801, 231);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Received data";
            // 
            // ReceivedData_TextBox
            // 
            this.ReceivedData_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReceivedData_TextBox.Location = new System.Drawing.Point(9, 24);
            this.ReceivedData_TextBox.Multiline = true;
            this.ReceivedData_TextBox.Name = "ReceivedData_TextBox";
            this.ReceivedData_TextBox.ReadOnly = true;
            this.ReceivedData_TextBox.Size = new System.Drawing.Size(786, 201);
            this.ReceivedData_TextBox.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sendData_textBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 322);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(801, 231);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send data";
            // 
            // sendData_textBox
            // 
            this.sendData_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendData_textBox.Location = new System.Drawing.Point(6, 24);
            this.sendData_textBox.Multiline = true;
            this.sendData_textBox.Name = "sendData_textBox";
            this.sendData_textBox.Size = new System.Drawing.Size(786, 201);
            this.sendData_textBox.TabIndex = 1;
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(12, 559);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(801, 34);
            this.send_button.TabIndex = 6;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 603);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.server_form_FormClosing);
            this.Load += new System.EventHandler(this.server_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Stop_button;
        private System.Windows.Forms.Button Start_button;
        private System.Windows.Forms.TextBox Port_textBox;
        private System.Windows.Forms.TextBox LocalIpAddress_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ReceivedData_TextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox sendData_textBox;
        private System.Windows.Forms.Button send_button;
    }
}


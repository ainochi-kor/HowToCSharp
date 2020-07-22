namespace CheckRadio
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.blue_rbtn = new System.Windows.Forms.RadioButton();
            this.green_rtbn = new System.Windows.Forms.RadioButton();
            this.red_rtbn = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.right_rbtn = new System.Windows.Forms.RadioButton();
            this.mid_rbtn = new System.Windows.Forms.RadioButton();
            this.left_rbtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.disable_cbox = new System.Windows.Forms.CheckBox();
            this.hide_cbox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.blue_rbtn);
            this.panel1.Controls.Add(this.green_rtbn);
            this.panel1.Controls.Add(this.red_rtbn);
            this.panel1.Location = new System.Drawing.Point(12, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(122, 115);
            this.panel1.TabIndex = 1;
            // 
            // blue_rbtn
            // 
            this.blue_rbtn.AutoSize = true;
            this.blue_rbtn.Location = new System.Drawing.Point(6, 68);
            this.blue_rbtn.Name = "blue_rbtn";
            this.blue_rbtn.Size = new System.Drawing.Size(58, 19);
            this.blue_rbtn.TabIndex = 2;
            this.blue_rbtn.TabStop = true;
            this.blue_rbtn.Tag = "Blue";
            this.blue_rbtn.Text = "파랑";
            this.blue_rbtn.UseVisualStyleBackColor = true;
            this.blue_rbtn.CheckedChanged += new System.EventHandler(this.ColorChanged);
            // 
            // green_rtbn
            // 
            this.green_rtbn.AutoSize = true;
            this.green_rtbn.Location = new System.Drawing.Point(6, 43);
            this.green_rtbn.Name = "green_rtbn";
            this.green_rtbn.Size = new System.Drawing.Size(58, 19);
            this.green_rtbn.TabIndex = 1;
            this.green_rtbn.TabStop = true;
            this.green_rtbn.Tag = "Green";
            this.green_rtbn.Text = "초록";
            this.green_rtbn.UseVisualStyleBackColor = true;
            this.green_rtbn.CheckedChanged += new System.EventHandler(this.ColorChanged);
            // 
            // red_rtbn
            // 
            this.red_rtbn.AutoSize = true;
            this.red_rtbn.Location = new System.Drawing.Point(6, 18);
            this.red_rtbn.Name = "red_rtbn";
            this.red_rtbn.Size = new System.Drawing.Size(58, 19);
            this.red_rtbn.TabIndex = 0;
            this.red_rtbn.TabStop = true;
            this.red_rtbn.Tag = "Red";
            this.red_rtbn.Text = "빨강";
            this.red_rtbn.UseVisualStyleBackColor = true;
            this.red_rtbn.CheckedChanged += new System.EventHandler(this.ColorChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.right_rbtn);
            this.panel2.Controls.Add(this.mid_rbtn);
            this.panel2.Controls.Add(this.left_rbtn);
            this.panel2.Location = new System.Drawing.Point(147, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(123, 115);
            this.panel2.TabIndex = 2;
            // 
            // right_rbtn
            // 
            this.right_rbtn.AutoSize = true;
            this.right_rbtn.Location = new System.Drawing.Point(10, 68);
            this.right_rbtn.Name = "right_rbtn";
            this.right_rbtn.Size = new System.Drawing.Size(73, 19);
            this.right_rbtn.TabIndex = 2;
            this.right_rbtn.TabStop = true;
            this.right_rbtn.Tag = "64";
            this.right_rbtn.Text = "오른쪽";
            this.right_rbtn.UseVisualStyleBackColor = true;
            this.right_rbtn.CheckedChanged += new System.EventHandler(this.AlignChanged);
            // 
            // mid_rbtn
            // 
            this.mid_rbtn.AutoSize = true;
            this.mid_rbtn.Location = new System.Drawing.Point(10, 43);
            this.mid_rbtn.Name = "mid_rbtn";
            this.mid_rbtn.Size = new System.Drawing.Size(58, 19);
            this.mid_rbtn.TabIndex = 1;
            this.mid_rbtn.TabStop = true;
            this.mid_rbtn.Tag = "32";
            this.mid_rbtn.Text = "중앙";
            this.mid_rbtn.UseVisualStyleBackColor = true;
            this.mid_rbtn.CheckedChanged += new System.EventHandler(this.AlignChanged);
            // 
            // left_rbtn
            // 
            this.left_rbtn.AutoSize = true;
            this.left_rbtn.Location = new System.Drawing.Point(10, 18);
            this.left_rbtn.Name = "left_rbtn";
            this.left_rbtn.Size = new System.Drawing.Size(58, 19);
            this.left_rbtn.TabIndex = 0;
            this.left_rbtn.TabStop = true;
            this.left_rbtn.Tag = "16";
            this.left_rbtn.Text = "왼쪽";
            this.left_rbtn.UseVisualStyleBackColor = true;
            this.left_rbtn.CheckedChanged += new System.EventHandler(this.AlignChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "배경색";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "텍스트 정렬";
            // 
            // disable_cbox
            // 
            this.disable_cbox.AutoSize = true;
            this.disable_cbox.Location = new System.Drawing.Point(12, 225);
            this.disable_cbox.Name = "disable_cbox";
            this.disable_cbox.Size = new System.Drawing.Size(89, 19);
            this.disable_cbox.TabIndex = 5;
            this.disable_cbox.Text = "사용금지";
            this.disable_cbox.UseVisualStyleBackColor = true;
            this.disable_cbox.CheckedChanged += new System.EventHandler(this.disable_cbox_CheckedChanged);
            // 
            // hide_cbox
            // 
            this.hide_cbox.AutoSize = true;
            this.hide_cbox.Location = new System.Drawing.Point(12, 250);
            this.hide_cbox.Name = "hide_cbox";
            this.hide_cbox.Size = new System.Drawing.Size(74, 19);
            this.hide_cbox.TabIndex = 6;
            this.hide_cbox.Text = "숨기기";
            this.hide_cbox.UseVisualStyleBackColor = true;
            this.hide_cbox.CheckedChanged += new System.EventHandler(this.hide_cbox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 302);
            this.Controls.Add(this.hide_cbox);
            this.Controls.Add(this.disable_cbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton blue_rbtn;
        private System.Windows.Forms.RadioButton green_rtbn;
        private System.Windows.Forms.RadioButton red_rtbn;
        private System.Windows.Forms.RadioButton right_rbtn;
        private System.Windows.Forms.RadioButton mid_rbtn;
        private System.Windows.Forms.RadioButton left_rbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox disable_cbox;
        private System.Windows.Forms.CheckBox hide_cbox;

    }
}


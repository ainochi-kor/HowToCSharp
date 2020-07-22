namespace LabelPos
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.x_tbox = new System.Windows.Forms.TextBox();
            this.y_tbox = new System.Windows.Forms.TextBox();
            this.str_tbox = new System.Windows.Forms.TextBox();
            this.Confirm_btn = new System.Windows.Forms.Button();
            this.Cancel_bth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "&X 좌표";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "&Y 좌표";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "문자열(S)";
            // 
            // x_tbox
            // 
            this.x_tbox.Location = new System.Drawing.Point(134, 77);
            this.x_tbox.Name = "x_tbox";
            this.x_tbox.Size = new System.Drawing.Size(100, 25);
            this.x_tbox.TabIndex = 3;
            // 
            // y_tbox
            // 
            this.y_tbox.Location = new System.Drawing.Point(134, 108);
            this.y_tbox.Name = "y_tbox";
            this.y_tbox.Size = new System.Drawing.Size(100, 25);
            this.y_tbox.TabIndex = 4;
            // 
            // str_tbox
            // 
            this.str_tbox.Location = new System.Drawing.Point(134, 139);
            this.str_tbox.Name = "str_tbox";
            this.str_tbox.Size = new System.Drawing.Size(100, 25);
            this.str_tbox.TabIndex = 5;
            // 
            // Confirm_btn
            // 
            this.Confirm_btn.Location = new System.Drawing.Point(54, 201);
            this.Confirm_btn.Name = "Confirm_btn";
            this.Confirm_btn.Size = new System.Drawing.Size(75, 23);
            this.Confirm_btn.TabIndex = 6;
            this.Confirm_btn.Text = "확인";
            this.Confirm_btn.UseVisualStyleBackColor = true;
            this.Confirm_btn.Click += new System.EventHandler(this.Confirm_btn_Click);
            // 
            // Cancel_bth
            // 
            this.Cancel_bth.Location = new System.Drawing.Point(159, 201);
            this.Cancel_bth.Name = "Cancel_bth";
            this.Cancel_bth.Size = new System.Drawing.Size(75, 23);
            this.Cancel_bth.TabIndex = 7;
            this.Cancel_bth.Text = "취소";
            this.Cancel_bth.UseVisualStyleBackColor = true;
            this.Cancel_bth.Click += new System.EventHandler(this.Cancel_bth_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.Cancel_bth);
            this.Controls.Add(this.Confirm_btn);
            this.Controls.Add(this.str_tbox);
            this.Controls.Add(this.y_tbox);
            this.Controls.Add(this.x_tbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox x_tbox;
        private System.Windows.Forms.TextBox y_tbox;
        private System.Windows.Forms.TextBox str_tbox;
        private System.Windows.Forms.Button Confirm_btn;
        private System.Windows.Forms.Button Cancel_bth;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form2 : Form
    {
        public int LabelX
        {
            get { return Convert.ToInt32(x_tbox.Text); }
            set { x_tbox.Text = value.ToString(); }
        }

        public int LabelY
        {
            get { return Convert.ToInt32(y_tbox.Text); }
            set { y_tbox.Text = value.ToString(); }
        }

        public string LabelText
        {
            get { return str_tbox.Text; }
            set { str_tbox.Text = value; }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Confirm_btn_Click(object sender, EventArgs e)
        {
            if(LabelX < 0 || LabelX > 300 || LabelY < 0 || LabelY > 300)
            {
                MessageBox.Show("좌표의 범위는 0~300까지입니다.");
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void Cancel_bth_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void x_tbox_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            int x;

            x = Convert.ToInt32(tb.Text);
            if(x < 0 || x > 300)
            {
                MessageBox.Show("좌표의 범위는 0~300까지입니다.");
                e.Cancel = true;
            }
        }



    }
}

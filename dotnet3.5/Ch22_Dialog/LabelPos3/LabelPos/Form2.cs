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
        public event EventHandler Apply;

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
            if(Apply != null)
            {
                Apply(this,new EventArgs());
            }
        }

        private void Cancel_bth_Click(object sender, EventArgs e)
        {
            Dispose();
        }



    }
}

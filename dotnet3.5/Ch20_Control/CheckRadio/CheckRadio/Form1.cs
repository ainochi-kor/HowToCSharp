using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckRadio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void red_rtbn_CheckedChanged(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
        }

        private void green_rtbn_CheckedChanged(object sender, EventArgs e)
        {
            button1.BackColor = Color.Green;
        }

        private void blue_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            button1.BackColor = Color.Blue;
        }

        private void left_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void mid_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void right_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            button1.TextAlign = ContentAlignment.MiddleRight;
        }

        private void disable_cbox_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void hide_cbox_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked)
            {
                button1.Visible = false;
            }
            else
            {
                button1.Visible = true;
            }

        }
    }
}

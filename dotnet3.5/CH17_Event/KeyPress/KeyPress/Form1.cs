using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyPress
{
    public partial class Form1 : Form
    {
        private string str;
        public Form1()
        {
            InitializeComponent();
            str = "";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(str, Font, Brushes.Black, 10, 10);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //str += e.KeyChar;
            if(e.KeyChar == ' ')
            {
                str ="";
            }
            else
            {
                str += e.KeyChar;   
            }
            
            Invalidate();
        }
    }
}

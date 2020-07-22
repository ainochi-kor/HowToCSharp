using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MouseClick
{
    public partial class Form1 : Form
    {
        private int count = 0;
        public Form1()
        {
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawString("Down", Font, Brushes.Black, e.X, e.Y);
            G.Dispose();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawString("Click", Font, Brushes.Black, e.X, e.Y);
            G.Dispose();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawString("Double Click", Font, Brushes.Black, e.X, e.Y);
            G.Dispose();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            count++;
            Text = count.ToString();
        }





    }
}

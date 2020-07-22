using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class Form1 : Form
    {
        Random R;

        public Form1()
        {
            InitializeComponent();
            R = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics G = CreateGraphics();
            G.DrawRectangle(Pens.Blue, R.Next(400), R.Next(300), 10, 10);
            G.Dispose();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                G.DrawEllipse(Pens.Red, e.X, e.Y, 10, 10);
                G.Dispose();
            }
        }

    }
}

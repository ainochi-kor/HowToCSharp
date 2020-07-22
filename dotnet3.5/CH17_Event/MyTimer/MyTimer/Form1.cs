using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyTimer
{
    public partial class Form1 : Form
    {
        private string Time;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(Time, this.Font, Brushes.Black, 10, 10);
        }

        private void Form1_Timer(object sender, System.EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToString();
            Invalidate();
        }
    }
}

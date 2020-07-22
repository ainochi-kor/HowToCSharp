using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MouseDown
{
    public partial class Form1 : Form
    {
        private ArrayList ar;

        public Form1()
        {
            ar = new ArrayList();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ar.Add(new Point(e.X, e.Y));
                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(Point p in ar)
            {
                e.Graphics.DrawEllipse(Pens.Red, p.X, p.Y, 10, 10);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Layout
{
    public partial class Form1 : Form
    {
        private string StrSize;
        private Rectangle Rect;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
            Rect = ClientRectangle;
            Rect.Inflate(-10, -10);
            StrSize = string.Format(" 폭 = {0}, 높이 = {1}", Rect.Width, Rect.Height);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Pens.Blue, Rect);
            e.Graphics.DrawString(StrSize, Font, Brushes.Black, 0, 0);
        }

    }
}

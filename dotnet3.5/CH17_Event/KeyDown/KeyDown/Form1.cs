using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyDown
{
    public partial class Form1 : Form
    {
        private int x, y;
        public Form1()
        {
            InitializeComponent();
            x = 10;
            y = 10;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Black, x - 8, y - 8, 16, 16);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    x -= 10;
                    Invalidate();
                    break;
                case Keys.Right:
                    x += 10;
                    Invalidate();
                    break;
                case Keys.Up:
                    y -= 10;
                    Invalidate();
                    break;
                case Keys.Down:
                    y += 10;
                    Invalidate();
                    break;
            }
        }
    }
}

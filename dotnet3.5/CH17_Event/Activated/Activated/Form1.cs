using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Activated
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            BackColor = Color.GreenYellow;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            BackColor = Color.Gray;
        }
    }
}

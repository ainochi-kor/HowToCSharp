using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizardFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle R = new Rectangle(10, 20, 30, 40);
            R.Offset(5, 5);
            R.Inflate(3, 3);
            
                
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Pens.Black, 10, 10, 200, 200);
            if(MessageBox.Show("질문","질문",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("에러", "에러", MessageBoxButtons.OK);
            }
        }

    }
}

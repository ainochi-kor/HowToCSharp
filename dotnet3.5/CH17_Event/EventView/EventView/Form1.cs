using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventView
{
    public partial class Form1 : Form
    {
        private int count = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void AddEvent(string Name)
        {
            if(checkBox1.Checked == false)
            {
                textBox1.Text += (count + " : " + Name + "\r\n");
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
                count++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddEvent("Load");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            count = 1;
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
            AddEvent("Layout");
        }
    }
}

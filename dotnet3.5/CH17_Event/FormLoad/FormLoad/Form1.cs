using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormLoad
{
    public partial class Form1 : Form
    {
        private bool NetworkConnect;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkConnect = true;
            Text = "네트워크에 연결되었습니다.";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetworkConnect = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("정말 종료하시겠습니까?","질문", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }


    }
}

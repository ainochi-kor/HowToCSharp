using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("이 프로그램은 셰어웨어 입니다. 정식 등록 후 사용해 주십시오.", "돈 내고 써");
        }
    }
}

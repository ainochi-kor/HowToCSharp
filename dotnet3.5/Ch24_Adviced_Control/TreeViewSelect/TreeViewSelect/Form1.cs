using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeViewSelect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Text == "대한민국")
            {
                label1.Text = "행선지를 선택하십시오.";
            }
            else
            {
                label1.Text = "부산에서 " + e.Node.Text + " (으)로 가는 표를 예매합니다.";
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if(e.Node.Text == "당진군")
            {
                label1.Text = "교통편이 존재하지 않습니다.";
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.CheckBoxes = true;
            treeView1.Nodes.Add("대한민국");
            treeView1.Nodes[0].Nodes.Add("서울특별시");
            treeView1.Nodes[0].Nodes.Add("경기도");
            treeView1.Nodes[0].Nodes.Add("충청도");
            treeView1.Nodes[0].Nodes[0].Nodes.Add("은평구");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("수원시");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("용인시");
            treeView1.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("죽전동");
            treeView1.Nodes[0].Nodes[2].Nodes.Add("당진군");
        }

    }
}

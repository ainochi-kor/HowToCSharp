using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListViewData2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("대한민국");
            treeView1.Nodes[0].Nodes.Add("서울특별시");
            treeView1.Nodes[0].Nodes.Add("경기도");
            treeView1.Nodes[0].Nodes.Add("충청도");
            treeView1.Nodes[0].Nodes[0].Nodes.Add("은평구");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("수원시");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("용인시");
            treeView1.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("죽전동");
            treeView1.Nodes[0].Nodes[2].Nodes.Add("당진구");

        }
    }
}

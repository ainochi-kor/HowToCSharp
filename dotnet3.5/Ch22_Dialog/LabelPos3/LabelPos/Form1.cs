using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPos
{
    public partial class Form1 : Form
    {
        private Form2 dlg;

        public Form1()
        {
            InitializeComponent();
        }
        private void OnApply(object sender, EventArgs e)
        {
            label1.Left = dlg.LabelX;
            label1.Top = dlg.LabelY;
            label1.Text = dlg.LabelText;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(dlg == null || dlg.IsDisposed)
            {
                dlg = new Form2();
                dlg.Owner = this;
                dlg.Apply += new EventHandler(OnApply);
                dlg.LabelX = label1.Left;
                dlg.LabelY = label1.Top;
                dlg.LabelText = label1.Text;
                dlg.Show();

            }
        }
        
    }
}

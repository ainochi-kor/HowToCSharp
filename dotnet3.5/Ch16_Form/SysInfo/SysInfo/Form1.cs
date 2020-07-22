using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SysInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("BootMode : " + (SystemInformation.BootMode == BootMode.Normal ? "정상부팅" : "안전모드"));
            listBox1.Items.Add("Border3DSize : " + SystemInformation.Border3DSize.ToString());
            listBox1.Items.Add("CaptionHeight : " + SystemInformation.CaptionHeight);
            listBox1.Items.Add("ComputerName : " + SystemInformation.ComputerName);
            listBox1.Items.Add("DoulbeClickSize : " + SystemInformation.DoubleClickSize.ToString());
            listBox1.Items.Add("DoubleClickTime : " + SystemInformation.DoubleClickTime);
            listBox1.Items.Add("DragFullWindows : " + SystemInformation.DragFullWindows);
            listBox1.Items.Add("DragSize : " + SystemInformation.DragSize.ToString());
            listBox1.Items.Add("IconSize : " + SystemInformation.IconSize.ToString());
            listBox1.Items.Add("KeyboardDelay : " + SystemInformation.KeyboardDelay);
            listBox1.Items.Add("KeyboardSpeed : " + SystemInformation.KeyboardSpeed);
            listBox1.Items.Add("UserName : " + SystemInformation.UserName);
            listBox1.Items.Add("WorkingArea : " + SystemInformation.WorkingArea.ToString());

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

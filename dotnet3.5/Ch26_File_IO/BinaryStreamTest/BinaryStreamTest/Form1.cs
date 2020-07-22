using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BinaryStreamTest
{
    public partial class Form1 : Form
    {
        Human Kim = new Human("김상형", 28);
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(
                @"C:\Users\MS.Kang\Desktop\Strdy\kim.bin", FileMode.Create,FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            Kim.Write(bw);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = " 파일을 읽는 중";
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(@"C:\Users\MS.Kang\Desktop\Strdy\kim.bin",
                FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Kim = Human.Read(br);
            fs.Close();
            label1.Text = Kim.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Kim.ToString();
        }
    }
    
    class Human
    {
        private string Name;
        private int Age;
        private float Temp;
        public Human(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
            this.Temp = 1.23f;
        }
        public override string ToString()
        {
            Temp += 1;
            return "이름 : " + Name + ", 나이 : " + Age;
        }
        public void Write(BinaryWriter bw)
        {
            bw.Write(Name);
            bw.Write(Age);
        }
        public static Human Read(BinaryReader br)
        {
            return new Human(br.ReadString(), br.ReadInt32());
        }
    }
}

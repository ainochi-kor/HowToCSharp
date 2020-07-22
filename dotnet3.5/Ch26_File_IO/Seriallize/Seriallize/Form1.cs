using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;

namespace Seriallize
{
    public partial class Form1 : Form
    {
        Human Kim = new Human("김상형", 28);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Kim.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"C:\Users\MS.Kang\Desktop\Strdy\Kim2.bin",
                FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, Kim);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "파일을 읽는 중";
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(@"C:\Users\MS.Kang\Desktop\Strdy\Kim2.bin",
                FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            Kim = (Human)bf.Deserialize(fs);
            fs.Close();
            label1.Text = Kim.ToString();
        }  
    }
    [Serializable]
    class Human
    {
        private string Name;
        private int Age;
        [NonSerialized]
        private float Temp;
        public Human(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
            Temp = 1.23f;
        }

        public override string ToString()
        {
            Temp += 1;
            return "이름 : " + Name + " 나이 : " + Age;
        }
    }
}

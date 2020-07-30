using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MakeTable
{
    public partial class Form1 : Form
    {
        private DataTable tblPeople;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable tblPeople = MakePeopleTable();
            dataGridView1.DataSource = tblPeople;
        }

        private DataTable MakePeopleTable()
        {
            DataTable tblPeople = new DataTable("tblPeople");

            DataColumn col;
            DataRow row;

            //열 등록
            col = new DataColumn("Name", typeof(String));
            col.MaxLength = 10;
            col.AllowDBNull = false;
            col.Unique = true;
            tblPeople.Columns.Add(col);

            tblPeople.PrimaryKey = new DataColumn[] { col };

            col = new DataColumn("Age", typeof(Int32));
            col.AllowDBNull = false;
            tblPeople.Columns.Add(col);

            col = new DataColumn("Male", typeof(bool));
            col.AllowDBNull = false;
            tblPeople.Columns.Add(col);

            //행 삽입
            row = tblPeople.NewRow();
            row["Name"] = "정우성";
            row["Age"] = 36;
            row["Male"] = true;
            tblPeople.Rows.Add(row);

            row = tblPeople.NewRow();
            row["Name"] = "고소영";
            row["Age"] = 32;
            row["Male"] = false;
            tblPeople.Rows.Add(row);

            row = tblPeople.NewRow();
            row["Name"] = "배용준";
            row["Age"] = 37;
            row["Male"] = true;
            tblPeople.Rows.Add(row);

            row = tblPeople.NewRow();
            row["Name"] = "김태희";
            row["Age"] = 29;
            row["Male"] = false;
            tblPeople.Rows.Add(row);

            tblPeople.AcceptChanges();
            return tblPeople;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            DataRow Result = tblPeople.Rows.Find(Name);
            if (Result == null)
            {
                MessageBox.Show("테이블에 존재하지 않는 사람입니다.");
            }
            else
            {
                MessageBox.Show(Name + "의 나이는 " + Result["Age"] + "살입니다.");
            }
        }

        private void DisplayResult(DataRow[] Result)
        {
            listBox1.Items.Clear();
            foreach(DataRow R in Result)
            {
                listBox1.Items.Add(R["Name"]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Filter = "Age >= " + textBox2.Text;
            DataRow[] Result = tblPeople.Select(Filter);

            DisplayResult(Result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow[] Result = tblPeople.Select("", "Name");
            DisplayResult(Result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataRow[] Result = tblPeople.Select("", "Name DESC");
            DisplayResult(Result);
        }
        
       

        
    }
}

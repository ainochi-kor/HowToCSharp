using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReadWriteXml
{
    public partial class Form1 : Form
    {
        private DataSet DbADOTest;
        private DataTable tblPeople, tblSale;
        private DataRelation RelBuy;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DbADOTest = new DataSet("DbADOTest");

            tblPeople = MakePeopleTable();
            tblSale = MakeSaleTable();
            DbADOTest.Tables.Add(tblPeople);
            DbADOTest.Tables.Add(tblSale);

            RelBuy = new DataRelation("Buy",
                DbADOTest.Tables["tblPeople"].Columns["Name"],
                DbADOTest.Tables["tblSale"].Columns["Customer"]);
            DbADOTest.Relations.Add(RelBuy);

            //ForeignKeyConstraint fk= (ForeignKeyConstraint)tblSale.Constraints["Buy"];
            //fk.DeleteRule = Rule.None;

            dataGridView1.DataSource = DbADOTest.Tables["tblPeople"];
            dataGridView2.DataSource = DbADOTest.Tables["tblSale"];
        }

        private DataTable MakePeopleTable()
        {
            DataTable tblPeople = new DataTable("tblPeople");

            DataColumn col;
            DataRow row;

            // 열 등록
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

            // 행 삽입
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

        private DataTable MakeSaleTable()
        {
            DataTable tblSale = new DataTable("tblSale");

            DataColumn col;
            DataRow row;

            // 열 등록
            col = new DataColumn("OrderNo", typeof(Int32));
            col.AllowDBNull = false;
            col.Unique = true;
            col.AutoIncrement = true;
            col.ReadOnly = true;
            tblSale.Columns.Add(col);

            tblSale.PrimaryKey = new DataColumn[] { col };

            col = new DataColumn("Customer", typeof(String));
            col.MaxLength = 10;
            col.AllowDBNull = false;
            tblSale.Columns.Add(col);

            col = new DataColumn("Item", typeof(String));
            col.MaxLength = 20;
            col.AllowDBNull = false;
            tblSale.Columns.Add(col);

            col = new DataColumn("ODate", typeof(DateTime));
            col.AllowDBNull = false;
            tblSale.Columns.Add(col);

            // 행 삽입
            row = tblSale.NewRow();
            row["Customer"] = "정우성";
            row["Item"] = "면도기";
            row["ODate"] = new DateTime(2008, 1, 1);
            tblSale.Rows.Add(row);

            row = tblSale.NewRow();
            row["Customer"] = "고소영";
            row["Item"] = "화장품";
            row["ODate"] = new DateTime(2008, 1, 2);
            tblSale.Rows.Add(row);

            row = tblSale.NewRow();
            row["Customer"] = "김태희";
            row["Item"] = "핸드폰";
            row["ODate"] = new DateTime(2008, 1, 3);
            tblSale.Rows.Add(row);

            row = tblSale.NewRow();
            row["Customer"] = "김태희";
            row["Item"] = "휘발유";
            row["ODate"] = new DateTime(2008, 1, 4);
            tblSale.Rows.Add(row);

            tblSale.AcceptChanges();
            return tblSale;
        }

        private void btnWriteXmlTable_Click(object sender, EventArgs e)
        {
            tblPeople.WriteXml("People.xml", XmlWriteMode.WriteSchema);
            MessageBox.Show("XML 파일로 저장했습니다.");
        }

        private void btnReadXmlTable_Click(object sender, EventArgs e)
        {
            DataTable tblPeople2 = new DataTable();
            try
            {
                tblPeople2.ReadXml("People.xml");
                dataGridView1.DataSource = tblPeople2;
            }
            catch
            {
                MessageBox.Show("XML 파일이 없습니다.");
            }
        }

        private void btnWriteXmlDataSet_Click(object sender, EventArgs e)
        {
            DbADOTest.WriteXml("DbADOTest.xml", XmlWriteMode.WriteSchema);
            MessageBox.Show("XML 파일로 저장했습니다.");
        }

        private void btnReadXmlDataSet_Click(object sender, EventArgs e)
        {
            DataSet DbADOTest2 = new DataSet();
            try
            {
                DbADOTest2.ReadXml("DbADOTest.xml");
                dataGridView1.DataSource = DbADOTest2.Tables["tblPeople"];
                dataGridView2.DataSource = DbADOTest2.Tables["tblSale"];
            }
            catch
            {
                MessageBox.Show("XML 파일이 없습니다.");
            }
        }
    }
}
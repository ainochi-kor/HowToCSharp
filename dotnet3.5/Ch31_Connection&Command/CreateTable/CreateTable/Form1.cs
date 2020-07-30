using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data.OracleClient;

namespace CreateTable
{
    public partial class Form1 : Form
    {
        DML dml = new DML();

        public Form1()
        {
            InitializeComponent();
        }
        private Tuple<string,string,string,string> info()
        {
            string bookid = textBox1.Text.Trim();
            string bookname = textBox2.Text.Trim();
            string publisher = textBox3.Text.Trim();
            string price = textBox4.Text.Trim();

            return new Tuple<string, string, string, string> (bookid, bookname, publisher, price);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            byte num = 1;
            Tuple<string, string, string, string> result;
            result = info();

            dml.Oracle_DML(result.Item1, result.Item2, result.Item3, result.Item4, num);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tuple<string, string, string, string> result;
            result = info();
            byte num = 2;
            dml.Oracle_DML(result.Item1, result.Item2, result.Item3, result.Item4, num);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tuple<string, string, string, string> result;
            result = info();
            byte num = 3;
            dml.Oracle_DML(result.Item1, result.Item2, result.Item3, result.Item4, num);
        } 

        private void button4_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection("User Id = madang ; Password = madang; Data Source = orcl");
            con.Open();
            string cmdQuery = "select * from book order by bookid";
            OracleCommand cmd = new OracleCommand(cmdQuery);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();

            DataTable datatable = new DataTable();
            datatable.Load(reader);
            dataGridView1.DataSource = datatable;

            cmd.Dispose();
            con.Dispose();
        }
    }

    class DML
    {
        const string strcon = "User Id = madang ; Password = madang; Data Source = orcl";
        OracleParameter[] prm = new OracleParameter[4];
        public void Oracle_DML(string bookid, string bookname, string publisher, string price,byte button)
        {
            OracleConnection con = new OracleConnection(strcon);
;
            con.Open();
            //cmd = con.CreateCommand();

            try
            {

                if(button == 1)
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand cmd;

                    cmd = new OracleCommand("INSERT INTO Dept (DeptNo, DName) " +
                                         "VALUES (:pDeptNo, :pDName)", con);
                    cmd.Parameters.Add("bookid", OracleType.Number, 10, bookid);
                    cmd.Parameters.Add("bookname", OracleType.NVarChar, 20, bookname);
                    cmd.Parameters.Add("publisher", OracleType.NVarChar, 20, publisher);
                    cmd.Parameters.Add("price", OracleType.Number, 10, price);
                    
                    da.InsertCommand = cmd;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success");
                    cmd.Dispose();
                }/*
                else if(button == 2)
                {
                    string upt_query = "";
                    upt_query += "UPDATE book SET ";
                    if (string.IsNullOrWhiteSpace(bookname) == false) upt_query += "bookname = :2,";
                    if (string.IsNullOrWhiteSpace(publisher) == false) upt_query += " publisher = :3,";
                    if (string.IsNullOrWhiteSpace(price) == false) upt_query += " price = :4,";
                    upt_query += upt_query.TrimEnd(',');
                    cmd.CommandText = upt_query + " WHERE bookid = :1";
                    
                    da.UpdateCommand = cmd;  

                    MessageBox.Show(cmd.CommandText);
                }

                else if(button == 3)
                {
                    cmd.CommandText = "DELETE FROM book WHERE bookid = :1, bookname = :2, publisher = :3, price = :4";
                    MessageBox.Show(cmd.CommandText);
                }

                
                
                

                MessageBox.Show("Success");
                */
                
            }
            catch (OracleException e1)
            {
                MessageBox.Show("Error: " + e1.Message + "\r\n" + e1);
            }
            catch (ArgumentException e2)
            {
                MessageBox.Show("Error: " + e2.Message + "\r\n" + e2);
            }
            finally
            {
                con.Close();
                
                con.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace DatatableTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strcon = "User Id = madang ; Password = madang; Data Source = orcl";
            OracleConnection con = new OracleConnection(strcon);
            con.Open(); 
            string cmdQuery = "select bookname, price from book";
            OracleCommand cmd = new OracleCommand(cmdQuery);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            dataGridView1.DataSource = datatable;
        }
    }
}

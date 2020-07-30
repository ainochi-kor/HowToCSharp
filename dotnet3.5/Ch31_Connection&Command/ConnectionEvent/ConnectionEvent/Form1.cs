using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ConnectionEvent
{
    public partial class Form1 : Form
    {
        string constr = "DATA SOURCE = orcl; User Id = madang; Password = madang";
        OracleConnection conn;
        OracleCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(constr);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string SQL = "SELECT * FROM book";
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = new OracleCommand(SQL, conn);
            ad.Fill(ds, "test");

            listBox1.Items.Add(ds.Tables["test"].Rows.ToString());

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        
    }
}

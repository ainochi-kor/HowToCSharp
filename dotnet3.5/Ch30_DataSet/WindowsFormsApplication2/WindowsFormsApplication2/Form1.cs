using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        string connect_info = String.Format("DATA SOURCE = orcl; User Id = madang; Password = madang");
        OracleConnection conn;
        OracleCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(connect_info);
            cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string SQL = "SELECT * FROM book";
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = new OracleCommand(SQL, conn);
            ad.Fill(ds, "test");

            MessageBox.Show(ds.Tables["test"].Rows[0][0].ToString());

            conn.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        string strcon = "User Id = madang ; Password = madang; Data Source = orcl";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(strcon);
            con.Open();
            string cmdQuery = "select bookname, price from book";
            OracleCommand cmd = new OracleCommand(cmdQuery);
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

               textBox1.Text += ("BookName : " + reader.GetString(0) + " \t " +
                    "Price : " + reader.GetDecimal(1) + "\r\n");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace asd
{
    public partial class Form1 : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool returnValue = false;

            try
            {
                returnValue = ConnectionDB("127.0.0.1", "orcl", "MADANG", "madang");
            }
            catch (Exception e1)
            {
                MessageBox.Show("DB connection fail.\n {e1.Message}", "Error");
            }
            if (returnValue == false)
            {
                return;
            }
        }
            private bool ConnectionDB(string dbIp, string dbName, string dbId, string dbPw)
              {
            bool retValue = false;
            try
            {
                pgOraConn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
                //pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST = 127.0.0.1)(PORT = 1521))(CONNECT_DATA=(SERVER DEDICATED)(SERVICE_NAME=orcl))); User ID={dbId};Password={dbPw};Connection Timeout=30;");
                pgOraConn.Open();
                pgOraCmd = pgOraConn.CreateCommand();
                MessageBox.Show("Success DB connecion.", "Information");

                retValue = true;
            }
            catch (Exception e)
            {
                retValue = false;
                MessageBox.Show("DB connection fail.\n {e.Message}", "Error");
            }

            return retValue;
        }
    }
}

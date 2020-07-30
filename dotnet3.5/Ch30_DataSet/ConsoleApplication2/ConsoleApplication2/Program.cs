using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connect_info = "DATA SOURCE=orcl; User Id=madang; Password=madang";
            OracleConnection conn;
            OracleCommand cmd;

            conn = new OracleConnection(connect_info);
            cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            string SQL = "SELECT * FROM book";
            OracleDataAdapter ad = new OracleDataAdapter();
            ad.SelectCommand = new OracleCommand(SQL, conn);
            ad.Fill(ds, "test");

            Console.WriteLine(ds.Tables["test"].Rows[0][0].ToString());

            conn.Close();
        }
    }
}

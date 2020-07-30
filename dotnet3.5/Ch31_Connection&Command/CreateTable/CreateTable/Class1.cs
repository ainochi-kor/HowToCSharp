using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace CreateTable
{

    class Oracle_DML
    {
        string strcon = "User Id = madang ; Password = madang; Data Source = orcl";
        OracleConnection connection;
        OracleDataAdapter da = new OracleDataAdapter();
        OracleCommand cmd;

        public void Insert_data(string bookid, string bookname, string publisher, string price)
        {

            OracleConnection con = new OracleConnection(strcon);
            con.Open();
            OracleParameter[] prm = new OracleParameter[4];

            OracleCommand cmd = con.CreateCommand();

            try
            {
                prm[0] = cmd.Parameters.Add("bookid", OracleDbType.Decimal, bookid, ParameterDirection.Input);
                prm[1] = cmd.Parameters.Add("bookname", OracleDbType.Varchar2, bookname, ParameterDirection.Input);
                prm[2] = cmd.Parameters.Add("publisher", OracleDbType.Varchar2, publisher, ParameterDirection.Input);
                prm[3] = cmd.Parameters.Add("price", OracleDbType.Decimal, price, ParameterDirection.Input);

                cmd.CommandText = "INSERT INTO book(bookid, bookname, publisher, price)" +
                    "VALUES(:1, :2, :3, :4)";

                cmd.ExecuteNonQuery();

                MessageBox.Show("Client has been added");
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
                cmd.Dispose();
                con.Dispose();
            }

        }
       
    }
}

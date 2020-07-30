using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace ExecuteNonQuerytest
{
    class Program
    {
        static void Main(string[] args)
        {
            string bookid = "32";
            string bookname = "123";
            string publisher = "123";
            string price = "123";

            Insert_data(bookid, bookname, publisher, price);
        }

        static void Insert_data(string bookid, string bookname, string publisher, string price)
        {
            string strcon = "User Id = madang ; Password = madang; Data Source = orcl";
            OracleConnection connection;
            var query = "INSERT INTO book (bookid, bookname, publisher, price) " +
                    "VALUES (:bookid, :bookname, :publisher, :price)";
            connection = new OracleConnection(strcon);
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                try
                {
                    //query,connection
                    command.Parameters.Add("bookid", bookid);
                    command.Parameters.Add("bookname", bookname);
                    command.Parameters.Add("publisher", publisher);
                    command.Parameters.Add("price", price);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();


                }
                catch (Exception e)
                {
                    Console.WriteLine("오류 : \r\n" + e);
                }
                finally
                {
                    string cmdQuery = "select * from book";
                    command.CommandText = cmdQuery;
                    command.CommandType = CommandType.Text;
                    OracleDataReader reader = command.ExecuteReader();

                    command.Dispose();
                    connection.Close();
                }
            }

        }
    }
}

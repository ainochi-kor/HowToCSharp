using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "User Id = madang; Password = madang; Data Source=orcl";
            OracleConnection con = new OracleConnection(constr);
            con.Open();

            string cmdQuery = "select bookname, price from book";

            OracleCommand cmd = new OracleCommand(cmdQuery);
            

            Console.WriteLine(cmd.Clone());
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            OracleDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("reader.Depth : " + reader.Depth);
            Console.WriteLine("reader.FieldCount : " + reader.FieldCount);
            Console.WriteLine("reader.HasRows : " + reader.HasRows);

            while(reader.Read())
            {
                
                Console.WriteLine("BookName : " + reader.GetString(0) + " , " +
                    "Price : " + reader.GetDecimal(1));
            }
            Console.WriteLine("end : " + reader.Read());

            reader.Dispose();
            cmd.Dispose();
            con.Dispose();
        }
    }
}

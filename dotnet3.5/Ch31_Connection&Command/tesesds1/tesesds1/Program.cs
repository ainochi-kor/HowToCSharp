using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace tesesds1
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = "User Id=madang;Password=madang;Data Source=orcl";
            OracleConnection con = new OracleConnection(constr);
            con.Open();

            OracleParameter[] prm = new OracleParameter[4];

            OracleCommand cmd = con.CreateCommand();

            prm[0] = cmd.Parameters.Add("bookid", OracleDbType.Decimal, 30, ParameterDirection.Input);
            prm[1] = cmd.Parameters.Add("bookname", OracleDbType.Varchar2, "집에가고싶어", ParameterDirection.Input);
            prm[2] = cmd.Parameters.Add("publisher", OracleDbType.Varchar2, "강민석", ParameterDirection.Input);
            prm[3] = cmd.Parameters.Add("price", OracleDbType.Decimal, 3000, ParameterDirection.Input);
            cmd.CommandText = "insert into book(bookid, bookname, publisher, price) values(:1, :2, :3, :4)";

            cmd.ExecuteNonQuery();

        }
    }
}

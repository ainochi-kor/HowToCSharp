using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace ConsoleApplication1
{
    class Program
    {
        private static OracleConnection Con = null;
        public static string DBConnString
        {
            get;
            private set;
        }

        public static bool bDBConnCheck = false;
        
        private static int errorBoxCount = 0;

        /// <summary>
        /// 생성자
        /// </summary>
        public DBHelper() {}

        public static OracleConnection DBConn
        {
            get
            {
                if(!ConnectToDB())
                {
                    return null;
                }
                return conn;
            }
        }

        static void Main(string[] args)
        {
            
            

        }
    }
}

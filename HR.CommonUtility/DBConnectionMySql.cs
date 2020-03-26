using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HR.CommonUtility
{
    public class DBConnectionMySql : IDBConnection
    {
        //public string Connect()
        //{
        //    return "server=172.16.2.129;port=3306;database=hr_proj;user=root;password=root2233;SslMode=None;";
        //}

        //public int ConnectionTimeout()
        //{
        //    return 30;
        //}

        DbContextOptionsBuilder dbContextOptionsBuilder;

        public string ConnectionString { get; private set; }

        //public string ConnectionString { get; set; }

        public string Connect()
        {
            ConnectionString = "server=172.16.2.129;port=3306;database=hr_proj;user=root;password=root2233;SslMode=None;";
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseMySQL(ConnectionString);
            }
            return ConnectionString;
        }

        public int ConnectionTimeout()
        {
            return 30;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.CommonUtility
{
    public class MySqlConnection : IDBConnection
    {
        DbContextOptionsBuilder dbContextOptionsBuilder;
        public string strConnectionString { get; set; }

        public string Connect()
        {
            strConnectionString = "server=172.16.2.129;port=3306;database=hr_proj;user=root;password=root2233;SslMode=None;";
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseMySQL(strConnectionString);
            }
            return strConnectionString;
        }

        public string ConnectionString()
        {
            return strConnectionString;
        }
    }
}

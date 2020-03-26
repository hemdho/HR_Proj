using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.CommonUtility
{
    public class DBConnectionOracle : IDBConnectionDemo
    {
        public string Connect()
        {
            return "server=172.16.2.129;port=3306;database=hr_proj;user=root;password=root2233;SslMode=None;";
        }

        public int ConnectionTimeout()
        {
            return 30;
        }
    }
}

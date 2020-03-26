using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.DAL
{
    public class SqlServerConnection : DbContext,IDBConnection
    {
        public DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
        public string ConnectionString { get; set; }

        public SqlServerConnection(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            this.dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public DbContextOptionsBuilder GetConn()
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionString);
            return dbContextOptionsBuilder;
        }

        //public string Connect()
        //{
        //    return ConnectionString;
        //}

        public DbContextOptionsBuilder Connect()
        {
            ConnectionString = "server=172.16.2.129;port=3306;database=hr_proj;user=root;password=root2233;SslMode=None;";
            dbContextOptionsBuilder.UseMySQL(ConnectionString);
            return dbContextOptionsBuilder;
        }
    }
}

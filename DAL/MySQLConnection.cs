using Microsoft.EntityFrameworkCore;

namespace HR.WebApi.DAL
{
    public class MySqlConnection : DbContext
    {
        public DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
        public string ConnectionString { get; set; }

        public MySqlConnection(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            this.dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public DbContextOptionsBuilder GetConn()
        {
            dbContextOptionsBuilder.UseMySQL(ConnectionString);
            return dbContextOptionsBuilder;
        }
    }
}

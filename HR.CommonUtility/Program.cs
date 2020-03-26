using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Unity;

namespace HR.CommonUtility
{
    public class Program
    {
        public static UnityContainer unityContainer = new UnityContainer();
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //RegisterInterfaces();
        }

        private static void RegisterInterfaces()
        {
            //implement this way
            //unityContainer.RegisterType<IDBConnection, MySqlConnection>("MySql");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace HR.CommonUtility
{
    public class DBConnection
    {
        IDBConnection dBConnetion;
        UnityContainer unityContainer = new UnityContainer();
        public string returnValue = string.Empty;
        public string ConnectionString = string.Empty;

        public DBConnection(string connectionType)
        {
            RegisterInterface();
            //dBConnetion = unityContainer.Resolve<IDBConnection>(connectionType);
            dBConnetion = unityContainer.Resolve<IDBConnection>(connectionType);
            returnValue = dBConnetion.Connect();
        }

        public int GetTimeout()
        {
            return dBConnetion.ConnectionTimeout();
        }

        public void RegisterInterface()
        {
            //unityContainer.RegisterType<IDBConnectionDemo, DBConnectionSqlServer>("SqlServer");
            //unityContainer.RegisterType<IDBConnectionDemo, DBConnectionOracle>("Oracle");
            unityContainer.RegisterType<IDBConnection, DBConnectionMySql>("MySql");
        }
    }
}

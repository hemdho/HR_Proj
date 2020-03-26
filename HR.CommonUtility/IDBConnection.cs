using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.CommonUtility
{
    public interface IDBConnection
    {
        string Connect();

        int ConnectionTimeout();

    }
}

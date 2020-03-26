using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Interfaces
{
    public interface IDatabaseConnection
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}

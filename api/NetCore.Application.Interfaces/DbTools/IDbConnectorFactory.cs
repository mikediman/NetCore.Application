using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetCore.Application.Interfaces
{
    public interface IDbConnectorFactory
    {
        IDbConnection Get(string name);
    }
}

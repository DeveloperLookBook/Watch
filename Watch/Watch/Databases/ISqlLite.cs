using System;
using System.Collections.Generic;
using System.Text;

namespace Watch.Databases
{
    public interface ISqlLite
    {
        string GetDatabasePath(string databaseName);
    }
}

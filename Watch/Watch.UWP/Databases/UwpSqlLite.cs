using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watch.Databases;
using Watch.UWP.Databases;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpSqlLite))]
namespace Watch.UWP.Databases
{
    public class UwpSqlLite : ISqlLite
    {
        public string GetDatabasePath(string sqlLiteFileName)
        {
            if (sqlLiteFileName is null) throw new ArgumentNullException(nameof(sqlLiteFileName));

            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqlLiteFileName);

            return path;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Watch.Databases;
using Watch.Droid.Databases;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSqlLite))]
namespace Watch.Droid.Databases
{
    public class AndroidSqlLite : ISqlLite
    {
        public AndroidSqlLite()
        {
        }

        public string GetDatabasePath(string sqlLiteFileName)
        {
            if (sqlLiteFileName is null) throw new ArgumentNullException(nameof(sqlLiteFileName));

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path          = Path.Combine(documentsPath, sqlLiteFileName);

            return path;
        }
    }
}
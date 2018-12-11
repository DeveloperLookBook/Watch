using System;
using Xamarin.Forms;
using System.IO;
using Watch.Databases;
using Watch.iOS.Databases;

[assembly: Dependency(typeof(IosSqlLite))]
namespace Watch.iOS.Databases
{
    public class IosSqlLite : ISqlLite
    {
        public IosSqlLite()
        {
        }

        public string GetDatabasePath(string sqlLiteFileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath   = Path.Combine(documentsPath, "..", "Library");
            string path          = Path.Combine(libraryPath, sqlLiteFileName);

            return path;
        }
    }
}
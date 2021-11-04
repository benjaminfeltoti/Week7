using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Week7.Data;
using Week7.Droid.Data;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Week7.Droid.Data
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string fileName = "LoginDB.db3";
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, fileName);

            return new SQLiteConnection(path);
        }
    }
}
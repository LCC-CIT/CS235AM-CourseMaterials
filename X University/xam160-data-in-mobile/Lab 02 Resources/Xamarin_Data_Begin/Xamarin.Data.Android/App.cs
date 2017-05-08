using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Data.Core.Orm;

namespace Xamarin.Data.Droid
{
    public class App
    {
        public static ConferenceDatabaseAsync Database { get { return databaseInstance; } }
        static readonly ConferenceDatabaseAsync databaseInstance = new ConferenceDatabaseAsync (ConferenceDatabaseAsync.DatabaseFilePath);

        private static App current;
        public static App Current
        {
            get { return current; }
        }

        static App()
        {
            current = new App();
        }
    }
}

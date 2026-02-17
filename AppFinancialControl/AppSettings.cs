using System;
using System.Collections.Generic;
using System.Text;

namespace AppFinancialControl
{
    public class AppSettings
    {
        private static string DataBaseName = "database.db";
        private static string DataBaseDirectory = FileSystem.AppDataDirectory;
        public static string DataBasePath = Path.Combine(DataBaseDirectory, DataBaseName);
    }
}
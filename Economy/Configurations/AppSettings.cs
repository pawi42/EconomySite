using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Economy.Configurations
{
    public class AppSettings
    {
        public static int GetAppSettingsInteger(string name)
        {
            return int.Parse(ConfigurationManager.AppSettings["YearsBack"]);
        }
    }
}
using Update.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdaterTest
{

    public static class AppSettings
    {
        const string configFile = "AppSettings.xml";

        private static string ConfigFile
        {
            get { return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile); }
        }

        private static string _UpdateUrl;

        public static string UpdateUrl
        {
            get
            {
                if (_UpdateUrl == null) _UpdateUrl = XmlIO.ReadXmlElement(ConfigFile, "UpdateUrl");
                return _UpdateUrl;
            }
            set
            {
                AppSettings._UpdateUrl = value;
                XmlIO.WriteXmlElement(ConfigFile, "UpdateUrl", value);
            }
        }

    }
}

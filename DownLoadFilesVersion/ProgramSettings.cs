using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoadFilesVersion
{
    class ProgramSettings
    {
        public static string  GetSetting(string key)
        {
            string keyValue = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location).AppSettings.Settings[key].Value;
            return keyValue;
        }
    }
}

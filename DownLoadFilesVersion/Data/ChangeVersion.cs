using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoadFilesVersion
{
    class ChangeVersion
    {
        public string idCurrentVersion;
        public string idNewVersion;

        public ChangeVersion(string idCurrentVersion, string idNewVersion)
        {
            this.idCurrentVersion = idCurrentVersion;
            this.idNewVersion = idNewVersion;
        }
    }
}

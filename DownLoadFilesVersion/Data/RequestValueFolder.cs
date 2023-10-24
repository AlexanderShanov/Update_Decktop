using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoadFilesVersion.Data
{
    class RequestValueFolder
    {
        public string currantUnit;
        public string targetFolder;
        public RequestValueFolder(string currantUnit, string targetFolder)
        {
            this.currantUnit = currantUnit;
            this.targetFolder = targetFolder;
        }
    }
}

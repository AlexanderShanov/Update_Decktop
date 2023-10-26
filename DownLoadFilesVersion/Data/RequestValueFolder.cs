using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoadFilesVersion.Data
{
    public class RequestValueFolder
    {
        public string currantUnit;
        public string idTargetFirstPath;
        public RequestValueFolder(string currantUnit, string idTargetFirstPath)
        {
            this.currantUnit = currantUnit;
            this.idTargetFirstPath = idTargetFirstPath;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoadFilesVersion
{
    public class ProgressChangedArgs : EventArgs
    {
        public string Progress { get; private set; }
        public string Message { get; private set; }
        public ProgressChangedArgs(string progress, string message)
        {
            Progress = progress;
            Message = message;
        }
    }
}

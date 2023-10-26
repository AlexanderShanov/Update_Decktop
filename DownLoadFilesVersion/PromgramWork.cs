using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownLoadFilesVersion
{
    public class PromgramWork
    {

        public static void UpdateFiles(string idCurrentVersion, string idNewVresion, string rootFolder)
        {
            
        }

        public static void AutoUpdateFiles()
        {
            FormPrograssBar form = new FormPrograssBar();
            ProgramVersion worker = new ProgramVersion();
            Thread workerThread;

            worker.ProgressChanged += new EventHandler<ProgressChangedArgs>(form.OnWorkerProgressChanged);
            workerThread = new Thread(new ThreadStart(worker.UpDate));
            workerThread.Start();
            form.ShowDialog();

        }
    }
}


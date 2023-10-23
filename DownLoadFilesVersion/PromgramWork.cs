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
            FormPrograssBar form = new FormPrograssBar();
            ProgramVersion worker = new ProgramVersion();
            worker.idCurrentVersion = idCurrentVersion;
            worker.idNewVresion = idNewVresion;
            worker.rootFolder = rootFolder;
            Thread workerThread;
            
            worker.ProgressChanged += new EventHandler<ProgressChangedArgs>(form.OnWorkerProgressChanged);
            workerThread = new Thread(new ThreadStart(worker.UpDate));
            workerThread.Start();
            form.ShowDialog();
        }

        public static void AutoUpdateFiles()
        {
            FormPrograssBar form = new FormPrograssBar();
            ProgramVersion worker = new ProgramVersion();
            Thread workerThread;

            

            var rootFolder = ProgramSettings.GetSetting("rootFolder");
            if (rootFolder == null)
            {
                return;
            }

            worker.rootFolder = rootFolder;
            worker.idCurrentVersion = worker.GetIdCurrentVersion(rootFolder);

            string needCurrentVersion = ProgramSettings.GetSetting("NeedCurrentVersion");
            if (needCurrentVersion == null)
            {
                return;
            }
            else if (needCurrentVersion == "last")
            {
                var buildVersions = worker.GetBuildVersions();
                worker.idNewVresion = buildVersions.versions.Last();
            }
            else
            {
                worker.idNewVresion = needCurrentVersion;
            }

            worker.ProgressChanged += new EventHandler<ProgressChangedArgs>(form.OnWorkerProgressChanged);
            workerThread = new Thread(new ThreadStart(worker.UpDate));
            workerThread.Start();
            form.ShowDialog();

        }
    }
}


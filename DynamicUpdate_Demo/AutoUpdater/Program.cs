using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bingo.Update;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdater
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            foreach (string pr in arg)
                Console.WriteLine(pr);
            UpdateManager updateManager = new UpdateManager();
            if (arg.Length != 3)
            {
                Console.WriteLine("Wrong command!");
                Console.WriteLine("FileName ApplicationDir ExeFileName DeploymentManifest");
                Console.ReadKey();
                return;
            }
            //Chua download file                    
            UpdateManager.ApplicationExecutionDir = arg[0];
            UpdateManager.ApplicationEntryPoint = arg[1];
            UpdateManager.CheckForUpdateBaseCode = arg[2];
            updateManager.newVersionUpdateInfo = UpdateManager.GetUpdateInfoFromFile(UpdateManager.CheckForUpdateBaseCode);
            //UpdateManager.ApplicationRunningProsessID = int.Parse(arg[2]);
            string downloadDirpath = UpdateManager.ApplicationExecutionDir;
            int i = 0;
            string dirName = "UpdateDownload" + DateTime.Now.ToString("yyMMddhhMM");
            do
            {
                i++;
                downloadDirpath = Path.Combine(Path.GetTempPath(), dirName + i.ToString());
            } while (Directory.Exists(downloadDirpath));
            UpdateManager.TempDownloadFileDir = downloadDirpath;            
            updateManager.CurrentUpdateState = UpdateState.DownloadingFiles;

            //updateManager.OnUpdateCompleted += new EventHandler(updateManager_OnUpdateCompleted);

            FormUpdateNotifier form = new FormUpdateNotifier();
            UpdateManager.Notifier = form;
            form.updateManager = updateManager;
            Application.Run(form);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            UpdateManager.RunApplication();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UpdateManager.RunApplication();
        }
        

    }
}

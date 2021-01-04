using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using Bingo.Update.Downloads;

namespace Bingo.Update
{    
    public delegate void BeginDownloadFileHandler(Object sender,String downloadFilePath);
    public interface IWriteLogable
    {
        void Write(string message);       
    }
    public class Logger
    {        
        static Logger _Instance;
        IWriteLogable log;
        private Logger()
        {}
        public static Logger Instance{
            get{if(_Instance==null)
                _Instance=new Logger();
                return _Instance;
            }
            set{
                _Instance=value;
            }
        }
        public void SetWriter(IWriteLogable w)
        {
            log=null;
        }
        public void WriteMessage(string message)
        {
            if(log!=null)
                log.Write(message);
        }
    }
    public abstract class Updater
    {        
        public event BeginDownloadFileHandler OnBeginDownloadFile;        
        //UpdateInfo updateInfo;        
        DeploymentManifest DeploymentManifest { get; set; }
        public FileDownloader FileDownloader { get; set; }
        public string TempDownloadFileDir { get; set; }

        public string ApplicationExecutionDir { get; set; }
        
        public abstract bool NewerVersionExisted();

        public void DownloadFiles()
        {
            Logger.Instance.WriteMessage("Begin Download Files");
            if (!Directory.Exists(TempDownloadFileDir))
                Directory.CreateDirectory(TempDownloadFileDir);
            FileDownloader = new FileDownloader();
            //foreach (UpdateFile updateFile in updateInfo.UpdateFileList)
            //{
            //    string destFilePath = Path.Combine(Path.Combine(TempDownloadFileDir, updateFile.DestinationDirectory), updateFile.DestinationFileName);
            //    FileDownloadInfo downloadInfo = new FileDownloadInfo(updateFile.DownloadFilePath, destFilePath);
            //    fileDownloader.FileInfoList.Add(downloadInfo);
            //}
            ApplicationManifest appManifest = DeploymentManifest.ApplicationManifest;
            foreach (DependencyElement dependency in appManifest.Dependencys)
            {
                DependencyAssembly depAsm = dependency.DependencyAssembly;
                if (depAsm != null )
                {
                    FileDownloadInfo downloadInfo = depAsm.GetDownloadInfo();
                    if (downloadInfo != null)
                        FileDownloader.FileInfoList.Add(downloadInfo);
                }
            }
            foreach (FileElement fileUpdate in appManifest.Files)
            {
                FileDownloadInfo downloadInfo = fileUpdate.GetDownloadInfo();
                if (downloadInfo != null)
                    FileDownloader.FileInfoList.Add(downloadInfo);
            }

            FileDownloader.OnAllFileDownloadCompleted += new AllFileDownloaderEvent(fileDownloader_OnAllFileDownloadCompleted);
            FileDownloader.OnFileDownloadError += new FileDownloadError(fileDownloader_OnFileDownloadError);
        }

        void fileDownloader_OnFileDownloadError(object sender, FileDownloadEventAgrs args)
        {
            
        }

        void fileDownloader_OnAllFileDownloadCompleted(FileDownloader sender)
        {
            throw new NotImplementedException();
        }
        
        public abstract void BackupFiles();
        public abstract void RollBackReplaceFile();
        public abstract void CopyFiles();
        public void UpdateFile()
        {
            try
            {
                BackupFiles();
                CopyFiles();
            }
            catch (Exception ex)
            {
                RollBackReplaceFile();
                throw ex;
            }
        }        
        public void PrepareForUpdate()
        {
            if (!NewerVersionExisted())
            {
                return;
            }
            DownloadFiles();
        }
        public void Update()
        {
            UpdateFile();
            StartApplication();
        }

        public void StartApplication()
        {
            
        }
    }
}

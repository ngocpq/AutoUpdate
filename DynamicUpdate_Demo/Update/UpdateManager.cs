using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using Bingo.Update.Downloads;

namespace Bingo.Update
{   
    
    public delegate void UpdateFileError(object sender,Exception ex);
    public class UpdateManager
    {        
        Logger logger;
        #region Events
        public event AllFileDownloaderEvent OnStartDownloadFile;
        public event UpdateFileError OnUpdateFileError;
        public event AllFileDownloaderEvent OnDownloadCompleted;
        public event EventHandler OnUpdateCompleted;
        #endregion
        #region Properties
        public UpdateState CurrentUpdateState { get; set; }
        public static IUpdateNotifier Notifier { get; set; }
        public UpdateInfo newVersionUpdateInfo;
        
        public UpdateInfo currentVersionUpdaeInfo;

        public static String _UpdateBaseCode;
        public static string CheckForUpdateBaseCode
        {
            get { return _UpdateBaseCode; }
            set
            {
                //Uri uri = new Uri(value);
                //_UpdateBaseCode = new Uri(value).AbsoluteUri;
                _UpdateBaseCode = value;
            }
        }
        public static string TempDownloadFileDir { get; set; }        
        public static string ApplicationExecutionDir { get; set; }
        public static string UpdateSourceDownloadDir { get; set; }
        public static string BackupDir { get; set; }
        #endregion
        #region Ctor
        public UpdateManager() {         
        }
        public UpdateManager(string updateInfoFile, string appDir, string tempDir)
        {
            UpdateSourceDownloadDir = Path.GetDirectoryName(updateInfoFile);
            ApplicationExecutionDir = appDir;
            TempDownloadFileDir = tempDir;          
            newVersionUpdateInfo = GetUpdateInfoFromFile(updateInfoFile);
            Notifier = null;
        }
        public UpdateManager(string updateInfoFile, string appDir, string tempDir,IUpdateNotifier notifier)
        {
            UpdateSourceDownloadDir = Path.GetDirectoryName(updateInfoFile);
            ApplicationExecutionDir = appDir;
            TempDownloadFileDir = tempDir;
            newVersionUpdateInfo = GetUpdateInfoFromFile(updateInfoFile);            
            Notifier = notifier;
        }
        #endregion
        #region method
        /// <summary>
        /// Download file cần backup vào thư mục tạm
        /// </summary>
        //TODO: lưu ds các file đã donwload, để có thể download nhiều lần không liên tục
        public void DownloadFiles()
        {
            if (Notifier != null)
                Notifier.BeginFileDownload(newVersionUpdateInfo.DownloadDirPath);
            try
            {
                CurrentUpdateState = UpdateState.DownloadingFiles;
                if (!Directory.Exists(TempDownloadFileDir))
                    Directory.CreateDirectory(TempDownloadFileDir);
                FileDownloader fileDownloader = new FileDownloader();
                fileDownloader.ContinueOnError = false;
                foreach (UpdateFile updateFile in newVersionUpdateInfo.UpdateFileList)
                {
                    String[] subDir = updateFile.DestinationDirectory.Split(Path.DirectorySeparatorChar);
                    
                    for (int i = 0; i < subDir.Length; i++)
                        if (!Directory.Exists(Path.Combine(TempDownloadFileDir, subDir[i])))
                            Directory.CreateDirectory(Path.Combine(TempDownloadFileDir, subDir[i]));
                    string destFilePath = Path.Combine(Path.Combine(TempDownloadFileDir, updateFile.DestinationDirectory), updateFile.DestinationFileName);
                    FileDownloadInfo downloadInfo = new FileDownloadInfo(updateFile.DownloadFilePath, destFilePath);                    
                    fileDownloader.FileInfoList.Add(downloadInfo);
                }
                fileDownloader.OnFileDownloadBegin += new FileDownloadBegin(fileDownloader_OnFileDownloadBegin);
                fileDownloader.OnFileDownloadCompleted += new FileDownloadCompleted(fileDownloader_OnFileDownloadCompleted);
                fileDownloader.OnAllFileDownloadCompleted += new AllFileDownloaderEvent(fileDownloader_OnAllFileDownloadCompleted);
                fileDownloader.OnFileDownloadError += new FileDownloadError(fileDownloader_OnFileDownloadError);
                fileDownloader.OnFileDownloaderStart += new AllFileDownloaderEvent(fileDownloader_OnFileDownloaderStart);
                fileDownloader.IsAsysc = true;
                fileDownloader.Download();                                
            }
            catch(Exception ex)
            {
                if (Notifier != null)
                    Notifier.Exception(ex);
                throw ex;
            }
        }        
        
        public bool UpdateFiles()
        {
            CurrentUpdateState = UpdateState.BeginUpdate;
            if (Notifier != null)
                Notifier.BeginUpdate(newVersionUpdateInfo);
            bool success = false;
            try
            {
                CopyDirectory(TempDownloadFileDir, ApplicationExecutionDir, BackupDir);
                success = true;
                CurrentUpdateState = UpdateState.UpdateFinish;
            }
            catch (Exception ex)
            {
                if (Notifier != null)
                {
                    Notifier.PrintMessage("Error while updating files: " + ex.Message);
                }
                success = false;
                CurrentUpdateState = UpdateState.UpdateError;
                if (OnUpdateFileError != null)
                    OnUpdateFileError(this, ex);
            }
            finally
            {
                if (!success)
                {
                    CurrentUpdateState = UpdateState.UpdateRollback;
                    if (Notifier != null)
                    {
                        Notifier.PrintMessage("Start recovering: " );
                    }
                    try
                    {
                        CopyDirectory(BackupDir, ApplicationExecutionDir, true);
                    }
                    catch (Exception ex)
                    {
                        if (Notifier != null)
                        {
                            Notifier.PrintMessage(String.Format(" + Error while recovering directory: {0} => {1}", BackupDir, ApplicationExecutionDir));
                        }
                    }
                    if (Notifier != null)
                    {
                        Notifier.PrintMessage("Recovering completed. ");
                    }
                }
            }
            
            if (OnUpdateCompleted != null)
                OnUpdateCompleted(null, null);
            if (Notifier != null)
                Notifier.EndUpdate();
            return success;
        }              
        
        public static void CopyDirectory(string source, string dest,bool contiueOnError)
        {
            if (source == dest)
                return;
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);
            foreach (string file in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(file);                
                string fileDestPath = Path.Combine(dest, fileName);
                if (Notifier != null)
                {
                    Notifier.PrintMessage(String.Format(" + Recovering file: {0} => {1}", file, fileDestPath));
                }
                try
                {
                    File.Copy(file, fileDestPath, true);
                }
                catch (Exception ex)
                {

                    if (!contiueOnError)
                        throw ex;

                    if (Notifier != null)
                    {
                        Notifier.PrintMessage(String.Format(" + Error while recovering file: {0} => {1}: {2}", file, fileDestPath,ex.Message));
                    }
                }
                if (Notifier != null)
                {
                    Notifier.PrintMessage(String.Format(" + Finished recovering file: {0} => {1}", file, fileDestPath));
                }
            }
            foreach (string dir in Directory.GetDirectories(source))
            {
                string dirName = Path.GetDirectoryName(source);
                string subDirPathSource = Path.Combine(source, dirName);
                string subDirPathDest = Path.Combine(dest, dirName);
                if (Notifier != null)
                {
                    Notifier.PrintMessage(String.Format(" + Star recovering directory: {0} => {1}", subDirPathSource, subDirPathDest));
                }
                CopyDirectory(subDirPathSource, subDirPathDest,contiueOnError);
                if (Notifier != null)
                {
                    Notifier.PrintMessage(String.Format(" + Finished recovering directory: {0} => {1}", subDirPathSource, subDirPathDest));
                }
            }
        }
        public static void CopyDirectory(string source, string dest,string backupDir)
        {
            if (Notifier != null)
                Notifier.PrintMessage(String.Format("Copying dir: Source: {0} => Dest: {1} => Backup: {2}", source, dest, backupDir));
            if (source == dest)
                return;
            if (!Directory.Exists(dest))
            {
                if (Notifier != null)
                    Notifier.PrintMessage("Create dest dir: "+dest);
                Directory.CreateDirectory(dest);
            }
            if (!Directory.Exists(backupDir))
            {
                if (Notifier != null)
                    Notifier.PrintMessage("Create backup dir: " + backupDir);
                Directory.CreateDirectory(backupDir);
            }
            foreach (string file in Directory.GetFiles(source))
            {
                string fileName = Path.GetFileName(file);
                string fileDestPath = Path.Combine(dest, fileName);
                //Backup
                string oldFilePath = Path.Combine(dest, fileName);
                string backupFilePath = Path.Combine(backupDir, fileName);
                if (File.Exists(oldFilePath))
                {
                    if (Notifier != null)
                        Notifier.PrintMessage("Start backup file: " + oldFilePath+" => "+backupFilePath);
                    File.Move(oldFilePath, backupFilePath);
                    if (Notifier != null)
                        Notifier.PrintMessage("End backup file: " + oldFilePath + " => " + backupFilePath);
                }
                //Copy

                if (Notifier != null)
                    Notifier.PrintMessage("Start copy file: " + file + " => " + fileDestPath);
                File.Copy(file, fileDestPath, true);
                if (Notifier != null)
                    Notifier.PrintMessage("End copy file: " + file + " => " + fileDestPath);
            }
            foreach (string dir in Directory.GetDirectories(source))
            {
                string dirName = Path.GetDirectoryName(dir);
                string subDirPathSource = Path.Combine(source, dirName);
                string subDirPathDest = Path.Combine(dest, dirName);
                string subDirPathBackup = Path.Combine(backupDir, dirName);
                CopyDirectory(subDirPathDest, subDirPathDest,subDirPathBackup);
            }
        }

        #endregion
        #region static members
        public static Version GetLastVersion()
        {
            UpdateInfo lastVersionInfo = GetUpdateInfoFromFile(CheckForUpdateBaseCode);
            return lastVersionInfo.CurrentVersion;
        }

        public static UpdateInfo GetUpdateInfoFromFile(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath);
            switch (ext)
            {
                case ".xml":
                    return new UpdateInfo(filePath);
                case ".application":
                    return new ClickOnceUpdateInfo(filePath);
                case ".manifest":
                    return new ApplicationManifestUpdateInfo(filePath);
                default:
                    throw new Exception("UpdateInfoFile is not supported");
            }
        }
        public static Version GetCurrentVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version;            
        }

        public static string GetUpDateInfoFilePath()
        {
            return CheckForUpdateBaseCode;
        }

        public static bool CheckForUpdate()
        {
            return CheckForUpdate(true);
        }
        public static bool CheckForUpdate(bool Confirm)
        {
            if (!NewerVersionExisted())
            {                
                return false;
            }
            DialogResult rs = DialogResult.Yes;
            if (Confirm)
            {
                rs = MessageBox.Show("New version is available, would you like to update?", "Update", MessageBoxButtons.YesNo);
            }
            if ( rs == DialogResult.Yes)
            {
                string ApplicationDir = Path.GetDirectoryName(Application.ExecutablePath);
                string updateEntry = UpdateManager.DefaultUpdateEnpoint;
                updateEntry = Path.Combine(ApplicationDir, updateEntry);
                string ExeFileName = Path.GetFileName(Application.ExecutablePath);
                string updaterCommandLineAgr = " \"" + ApplicationDir + "\" " + ExeFileName + " \"" + UpdateManager.CheckForUpdateBaseCode + "\"";
                ProcessStartInfo startInfo = new ProcessStartInfo(updateEntry, updaterCommandLineAgr);
                Process.Start(startInfo);
                return true;
            }
            return false;
        }

        public static bool NewerVersionExisted()
        {
            return GetLastVersion().CompareTo(GetCurrentVersion()) == 1;
        }
        /// <summary>
        /// Kiem tra assembly co can update hay khong
        /// </summary>
        /// <param name="asmPath"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        public static bool IsNewerVersion(string asmPath, Version newVersion)
        {
            if (!File.Exists(asmPath))
                return true;
            Assembly asm = Assembly.ReflectionOnlyLoadFrom(asmPath);
            return asm.GetName().Version.CompareTo(newVersion) < 0;            
        }
        public static string ApplicationEntryPoint { get; set; }

        public static int ApplicationRunningProsessID { get; set; }

        public static bool IsApplicationRunning()
        {
            String exeFilePath= Path.Combine( UpdateManager.ApplicationExecutionDir,ApplicationEntryPoint);
            Process[] pro = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(UpdateManager.ApplicationEntryPoint));
            foreach (Process p in pro)
            {                
                if (p.MainModule.FileName.ToLower()== exeFilePath.ToLower())
                    return true;
            }
            return false;
        }


        public static String DefaultUpdateEnpoint { get { return "Updater/AutoUpdater.exe"; } }
        public static String DefaultApplicationEnpoint { get { return "App.exe"; } }
        #endregion
        #region EventHandler
        void fileDownloader_OnFileDownloadError(object sender, FileDownloadEventAgrs args)
        {
            if (Notifier != null)
                Notifier.PrintMessage(String.Format("Download file error: from '{0}' To '{1}'.\nError Message: {2}", args.Info.DownloadSourcePath, args.Info.DestFilePath, args.Message));
            if (Notifier != null)
                Notifier.EndFileDownload(args.Info.DownloadSourcePath);
        }

        void fileDownloader_OnAllFileDownloadCompleted(FileDownloader sender)
        {
            if (Notifier != null)
                Notifier.FinishDownload();
            if (OnDownloadCompleted != null)
                OnDownloadCompleted(sender);
            
        }
        void fileDownloader_OnFileDownloadCompleted(object sender, FileDownloadEventAgrs args)
        {
            if (Notifier != null)
                Notifier.PrintMessage(String.Format("Đã tải xong file: from '{0}' To '{1}'", args.Info.DownloadSourcePath, args.Info.DestFilePath));
            if (Notifier != null)
                Notifier.EndFileDownload(args.Info.DownloadSourcePath);
        }

        void fileDownloader_OnFileDownloadBegin(object sender, FileDownloadEventAgrs args)
        {
            if (Notifier != null)
                Notifier.PrintMessage("Bắt đầu download file: " + args.Info.DownloadSourcePath);
        }
        void fileDownloader_OnFileDownloaderStart(FileDownloader sender)
        {
            if (Notifier != null)
                Notifier.StartDownload();
            if (OnStartDownloadFile != null)
                OnStartDownloadFile(sender);
        }
        #endregion

        

        public static void RunApplication()
        {
            string exePath = Path.Combine(UpdateManager.ApplicationExecutionDir, UpdateManager.ApplicationEntryPoint);
            Process.Start(exePath);
            Application.Exit();
        }
    }
}

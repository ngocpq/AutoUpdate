using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Threading;
using Bingo.Update.Downloads;

namespace Bingo.Update
{
               
   
    public class FileDownloader
    {        
        IFileDownloadHelper GetDownloaderHelper(string downloadPath)
        {
            if (downloadPath.StartsWith("http://") || downloadPath.StartsWith("https://") || downloadPath.StartsWith("www."))
                return new HttpFileDownloaderHelper();
            return new LanFileDownloaderHelper();//TODO: lay theo file URI
        }
        public event AllFileDownloaderEvent OnAllFileDownloadCompleted;
        public event AllFileDownloaderEvent OnFileDownloaderStart;
        public event FileDownloadError OnFileDownloadError;
        public event FileDownloadCompleted OnFileDownloadCompleted;
        public event FileDownloadBegin OnFileDownloadBegin;    
    
        public List<FileDownloadInfo> FileInfoList=new List<FileDownloadInfo>();
        public List<FileDownloadInfo> DownloadingFileInfoList = new List<FileDownloadInfo>();
        public List<FileDownloadInfo> DownloadedFileInfoList = new List<FileDownloadInfo>();
        public List<FileDownloadInfo> ErroredDownloadingFileInfoList = new List<FileDownloadInfo>();        
        public bool IsAsysc { get; set; }
        public bool ContinueOnError { get; set; }
        public bool IsCanceled { get; protected set; }
        public bool IsDownloading { get; private set; }
        private bool IsErrored { get; set; }
        public void Cancel()
        {
            IsCanceled = true;
        }        
        private List<Thread> DownloadThreadList = new List<Thread>();
        public void Download()
        {
            if (OnFileDownloaderStart != null)
                OnFileDownloaderStart(this);
            for (int i = FileInfoList.Count - 1; i >= 0; i--)
            {
                FileDownloadInfo dInfo = FileInfoList[i];
                if (IsCanceled)
                    break;
                if (!ContinueOnError && IsErrored)
                    break;
                if (!IsAsysc)
                    DownloadFileSysc(dInfo);
                else
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(DownloadFileSysc));
                    DownloadThreadList.Add(thread);
                    thread.Start(dInfo);
                }
            }
        }
        protected void DownloadFileSysc(Object downloadFileInfo)
        {
            FileDownloadInfo info = (FileDownloadInfo)downloadFileInfo;                        
            IFileDownloadHelper downloaderHelper = GetDownloaderHelper(info.DownloadSourcePath);
            downloaderHelper.OnDownloadBegin += new FileDownloadBegin(downloaderHelper_OnDownloadBegin);
            downloaderHelper.OnDownloadCompleted += new FileDownloadCompleted(downloaderHelper_OnDownloadCompleted);
            downloaderHelper.OnDownloadError += new FileDownloadError(downloaderHelper_OnDownloadError);
            FileInfoList.Remove(info);
            downloaderHelper.Download(info);
        }
        void downloaderHelper_OnDownloadError(object sender, FileDownloadEventAgrs args)
        {
            IsErrored = true;
            ErroredDownloadingFileInfoList.Add(args.Info);
            DownloadingFileInfoList.Remove(args.Info);
            if (OnFileDownloadError != null)
                OnFileDownloadError(this, args);
            if (CheckCompleted())
                if (OnAllFileDownloadCompleted != null)
                    OnAllFileDownloadCompleted(this);
        }
        bool CheckCompleted()
        {
            return (DownloadingFileInfoList.Count == 0) && (FileInfoList.Count==0);
        }
        void downloaderHelper_OnDownloadCompleted(object sender, FileDownloadEventAgrs args)
        {
            DownloadedFileInfoList.Add(args.Info);
            DownloadingFileInfoList.Remove(args.Info);
            if (OnFileDownloadCompleted != null)
                OnFileDownloadCompleted(this, args);
            if (CheckCompleted())
                if (OnAllFileDownloadCompleted != null)
                    OnAllFileDownloadCompleted(this);
        }
        void downloaderHelper_OnDownloadBegin(object sender, FileDownloadEventAgrs args)
        {
            DownloadingFileInfoList.Add(args.Info);
            FileInfoList.Remove(args.Info);
            if (OnFileDownloadBegin != null)
                OnFileDownloadBegin(this, args);
        }
    }
}

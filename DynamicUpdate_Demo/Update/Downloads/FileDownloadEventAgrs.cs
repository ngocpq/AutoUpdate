using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update.Downloads
{
    public delegate void AllFileDownloaderEvent(FileDownloader sender);
    public delegate void FileDownloadCanceled(FileDownloader sender);


    public delegate void FileDownloadBegin(Object sender, FileDownloadEventAgrs args);
    public delegate void FileDownloadCompleted(Object sender, FileDownloadEventAgrs args);
    public delegate void FileDownloadError(Object sender, FileDownloadEventAgrs args);
    
    public class FileDownloadEventAgrs : EventArgs
    {
        public FileDownloadInfo Info { get; set; }
        public string Message { get; set; }
        public FileDownloadEventAgrs(FileDownloadInfo info)
        {
            Info = info;
        }
        public FileDownloadEventAgrs(string message)
        {
            Message = message;
        }
        public FileDownloadEventAgrs(FileDownloadInfo info, string ms)
        {
            Info = info; Message = ms;
        }
        public FileDownloadEventAgrs() { }
    }

}

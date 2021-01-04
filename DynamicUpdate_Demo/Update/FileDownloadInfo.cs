using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update.Downloads
{
    public class FileDownloadInfo
    {
        public FileDownloadInfo()
        {
            DownloadState = FileDownloadState.WaitingToDownload;
            ReDownloadedCount = 0;
        }
        public FileDownloadInfo(string downloadUrl, string savePath)
        {
            DownloadSourcePath = downloadUrl;
            DestFilePath = savePath;
            DownloadState = FileDownloadState.WaitingToDownload;
            ReDownloadedCount = 0;
        }
        public string DownloadSourcePath { get; set; }
        public string DestFilePath { get; set; }
        public FileDownloadState DownloadState { get; set; }
        public Object Tag { get; set; }
        public int ReDownloadedCount { get; set; }
    }
}

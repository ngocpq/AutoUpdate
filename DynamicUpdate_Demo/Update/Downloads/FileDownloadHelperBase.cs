using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update.Downloads
{
    public abstract class FileDownloadHelperBase : IFileDownloadHelper
    {
        protected abstract void Download(string sourcePath, string destPath);
        
        public void Download(FileDownloadInfo info)
        {
            if (OnDownloadBegin != null)
                OnDownloadBegin(this, new FileDownloadEventAgrs(info));
            bool success = false;
            try
            {
                info.DownloadState = FileDownloadState.Downloading;
                Download(info.DownloadSourcePath, info.DestFilePath);//Copy file
                info.DownloadState = FileDownloadState.Downloaded;
                success = true;
            }
            catch (Exception ex)
            {
                info.DownloadState = FileDownloadState.DownloadError;
                if (OnDownloadError != null)
                    OnDownloadError(this, new FileDownloadEventAgrs(info, ex.Message));
            }
            if (success && OnDownloadCompleted != null)
                OnDownloadCompleted(this, new FileDownloadEventAgrs(info));
        }

        public event FileDownloadCompleted OnDownloadCompleted;

        public event FileDownloadError OnDownloadError;

        public event FileDownloadBegin OnDownloadBegin;

    }
}

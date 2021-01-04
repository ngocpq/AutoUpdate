using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update.Downloads
{
    public interface IFileDownloadHelper
    {
        void Download(FileDownloadInfo info);

        event FileDownloadBegin OnDownloadBegin;
        event FileDownloadCompleted OnDownloadCompleted;
        event FileDownloadError OnDownloadError;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update.Downloads
{
    public enum FileDownloadState
    {
        WaitingToDownload = 1,
        Downloading = 2,
        Downloaded = 3,
        DownloadError = 4,
        DownloadCancel = 5
    }
}

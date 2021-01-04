using System;
using System.Collections.Generic;

using System.Text;
using Bingo.Update.Downloads;

namespace Bingo.Update
{
    public interface IDownloadableFile
    {
        FileDownloadInfo GetDownloadInfo();
        //string DownloadFilePath { get; }
        //string DestinationDirectory { get;  }
        //string DestinationFileName { get; }
    }
}

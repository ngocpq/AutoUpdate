using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Bingo.Update.Downloads
{
    public class HttpFileDownloaderHelper : FileDownloadHelperBase
    {
        protected override void Download(string sourcePath, string destPath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(sourcePath, destPath);            
        }
    }
        
}

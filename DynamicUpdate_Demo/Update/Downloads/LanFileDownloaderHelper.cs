using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bingo.Update.Downloads
{
    public class LanFileDownloaderHelper : FileDownloadHelperBase
    {
        protected override void Download(string sourcePath, string destPath)
        {
            File.Copy(sourcePath, destPath, true);
        }
    }

}

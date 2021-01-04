using System;
using System.Collections.Generic;

using System.Text;

namespace Bingo.Update
{
    public enum UpdateState
    {
        CheckingForUpdate,
        DownloadingFiles,
        DownloadFileComplited,
        BeginUpdate,
        Updatting,
        UpdateFinish,
        UpdateError,
        UpdateRollback
    }
}

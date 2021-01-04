using System;
using System.Collections.Generic;

using System.Text;

namespace Bingo.Update
{

    
    public interface IUpdateNotifier
    {
        void PrintMessage(string mss);
        void BeginFileDownload(string downloadPath);
        void EndFileDownload(string downloadPath);
        void BeginUpdate(UpdateInfo updateInfo);
        void EndUpdate();
        void Exception(Exception ex);

        void StartDownload();
        void FinishDownload();

        bool IsStepByStepMode { get; set; }
    }
}

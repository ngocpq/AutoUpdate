using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Bingo.Update.Downloads;

namespace Bingo.Update
{
    public partial class FormUpdateNotifier : Form,IUpdateNotifier
    {
        public FormUpdateNotifier()
        {
            InitializeComponent();
        }
        public FormUpdateNotifier(string updateInfoFilePath,string appDir,string tempDir)
        {
            InitializeComponent();
            updateManager = new UpdateManager(updateInfoFilePath, appDir, tempDir);
        }
        #region Invoke methods
        delegate void InvokeSetTextDelegate(Control ctr, String value);
        delegate void InvokeIncProgressValue();
        delegate void InvokeInitProgressValue(int max,int min,int value);
        public void AppendMessage(Control ctr, String text)
        {
            ctr.Text += text;
            RichTextBox rtxt = ctr as RichTextBox;
            if (rtxt != null)
            {
                rtxt.SelectionStart = rtxt.Text.Length;
                rtxt.ScrollToCaret();
            }
        }
        public void SetText(Control ctr, String text)
        {
            ctr.Text = text;
        }                
        public void IncProgress()
        {
            progressBar1.Value++;
        }
        public void SetProgress(int max, int min, int v)
        {
            progressBar1.Maximum = max;
            progressBar1.Minimum = min;
            progressBar1.Value = v;
        }
        public void InitProgressValue(int max,int min,int v)
        {
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(new InvokeInitProgressValue(SetProgress), max, min, v);
            else
                SetProgress(max,min,v);
        }
        public void UpdateProgress()
        {
            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(new InvokeIncProgressValue(IncProgress));
            else
                IncProgress();
        }
        public void UpdateStatusLabel(string mss)
        {
            if (label1.InvokeRequired)
                lblCurrentWorking.Invoke(new InvokeSetTextDelegate(SetText),lblCurrentWorking,mss);
            else
                AppendMessage(lblCurrentWorking, mss);
        }
        #endregion
        #region IUpdateNotifier Members

        bool _IsStepByStepMode=false;

        public bool IsStepByStepMode
        {
            get { return _IsStepByStepMode; }
            set { _IsStepByStepMode = value; }
        }
        public void PrintMessage(string mss)
        {
            if (rtxtMessage.InvokeRequired)
                rtxtMessage.Invoke(new InvokeSetTextDelegate(AppendMessage),rtxtMessage,mss+Environment.NewLine);
            else
                AppendMessage(rtxtMessage, mss + Environment.NewLine);            
        }

        public void BeginFileDownload(string downloadPath)
        {
            PrintMessage("Downloading file: " + downloadPath);            
        }
        public void StartDownload()
        {
            PrintMessage("Begin download...");            
        }
        public void FinishDownload()
        {
            PrintMessage("Download finished");            
        }
        public void EndFileDownload(string downloadPath)
        {
            PrintMessage("Finished download file: " + downloadPath);
            UpdateProgress();
        }

        public void BeginUpdate(UpdateInfo updateInfo)
        {
            PrintMessage("Begin update files");            
        }

        public void EndUpdate()
        {
            PrintMessage("End update files.");
            btnFinish.Enabled = true;
        }

        public void Exception(Exception ex)
        {
            PrintMessage("Error: " + ex.Message);
        }

        #endregion

        #region UpdateManager Eventhandler
        public UpdateManager updateManager { get; set; }
        
        void updateManager_OnStartDownloadFile(FileDownloader sender)
        {
            progressBar1.Maximum = updateManager.newVersionUpdateInfo.UpdateFileList.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = progressBar1.Minimum;
            PrintMessage("List of files to download:");
            foreach (FileDownloadInfo file in sender.FileInfoList)
            {
                PrintMessage(String.Format(" + {0} => {1}", file.DownloadSourcePath, file.DestFilePath));
            }
        }
        void updateManager_OnDownloadCompleted(FileDownloader downloader)
        {
            PrintMessage("Download completed: list of downloaded files");
            foreach (FileDownloadInfo file in downloader.DownloadedFileInfoList)
            {
                PrintMessage(String.Format(" + {0} => {1}", file.DownloadSourcePath, file.DestFilePath));
            }

            PrintMessage("Download successful, click Start Update to update.\n"
                          + "(Noted: save all of your data before click the Start Update button)");
            btnStartUpdate.Enabled = true;
            btnDownload.Enabled = false;
            if (!IsStepByStepMode)
                btnStartUpdate.PerformClick();
        }
        void updateManager_UpdateFileError(object sender, Exception ex)
        {
            MessageBox.Show("Error while updating files: " + ex.Message);
        }
        #endregion

        #region Control Event Handler

        bool isUpdating = false;
        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            while (UpdateManager.IsApplicationRunning())
            {
                string mss = string.Format("Close all instances of \"{0}\" to start the updating.\nClose all \"{0}\" windowns, and press Retry button to continue.",UpdateManager.ApplicationEntryPoint);
                DialogResult rs = MessageBox.Show(mss, "Update software", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (rs != DialogResult.Retry)
                {
                    MessageBox.Show("Update not completed.");
                    return;
                }
            }
            btnStartUpdate.Enabled = false;
            try
            {
                isUpdating = true;
                int i = 0;
                string backupDirpath = UpdateManager.ApplicationExecutionDir;
                string dirName = "Backup" + DateTime.Now.ToString("yyMMddhhMM");
                do
                {
                    i++;
                    backupDirpath = Path.Combine(Path.GetTempPath(), dirName + i.ToString());
                } while (Directory.Exists(backupDirpath));

                Directory.CreateDirectory(backupDirpath);
                UpdateManager.BackupDir = backupDirpath;                
                PrintMessage("Start update files");
                bool success = updateManager.UpdateFiles();
                PrintMessage("Finish update files");
                if (success)
                    MessageBox.Show("Update finished successful, click Finish");                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating: " + ex.Message);
            }
            finally
            {
                isUpdating = false;

            }
        }
        private void FormUpdateNotifier_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUpdating)
            {
                e.Cancel = true;
                MessageBox.Show("Cannot exit the program at this time, the update is in process!");
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            UpdateManager.RunApplication();
        }
        private void FormUpdateNotifier_Load(object sender, EventArgs e)
        {
            if (updateManager.CurrentUpdateState >= UpdateState.DownloadFileComplited)
            {
                rtxtMessage.Text = "Download completed, start update files.";                
                btnStartUpdate.Enabled = true;
                if (!IsStepByStepMode)
                    btnStartUpdate.PerformClick();
            }
            else if (updateManager.CurrentUpdateState == UpdateState.DownloadingFiles)
            {
                rtxtMessage.Text = "Click Download to begin download files.";
                btnDownload.Enabled = true;
                if (!IsStepByStepMode)
                    btnDownload.PerformClick();
            }
            else if (updateManager.CurrentUpdateState == UpdateState.CheckingForUpdate)
            {
                try
                {
                    PrintMessage("Checking update...");
                    if (UpdateManager.NewerVersionExisted())
                    {
                        PrintMessage("The version you are using is not the latest version, click Next to start downloading the update.");
                        btnDownload.Enabled = true;
                    }
                    else
                    {
                        PrintMessage("The version you are using is the latest version.");
                        btnFinish.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    PrintMessage("Cannot check for updates: " + ex.Message);
                    btnFinish.Enabled = true;
                }
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                btnDownload.Enabled = false;                
                updateManager.OnDownloadCompleted += new AllFileDownloaderEvent(updateManager_OnDownloadCompleted);
                updateManager.OnStartDownloadFile += new AllFileDownloaderEvent(updateManager_OnStartDownloadFile);
                updateManager.OnUpdateFileError += new UpdateFileError(this.updateManager_UpdateFileError);                
                updateManager.DownloadFiles();
            }
            catch (Exception ex)
            {
                PrintMessage("Error while downloading file: " + ex.Message);
            }

        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

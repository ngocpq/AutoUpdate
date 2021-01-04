using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo.Update
{
    public class ApplicationManifestUpdateInfo:UpdateInfo
    {
        ApplicationManifest appManifest;
        public ApplicationManifestUpdateInfo(string appManifestFilePath)            
        {
            appManifest = new ApplicationManifest(appManifestFilePath);
            DownloadDirPath = appManifest.DirPath;
            CurrentVersion = new Version(appManifest.AssemblyIdentity.version);
            //TODO: remove hardcode
            UpdateEntryPoint = UpdateManager.DefaultUpdateEnpoint;// "AutoUpdater.exe";
            UpdateAdditionPara = " ";
            //Lay danh sach file can update cua ung dung            
            UpdateFileList = new List<UpdateFile>();                        
            foreach (DependencyElement dependency in appManifest.Dependencys)
            {
                if (dependency.DependencyAssembly!=null && dependency.DependencyAssembly.CodeBase!=null)
                {
                    string codebase= dependency.DependencyAssembly.CodeBase;
                    string fileName =codebase.Substring(codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar)+1);

                    string destDir = codebase.Substring(0, codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                    string destName = fileName;
                    if (MapFileExtensions)
                        fileName += ".deploy";
                    UpdateFileList.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
                }
            }
            foreach (FileElement file in appManifest.Files)
            {
                string codebase = file.FileName;
                string fileName = codebase.Substring(codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                string destDir = codebase.Substring(0, codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                string destName = fileName;
                if (MapFileExtensions)
                    fileName += ".deploy";
                UpdateFileList.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
            }
            //Lay danh sach file cua updater
            UpdaterFiles = new List<UpdateFile>();
        }
    }
}

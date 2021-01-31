using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class ClickOnceUpdateInfo:UpdateInfo
    {        
        DeploymentManifest deployManifest;
        public ClickOnceUpdateInfo(string deploymentManifestFilePath)            
        {
            deployManifest = new DeploymentManifest(deploymentManifestFilePath);
            CurrentVersion = new Version(deployManifest.AssemblyIdentity.version);
            
            /*DownloadDirPath = deployManifest.ApplicationManifest.DirPath; //System.IO.Path.GetDirectoryName(deployManifest.Deployment.deploymentProvider.codebase);            
            //TODO: remove hardcode
            UpdateEntryPoint = UpdateManager.DefaultUpdateEnpoint;// "AutoUpdater.exe";
            UpdateAdditionPara = " ";
            //Lay danh sach file can update cua ung dung
            MapFileExtensions = deployManifest.Deployment.mapFileExtensions;
            UpdateFileList = new List<UpdateFile>();

            foreach (DependencyElement dependency in deployManifest.Dependencys)
            {
                if (dependency.DependencyAssembly != null && dependency.DependencyAssembly.CodeBase != null)
                {
                    string codebase = dependency.DependencyAssembly.CodeBase;
                    string fileName = codebase.Substring(codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);

                    string destDir = "";// codebase.Substring(0, codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                    string destName = fileName;
                    if (MapFileExtensions)
                        fileName += ".deploy";
                    UpdateFileList.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
                }
            }
            foreach (DependencyElement dependency in deployManifest.ApplicationManifest.Dependencys)
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
            foreach (FileElement file in deployManifest.ApplicationManifest.Files)
            {
                string codebase = file.FileName;
                string fileName = codebase.Substring(codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                string destDir = codebase.Substring(0, codebase.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                string destName = fileName;
                if (MapFileExtensions)
                    fileName += ".deploy";
                UpdateFileList.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
            }*/
            //Lay danh sach file cua updater
            UpdaterFiles = new List<UpdateFile>();
        }
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;

namespace Bingo.Update
{
    public class UpdateInfo
    {                
        public string UpdateEntryPoint { get; set; }
        public string UpdateAdditionPara { get; set; }

        public string ApplicationEntryPoint { get; set; }
        public Version CurrentVersion { get; set; }
        public List<UpdateFile> UpdateFileList { get; set; }
        public List<UpdateFile> UpdaterFiles { get; set; }

        public bool MapFileExtensions { get; set; }
        public string DownloadDirPath { get; set; }
        
        protected UpdateInfo() {
            InitConstanses();
        }
        
        protected virtual void InitConstanses()
        {
            MapFileExtensions=false;
            ApplicationEntryPoint = UpdateManager.DefaultApplicationEnpoint;
            UpdateEntryPoint=UpdateManager.DefaultUpdateEnpoint;            
        }
        public UpdateInfo(XmlDocument doc)
        {
            InitConstanses();
            LoadFromXml(doc);
        }
        public UpdateInfo(string xmlFile)
        {
            InitConstanses();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            LoadFromXml(doc);            
        }
        public virtual void LoadFromXml(XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;            
            DownloadDirPath =root.Attributes["DownloadPath"].Value;
            
            CurrentVersion = new Version(doc.DocumentElement.Attributes["Version"].Value);
            //Get update entry point
            XmlNode updateEntryNode = root.SelectSingleNode("./UpdateEntryPoint");
            if (updateEntryNode == null)
            {
                UpdateEntryPoint = UpdateManager.DefaultUpdateEnpoint;
                UpdateAdditionPara = " ";
            }
            else
            {
                UpdateEntryPoint = updateEntryNode.Attributes["Name"].Value;
                if (updateEntryNode.Attributes["Arg"] != null)
                    UpdateAdditionPara = updateEntryNode.Attributes["Arg"].Value;
            }
            //Lay danh sach file can update cua ung dung
            MapFileExtensions = Boolean.Parse(doc.DocumentElement.Attributes["mapFileExtensions"].Value);
            UpdateFileList = new List<UpdateFile>();
            string fileXpath = "ApplicationUpdateFiles/File";
            XmlNodeList fileNodeLists = doc.DocumentElement.SelectNodes(fileXpath);
            foreach (XmlNode node in fileNodeLists)
            {                
                string fileName = node.Attributes["FileName"].Value;
                string destDir = node.Attributes["TargetDir"].Value;
                string destName = fileName;
                string version = null;
                if (node.Attributes["Version"] != null)
                    version = node.Attributes["Version"].Value;
                if (version != null)
                {
                    Version newVersion = new Version(version);
                    string destFilePath=UpdateManager.ApplicationExecutionDir;
                    if (destDir != null && destDir != "")
                        destFilePath = Path.Combine(destFilePath, destDir);

                    destFilePath = Path.Combine(destFilePath, fileName);
                    if (UpdateManager.IsNewerVersion(destFilePath, newVersion) == false)
                        continue;
                }
                string downloadPath = null;
                if (node.Attributes["DownloadUri"] != null)
                    downloadPath = node.Attributes["DownloadUri"].Value;
                if (MapFileExtensions)
                    fileName += ".deploy";
                if (downloadPath == null || downloadPath == "")
                    downloadPath = System.IO.Path.Combine(DownloadDirPath, fileName);                
                UpdateFileList.Add(new UpdateFile(downloadPath, destDir, destName));
            }
            //Lay danh sach file cua updater
            UpdaterFiles = new List<UpdateFile>();
            fileXpath = "UpdaterFiles/File";
            fileNodeLists = root.SelectNodes(fileXpath);
            foreach (XmlNode node in fileNodeLists)
            {
                string fileName = node.Attributes["FileName"].Value;
                string destDir = node.Attributes["TargetDir"].Value;
                string destName = fileName;
                if (MapFileExtensions)
                    fileName += ".deploy";
                UpdaterFiles.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bingo.Update
{
    class ApplicationManifest:UpdateInfo
    {
        public ApplicationManifest(string XmlFile):base(XmlFile)
        { }
        public void LoadApplicationManifest(System.Xml.XmlDocument doc)
        { 
            
        }
        public override void LoadFromXml(System.Xml.XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;
            
            XmlNode assemblyIdentityNode = root.SelectSingleNode("./assemblyIdentity");
            CurrentVersion = new Version(assemblyIdentityNode.Attributes["version"].Value);

            XmlNode deploymentNode = root.SelectSingleNode("./deployment");
            MapFileExtensions = Boolean.Parse(deploymentNode.Attributes["mapFileExtensions"].Value);

            XmlNode dependentAssemblyNode = root.SelectSingleNode("dependency/dependentAssembly");

            DownloadDirPath = dependentAssemblyNode.Attributes["codebase"].Value;
            
            //Get update entry point
            XmlNode updateEntryNode = root.SelectSingleNode("./UpdateEntryPoint");
            if (updateEntryNode == null)
            {
                UpdateEntryPoint = "AutoUpdater.exe";
                UpdateAdditionPara = " ";
            }
            else
            {
                UpdateEntryPoint = updateEntryNode.Attributes["Name"].Value;
                if (updateEntryNode.Attributes["Arg"] != null)
                    UpdateAdditionPara = updateEntryNode.Attributes["Arg"].Value;
            }
            //Lay danh sach file can update cua ung dung
            //MapFileExtensions = Boolean.Parse(doc.DocumentElement.Attributes["mapFileExtensions"].Value);
            UpdateFileList = new List<UpdateFile>();
            string fileXpath = "ApplicationUpdateFiles/File";
            XmlNodeList fileNodeLists = doc.DocumentElement.SelectNodes(fileXpath);
            foreach (XmlNode node in fileNodeLists)
            {
                string fileName = node.Attributes["FileName"].Value;

                string destDir = node.Attributes["TargetDir"].Value;
                string destName = fileName;
                if (MapFileExtensions)
                    fileName += ".deploy";
                UpdateFileList.Add(new UpdateFile(System.IO.Path.Combine(DownloadDirPath, fileName), destDir, destName));
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

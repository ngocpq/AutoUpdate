using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using Bingo.Update.Downloads;

namespace Bingo.Update
{
    public class DependencyAssembly:ManifestElement, IDownloadableFile
    {
        public DependencyAssembly(Manifest manifest, XmlNode node, XmlNamespaceManager namespaceManager)
            : base(manifest)
        {
            switch (node.Attributes["dependencyType"].Value)
            {
                case "preRequisite":
                    this.DependencyType = DependencyType.PreRequisite;
                    break;
                case "install":
                    this.DependencyType = DependencyType.Install;
                    break;
            }
            if (DependencyType == DependencyType.Install)
            {
                CodeBase = node.Attributes["codebase"].Value;
                Size = long.Parse(node.Attributes["size"].Value);
            }
            XmlNode asmNode = node.SelectSingleNode("./asmv2:assemblyIdentity", namespaceManager);
            AssemblyIdentity = new AssemblyIdentityElement(asmNode);

        } 
        public DependencyType DependencyType { get; set; }
        public string CodeBase { get; set; }
        public long Size { get; set; }
        public AssemblyIdentityElement AssemblyIdentity { get; set; }

        #region IDownloadableFile Members

        public FileDownloadInfo GetDownloadInfo()
        {
            if (CodeBase==null)
                return null;
            FileDownloadInfo downInfo = new FileDownloadInfo();
            downInfo.DownloadSourcePath = System.IO.Path.Combine(ContainerManifest.DirPath, CodeBase);
            downInfo.DestFilePath = System.IO.Path.Combine(UpdateManager.TempDownloadFileDir, CodeBase);
            return downInfo;
        }

        #endregion
    }
}

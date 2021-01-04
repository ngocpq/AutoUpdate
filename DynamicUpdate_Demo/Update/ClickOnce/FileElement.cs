using System;
using System.Collections.Generic;

using System.Text;
using Bingo.Update.Downloads;

namespace Bingo.Update
{
    public class FileElement:ManifestElement,IDownloadableFile
    {
        private System.Xml.XmlNode fileNode;
        private System.Xml.XmlNamespaceManager NamespaceManager;
        public String FileName { get; set; }
        public long FileSize { get; set; } 
        public FileElement(Manifest manifest,System.Xml.XmlNode fileNode, System.Xml.XmlNamespaceManager NamespaceManager):base(manifest)
        {
            // TODO: Complete member initialization
            this.fileNode = fileNode;
            this.NamespaceManager = NamespaceManager;
            FileName = fileNode.Attributes["name"].Value;
            FileSize = long.Parse(fileNode.Attributes["size"].Value);
        }

        #region IDownloadableFile Members

        public FileDownloadInfo GetDownloadInfo()
        {            
            FileDownloadInfo downInfo = new FileDownloadInfo();
            downInfo.DownloadSourcePath = System.IO.Path.Combine(ContainerManifest.DirPath, FileName);
            downInfo.DestFilePath = System.IO.Path.Combine(UpdateManager.TempDownloadFileDir, FileName);
            return downInfo;
        }

        //public string DownloadFilePath
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string DestinationDirectory
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string DestinationFileName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        #endregion
    }
}

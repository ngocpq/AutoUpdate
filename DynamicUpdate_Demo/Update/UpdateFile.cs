using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class UpdateFile 
    {
        public string DownloadFilePath{get;set;}
        public string DestinationDirectory { get; set; }
        public string DestinationFileName { get; set; }
        
        public UpdateFile(string downloadFilePath,string destinationDir,string destinationName)
        {
            DownloadFilePath = downloadFilePath;
            DestinationDirectory = destinationDir;
            DestinationFileName = destinationName;
        }
       
    }
}

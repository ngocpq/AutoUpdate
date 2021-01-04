using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class ApplicationManifest:Manifest
    {
        public ApplicationManifest(string path):base(path)
        { 
            
        }
        public override void Load(string path)
        {
            base.Load(path);
            XmlElement root = xmlDoc.DocumentElement;
            XmlNode entryPointElement = root.SelectSingleNode("/asmv1:assembly/asmv2:entryPoint", NamespaceManager);
            EntryPoint = new EntryPointElement(entryPointElement, NamespaceManager);            
            Files=new List<FileElement>();
            foreach (XmlNode fileNode in root.SelectNodes("/asmv1:assembly/asmv2:file",NamespaceManager))
            {
                FileElement file = new FileElement(this, fileNode, NamespaceManager);
                Files.Add(file);
            }

        }
        public EntryPointElement EntryPoint { get; set; }
        public List<FileElement> Files { get; set; }
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
namespace Bingo.Update
{
    public class Manifest
    {        
        public Manifest()
        { }
        public Manifest(string path)
        {
            if (path == null)
                throw new Exception("file path is null");
            FilePath = path;
            DirPath = System.IO.Path.GetDirectoryName(path);
            Load(path);
        }
        public String FilePath { get; set; }
        public String DirPath { get; set; }
        protected XmlNamespaceManager NamespaceManager { get; set; }
        protected XmlDocument xmlDoc { get; set; }
        public AssemblyIdentityElement AssemblyIdentity { get; protected set; }
        public List<DependencyElement> Dependencys { get; protected set; }
        protected virtual void InitNamespaceManager()
        {
            NamespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            NamespaceManager.AddNamespace(String.Empty, "urn:schemas-microsoft-com:asm.v2");
            NamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
            NamespaceManager.AddNamespace("asmv1", "urn:schemas-microsoft-com:asm.v1");            
            NamespaceManager.AddNamespace("asmv2", "urn:schemas-microsoft-com:asm.v2");
            NamespaceManager.AddNamespace("xrml", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
            NamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }
        public virtual void Load(string path)
        {
            //Stream reader = File.OpenRead(path);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlElement root = xmlDoc.DocumentElement;
            InitNamespaceManager();
            XmlNode assemblyIdentityNode = root.SelectSingleNode("/asmv1:assembly/asmv1:assemblyIdentity", NamespaceManager);
            
            AssemblyIdentity = new AssemblyIdentityElement(assemblyIdentityNode);
            Dependencys = new List<DependencyElement>();

            foreach (XmlNode node in root.SelectNodes("/asmv1:assembly/asmv2:dependency",NamespaceManager))
            {
                Dependencys.Add(new DependencyElement(this,node,NamespaceManager));
            }
        }

        public virtual void LoadXml(string xml)
        {
            //Stream reader = File.OpenRead(path);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlElement root = xmlDoc.DocumentElement;
            InitNamespaceManager();
            XmlNode assemblyIdentityNode = root.SelectSingleNode("/asmv1:assembly/asmv1:assemblyIdentity", NamespaceManager);

            AssemblyIdentity = new AssemblyIdentityElement(assemblyIdentityNode);
            Dependencys = new List<DependencyElement>();

            foreach (XmlNode node in root.SelectNodes("/asmv1:assembly/asmv2:dependency", NamespaceManager))
            {
                Dependencys.Add(new DependencyElement(this, node, NamespaceManager));
            }
        }
    }
}

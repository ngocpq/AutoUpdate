using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bingo.Update
{
    class DeploymentManifest:UpdateInfo
    {
        public string AssemblyIdentityName { get; set; }
        public Version Version { get; set; }
        public bool MapFileExtention { get; set; }
        public string CodeBase { get; set; }
        public string DependentAssemblyCodeBase { get; set; }

        public DeploymentManifest(XmlDocument doc):base()
        {
            LoadDocument(doc);
        }
        
        void LoadDocument(XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;

            XmlNode assemblyIdentityNode = root.SelectSingleNode("./assemblyIdentity");
            Version = new Version(assemblyIdentityNode.Attributes["version"].Value);

            XmlNode deploymentNode = root.SelectSingleNode("./deployment");
            MapFileExtention = Boolean.Parse(deploymentNode.Attributes["mapFileExtensions"].Value);
            XmlNode deploymentProviderNode = deploymentNode.SelectSingleNode("deploymentProvider");
            CodeBase = deploymentProviderNode.Attributes["codebase"].Value;

            XmlNode dependentAssemblyNode = root.SelectSingleNode("dependency/dependentAssembly");

            DependentAssemblyCodeBase = dependentAssemblyNode.Attributes["codebase"].Value;

            XmlDocument manifestDoc = new XmlDocument();
            manifestDoc.Load(DependentAssemblyCodeBase);

        }
    }
}

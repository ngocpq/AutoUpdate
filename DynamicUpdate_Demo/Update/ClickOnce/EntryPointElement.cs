using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class EntryPointElement
    {
        public AssemblyIdentityElement AsemblyIdentity{get;set;}
        public String CommandLineFile { get; set; }
        public String CommandLineParameters{ get; set; }
        public EntryPointElement(XmlNode node, XmlNamespaceManager nsmng)
        {
            XmlNode asmIdentityNode = node.SelectSingleNode("./asmv2:assemblyIdentity", nsmng);
            AsemblyIdentity = new AssemblyIdentityElement(asmIdentityNode);
            XmlNode cmdLineNode = node.SelectSingleNode("./asmv2:commandLine", nsmng);
            CommandLineFile = cmdLineNode.Attributes["file"].Value;
            CommandLineParameters = cmdLineNode.Attributes["parameters"].Value;
        }
    }
}

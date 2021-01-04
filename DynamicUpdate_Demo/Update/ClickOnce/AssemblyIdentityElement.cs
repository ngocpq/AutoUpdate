using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class AssemblyIdentityElement
    {
        public AssemblyIdentityElement(XmlNode node)            
        {
            name = node.Attributes["name"].Value;
            version = node.Attributes["version"].Value;
            //publicKeyToken = node.Attributes["publicKeyToken"].Value;
            //processorArchitecture = node.Attributes["processorArchitecture"].Value;            
        }
        public String name { get; set; }

        public string version { get; set; }

        //public string publicKeyToken { get; set; }

        //public string processorArchitecture { get; set; }

        //public string culture { get; set; }
    }
}

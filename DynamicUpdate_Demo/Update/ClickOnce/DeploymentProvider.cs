using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class DeploymentProvider
    {
        public DeploymentProvider(XmlNode node)            
        {
            codebase = node.Attributes["codebase"].Value;
        }
        public String codebase { get; set; }

    }
}

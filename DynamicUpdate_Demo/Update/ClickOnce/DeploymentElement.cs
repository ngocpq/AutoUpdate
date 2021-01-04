using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class DeploymentElement
    {
        private System.Xml.XmlNode deploymentNode;
        protected XmlNamespaceManager NamespaceManager;
        public DeploymentElement(System.Xml.XmlNode deploymentNode,XmlNamespaceManager nsmr)
        {
            // TODO: Complete member initialization
            this.deploymentNode = deploymentNode;
            this.NamespaceManager = nsmr;
            XmlNode node = deploymentNode.SelectSingleNode("./asmv2:deploymentProvider", NamespaceManager);
            deploymentProvider = new DeploymentProvider(node);

            install =bool.Parse( deploymentNode.Attributes["install"].Value);
            if (deploymentNode.Attributes["mapFileExtensions"] != null)
                mapFileExtensions = bool.Parse(deploymentNode.Attributes["mapFileExtensions"].Value);
        }
        public DeploymentProvider deploymentProvider { get; set; }
        public bool install { get; set; }

        //public string minimumRequiredVersion { get; set; }
        public bool mapFileExtensions { get; set; }

        //public string disallowUrlActivation { get; set; }

        //public string trustUrlParameters { get; set; }
    }
}

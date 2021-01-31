using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class DeploymentManifest: Manifest
    {
        #region Properties        
        public DeploymentElement Deployment { get; set; }
        public ApplicationManifest ApplicationManifest { get; protected set; }
        #endregion
        #region Ctor
        public DeploymentManifest(string path):base(path)
        {
        }
        #endregion
        #region Method
        public override void Load(string path)
        {
            base.Load(path);
            
            XmlElement root = xmlDoc.DocumentElement;
            XmlNode deploymentNode = root.SelectSingleNode("/asmv1:assembly/asmv2:deployment", NamespaceManager);
            Deployment = new DeploymentElement(deploymentNode,NamespaceManager);
            LoadApplicationManifest();
        }
        protected void LoadApplicationManifest()
        {
            //TODO: 
            string subPath = Dependencys[0].DependencyAssembly.CodeBase;
            string leftPath = System.IO.Path.GetDirectoryName(this.FilePath);
            string filePath = System.IO.Path.Combine(leftPath, subPath);            
            //ApplicationManifest = new ApplicationManifest(filePath);
        }
        #endregion
    }
}

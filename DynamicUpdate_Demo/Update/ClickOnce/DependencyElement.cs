using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public class DependencyElement:ManifestElement
    {
        protected XmlNode DependencyNode;
        protected XmlNamespaceManager NamespaceManager;
        public DependencyElement(Manifest manifest, XmlNode node,XmlNamespaceManager namespaceManager)           :base(manifest)
        {
            DependencyNode = node;
            NamespaceManager = namespaceManager;
            XmlNode dependentASM=node.SelectSingleNode("./asmv2:dependentAssembly",NamespaceManager);
            if (dependentASM != null)
                DependencyAssembly = new DependencyAssembly(ContainerManifest,dependentASM, namespaceManager);
        }
        public DependencyAssembly DependencyAssembly { get; set; }
        //public String name
        //{
        //    get
        //    {
        //        return this["name"];
        //    }
        //    set
        //    {
        //        this["name"]=value;
        //    }
        //}

        //public int preRequisite
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        //public int visible
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        //public int dependencyType
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        //public int codebase
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        //public int size
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}
    }
}

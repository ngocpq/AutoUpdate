using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;

namespace Bingo.Update
{
    public abstract class ManifestElement
    {
        public Manifest ContainerManifest { get; set; }
        public ManifestElement(Manifest manifest)
        { ContainerManifest = manifest; }
        public ManifestElement() { }
        //public Dictionary<String, String> Attributes { get; set; }
        //public List<ManifestElement> Childs { get; set; }
        //public ManifestElement()
        //{
        //    Attributes = new Dictionary<string, string>();
        //    Childs = new List<ManifestElement>();
        //}
        //public ManifestElement(XmlNode node, params string[] attributesNames)
        //{
        //    Attributes = new Dictionary<string, string>();
        //    Childs = new List<ManifestElement>();
        //    foreach (string att in attributesNames)
        //    {
        //        Attributes.Add(att, node.Attributes[att].Value);
        //    }
        //}
        //public string this[string att]
        //{
        //    get { return Attributes[att]; }
        //    set { Attributes[att] = value; }
        //}
    }
}

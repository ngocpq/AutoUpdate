using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoUpdaterTest.Utils
{
    public static class XmlIO
    {
        public static string ReadXmlElement(string FileName, string XPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            XmlNode node = doc.DocumentElement.SelectSingleNode(XPath);

            if (node != null) return node.InnerText;
            else return "";
        }

        public static void WriteXmlElement(string FileName, string XPath, string Content)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            XmlElement root = doc.DocumentElement;

            XmlNode node = root.SelectSingleNode(XPath);
            if (node == null)
            {
                node = doc.CreateElement(XPath);
                root.AppendChild(node);
            }

            node.InnerText = Content;
            doc.Save(FileName);
        }

    }
}

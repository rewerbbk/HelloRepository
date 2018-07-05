using System;
using System.Xml;
using System.Xml.Linq;

namespace Utility.Extensions
{
    public static class XConverter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            if (xDocument == null)
            {
                throw new ArgumentNullException("xDocument");
            }

            var xmlDocument = new XmlDocument();

            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }
}
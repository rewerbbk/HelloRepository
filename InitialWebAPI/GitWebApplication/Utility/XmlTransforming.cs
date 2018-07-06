using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Utility
{
    /// <summary>
    /// Good for one offs but dont use this in Prod.
    /// Make a transform class to reuse the compiledTransform repeatedly.
    /// </summary>
    public static class XmlTransforming
    {

        public static XDocument TransformXsl(XDocument transformDoc, XDocument input)
        {
            if (transformDoc == null)
                throw new System.ArgumentNullException("transformDoc");

            if (input == null)
                throw new System.ArgumentNullException("input");

            XDocument resultDoc = new XDocument();

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(transformDoc.CreateReader());

            using (MemoryStream outXmlStream = new MemoryStream())
            using (XmlWriter outWriter = XmlWriter.Create(outXmlStream))
            {
                transform.Transform(input.CreateReader(), outWriter);
                outXmlStream.Position = 0;
                resultDoc = XDocument.Load(outXmlStream);
            }

            return resultDoc;
        }

        public static string TransformXsl(string transformDoc, string input)
        {
            if (string.IsNullOrEmpty(transformDoc))
                throw new System.ArgumentNullException("transformDoc");

            if (string.IsNullOrEmpty(input))
                throw new System.ArgumentNullException("input");

            XDocument xslt = XDocument.Parse(transformDoc);
            XDocument inputXml = XDocument.Parse(input);

            XDocument result = TransformXsl(xslt, inputXml);

            return result.ToString();
        }
    }
}
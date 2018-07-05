using System;
using System.Linq;
using System.Xml.Linq;

namespace Utility.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse the string into an Enum type.  If the string is not parseable, defValue is returned. 
        /// </summary>
		/// <param name="defaultValue">The desired default value of the target Enum type.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            //if (Enum.IsDefined(typeof(T), value))
            if (Enum.GetNames(typeof(T)).Any(x => x.ToLower() == value.ToLower()))
                return (T)Enum.Parse(typeof(T), value, true);
            else
                return defaultValue;
        }

        /// <summary>
        /// Get the string's value or default value if the string is null or empty.
        /// </summary>
		/// <param name="defaultValue"></param>
        /// <returns></returns>
		public static string DefaultIfEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        /// Get the substring of the string's last number of characters.
        /// </summary>
        /// <param name="numberOfChars"></param>
        /// <returns></returns>
        public static string Tail(this string value, int numberOfChars)
        {
            if (numberOfChars < 0)
                throw new IndexOutOfRangeException("Number of chars can not be less than zero");

            if (value.Length < numberOfChars)
                throw new IndexOutOfRangeException("Number of chars exceeds string length");

            return value.Substring(value.Length - numberOfChars);
        }

        /// <summary>
        /// Remove the xml declaration tag at the beginning of the xmls in string form.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string RemoveXmlDeclarationTag(this string xml)
        {
            if (xml.StartsWith("<?", StringComparison.InvariantCultureIgnoreCase))
                xml = xml.Substring(xml.IndexOf("?>") + 2);

            return xml;
        }

        /// <summary>
        /// Load string into XDocument.  Returns True/False if successfull. 
        /// </summary>
        /// <param name="inString"></param>
        /// <param name="outXDocument"></param>
        /// <returns></returns>
        public static bool ToXDocument(this string inString, out XDocument outXDocument)
        {
            outXDocument = new XDocument();

            try
            {
                outXDocument = XDocument.Parse(inString);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
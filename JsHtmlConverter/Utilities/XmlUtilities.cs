namespace JsHtmlConverter
{
    using System.Xml.Linq;

    public static class XmlUtilities
    {
        /// <summary>
        /// Removes all attributes in input string.
        /// </summary>
        /// <param name="html">Input html</param>
        /// <returns>Html without attributes</returns>
        public static string RemoveAllAttributes(string html)
        {
            var document = XDocument.Parse(html);
            foreach (var element in document.Descendants())
            {
                element.RemoveAttributes();
            }

            return document.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDesigner
{
    public static class ReactUtilities
    {
        /// <summary>
        /// Returns class names for react jsx/js file content.
        /// Example "class Photos extends React.Component"
        /// Output "{ Photos }"
        /// </summary>
        /// <param name="text">Content of react jsx/js file</param>
        /// <returns>String list of react class names</returns>
        public static IList<string> GetClassNames(string text)
        {
            var names = new List<string>();

            var classIndex = text.IndexOf("class ");
            var reactComponentIndex = text.IndexOf(" extends ");
            if (classIndex >= 0 &&
                reactComponentIndex >= 0)
            {
                var elementName = text.Substring(classIndex + 6, reactComponentIndex - classIndex + 6);
                names.Add(elementName);
            }

            return names;
        }
    }
}

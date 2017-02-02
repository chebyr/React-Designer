namespace ReactDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StringExtensions;

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

        /// <summary>
        /// Removes all import, export and PropTypes lines from content
        /// </summary>
        /// <param name="jsx">Raw jsx</param>
        /// <returns>Jsx without server-side components</returns>
        public static string ClearJsx(string jsx)
        {
            jsx = string.Join(Environment.NewLine, jsx.ToLines().Where(
                    line =>
                        !line.Trim(' ').StartsWith("import", StringComparison.OrdinalIgnoreCase) &&
                        !line.Trim(' ').StartsWith("export default connect", StringComparison.OrdinalIgnoreCase) &&
                        !line.Contains("PropTypes")));

            jsx = jsx.Replace("export default ", "");

            return jsx;
        }

        /// <summary>
        /// Returns ReactDOM.render call
        /// </summary>
        /// <param name="element">First argument for ReactDOM.render()</param>
        /// <param name="id">Element id for render to</param>
        /// <returns>return ReactDOM.render(element, document.getElementById('id'));</returns>
        public static string GetReactDOMRender(string element, string id)
        {
            return $@"ReactDOM.render(
  {element},
  document.getElementById('{id}')
);";
        }

        /// <summary>
        /// Returns ReactDOM.render call for error rendering
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>return ReactDOM.render(&lt;h1&gt;{message}&lt;/h1&gt;, document.getElementById('error'));</returns>
        public static string GetReactDOMRenderError(string message)
        {
            return GetReactDOMRender($"<h1>{message}</h1>", "error");
        }

        /// <summary>
        /// Returns jsx for output page.
        /// Removes all import/export directivies and PropTypes from input jsx.
        /// </summary>
        /// <param name="jsx">Raw jsx from file(must be with import/export directivies)</param>
        /// <param name="elementName">Element name for ReactDOM.render</param>
        /// <returns>jsx text with try/catch and ReactDOM.render</returns>
        public static string CreateRenderedJsx(string jsx, string elementName)
        {
            if (string.IsNullOrWhiteSpace(elementName))
            {
                return GetReactDOMRenderError("This file does not contains classes for rendering");
            }

            return $@"try {{
  {ClearJsx(jsx)}

  {GetReactDOMRender($"<{elementName} />", "root")}
}}
catch(e) {{
  {GetReactDOMRenderError(@"{""Error "" + e.name + "": "" + e.message}")}
}}";
        }

        /// <summary>
        /// Returns html with css and js for output page.
        /// </summary>
        /// <param name="css">Raw css text</param>
        /// <param name="jsx">Rendered jsx text</param>
        /// <param name="pathToJs">Path to directory with js</param>
        /// <returns>html for use with css and js</returns>
        public static string CreateJsxHtml(string css, string jsx, string pathToJs)
        {
            pathToJs = pathToJs.Replace('\\', '/');
            return $@"<!DOCTYPE html>
<html>
<head>
  <meta charset=""utf-8"">
  <title>Element View</title>
  <script src=""file:///{pathToJs}/react.js""></script>
  <script src=""file:///{pathToJs}/react-dom.js""></script>
  <script src=""file:///{pathToJs}/babel.min.js""></script>
  <style type=""text/css"">
{css}
  </style>
</head>
<body>
<div id=""root"">
</div>
<div id=""error"" style=""color: red; "" >
</div>
<script type=""text/babel"" >
{jsx}
</script>
</body>
</html>";
        }
    }
}

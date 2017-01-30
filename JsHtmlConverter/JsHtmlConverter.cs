using System.Diagnostics;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using React;

namespace JsHtmlConverter
{
    public static class JsHtmlConverter
    {
        public static string ConvertToHtml(string js)
        {
            var configuration = new ReactSiteConfiguration();
            var filesystem = new SimpleFileSystem();
            var cache = new MemoryFileCache();
            var hash = new FileCacheHash();
            JsEngineSwitcher.Instance.EngineFactories.AddV8();
            using (var factory = new JavaScriptEngineFactory(JsEngineSwitcher.Instance, configuration, filesystem))
            {
                using (var environment = new ReactEnvironment(factory, configuration, cache, filesystem, hash))
                {
                    var babeled = environment.Babel.Transform(js);
                    Debug.WriteLine($@"Babel output:
{babeled}");

                    return environment.Execute<string>(babeled);
                }
            }
        }

        public static string ConvertToHtml(string jsx, string name)
        {
            return ConvertToHtml($@"{jsx}

ReactDOMServer.renderToString(
  {name}
);");
        }
    }
}

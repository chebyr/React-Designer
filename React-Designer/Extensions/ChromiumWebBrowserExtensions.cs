namespace ReactDesigner.ChromiumWebBrowserExtensions
{
    using System;
    using System.Threading.Tasks;
    using CefSharp;
    using CefSharp.WinForms;

    /// <summary>
    /// This class provides extensions for the ChromiumWebBrowser.
    /// </summary>
    public static class ChromiumWebBrowserExtensions
    {
        public static void WaitForBrowserToInitialize(this ChromiumWebBrowser browser)
        {
            var source = new TaskCompletionSource<bool>();

            EventHandler<IsBrowserInitializedChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                browser.IsBrowserInitializedChanged -= handler;
                source.TrySetResult(true);
            };
            browser.IsBrowserInitializedChanged += handler;

            source.Task.Wait();
        }

        public static void WaitForPage(this ChromiumWebBrowser browser)
        {
            var source = new TaskCompletionSource<bool>();
            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not 
                // just the first frame
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    source.TrySetResult(true);
                }
            };

            browser.LoadingStateChanged += handler;
            source.Task.Wait();
        }
    }
}

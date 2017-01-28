using CefSharp.WinForms;
using System;
using System.Threading.Tasks;

namespace ReactDesigner.ChromiumWebBrowserExtensions
{
    /// <summary>
    /// This class provides extensions for the ChromiumWebBrowser.
    /// </summary>
    public static class ChromiumWebBrowserExtensions
    {
        public static void WaitForBrowserToInitialize(this ChromiumWebBrowser browser)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler<CefSharp.IsBrowserInitializedChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                browser.IsBrowserInitializedChanged -= handler;
                tcs.TrySetResult(true);
            };
            browser.IsBrowserInitializedChanged += handler;

            tcs.Task.Wait();
        }
    }
}

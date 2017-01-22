namespace ReactDesigner
{
    using CefSharp.WinForms;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// This class provides room for extension of a ChromiumWebBrowser.
    /// </summary>
    public class EditorControl : ChromiumWebBrowser
    {
        #region Constructor
        /// <summary>
        /// Explicitly defined default constructor.
        /// Initialize new instance of the EditorControl.
        /// </summary>
        public EditorControl(string url) : base(url)
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {}
        #endregion
    }

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

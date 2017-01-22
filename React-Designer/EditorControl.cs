namespace ReactDesigner
{
    using CefSharp.WinForms;

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
}

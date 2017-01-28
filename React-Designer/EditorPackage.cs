namespace ReactDesigner
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio;
    using EnvDTE;

    /// <summary>
    /// This class implements Visual studio package that is registered within Visual Studio IDE.
    /// The EditorPackage class uses a number of registration attribute to specify integration parameters.
    /// </summary>
    /// <remarks>
    /// <para>A description of the different attributes used here is given below:</para>
    /// <para>PackageRegistration: Used to determine if the package registration tool should look for additional 
    ///                      attributes. We currently specify that the package commands are described in a 
    ///                      managed package and not in a separate satellite UI binary.</para>
    /// <para>ProvideMenuResource: Provides information about menu resources. 
    ///     We specify ResourceId=1000 and version=1.</para>
    /// <para>ProvideEditorLogicalView: Indicates that our editor supports LOGVIEWID_Designer logical view and 
    ///     registers EditorFactory class to manage this view.</para>
    /// <para>ProvideEditorExtension: With this attribute we register our AddNewItem Templates 
    ///     for specified project types.</para>
    /// </remarks>

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#100", "#102", "10.0", IconResourceID = 400)]
    [ProvideEditorFactory(typeof(EditorFactory), 106)]
    [ProvideEditorLogicalView(typeof(EditorFactory), VSConstants.LOGVIEWID.Designer_string, IsTrusted = true)]
    [ProvideEditorExtension(typeof(EditorFactory), ".js", 50)]
    [ProvideEditorExtension(typeof(EditorFactory), ".jsx", 50)]
    [ProvideEditorExtension(typeof(EditorFactory), ".*", 1)]
    [Guid(GuidStrings.GuidClientPackage)]
    public class EditorPackage : Package, IDisposable
    {
        #region Fields

        private EditorFactory editorFactory;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes new instance of the EditorPackage.
        /// </summary>
        public EditorPackage()
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Create EditorPackage context.
        /// </summary>
        protected override void Initialize()
        {
            //Create Editor Factory
            base.Initialize();
            editorFactory = new EditorFactory();
            RegisterEditorFactory(editorFactory);

            DTE = (DTE)GetService(typeof(DTE));

            var settings = new CefSharp.CefSettings { LogSeverity = CefSharp.LogSeverity.Verbose };
            if (!CefSharp.Cef.Initialize(settings))
            {
                throw new Exception("Unable to Initialize Cef");
            }
        }

        /// <summary>
        /// Returns package directory path.
        /// </summary>
        public static string PackagePath {
            get {
                var uri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().EscapedCodeBase);
                string path = System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(uri.AbsolutePath));
                return path + "\\";
            }
        }

        /// <summary>
        /// Returns DTE object.
        /// </summary>
        public static DTE DTE { get; private set; }

        #region IDisposable Pattern
        /// <summary>
        /// Releases the resources used by the Package object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases the resources used by the Package object.
        /// </summary>
        /// <param name="disposing">This parameter determines whether the method has been called directly or indirectly by a user's code.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Dispose() of: {0}", ToString()));
                if (disposing)
                {
                    if (editorFactory != null)
                    {
                        editorFactory.Dispose();
                        editorFactory = null;
                    }
                    GC.SuppressFinalize(this);
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion

        #endregion Methods
    }
}

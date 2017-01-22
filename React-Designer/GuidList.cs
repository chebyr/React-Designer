namespace ReactDesigner
{
    using System;

    /// <summary>
    /// This class contains a list of GUIDs specific to this project.
    /// </summary>
    public static class GuidStrings
    {
        public const string GuidClientPackage = "903A0B92-9903-4C1F-9338-56A4F696AD1A";
        public const string GuidClientCmdSet = "8E1E280E-C088-4939-A9B9-666956E56B92";
        public const string GuidEditorFactory = "19DA1E75-1352-4AEA-BCE0-ED9D1783E166"; 
    }

    /// <summary>
    /// List of the GUID objects.
    /// </summary>
    internal static class GuidList
    {
        public static readonly Guid guidEditorCmdSet = new Guid(GuidStrings.GuidClientCmdSet);
        public static readonly Guid guidEditorFactory = new Guid(GuidStrings.GuidEditorFactory);
    };
}
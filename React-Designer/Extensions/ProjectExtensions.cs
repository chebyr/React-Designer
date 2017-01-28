using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;

namespace ReactDesigner.ProjectExtensions
{
    public static class ProjectExtensions
    {
        public static IList<string> GetProjectItemExtensionPaths(ProjectItems items, string extension)
        {
            var paths = new List<string>();
            if (items == null)
            {
                return paths;
            }

            foreach (ProjectItem item in items)
            {
                for (short i = 0; i < item.FileCount; ++i)
                {
                    var filename = item.FileNames[i];
                    if (Path.GetExtension(filename).Equals(extension, StringComparison.OrdinalIgnoreCase) ||
                        extension == ".*")
                    {
                        paths.Add(filename);
                    }
                }
                paths.AddRange(GetProjectItemExtensionPaths(item.ProjectItems, extension));
            }

            return paths;
        }

        public static IList<string> GetProjectItemExtensionPaths(this Project project, string extension)
        {
            return GetProjectItemExtensionPaths(project.ProjectItems, extension);
        }
    }
}
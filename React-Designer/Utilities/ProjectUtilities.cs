using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace ReactDesigner
{
    public static class ProjectUtilities
    {
        /// <summary>
        /// This method returned selected project in manager
        /// or active window project
        /// </summary>
        /// <param name="dte">Automation IDE Object</param>
        /// <returns>Return selected project</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Project GetSelectedProject(DTE dte)
        {
            if (dte == null)
            {
                throw new ArgumentNullException(nameof(dte));
            }

            var projects = (Array)dte.ActiveSolutionProjects;
            foreach (var project in projects)
            {
                return (Project)project;
            }

            return dte.ActiveWindow.Project;
        }

        /// <summary>
        /// Returns project for selected item
        /// </summary>
        /// <returns>Project for selected item</returns>
        public static Project GetCurrentProject()
        {
            return GetSelectedProject(EditorPackage.DTE);
        }
    }
}

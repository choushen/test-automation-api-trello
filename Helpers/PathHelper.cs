using System;
using System.IO;

namespace RestSharpProject.Helpers
{
    public static class PathHelper
    {
        
        // PROPERTIES


        // METHODS
        public static string GetProjectRootPath()
        {
            // Get the base directory (bin\Debug or bin\Release folder)
            var baseDirectory = AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(baseDirectory);

            while (directoryInfo != null && !File.Exists(Path.Combine(directoryInfo.FullName, "RestSharpSolution.sln")))
            {
                directoryInfo = directoryInfo.Parent;
            }

            return directoryInfo.FullName;
        } // GetProjectRootPath end


        public static string GetFilePath(string relativePath)
        {
            var projectRootPath = GetProjectRootPath();
            return Path.Combine(projectRootPath, relativePath);
        } // GetConfigFilePath end

    }
}
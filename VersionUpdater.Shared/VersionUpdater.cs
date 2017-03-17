using Microsoft.Build.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CustomBuildTasks
{
    // (Details on MSBUILD tasks: http://msdn.microsoft.com/en-us/library/t9883dzc.aspx)
    public class VersionUpdater : ITask
    {
        public bool Execute()
        {
            const string assemblyInfoFilePath = @"Properties\AssemblyInfo.cs";
            const string assemblyVersionPattern = "\\[assembly: AssemblyVersion\\(\"(?<1>\\d+)\\.(?<2>\\d+)\\.(?<3>\\d+)\\.(?<4>\\d+)\"\\)\\]";
            const string assemblyFileVersionPattern = "\\[assembly: AssemblyFileVersion\\(\"(?<1>\\d+)\\.(?<2>\\d+)\\.(?<3>\\d+)\\.(?<4>\\d+)\"\\)\\]";

            try
            {
                string assemblyInfo = File.ReadAllText(assemblyInfoFilePath);
                DateTime lastBuildTime = File.GetLastWriteTime(assemblyInfoFilePath);

                var currentAssemblyVersion = Regex.Match(assemblyInfo, assemblyVersionPattern); // use the major and minor numbers from the assembly version.
                int major = Convert.ToInt16(currentAssemblyVersion.Groups[1].Value);
                int minor = Convert.ToInt16(currentAssemblyVersion.Groups[2].Value);

                currentAssemblyVersion = Regex.Match(assemblyInfo, assemblyFileVersionPattern); // only update the build and revision of the file version! (this allows other consumers of the DLL to not required updates on non-breaking builds)
                int build = Convert.ToInt16(currentAssemblyVersion.Groups[3].Value);
                int revision = Convert.ToInt16(currentAssemblyVersion.Groups[4].Value);

                if (lastBuildTime.Day == DateTime.Now.Day) // Last build was today
                    revision++;
                else
                {
                    build++;
                    revision = 0;
                }

                var newAssemblyVersion = string.Format("[assembly: AssemblyVersion(\"{0}.{1}.{2}.{3}\")]", major, minor, DateTime.UtcNow.Year, DateTime.UtcNow.Month);
                var updatedAssemblyInfo = Regex.Replace(assemblyInfo, assemblyVersionPattern, newAssemblyVersion);
                var newAssemblyFileVersion = string.Format("[assembly: AssemblyFileVersion(\"{0}.{1}.{2}.{3}\")]", major, minor, build, revision);
                updatedAssemblyInfo = Regex.Replace(updatedAssemblyInfo, assemblyFileVersionPattern, newAssemblyFileVersion);

                File.WriteAllText(assemblyInfoFilePath, updatedAssemblyInfo);

                Console.WriteLine("Successfully updated assembly information with version: {0}.{1}.{2}.{3}", major, minor, build, revision);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }
    }
}

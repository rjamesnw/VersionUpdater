using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CustomBuildTasks
{
    class Program
    {
        static void Main(string[] args) // (note: it is assumed the working directory is set properly in the project settings)
        {
            var vu = new VersionUpdater();
            vu.Execute();
            Console.ReadLine(); // (simple pause)
        }
    }
}

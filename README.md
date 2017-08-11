## VersionUpdater

The assembly produced by this project automatically updates Visual Studio .Net project versions on every build.
Normally an assembly version is in the format `[major version].[minor version].[build number].[revision]`.
By default, this ends up being `1.0.0.0` for new projects, and does not auto update, resulting in the following static versions for many projects:

    [assembly: AssemblyVersion("1.0.0.0")]
    [assembly: AssemblyFileVersion("1.0.0.0")]

You can use the "1.0.\*" format, but it will be based on number of dats since Jan 1, 2000, and personally I like to start at 0. ;) Then there's this issue: https://goo.gl/QuSdgv (among others)

This assembly is loaded by a build task by placing a build task entry in the project settings file (usually `.csproj` or `.xproj`).
On every build, it increments the revision number, and on each new day, is increments the build number and resets the revision to 0.

The major and minor numbers are never touched. This process ensures that during routine debugging and testing one doesn't forget to update the version numbers, especially when working with clients during UAT or quick post go-live patch updates.

#### Installation - .Net

Put `VersionUpdater.dll` in the root of your PROJECT folder and add this to the end of your project settings file:

    <UsingTask TaskName="VersionUpdater" AssemblyFile="VersionUpdater.dll" />
    <Target Name="BeforeBuild">
      <VersionUpdater />
    </Target>

#### Installation - .Net Core

*(not, this is not working yet ;))*

Put `VersionUpdater.Core.dll` in the root of your PROJECT folder and add this to the end of your project settings file:

    <UsingTask TaskName="VersionUpdater" AssemblyFile="VersionUpdater.Core.dll" />
    <Target Name="BeforeBuild">
      <VersionUpdater />
    </Target>

#### Misc.

*License: https://creativecommons.org/licenses/by-sa/4.0/*

*No NuGet package is available yet.*

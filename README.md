# VersionUpdater

The assembly produced by this project automatically updates Visual Studio .Net project versions on every build.
Normally an assembly version is in the format `[major version].[minor version].[build number].[revision]`.
By default, this ends up being `1.0.0.0` for new projects, and does not auto update, resulting in the following static versions for many projects:

    [assembly: AssemblyVersion("1.0.0.0")]
    [assembly: AssemblyFileVersion("1.0.0.0")]

This assembly is loaded by a build task by placing a build task entry in the project settings file (usually `.csproj` or `.xproj`).
On every build, it increments the revision number, and on each new day, is increments the build number and resets the revision to 0.

The major and minor numbers are never touched. This process ensures that during routine debugging and testing one doesn't forget to update the version numbers, especially when working with clients during UAT or quick post go-live patch updates.


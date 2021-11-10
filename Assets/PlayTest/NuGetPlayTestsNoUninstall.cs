using System;
using System.Collections;
using System.Runtime.InteropServices;
using NugetForUnity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Play mode tests allow us to install NuGet packages with Native code before play mode starts, then when play mode
/// runs the native libraries are available for use.
///
/// There seems to be some Unity internals that prevents an edit mode test from adding a Native library and finding it
/// in the same run. 
/// </summary>
public class NuGetPlayTestsNoUninstall : IPrebuildSetup, IPostBuildCleanup
{
    NugetPackageIdentifier sqlite = new NugetPackageIdentifier("SQLitePCLRaw.lib.e_sqlite3", "2.0.7");
    // Version of the SQLite library does not match the NuGet package version
    private readonly string _expectedVersion = "3.35.5";

    [UnityTest]
    public IEnumerator InstallAndRunSqlite()
    {
        yield return new WaitForFixedUpdate();

        // Test the actual library by calling a "extern" method
        var version = Marshal.PtrToStringAuto(sqlite3_libversion());
        Assert.That(version, Is.EqualTo(_expectedVersion));
    }

    /// <summary>
    /// Call to the SQLite native file, this actually tests loading and access the library when called
    /// </summary>
    /// <returns></returns>
    [DllImport("e_sqlite3")]
    private static extern IntPtr sqlite3_libversion();

    public void Setup()
    {
        try
        {
            sqlite3_libversion();
            Assert.Fail("e_sqlite3 dll loaded, but should not be available");
        }
        catch (DllNotFoundException)
        {
        }

        NugetHelper.InstallIdentifier(sqlite);
        Assert.IsTrue(NugetHelper.IsInstalled(sqlite), "The package was NOT installed: {0} {1}", sqlite.Id,
            sqlite.Version);
    }

    public void Cleanup()
    {
    }
}
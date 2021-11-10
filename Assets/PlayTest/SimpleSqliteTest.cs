using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayTest
{
    public class SimpleSqliteTest
    {

        private readonly string _expectedVersion = "3.35.5";

        [UnityTest]
        public IEnumerator CallSqlite()
        {
            yield return new WaitForFixedUpdate();

            // Test the actual library by calling a "extern" method
            
            var version = Marshal.PtrToStringAnsi(sqlite3_libversion());
            Assert.That(version, Is.EqualTo(_expectedVersion));
        }

        /// <summary>
        /// Call to the SQLite native file, this actually tests loading and access the library when called
        /// </summary>
        /// <returns></returns>
        [DllImport("e_sqlite3")]
        private static extern IntPtr sqlite3_libversion();

    }
}

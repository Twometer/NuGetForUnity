using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayTest
{
    public class Analyzer
    {
        [UnityTest]
        public IEnumerator AnalyzeConfig()
        {
            yield return new WaitForFixedUpdate();

            var plugin = AssetImporter.GetAtPath("Assets/Packages/SQLitePCLRaw.lib.e_sqlite3.2.0.7/runtimes/win-x64/native/e_sqlite3.dll") as PluginImporter;
            if (plugin == null)
            {
                Debug.LogError("Plugi not found");
            }
            else
            {
                Debug.Log("FOUND -> " + plugin.GetEditorData("CPU"));
            }
        }
    }
}

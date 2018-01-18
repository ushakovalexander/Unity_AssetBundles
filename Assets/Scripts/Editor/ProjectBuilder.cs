using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Diagnostics;

public class ProjectBuilder  {
  [MenuItem("Project Builder/Build Win32")]
  public static void BuildProjectWin32() {
    Build(BuildTarget.StandaloneWindows);
  }

  private static void Build(BuildTarget target) {
    BuildAssetBundles(target);
    BuildGame(target);
  }

  private static void BuildGame(BuildTarget target) {
    string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
    string[] levels = new string[] {"Assets/Main.unity"};

    BuildPipeline.BuildPlayer(levels, path + "/Game.exe", target, BuildOptions.None);

    Process proc = new Process();
    proc.StartInfo.FileName = path + "/Game.exe";
    proc.Start();
  }

  private static void BuildAssetBundles(BuildTarget target) {
    var bundleNames = AssetDatabase.GetAllAssetBundleNames();
    foreach (var name in bundleNames) {
      UnityEngine.Debug.Log ("AssetBundle: " + name);
    }
    BuildPipeline.BuildAssetBundles(CheckBundleTargetFolder(GetBundleTargetFolderPath()), BuildAssetBundleOptions.None, target);
  }

  private static string GetBundleTargetFolderPath() {
    return Application.dataPath + "/StreamingAssets";
  }

  private static string CheckBundleTargetFolder(string path) {
     if(!Directory.Exists(path)) {
       Directory.CreateDirectory(path);
     }
     return path;
  }
}

using UnityEditor;
using UnityEngine;
using System.Collections;

public class ExportAssetBundle : Editor {

    [MenuItem("Export/Export Selected Atlas")]
    public static void CreateImageBundle()
    {
        string path = EditorUtility.SaveFilePanel("Save ImageBundle", "", "", "");

        if (path.Length > 0)
        {
            path += "/";
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            BuildPipeline.BuildAssetBundle(null, selection, System.IO.Path.GetFileName(path) + ".dp",
                BuildAssetBundleOptions.CollectDependencies |
                BuildAssetBundleOptions.CompleteAssets |
                BuildAssetBundleOptions.UncompressedAssetBundle,
                BuildTarget.Android);
        }
        Debug.Log("Image Bundle Create");
    }
	
}

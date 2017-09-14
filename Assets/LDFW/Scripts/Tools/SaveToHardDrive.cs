using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LDFW.Tools
{
    
    public class SaveToHardDrive
    {
        public static void SaveAssetToFile(Object asset, string path, string extension = "asset")
        {
            if (asset == null)
            {
                Debug.LogError("asset is null!");
                return;
            }
            #if UNITY_EDITOR
            AssetDatabase.CreateAsset(asset, path + "." + extension);
            #endif
        }

        public static void SaveTextToFile(string text, string path)
        {
            System.IO.File.WriteAllText(path, text, System.Text.Encoding.ASCII);
        }
    }

}


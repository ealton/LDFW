using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LDFW.Tools
{
    
    /// <summary>
    /// Save an obj to file as a .asset file
    /// </summary>
    public class SaveToHardDrive
    {
        public static void SaveToFile(Object obj, string path)
        {
            if (obj == null)
            {
                Debug.LogError("Mesh is null!");
                return;
            }
            #if UNITY_EDITOR
            AssetDatabase.CreateAsset(obj, path + ".asset");
            #endif
        }
    }

}


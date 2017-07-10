using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LDFW.Tools
{
    
    public class SaveToHardDrive
    {
        public static void SaveToFile(Object mesh, string path)
        {
            if (mesh == null)
            {
                Debug.LogError("Mesh is null!");
                return;
            }
            #if UNITY_EDITOR
            AssetDatabase.CreateAsset(mesh, path + ".asset");
            #endif
        }
    }

}


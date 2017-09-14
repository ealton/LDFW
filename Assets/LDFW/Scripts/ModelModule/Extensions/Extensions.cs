using UnityEngine;
using System.Collections;

using LDFW.Extensions;

namespace LDFW.Model
{
    
    public static class Extensions
    {
        /// <summary>
        /// Print mesh data
        /// </summary>
        /// <param name="mesh"></param>
        public static void PrintMeshData(this Mesh mesh)
        {
            string result = "Vertices data: ";
            foreach (var vec in mesh.vertices)
                result += "\n " + vec.ToPreciseString();

            result += "\nTriangle data: ";
            int triangleCount = mesh.triangles.Length;
            for (int i = 0; i < triangleCount; i += 3)
                result += "\n" + mesh.triangles[i] + ", " + mesh.triangles[i + 1] + ", " + mesh.triangles[i + 2];

            result += "\nUV data: ";
            foreach (var vec in mesh.uv)
                result += "\n " + vec.ToString();

            Debug.Log(result);
        }
    }
    
}
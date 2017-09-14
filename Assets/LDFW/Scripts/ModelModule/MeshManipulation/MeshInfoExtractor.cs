using UnityEngine;
using System.Text;
using System.Collections;

using LDFW.Extensions;

namespace LDFW.Model
{

    public class MeshInfoExtractor : MonoBehaviour
    {
        
        /// <summary>
        /// Generate mesh vertices data
        /// </summary>
        /// <param name="targetMesh"></param>
        /// <returns></returns>
        public static string GetMeshVerticesData(Mesh targetMesh)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Vertices: \n");

            foreach (var vertex in targetMesh.vertices)
                builder.Append(vertex.ToPreciseString()).Append("\n");

            return builder.ToString();
        }

        /// <summary>
        /// Generate uv data
        /// </summary>
        /// <param name="targetMesh"></param>
        /// <returns></returns>
        public static string GetMeshUVData(Mesh targetMesh)
        {
            StringBuilder builder = new StringBuilder();
            if (targetMesh.uv != null)
                builder.Append("UV length: ").Append(targetMesh.uv.Length).Append('\n');
            else
                builder.Append("UV is null").Append('\n');

            if (targetMesh.uv2 != null)
                builder.Append("UV2 length: ").Append(targetMesh.uv2.Length).Append('\n');
            else
                builder.Append("UV2 is null").Append('\n');

            if (targetMesh.uv3 != null)
                builder.Append("UV3 length: ").Append(targetMesh.uv3.Length).Append('\n');
            else
                builder.Append("UV3 is null").Append('\n');

            if (targetMesh.uv4 != null)
                builder.Append("UV4 length: ").Append(targetMesh.uv4.Length).Append('\n');
            else
                builder.Append("UV4 is null").Append('\n');

            builder.Append("UV\n");
            if (targetMesh.uv != null)
                foreach (var uv in targetMesh.uv)
                    builder.Append(uv.ToString("0.000000")).Append('\n');

            builder.Append("UV2\n");
            if (targetMesh.uv2 != null)
                foreach (var uv in targetMesh.uv2)
                    builder.Append(uv.ToString("0.000000")).Append('\n');

            builder.Append("UV3\n");
            if (targetMesh.uv3 != null)
                foreach (var uv in targetMesh.uv3)
                    builder.Append(uv.ToString("0.000000")).Append('\n');

            builder.Append("UV4\n");
            if (targetMesh.uv4 != null)
                foreach (var uv in targetMesh.uv4)
                    builder.Append(uv.ToString("0.000000")).Append('\n');

            return builder.ToString();
        }

        /// <summary>
        /// Get triangle data
        /// </summary>
        /// <param name="targetMesh"></param>
        /// <returns></returns>
        public static string GetTriangleData(Mesh targetMesh)
        {
            StringBuilder builder = new StringBuilder();
            int triangleVertexCount = targetMesh.triangles.Length;
            for (int i = 0; i < triangleVertexCount; i += 3)
                builder.Append(targetMesh.triangles[i]).Append('-')
                    .Append(targetMesh.triangles[i + 1]).Append('-')
                    .Append(targetMesh.triangles[i + 2]).Append('\n');

            return builder.ToString();
        }

        /// <summary>
        /// Get submesh data
        /// </summary>
        /// <param name="targetMesh"></param>
        /// <returns></returns>
        public static string GetSubmeshData(Mesh targetMesh)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Submesh count: ").Append(targetMesh.subMeshCount).Append('\n');

            if (targetMesh.subMeshCount > 0)
            {
                for (int i = 0; i < targetMesh.subMeshCount; i++)
                {
                    int[] indices = targetMesh.GetIndices(i);
                    builder.Append(i).Append("'s SubMesh: ").Append(indices.Length).Append("\n");
                    foreach (var index in indices)
                        builder.Append(index).Append('\n');
                }
            }

            return builder.ToString();
        }

    }

}

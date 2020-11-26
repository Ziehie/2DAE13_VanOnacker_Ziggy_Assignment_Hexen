using UnityEngine;
using System.Collections.Generic;

namespace BoardSystem
{
    public class Tile : MonoBehaviour
    {
        public CubeIndex index;

        public static Vector3 GetCorner(Vector3 origin, float radius, int corner)
        {
            float angle = 60 * corner;
            angle *= Mathf.PI / 180;
            return new Vector3(origin.x + radius * Mathf.Cos(angle), 0.0f, origin.z + radius * Mathf.Sin(angle));
        }

        public static void GetHexMesh(float radius, ref Mesh mesh)
        {
            mesh = new Mesh();

            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            for (int i = 0; i < 6; i++)
                vertices.Add(GetCorner(Vector3.zero, radius, i));

            tris.Add(0);
            tris.Add(2);
            tris.Add(1);

            tris.Add(0);
            tris.Add(5);
            tris.Add(2);

            tris.Add(2);
            tris.Add(5);
            tris.Add(3);

            tris.Add(3);
            tris.Add(5);
            tris.Add(4);

            //UVs are incorrect
            uvs.Add(new Vector2(0.5f, 1f));
            uvs.Add(new Vector2(1, 0.75f));
            uvs.Add(new Vector2(1, 0.25f));
            uvs.Add(new Vector2(0.5f, 0));
            uvs.Add(new Vector2(0, 0.25f));
            uvs.Add(new Vector2(0, 0.75f));

            mesh.vertices = vertices.ToArray();
            mesh.triangles = tris.ToArray();
            mesh.uv = uvs.ToArray();

            mesh.name = "Hexagonal Plane";

            mesh.RecalculateNormals();
        }

        public static Tile operator+(Tile one, Tile two)
        {
            return new Tile { index = one.index + two.index };
        }

        public int MoveCost { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;
        public Tile Parent { get; set; }
    }


}

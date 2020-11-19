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

        public void SetLineColor(Color color)
        {
            LineRenderer lines = GetComponent<LineRenderer>();
            if (lines)
            {
                lines.startColor = color;
                lines.endColor = color;
            }
        }

        public void SetLineWidth(float width)
        {
            LineRenderer lines = GetComponent<LineRenderer>();
            if (lines)
            {
                lines.startWidth = width;
                lines.endWidth = width;
            }
        }

        public int MoveCost { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;
        public Tile Parent { get; set; }
    }

    [System.Serializable]
    public struct OffsetIndex
    {
        public int row, col;

        public OffsetIndex(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    [System.Serializable]
    public struct CubeIndex
    {
        public int x, y, z;
        public CubeIndex(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public CubeIndex(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.y = -x - z;
        }

        public static CubeIndex operator +(CubeIndex one, CubeIndex two)
        {
            return new CubeIndex(one.x + two.x, one.y + two.y, one.z + two.z);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CubeIndex cubeIdx = (CubeIndex)obj;

            return ((x == cubeIdx.x) && (y == cubeIdx.y) && (z == cubeIdx.z));
        }

        public override int GetHashCode()
        {
            return (x.GetHashCode() ^ (y.GetHashCode() + (int) (Mathf.Pow(2, 32) / (1 + Mathf.Sqrt(5)) / 2) +
                                       (x.GetHashCode() << 6) + (x.GetHashCode() >> 2)));
        }

        public override string ToString()
        {
            return string.Format("[" + x + "," + y + "," + z + "]");
        }
    }
}

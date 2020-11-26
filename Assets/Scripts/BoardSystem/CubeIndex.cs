using UnityEngine;

namespace BoardSystem
{
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
            return (x.GetHashCode() ^ (y.GetHashCode() + (int)(Mathf.Pow(2, 32) / (1 + Mathf.Sqrt(5)) / 2) +
                                       (x.GetHashCode() << 6) + (x.GetHashCode() >> 2)));
        }

        public override string ToString()
        {
            return string.Format("[" + x + "," + y + "," + z + "]");
        }
    }
}
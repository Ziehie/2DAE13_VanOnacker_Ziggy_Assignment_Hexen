using UnityEngine;

namespace GameSystem.Utils
{
    public class HexUtils
    {

        public static float ManhattanDistance((float x, float y, float z) a,
            (float x, float y, float z) b)
        {
            return (float)((Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2.0);
        }

        public static float StraightLineDistance((float x, float y, float z) a,
            (float x, float y, float z) b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2f) + Mathf.Pow(a.y - b.y, 2f) + Mathf.Pow(a.z - b.z, 2f));
        }
    }
}

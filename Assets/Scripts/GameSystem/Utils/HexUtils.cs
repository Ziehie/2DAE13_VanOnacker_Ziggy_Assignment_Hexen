using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Utils
{
    internal static class HexUtils
    {
        public static (float w, float h) PointyDimension(float size) => (Mathf.Sqrt(3f) * size, 2f * size);
    }
}

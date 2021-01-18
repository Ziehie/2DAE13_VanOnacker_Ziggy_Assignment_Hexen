using System.Collections.Generic;

namespace GameSystem.Utils
{
    public static class ListExtensions
    {
        public static T Random<T>(this List<T> list)
        {
            int idx = new System.Random().Next(list.Count);

            return list[idx];
        }
    }
}
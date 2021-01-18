using System.Collections.Generic;

namespace GameSystem.Utils
{
    public static class ListExtensions
    {
        public static T Random<T>(this List<T> list)
        {
            int index = new System.Random().Next(list.Count);

            return list[index];
        }
    }
}
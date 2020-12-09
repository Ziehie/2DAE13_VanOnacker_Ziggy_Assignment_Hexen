using BoardSystem;
using UnityEngine;

namespace GameSystem.Utils
{
    public static class PositionExtensions
    {
        public static Vector3 AsVector3(this Position position) //this indicates that something is being added to Board
        {
            return new Vector3(position.X, position.Y , position.Z);
        }
    }
}
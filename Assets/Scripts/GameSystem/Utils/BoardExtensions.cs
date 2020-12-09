using BoardSystem;
using UnityEngine;

namespace GameSystem.Utils
{
    public static class BoardExtensions
    {
        public static Vector3 AsVector3<TPiece>(this Board<TPiece> board) where TPiece : class, IAction<TPiece>
        {
            return new Vector3(board.Radius, 0.1f, board.Radius);
        }
    }
}
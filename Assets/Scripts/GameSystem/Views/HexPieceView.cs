using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public abstract class HexPieceView : MonoBehaviour, IAction
    {
        public abstract void Moved(Tile fromTile, Tile toTile);
        public abstract void Taken();
    }
}
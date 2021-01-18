using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public abstract class HexPieceView : MonoBehaviour, IAction
    {
        public Tile FinalPosition { get; set; }
        public abstract void Moved(Tile fromTile, Tile toTile);
        public abstract void Taken();
    }
}
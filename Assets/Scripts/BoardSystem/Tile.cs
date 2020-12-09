using System;
using UnityEngine;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HighlightStatusChanged;
        private bool _isHighlighted = false;
        public CubeIndex index;
        public Position Position { get; }

        public bool IsHighlighted
        {
            get => _isHighlighted;
            internal set { _isHighlighted = value; OnHighlightStatusChanged(EventArgs.Empty); }
        }

        public Tile(int x, int y, int z)
        {
            Position = new Position { X = x, Y = y, Z = z };
        }

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);
        }
    }
}

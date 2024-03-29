﻿using System;

namespace BoardSystem
{
    public class Tile
    {
        private bool _isHighlighted;

        public event EventHandler HighlightStatusChanged;
        public Position Position { get; }

        public bool IsHighlighted
        {
            get => _isHighlighted;
            internal set
            {
                _isHighlighted = value; 
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }

        public Tile(Position position) => Position = position;

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);
        }
    }
}

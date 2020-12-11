using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace BoardSystem
{
    public class Board<TPiece> where TPiece : class, IAction<TPiece>
    {
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();
        private List<Tile> _keys = new List<Tile>();
        private List<TPiece> _values = new List<TPiece>();
        public readonly int Radius;
        public List<Tile> Tiles => _tiles.Values.ToList();
        public List<Position> Positions => _tiles.Keys.ToList();

        public Board(int radius)
        {
            Radius = radius;

            InitiateTiles();
        }

        private void InitiateTiles()
        {
            for (var q = -Radius; q <= Radius; q++)
            {
                var r1 = Math.Max(-Radius, -q - Radius);
                var r2 = Math.Min(Radius, -q + Radius);

                for (var r = r1; r <= r2; r++)
                {
                    _tiles.Add(new Position { X = q, Y = r, Z = -q - r }, new Tile(new Position(r, -q - r)));
                }
            }
        }

        public Tile TileAt(Position position)
        {
            if (_tiles.TryGetValue(position, out var tile)) return tile;

            return null;
        }

        public Tile TileOf(TPiece piece)
        {
            var index = _values.IndexOf(piece);

            if (index == -1) return null;

            return _keys[index];
        }

        public TPiece PieceAt(Tile tile)
        {
            var index = _keys.IndexOf(tile);

            if (index == -1) return null;

            return _values[index];
        }

        public TPiece Take(Tile fromTile)
        {
            var index = _keys.IndexOf(fromTile);

            if (index == -1) return null;

            TPiece piece = _values[index];
            _values.RemoveAt(index);
            _keys.RemoveAt(index);
            piece.Taken();

            return piece;
        }

        public void Move(Tile fromTile, Tile toTile)
        {
            int index = _keys.IndexOf(fromTile);

            if (index == -1 || PieceAt(toTile) != null) return;

            _keys[index] = toTile;
            _values[index].Moved(fromTile, toTile);
        }

        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile) || _values.Contains(piece)) return;

            _keys.Add(toTile);
            _values.Add(piece);
        }

        public void Highlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = true;
            }
        }

        public void UnHighlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = false;
            }
        }
    }
}


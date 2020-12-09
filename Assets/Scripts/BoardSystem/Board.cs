using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace BoardSystem
{
    public class PiecePlacedEventArgs<TPiece> : EventArgs where TPiece : class, IAction<TPiece>
    {
        public TPiece Piece { get; }
        public PiecePlacedEventArgs(TPiece piece)
        {
            Piece = piece;
        }
    }

    public class Board<TPiece> where TPiece : class, IAction<TPiece>
    {
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();
        private List<Tile> _keys = new List<Tile>();
        private List<TPiece> _values = new List<TPiece>();

        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;
        public List<Tile> Tiles => _tiles.Values.ToList();

        public readonly int Radius;

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
                    _tiles.Add(new Position { X = q, Y = r, Z = -q - r }, new Tile(q, r, -q - r));
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

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> args)
        {
            EventHandler<PiecePlacedEventArgs<TPiece>> handler = PiecePlaced;
            handler?.Invoke(this, args);
        }

        public List<Tile> TilesInRange(Tile center, int range)
        {
            List<Tile> tiles = new List<Tile>();
            CubeIndex cubeIdx;

            for (int dx = -range; dx <= range; dx++)
            {
                for (int dy = Mathf.Max(-range, -dx - range); dy <= Mathf.Min(range, -dx + range); dy++)
                {
                    cubeIdx = new CubeIndex(dx, dy, -dx - dy) + center.index;
                    //if (_board.ContainsKey(cubeIdx.ToString()))
                    //{
                    //    tiles.Add(_board[cubeIdx.ToString()]);
                    //}
                }
            }
            return tiles;
        }

        public List<Tile> TilesInRange(Position position, int range)
        {
            return TilesInRange(TileAt(position), range);
        }

        public int Distance(CubeIndex a, CubeIndex b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }

        public int Distance(Tile a, Tile b)
        {
            return Distance(a.index, b.index);
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


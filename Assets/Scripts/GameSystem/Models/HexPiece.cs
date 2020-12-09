using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;

namespace GameSystem.Models
{
    public class HexPieceMovedEventArgs : EventArgs
    {
        public Board<HexPiece> Board { get; }
        public Tile From { get; }
        public Tile To { get; }

        public HexPieceMovedEventArgs(Board<HexPiece> board, Tile from, Tile to)
        {
            Board = board;
            From = from;
            To = to;
        }
    }

    public class HexPiece : IAction<HexPiece>
    {
        public event EventHandler<HexPieceMovedEventArgs> HexPieceMoved;
        public event EventHandler HexPieceTaken;
        public bool HasMoved { get; set; }
        public string MovementName { get; internal set; }

        public HexPiece(string movementName)
        {
            MovementName = movementName;
        }

        void IAction<HexPiece>.Moved(Board<HexPiece> board, Tile fromTile, Tile toTile)
        {
            OnHexPieceMoved(new HexPieceMovedEventArgs(board, fromTile, toTile));
        }

        void IAction<HexPiece>.Taken(Board<HexPiece> board)
        {
            OnHexPieceTaken(EventArgs.Empty);
        }

        protected virtual void OnHexPieceMoved(HexPieceMovedEventArgs args)
        {
            EventHandler<HexPieceMovedEventArgs> handler = HexPieceMoved;
            handler?.Invoke(this, args);
        }

        protected virtual void OnHexPieceTaken(EventArgs args)
        {
            EventHandler handler = HexPieceTaken;
            handler?.Invoke(this, args);
        }

        public override string ToString()
        {
            return $"{base.ToString()} - {MovementName}";
        }
    }
}

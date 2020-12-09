using System;
using BoardSystem;
using GameSystem.Models;
using UnityEngine;


namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {

        [SerializeField] private TileViewFactory _tileViewFactory;
        [SerializeField] private readonly HexPieceViewFactory _hexPieceViewFactory = null;
        private Board<HexPiece> _model;

        public Board<HexPiece> Model
        {
            get => _model;
            set
            {
                //if (_model == null) value.PiecePlaced -= OnPiecePlaced;

                _model = value;

                // (_model != null) value.PiecePlaced += OnPiecePlaced;
            }
        }

        private void OnDestroy()
        {
            Model = null;
        }
        private void OnPiecePlaced(object sender, PiecePlacedEventArgs<HexPiece> e)
        {
            var board = sender as Board<HexPiece>;
            var piece = e.Piece;

            _hexPieceViewFactory.CreateHexPieceView(board, piece);
        }
    }
}


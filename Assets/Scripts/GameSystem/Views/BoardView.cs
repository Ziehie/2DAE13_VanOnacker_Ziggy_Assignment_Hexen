using System;
using BoardSystem;
using GameSystem.Models;
using UnityEngine;


namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {

        [SerializeField] private TileViewFactory _tileViewFactory;
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
    }
}


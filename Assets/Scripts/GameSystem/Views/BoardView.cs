using System;
using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {

        [SerializeField] private TileViewFactory _tileViewFactory;
        private Board<HexPieceView> _model;

        public void Start()
        {
            GameLoop.Instance.Initialized += OnGameLoopInitialized;
        }

        private void OnGameLoopInitialized(object sender, EventArgs e)
        {
            _model = GameLoop.Instance.Board;
        }
    }
}


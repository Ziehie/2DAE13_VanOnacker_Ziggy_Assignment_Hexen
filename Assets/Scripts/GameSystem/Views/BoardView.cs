using System;
using BoardSystem;
using UnityEngine;
using Utils;


namespace GameSystem.Views
{
    public class BoardView : MonoBehaviour
    {

        [SerializeField] private TileViewFactory _tileViewFactory;
        private Board<HexPieceView> _model;

        public void Start()
        {
            SingletonMonobehaviour<GameLoop>.Instance.Initialized += OnGameLoopInitialized;
        }

        private void OnGameLoopInitialized(object sender, EventArgs e)
        {
            _model = SingletonMonobehaviour<GameLoop>.Instance.Board;
        }
    }
}


using System;
using GameSystem.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class HexPieceView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PositionHelper _positionHelper = null;
        [SerializeField] private string _movementName = null;
        private HexPiece _model;
        public string MovementName => _movementName;
        public HexPiece Model
        {
            get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.HexPieceMoved -= ModelMoved;
                    _model.HexPieceTaken -= ModelTaken;
                }

                _model = value;

                if (_model != null)
                {
                    _model.HexPieceMoved += ModelMoved;
                    _model.HexPieceTaken += ModelTaken;
                }
            }
        }

        private void ModelTaken(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void ModelMoved(object sender, HexPieceMovedEventArgs e)
        {
            var worldPosition = _positionHelper.ToWorldPosition(e.To.Position);
            transform.position = worldPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var gameLoop = GameLoop.Instance;
            //gameLoop.Select(Model);
        }

        private void OnDestroy()
        {
            Model = null;
        }
    }
}
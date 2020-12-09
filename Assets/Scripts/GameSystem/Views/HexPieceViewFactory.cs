using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using GameSystem.Models;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultHexPieceViewFactory", menuName = "GameSystem/HexPieceViewFactory")]
    public class HexPieceViewFactory : ScriptableObject
    {
        [SerializeField] private List<HexPieceView> _hexPieceViews = new List<HexPieceView>();
        [SerializeField] private List<string> _movementNames = new List<string>();
        [SerializeField] private PositionHelper _positionHelper = null;

        public HexPieceView CreateHexPieceView(Board<HexPiece> board, HexPiece model)
        {
            var index = this._movementNames.IndexOf(model.MovementName);
            var prefab = _hexPieceViews[index];

            var hexPieceView = GameObject.Instantiate<HexPieceView>(prefab);
            var tile = board.TileOf(model);

            hexPieceView.transform.position = _positionHelper.ToWorldPosition(board, tile.Position);
            hexPieceView.name = $"Spawned HexPiece ( {model.MovementName} )";
            hexPieceView.Model = model;

            return hexPieceView;
        }
    }
}
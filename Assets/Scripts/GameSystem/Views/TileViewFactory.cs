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
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/TileViewFactory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField] private TileView _tileView = null;
        [SerializeField] private PositionHelper _positionHelper = null;

        public TileView CreateTileView(Board<HexPiece> board, Tile tile, Transform parent)
        {
            var position = _positionHelper.ToWorldPosition(board, tile.Position);
            var tileView = Instantiate(_tileView, position, Quaternion.identity, parent);

            tileView.Size = _positionHelper.TileSize;
            tileView.name = $"Tile [ {tile.Position.X}, {tile.Position.Y}, {tile.Position.Z} ]";

            tileView.Model = tile;
            return tileView;
        }
    }
}
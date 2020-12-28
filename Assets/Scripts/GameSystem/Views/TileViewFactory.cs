using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultTileViewFactory", menuName = "GameSystem/TileViewFactory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField] private TileView _tileView = null;
        [SerializeField] private PositionHelper _positionHelper = null;

        public TileView CreateTileView(Tile tile, Transform parent)
        {
            var tileView = Instantiate(_tileView, _positionHelper.ToLocalPosition(tile.Position), Quaternion.identity, parent);

            tileView.Radius = _positionHelper.TileRadius;
            tileView.name = $"Tile [ {tile.Position.X}, {tile.Position.Y}, {tile.Position.Z} ]";

            return tileView;
        }
    }
}
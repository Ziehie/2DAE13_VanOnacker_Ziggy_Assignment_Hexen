using UnityEngine;
using BoardSystem;

namespace GameSystem.Views
{
    public class EnemyView : HexPieceView
    {
        [SerializeField] private PositionHelper _positionHelper = null;
        private Transform _boardView;

        private void Start()
        {
            _boardView = Object.FindObjectOfType<BoardView>().transform;
        }

        public override void Moved(Tile fromTile, Tile toTile)
        {
            transform.position = _positionHelper.ToWorldPosition(_boardView, toTile.Position);
        }

        public override void Taken()
        {
            Destroy(gameObject);
        }
    }
}

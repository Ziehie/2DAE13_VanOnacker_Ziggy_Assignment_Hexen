using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using GameSystem.BoardCalculations;

namespace GameSystem.Views
{
    public class MoveCalculationHelper
    {
        private Board<HexPieceView> _board;
        private HexPieceView _player;
        private BoardCalculationHelper _boardCalculationHelper;
        private List<EnemyView> _enemyViews;
        static Random rnd = new Random();

        public MoveCalculationHelper(Board<HexPieceView> board, HexPieceView player)
        {
            _board = board;
            _player = player;
            _boardCalculationHelper = new BoardCalculationHelper(board);
            _enemyViews = new List<EnemyView>(6);
        }

        public void MoveToFinalPosition()
        {
            foreach (var enemyView in _enemyViews)
            {
                if (enemyView.FinalPosition != null)
                {
                    _board.Move(_board.TileOf(enemyView), enemyView.FinalPosition);
                }
            }
        }

        public void UpdateFinalPositions()
        {
            var fromTile = _board.TileOf(_player); 
            var tileList = _boardCalculationHelper.GetRadius(fromTile, 1);
            var positionsList = _boardCalculationHelper.GetBFSPositions(fromTile, 2);
            
            bool IsPieceAt(Tile tile) => _board.PieceAt(tile) == null;
            var list = positionsList.Where(IsPieceAt).ToList();

            int idx = rnd.Next(list.Count);
            
            foreach (var enemyView in _enemyViews)
            {
                if (enemyView != _player)
                {
                    enemyView.FinalPosition = null;

                    var tile1 = _board.TileOf(enemyView);
                    if (!tileList.Contains(tile1))
                    {
                        if (list.Count <= 0) break;

                        var tile2 = list[idx];

                        list.Remove(tile2);

                        enemyView.FinalPosition = tile2;
                    }
                }
            }
        }

    }
}

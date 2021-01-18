using System.Collections.Generic;
using System.Linq;
using BoardSystem;
using GameSystem.BoardCalculations;
using GameSystem.Utils;
using UnityEngine;

namespace GameSystem.Views
{
    public class MoveCalculationHelper
    {
        private readonly Board<HexPieceView> _board;
        private readonly HexPieceView _player;
        private readonly BoardCalculationHelper _boardCalculationHelper;
        private List<EnemyView> _enemyViews;

        public MoveCalculationHelper(Board<HexPieceView> board, HexPieceView player)
        {
            _board = board;
            _player = player;
            _boardCalculationHelper = new BoardCalculationHelper(board);
        }

        public void MoveToFinalPosition()
        {
            _enemyViews = Object.FindObjectsOfType<EnemyView>().ToList();

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
            var positionsList = _boardCalculationHelper.GetPositions(fromTile, 2);

            bool IsPieceAt(Tile tile) => _board.PieceAt(tile) == null; //local function 
            var list = positionsList.Where(IsPieceAt).ToList();

            foreach (var enemyView in _enemyViews)
            {
                if (enemyView != _player)
                {
                    enemyView.FinalPosition = null;

                    var tile1 = _board.TileOf(enemyView);
                    if (!tileList.Contains(tile1))
                    {
                        if (list.Count <= 0) break;

                        var tile2 = list.Random();

                        list.Remove(tile2);

                        enemyView.FinalPosition = tile2;
                    }
                }
            }
        }
    }
}

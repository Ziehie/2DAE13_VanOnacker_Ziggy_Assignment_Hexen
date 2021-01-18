using System.Collections.Generic;
using BoardSystem;
using GameSystem.BoardCalculations;
using GameSystem.Utils;
using GameSystem.Views;

namespace GameSystem.Abilities
{
    [Ability("PathFind")]
    public class SearchPathAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board;
        private AStarPathFinding _pathFinder;
        private BoardCalculationHelper _boardCalculationHelper;

        public SearchPathAbility(Board<HexPieceView> board)
        {
            _board = board;
            _boardCalculationHelper = new BoardCalculationHelper(_board);
            //_pathFinder = new AStarPathFinding(_boardCalculationHelper.GetNeighbours(new Tile(new Position())), _boardCalculationHelper.Distance, _boardCalculationHelper.Distance);
        }

        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            var validTiles = _boardCalculationHelper.GetNeighbours(playerTile);

            return !validTiles.Contains(holdTile) ? validTiles : _pathFinder.Path(playerTile, holdTile);
        }

        public override void OnTileRelease(Tile playerTile, Tile holdTile)
        {
            if (!OnTileHold(playerTile, holdTile).Contains(holdTile)) return;
            _board.Move(playerTile, holdTile);
        }
    }
}
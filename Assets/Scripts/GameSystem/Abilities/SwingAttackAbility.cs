using System.Collections.Generic;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.BoardCalculations;
using GameSystem.Views;

namespace Assets.Scripts.GameSystem.Abilities
{
    [Ability("SwingAttack")]
    public class SwingAttackAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board;
        private readonly BoardCalculationHelper _boardCalculationHelper;

        public SwingAttackAbility(Board<HexPieceView> board)
        {
            _board = board;
            _boardCalculationHelper = new BoardCalculationHelper(board);
        }

        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            var tileList = _boardCalculationHelper.GetRadius(playerTile, 1);

            if (!tileList.Contains(holdTile)) return tileList;

            int idx1 = tileList.IndexOf(holdTile);
            int idx2 = _boardCalculationHelper.TrueModulo(idx1 - 1, tileList.Count - 1);
            int idx3 = _boardCalculationHelper.TrueModulo(idx1 + 1, tileList.Count - 1);

            return new List<Tile>()
            {
                tileList[idx2],
                tileList[idx1],
                tileList[idx3]
            };
        }

        public override void OnTileRelease(Tile playerTile, Tile holdTile)
        {
            var tileList = OnTileHold(playerTile, holdTile);

            if (!tileList.Contains(holdTile)) return;

            foreach (var fromTile in tileList)
            {
                _board.Take(fromTile);
            }
        }
    }
}

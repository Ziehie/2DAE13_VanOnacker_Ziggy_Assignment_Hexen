using System;
using System.Collections.Generic;
using System.Linq;
using BoardSystem;
using GameSystem.BoardCalculations;
using GameSystem.Views;
using UnityEngine;

namespace GameSystem.Abilities
{
    [Ability("ForwardAttack")]
    public class ForwardAttackAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board;
        private readonly BoardCalculationHelper _boardCalculationHelper;

        public ForwardAttackAbility(Board<HexPieceView> board)
        {
            _board = board;
            _boardCalculationHelper = new BoardCalculationHelper(board);
        }

        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
           var tileList1 = new List<Tile>();

            foreach (var direction in (OffsetDirection[])Enum.GetValues(typeof(OffsetDirection)))
            {
                var tileList2 = _boardCalculationHelper.GetLines(playerTile, direction);
                if (tileList2.Contains(holdTile))
                {
                    return tileList2;
                }
                tileList1.AddRange(tileList2);
            }
            return tileList1;
        }

        public override void OnTileRelease(Tile playerTile, Tile holdTile)
        {
            var tileList = OnTileHold(playerTile, holdTile);

            if (!tileList.Contains(holdTile)) return;

            foreach (var tile in tileList.Where(tile => _board.PieceAt(tile) != null))
            {
                _board.Take(tile);
            }
        }
    }
}

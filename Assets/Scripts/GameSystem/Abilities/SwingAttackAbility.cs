using System;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.BoardCalculations;
using GameSystem.Views;
using UnityEngine;

namespace Assets.Scripts.GameSystem.Abilities
{
    [Ability("SwingAttack")]
    public class SwingAttackAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board;
        private readonly BoardCalculationHelper _boardCalculationHelper;

        public SwingAttackAbility(Board<HexPieceView> board)
        {
            _boardCalculationHelper = new BoardCalculationHelper(board);
            _board = board;
        }

        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            var tileList = _boardCalculationHelper.GetRadius(playerTile, 1);

            if (!tileList.Contains(holdTile)) return tileList;

            int index1 = tileList.IndexOf(holdTile);
            int index2 = _boardCalculationHelper.TrueModulo(index1 - 1, tileList.Count - 1);
            int index3 = _boardCalculationHelper.TrueModulo(index1 + 1, tileList.Count - 1);

            return new List<Tile>()
            {
                tileList[index2],
                tileList[index1],
                tileList[index3]
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

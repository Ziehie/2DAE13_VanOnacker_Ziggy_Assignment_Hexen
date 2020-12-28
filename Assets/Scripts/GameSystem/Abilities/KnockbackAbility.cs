﻿using System.Collections.Generic;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.BoardCalculations;
using GameSystem.Views;

namespace Assets.Scripts.GameSystem.Abilities
{
    [Ability("Knockback")]
    public class KnockbackAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board;
        private readonly BoardCalculationHelper _boardCalculationHelper;

        public KnockbackAbility(Board<HexPieceView> board)
        {
            _boardCalculationHelper = new BoardCalculationHelper(board);
            _board = board;
        }
        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            var tileList = _boardCalculationHelper.GetRadius(playerTile, 1);

            if (!tileList.Contains(holdTile))
            {
                return tileList;
            }
            
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
            List<Tile> tileList = OnTileHold(playerTile, holdTile);
            if (!tileList.Contains(holdTile)) return;

            Position position1 = playerTile.Position;
            foreach (Tile fromTile in tileList)
            {
                Position position2 = fromTile.Position;
                int num1 = position2.X - position1.X;
                int num2 = position2.Y - position1.Y;
                int num3 = position2.Z - position1.Z;

                var toTile = _board.TileAt(new Position(position2.X + num1, position2.Y + num2, position2.Z + num3));
                if (toTile == null)
                {
                    _board.Take(fromTile);
                }
                else
                {
                    _board.Move(fromTile, toTile);
                }
            }
        }
    }
}

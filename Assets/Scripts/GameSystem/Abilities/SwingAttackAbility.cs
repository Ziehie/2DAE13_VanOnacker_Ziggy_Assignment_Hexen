using System;
using System.Collections.Generic;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.Views;
using UnityEngine;

namespace Assets.Scripts.GameSystem.Abilities
{
    [Ability("SwingAttack")]
    public class SwingAttackAbility : AbilityBase
    {
        private readonly Board<HexPieceView> _board = null;
        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            List<Tile> tileList = new List<Tile>();

            if (_board.PieceAt(holdTile) == null)
            {
                tileList.Add(holdTile);
            }
            return tileList;
        }

        public override void OnTileRelease(Tile playerTile, Tile holdTile)
        {
            Debug.Log("SwingAttack");
        }
    }
}

using System;
using System.Collections.Generic;
using BoardSystem;

namespace GameSystem.Abilities
{
    [Ability("ForwardAttack")]
    public class ForwardAttackAbility : AbilityBase
    {
        public override List<Tile> OnTileHold(Tile playerTile, Tile holdTile)
        {
            throw new NotImplementedException();
        }

        public override void OnTileRelease(Tile playerTile, Tile holdTile)
        {
            throw new NotImplementedException();
        }
    }
}

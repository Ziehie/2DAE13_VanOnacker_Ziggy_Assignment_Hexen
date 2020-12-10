using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using GameSystem.Abilities;

namespace Assets.Scripts.GameSystem.Abilities
{
    [Ability("SwingAttack")]
    public class SwingAttackAbility : AbilityBase
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

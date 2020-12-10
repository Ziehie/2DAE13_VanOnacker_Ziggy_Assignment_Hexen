using System.Collections.Generic;
using BoardSystem;

namespace GameSystem.Abilities
{
    public abstract class AbilityBase
    {
        public abstract List<Tile> OnTileHold(Tile playerTile, Tile holdTile);

        public abstract void OnTileRelease(Tile playerTile, Tile holdTile);
    }
}

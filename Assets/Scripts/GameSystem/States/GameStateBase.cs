using BoardSystem;
using StateSystem;

namespace GameSystem.States
{
    public abstract class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; set; }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void OnEnterTile(Tile holdTile) { }

        public virtual void OnExitTile(Tile holdTile) { }

        public virtual void OnAbilityBeginDrag(string ability) { }

        public virtual void OnAbilityReleased(Tile holdTile) { }

        public virtual void EndTurn() { }
    }
}
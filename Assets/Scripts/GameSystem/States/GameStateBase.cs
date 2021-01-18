﻿using BoardSystem;
using StateSystem;

namespace GameSystem.States
{
    public abstract class GameStateBase : IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; set; }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void OnEnterTile() { }

        public virtual void OnExitTile() { }

        public virtual void OnAbilityBeginDrag(string ability) { }

        public virtual void OnAbilityHoldActivity(Tile holdTile, string ability, bool active) { }

        public virtual void OnAbilityReleased(string ability, Tile holdTile) { }

        public virtual void EndTurn() { }
    }
}
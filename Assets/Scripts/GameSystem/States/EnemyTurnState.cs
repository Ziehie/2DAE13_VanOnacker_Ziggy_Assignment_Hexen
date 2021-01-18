﻿using BoardSystem;
using GameSystem.Views;

namespace GameSystem.States
{
    public class EnemyTurnState : GameStateBase
    {
        private MoveCalculationHelper _moveCalculationHelper;
        private EnemyTurnState(Board<HexPieceView> board, HexPieceView player)
        {
            _moveCalculationHelper = new MoveCalculationHelper(board, player);
        }

        public override void OnEnter()
        {
            _moveCalculationHelper.MoveToFinalPosition();
            _moveCalculationHelper.UpdateFinalPositions();
            StateMachine.MoveTo(GameStates.Player);
        }
    }
}
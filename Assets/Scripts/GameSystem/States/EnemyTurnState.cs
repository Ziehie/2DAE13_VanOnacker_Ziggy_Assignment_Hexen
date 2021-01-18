using BoardSystem;
using GameSystem.Views;

namespace GameSystem.States
{
    public class EnemyTurnState : GameStateBase
    {
        private Board<HexPieceView> _board;
        private MoveCalculationHelper _moveCalculationHelper;
        private EnemyTurnState(Board<HexPieceView> board, PlayerView player)
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
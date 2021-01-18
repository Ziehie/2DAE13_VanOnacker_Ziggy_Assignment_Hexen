using System.Collections.Generic;
using AbilitySystem;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.BoardCalculations;
using GameSystem.Views;
using UnityEngine;

namespace GameSystem.States
{
    public class PlayerTurnState : GameStateBase
    {
        private PlayerView _player;
        private List<Tile> _validTiles = new List<Tile>();
        private AbilityBase _draggedAbility;
        private string _ability;
        private Board<HexPieceView> _board;
        private Pile<AbilityBase> _pile;
        private ActiveHand<AbilityBase> _activeHand;
        private int _amountOfAbilitiesUsed;
        private BoardCalculationHelper _boardCalculationHelper;

        public PlayerTurnState(Board<HexPieceView> board, Pile<AbilityBase> pile, ActiveHand<AbilityBase> activeHand, PlayerView player)
        {
            _board = board;
            _pile = pile;
            _activeHand = activeHand;
            _player = player;
            _boardCalculationHelper = new BoardCalculationHelper(board);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _amountOfAbilitiesUsed = 0;
        }

        public override void OnEnterTile(Tile holdTile)
        {
            if (_draggedAbility != null)
            {
                _validTiles = _draggedAbility.OnTileHold(_board.TileOf(_player), holdTile);

                _board.Highlight(_validTiles);
            }
            else
            {
                var hexPieceView = _board.PieceAt(holdTile);

                if (hexPieceView == null || hexPieceView.FinalPosition == null) return;

                _validTiles = _boardCalculationHelper.GetPositions(holdTile, hexPieceView.FinalPosition);

                _board.Highlight(_validTiles);
            }
        }

        public override void OnExitTile(Tile holdTile)
        {
            _board.UnHighlight(_validTiles);
            _validTiles.Clear();
        }

        public override void OnAbilityBeginDrag(string ability)
        {
            _ability = ability;
            _draggedAbility = _pile.GetAbilityAction(ability);
        }

        public override void OnAbilityReleased(Tile holdTile)
        {
            if (_draggedAbility == null) return;

            _board.UnHighlight(_validTiles);

            if (!_validTiles.Contains(holdTile))
            {
                _draggedAbility = null;
            }
            else
            {
                _draggedAbility.OnTileRelease(_board.TileOf(_player), holdTile);
                _activeHand.RemoveAbility(_ability);
                _activeHand.InitializeActiveHand();
                _draggedAbility = null;
                _ability = null;
                ++_amountOfAbilitiesUsed;
            }
            _validTiles.Clear();
            if (_amountOfAbilitiesUsed <= 1) return;

            StateMachine.MoveTo(GameStates.Enemy);
        }


    }
}
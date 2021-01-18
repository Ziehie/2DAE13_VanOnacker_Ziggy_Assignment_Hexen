﻿using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using Assets.Scripts.GameSystem.Abilities;
using GameSystem.Views;
using UnityEngine;
using BoardSystem;
using GameSystem.Abilities;
using GameSystem.States;
using StateSystem;
using Utils;

namespace GameSystem
{
    public class GameLoop : SingletonMonobehaviour<GameLoop>
    {
        [SerializeField] private PositionHelper _positionHelper = null;

        private BoardView _boardView;
        private PlayerView _playerView;

        public event EventHandler Initialized;

        public Board<HexPieceView> Board = new Board<HexPieceView>(3);
        public Pile<AbilityBase> Pile { get; private set; }
        public ActiveHand<AbilityBase> ActiveHand { get; set; }

        private StateMachine<GameStateBase> _gameStateMachine;

        private void Start()
        {
            Pile = new Pile<AbilityBase>();
            AddAbilities();

            ActiveHand = Pile.CreateActiveHand(5);
            _boardView = FindObjectOfType<BoardView>();

            ConnectPlayer();
            ConnectEnemies();



            StartCoroutine(PostStart());
        }

        private IEnumerator PostStart()
        {
            yield return new WaitForEndOfFrame();
            OnInitialized(EventArgs.Empty);
        }

        protected virtual void OnInitialized(EventArgs arg)
        {
            EventHandler handler = Initialized;
            handler?.Invoke(this, arg);
        }

        private void AddAbilities()
        {
            Pile.AddAbilityAction("ForwardAttack", new ForwardAttackAbility(Board));
            Pile.AddAbilityAction("SwingAttack", new SwingAttackAbility(Board));
            Pile.AddAbilityAction("Teleport", new TeleportAbility(Board));
            Pile.AddAbilityAction("Knockback", new KnockbackAbility(Board));

            Pile.AddAbility("ForwardAttack", 3);
            Pile.AddAbility("SwingAttack", 3);
            Pile.AddAbility("Teleport", 3);
            Pile.AddAbility("Knockback", 3);
        }

        internal void OnEnterTile(Tile holdTile) => _gameStateMachine.CurrentState.OnEnterTile(holdTile);
        internal void OnExitTile(Tile holdTile) => _gameStateMachine.CurrentState.OnExitTile(holdTile);
        internal void OnAbilityBeginDrag(string ability) => _gameStateMachine.CurrentState.OnAbilityBeginDrag(ability);
        internal void OnAbilityReleased(Tile holdTile) => _gameStateMachine.CurrentState.OnAbilityReleased(holdTile);
        internal void EndTurn() => _gameStateMachine.CurrentState.EndTurn();

        private void ConnectPlayer()
        {
            _playerView = FindObjectOfType<PlayerView>();
            Board.Place(Board.TileAt(_positionHelper.ToBoardPosition(_boardView.transform, _playerView.transform.position)), _playerView);
        }

        private void ConnectEnemies()
        {
            foreach (var enemyView in FindObjectsOfType<EnemyView>())
            {
                Board.Place(Board.TileAt(_positionHelper.ToBoardPosition(_boardView.transform, enemyView.transform.position)), enemyView);
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using Assets.Scripts.GameSystem.Abilities;
using GameSystem.Views;
using UnityEngine;
using BoardSystem;
using GameSystem.Abilities;
using Utils;

namespace GameSystem
{
    public class GameLoop : SingletonMonobehaviour<GameLoop>
    {
        [SerializeField] private PositionHelper _positionHelper = null;

        private BoardView _boardView;
        private PlayerView _playerView;
        private List<Tile> _validTiles = new List<Tile>();

        private AbilityBase _draggedAbility;

        public Board<HexPieceView> Board = new Board<HexPieceView>(3);
        public Pile<AbilityBase> Pile { get; private set; }
        public ActiveHand<AbilityBase> ActiveHand { get; set; }

        public event EventHandler Initialized;

        private void Start()
        {
            Pile = new Pile<AbilityBase>();
            AddAbilities();

            ActiveHand = Pile.CreateActiveHand(5);
            _boardView = FindObjectOfType<BoardView>();

            //ConnectPlayer();
            //ConnectEnemies();

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
            Pile.AddAbilityAction("ForwardAttack", new ForwardAttackAbility());
            Pile.AddAbilityAction("SwingAttack", new SwingAttackAbility());
            Pile.AddAbilityAction("Teleport", new TeleportAbility());
            Pile.AddAbilityAction("Knockback", new KnockbackAbility());
            Pile.AddAbility("ForwardAttack", 3);
            Pile.AddAbility("SwingAttack", 3);
            Pile.AddAbility("Teleport", 3);
            Pile.AddAbility("Knockback", 3);
        }

        internal void OnAbilityBeginDrag(string ability)
        {
            _draggedAbility = Pile.GetAbilityAction(ability);
        }

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


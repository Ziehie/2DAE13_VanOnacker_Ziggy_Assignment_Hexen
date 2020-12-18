using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using GameSystem.Views;
using UnityEngine;
using BoardSystem;

using GameSystem.Abilities;
using Utils;

public class GameLoop : SingletonMonobehaviour<GameLoop>
{
    
    [SerializeField] private PositionHelper _positionHelper = null;

    private BoardView _boardView;
    private PlayerView _playerView;
    private List<Tile> _validTiles = new List<Tile>();

    private AbilityBase _draggedAbility;

    public Board<HexPieceView> Board = new Board<HexPieceView>(3);
    public Pile<AbilityBase> Pile { get; set; }
    public ActiveHand<AbilityBase> ActiveHand { get; set; }

    public event EventHandler Initialized;

    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
    }

    internal void OnAbilityBeginDrag(string ability)
    {
        _draggedAbility = Pile.GetAbilityAction(ability);
    }
}

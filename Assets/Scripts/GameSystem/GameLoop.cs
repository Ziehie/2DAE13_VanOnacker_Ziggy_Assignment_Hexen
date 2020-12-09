using System;
using System.Collections;
using System.Collections.Generic;
using GameSystem.Views;
using UnityEngine;
using BoardSystem;
using GameSystem.Models;
using MoveSystem;
using Utils;

public class GameLoop : SingletonMonobehaviour<GameLoop>
{
    public event EventHandler Initialized;
    //[SerializeField] private PositionHelper _positionHelper = null;

    private HexPiece _selectedPiece = null;
    private List<Tile> _validTiles = new List<Tile>();
    private IMoveCommand<HexPiece> _currentMoveCommand;

    public MoveManager<HexPiece> MoveManager { get; internal set; }

    public Board<HexPiece> Board { get; } = new Board<HexPiece>(3);
    public HexPiece SelectedPiece => _selectedPiece;

    protected virtual void OnInitialized(EventArgs arg)
    {
        EventHandler handler = Initialized;
        handler?.Invoke(this, arg);
    }
}

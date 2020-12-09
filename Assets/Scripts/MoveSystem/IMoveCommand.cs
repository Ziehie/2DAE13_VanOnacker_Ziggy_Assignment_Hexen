﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardSystem;

namespace MoveSystem
{
    public interface IMoveCommand<TPiece> where TPiece : class, IAction<TPiece>
    {
        bool CanExecute(Board<TPiece> board, TPiece piece);
        List<Tile> Tiles(Board<TPiece> board, TPiece piece);

        void Execute(Board<TPiece> board, TPiece piece, Tile toTile);
    }
}
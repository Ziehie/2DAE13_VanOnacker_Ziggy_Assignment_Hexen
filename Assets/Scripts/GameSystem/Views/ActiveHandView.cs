﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public class ActiveHandView : HexPieceView
    {
        [SerializeField] private PositionHelper _positionHelper = null;
        private Transform _boardView;

        private void Start()
        {
            _boardView = Object.FindObjectOfType<BoardView>().transform;
        }

        public override void Moved(Tile fromTile, Tile toTile)
        {
            transform.position = _positionHelper.ToWorldPosition(_boardView, toTile.Position);
        }

        public override void Taken()
        {
            Destroy(gameObject);
        }
    }
}

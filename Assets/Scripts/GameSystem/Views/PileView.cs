using System;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using GameSystem.Abilities;
using Utils;

namespace GameSystem.Views
{
    public class PileView : MonoBehaviour
    {
        private Pile<AbilityBase> _model;

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameLoopInitialized;
        }

        private void InitializeAbilities()
        {
           var abilities = _model.Abilities;
        }

        private void OnGameLoopInitialized(object sender, EventArgs e)
        {
            _model = GameLoop.Instance.Pile;
            InitializeAbilities();
        }
    }
}

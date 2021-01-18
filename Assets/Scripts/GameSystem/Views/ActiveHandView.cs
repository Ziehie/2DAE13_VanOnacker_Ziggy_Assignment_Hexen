using System;
using System.Collections.Generic;
using AbilitySystem;
using GameSystem.Abilities;
using UnityEngine;

namespace GameSystem.Views
{
    public class ActiveHandView : MonoBehaviour
    {
        [SerializeField] private AbilityViewFactory _abilityViewFactory = null;
        private ActiveHand<AbilityBase> _model;
        private readonly List<string> _abilities = new List<string>();
        private readonly List<AbilityView> _abilityViews = new List<AbilityView>();

        private void Start()
        {
            GameLoop.Instance.Initialized += OnGameLoopInitialized;
        }

        private void OnGameLoopInitialized(object sender, EventArgs e)
        {
           _model = GameLoop.Instance.ActiveHand;
           _model.AbilityAdded += OnAbilityAdded;
           _model.AbilityRemoved += OnAbilityRemoved;

           InitializeAbilityView();
        }

        private void OnAbilityRemoved(object sender, AbilityEventArgs e)
        {
            int index = _abilities.IndexOf(e.Ability);
            var abilityView = _abilityViews[index];

            _abilities.RemoveAt(index);
            _abilityViews.RemoveAt(index);
            abilityView.Destroy();
        }

        private void OnAbilityAdded(object sender, AbilityEventArgs e)
        {
            InitializeAbilityView(e.Ability);
        }

        public void InitializeAbilityView()
        {
            foreach (string ability in _model.Abilities)
            {
                InitializeAbilityView(ability);
            }
        }

        private void InitializeAbilityView(string ability)
        {
            var view = _abilityViewFactory.CreateAbilityView(transform, ability);

            view.transform.SetParent(transform);
            view.name = $"Ability ( {ability} )";

            _abilities.Add(ability);
            _abilityViews.Add(view);
        }
    }
}

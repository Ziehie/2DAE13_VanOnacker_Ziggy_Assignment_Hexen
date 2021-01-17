using System;
using System.Collections.Generic;

namespace AbilitySystem
{
    public class ActiveHand<TAbilityAction>
    {
        private readonly Pile<TAbilityAction> _pile;
        private readonly int _maxAmountOfAbilities;

        public event EventHandler<AbilityEventArgs> AbilityAdded;
        public event EventHandler<AbilityEventArgs> AbilityRemoved;

        public List<string> Abilities { get; } = new List<string>();

        internal ActiveHand(Pile<TAbilityAction> pile, int maxAmountOfAbilities)
        {
            _pile = pile;
            _maxAmountOfAbilities = maxAmountOfAbilities;
            InitializeActiveHand();
        }

        protected virtual void OnAbilityAdded(AbilityEventArgs args)
        {
            EventHandler<AbilityEventArgs> handler = AbilityAdded;
            handler?.Invoke(this, args);
        }

        protected virtual void OnAbilityRemoved(AbilityEventArgs args)
        {
            EventHandler<AbilityEventArgs> handler = AbilityRemoved;
            handler?.Invoke(this, args);
        }

        public void AddAbility(string ability)
        {
            Abilities.Add(ability);
            OnAbilityAdded(new AbilityEventArgs(ability));
        }

        public void RemoveAbility(string ability)
        {
            Abilities.Remove(ability);
            OnAbilityRemoved(new AbilityEventArgs(ability));
        }

        public void InitializeActiveHand()
        {
            for (int count = Abilities.Count; count < _maxAmountOfAbilities; count++)
            {
                if (_pile.TryTakeAbility(out string ability))
                {
                    AddAbility(ability);
                }
            }
        }
    }
}

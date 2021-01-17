using System;
using System.Collections.Generic;
using System.Linq;

namespace AbilitySystem
{
    public class Pile<TAbilityAction>
    {
        private readonly Dictionary<string, TAbilityAction> _abilityActions = new Dictionary<string, TAbilityAction>();
        private readonly Random _random = new Random();
        public List<string> Abilities { get; } = new List<string>();

        public void AddAbility(string ability, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Abilities.Add(ability);
            }
        }

        public void AddAbilityAction(string ability, TAbilityAction tAbilityAction)
        {
            if (_abilityActions.ContainsKey(ability)) return;
            _abilityActions.Add(ability, tAbilityAction);
        }

        public ActiveHand<TAbilityAction> CreateActiveHand(int maxAmountOfAbilities)
        {
            return new ActiveHand<TAbilityAction>(this, maxAmountOfAbilities);
        }

        public bool TryTakeAbility(out string ability)
        {
            ability = null;

            if (Abilities.Count == 0) return false;

            int idx = _random.Next(Abilities.Count);
            ability = Abilities.ElementAt(idx);

            Abilities.RemoveAt(idx);

            return true;
        }

        public TAbilityAction GetAbilityAction(string ability) => _abilityActions.ContainsKey(ability) ? _abilityActions[ability] : default;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem
{
    public class Pile<TAbilityAction>
    {
        private List<string> _abilities = new List<string>(); 
        private Dictionary<string, TAbilityAction> _abilityActions = new Dictionary<string, TAbilityAction>();

        public List<string> Abilities => _abilities;

        public void AddAbility(string ability, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                _abilities.Add(ability);
            }
        }

        public void AddAbilityActions(string ability, TAbilityAction tAbilityAction)
        {
            if (_abilityActions.ContainsKey(ability))
                return;
            _abilityActions.Add(ability, tAbilityAction);
        }

        public ActiveHand<TAbilityAction> CreateActiveHand(int maxAmountOfAbilities)
        {
            return new ActiveHand<TAbilityAction>(this, maxAmountOfAbilities);
        }

        public bool TryTakeAbility(out string ability)
        {
            Random random = new Random();
            ability = null;

            if (_abilities.Count == 0) return false;

            int idx = random.Next(_abilities.Count);
            ability = _abilities.ElementAt(idx);

            _abilities.RemoveAt(idx);

            return true;
        }

        public TAbilityAction GetAbilityAction(string ability) => _abilityActions.ContainsKey(ability) ? _abilityActions[ability] : default;
    }
}

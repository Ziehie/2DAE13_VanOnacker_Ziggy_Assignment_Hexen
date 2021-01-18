using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultAbilityViewFactory", menuName = "GameSystem/AbilityViewFactory")]
    public class AbilityViewFactory : ScriptableObject
    {
        [SerializeField] private List<AbilityView> _abilityViews = new List<AbilityView>();
        [SerializeField] private List<string> _abilityNames = new List<string>();

        public AbilityView CreateAbilityView(Transform transform, string ability)
        {
            int idx = _abilityNames.IndexOf(ability);

            if (idx == -1)
            {
                Debug.Log("No name found for " + ability);
            }
            var abilityView = Instantiate(_abilityViews.ElementAt(idx), transform);

            abilityView.Model = ability;
            abilityView.name = ability;
            return abilityView;
        }
    }
}

using System;

namespace GameSystem.Abilities
{
    public class AbilityAttribute : Attribute
    {
        public string Name;

        public AbilityAttribute(string name)
        {
            Name = name;
        }
    }
}

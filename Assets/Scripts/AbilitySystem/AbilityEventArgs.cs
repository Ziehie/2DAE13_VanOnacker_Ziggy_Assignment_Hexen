using System;

namespace AbilitySystem
{
    public class AbilityEventArgs : EventArgs 
    {
        public string Ability { get; }

        public AbilityEventArgs(string ability)
        {
            Ability = ability;
        }
    }
}

using System.Collections.Generic;

namespace StateSystem
{
    public class StateMachine<TState> where TState : IState<TState>
    {
        private readonly Dictionary<string, TState> _states = new Dictionary<string, TState>();
        public TState CurrentState { get; internal set; }

        public void RegisterState(string name, TState state)
        {
            state.StateMachine = this;
            _states.Add(name, state);
        }

        public void MoveTo(string name)
        {
            var state = _states[name];

            CurrentState?.OnExit();

            CurrentState = state;

            CurrentState?.OnEnter();
        }

        public void SetStartState(string name)
        {
            CurrentState = _states[name];
            CurrentState.OnEnter();
        }
    }
}
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class State<TState> : IState<TState>
    {
        public override string ToString() => $"{GetType().Name}";
        public abstract TState Fail { get; }
        public TState Undefined(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
    }
}
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class State<TState> : IState<TState>
    {
        public override string ToString() => $"{GetType().Name}";
        public TState UndefinedTransition(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
        public TState FaíledTransition(string transition) => throw new TransitionFailedException(transition, GetType().Name);
    }
}
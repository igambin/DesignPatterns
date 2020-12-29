using IG.SimpleStateWithActions.StateEngineShared.Exceptions;
using System;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class State<TState> : IState<TState>
    {
        public abstract TState T_Error(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception);
        public override string ToString() => $"{GetType().Name}";
        public TState UndefinedTransition(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
        public TState FaíledTransition(string transition) => throw new TransitionFailedException(transition, GetType().Name);
    }
}
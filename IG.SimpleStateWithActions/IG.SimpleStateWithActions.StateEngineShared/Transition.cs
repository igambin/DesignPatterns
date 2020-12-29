using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class Transition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        public Transition(
              TState fromState
            , Expression<Func<TState, TState>> stateTransitionOnSuccess
            , Func<TEntity, bool> onTransitioning
            , Expression<Func<TState, TState>> stateTransitionOnFailed
            , Func<TEntity, bool> onTransitionFailed
            )
        {
            From = fromState;
            StateTransitionOnSuccess = stateTransitionOnSuccess;
            OnTransitioning = onTransitioning;
            StateTransitionOnFailed = stateTransitionOnFailed;
            OnTransitionFailed = onTransitionFailed;
        }

        public string Name => $"{From.GetType().Name}.{StateTransitionOnSuccess.TransitionName()}";
        public TState From { get; }
        public Expression<Func<TState, TState>> StateTransitionOnSuccess { get; }
        public Func<TEntity, bool> OnTransitioning { get; }
        public Expression<Func<TState, TState>> StateTransitionOnFailed { get; }
        public Func<TEntity, bool> OnTransitionFailed { get; }
    }

}

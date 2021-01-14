using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class Transition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public Transition(
              TState fromState
            , Expression<Func<TState, TState>> stateTransitionOnSuccess
            , Func<TEntity, Task<bool>> onTransitioning
            , Expression<Func<TState, TState>> stateTransitionOnFailed
            , Func<TEntity, Task<bool>> onTransitionFailed
            )
        {
            From = fromState;
            StateTransitionOnSuccess = stateTransitionOnSuccess;
            OnTransitioning = onTransitioning;
            StateTransitionOnFailed = stateTransitionOnFailed;
            OnTransitionFailed = onTransitionFailed;
        }

        public string Name => $"{From.GetType().Name}.{StateTransitionOnSuccess.TransitionName<TState, TStateEnum>()}";
        public TState From { get; }
        public Expression<Func<TState, TState>> StateTransitionOnSuccess { get; }
        public Func<TEntity, Task<bool>> OnTransitioning { get; }
        public Expression<Func<TState, TState>> StateTransitionOnFailed { get; }
        public Func<TEntity, Task<bool>> OnTransitionFailed { get; }
    }

}

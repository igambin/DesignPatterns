using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        TEntity StatedEntity { get; }
        List<Transition<TEntity, TState, TStateEnum>> Transitions { get; }
        Expression<Func<TState, TState>> TransitionToInvoke { get; }
        Func<TEntity, bool> PreCondition { get; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnError { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnSuccess { get; set; }
        public Action<TEntity, IState<TState, TStateEnum>> ActionOnFailed { get; set; }

    }
}
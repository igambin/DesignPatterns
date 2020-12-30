using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransition<TEntity, TState> : IStateTransition<TEntity, TState>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        public TEntity StatedEntity { get; set; }
        public List<Transition<TEntity, TState>> Transitions { get; set; }
        public Expression<Func<TState, TState>> TransitionToInvoke { get; set; }
        public Func<TEntity, bool> PreCheck { get; set; }
        public Action<Exception> ActionOnError { get; set; }
        public Action ActionOnSuccess { get; set; }
        public Action ActionOnFailed { get; set; }

    }
}
using System;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransitionBuilder<TEntity, TState> : IStateTransition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        IStateTransitionValidator<TEntity, TState> InvokeTransition(Expression<Func<TState, TState>> transition);
    }
}
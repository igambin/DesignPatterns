using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransitionValidator<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        IStateTransitionRunner<TEntity, TState, TStateEnum> WithPreValidation(Func<TEntity, bool> preCondition);
        IStateTransitionRunner<TEntity, TState, TStateEnum> WithoutPreValidation();
    }
}
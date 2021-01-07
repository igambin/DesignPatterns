using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransitionRunner<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        bool IsTransitionAllowed();
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnError(Action<Exception> onError);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnSuccess(Action onSuccess);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnFailed(Action onFailed);
        TState Execute();
    }
}
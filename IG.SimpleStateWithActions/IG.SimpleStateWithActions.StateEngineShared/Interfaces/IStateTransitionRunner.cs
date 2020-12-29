using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransitionRunner<TEntity, TState> : IStateTransition<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        bool IsTransitionAllowed(TState targetState);
        IStateTransitionRunner<TEntity, TState> OnError(Action<Exception> onError);
        IStateTransitionRunner<TEntity, TState> OnSuccess(Action onSuccess);
        IStateTransitionRunner<TEntity, TState> OnFailed(Action onFailed);
        TState Execute();
    }
}
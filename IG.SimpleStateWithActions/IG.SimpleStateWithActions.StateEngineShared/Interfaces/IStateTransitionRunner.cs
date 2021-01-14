using System;
using System.Threading.Tasks;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStateTransitionRunner<TEntity, TState, TStateEnum> : IStateTransition<TEntity, TState, TStateEnum>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        bool IsTransitionAllowed();
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnError(Action<TEntity, IState<TState, TStateEnum>> onError);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnSuccess(Action<TEntity, IState<TState, TStateEnum>> onSuccess);
        IStateTransitionRunner<TEntity, TState, TStateEnum> OnFailed(Action<TEntity, IState<TState, TStateEnum>> onFailed);
        Task<TState> Execute();
        bool IsTransitionSuccessful { get; }
        bool IsRollbackSuccessful { get; }
        bool IsPreconditionOk { get; }
    }
}
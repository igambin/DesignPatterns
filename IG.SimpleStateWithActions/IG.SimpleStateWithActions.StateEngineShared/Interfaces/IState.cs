using System;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IState<TState, TStateEnum>
    {
        TState T_Error(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception);
        TState UndefinedTransition(string transition);
        TState FaíledTransition(string transition);
        TStateEnum EnumState { get; }
    }
}
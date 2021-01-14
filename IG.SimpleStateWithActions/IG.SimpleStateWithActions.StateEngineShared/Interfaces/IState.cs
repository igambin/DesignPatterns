using System;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IState<TState, TStateEnum>
    {
        TState UndefinedTransition(string transition);
        TState FaíledTransition(string transition);
        TStateEnum Name { get; }
        bool Equals<TExpectedState>();

        TState TechnicalError();
        void Init(TState previousState, Expression<Func<TState, TState>> attemptedTransition, Exception exception = null);

        string PreviousState { get; set; }
        string AttemptedTransition { get; set; }
        Exception Exception { get; set; }
        string ExceptionType { get; set; }
    }
}
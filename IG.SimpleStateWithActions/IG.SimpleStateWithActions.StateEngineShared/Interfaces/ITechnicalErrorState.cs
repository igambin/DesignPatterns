using System;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface ITechnicalErrorState<TState>
    {
        TState PreviousState { get; set; }
        Expression<Func<TState, TState>> AttemptedTransition { get; set; }
        Exception Exception { get; set; }
    }
}
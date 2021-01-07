using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionBuilder<TEntity, TState, TStateEnum> : StateTransition<TEntity, TState, TStateEnum>, IStateTransitionBuilder<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public StateTransitionBuilder(TEntity statedEntity, List<Transition<TEntity, TState, TStateEnum>> transitions)
        {
            if (statedEntity == null) throw new ArgumentNullException(nameof(statedEntity));
            if (transitions == null) throw new ArgumentNullException(nameof(transitions));
            if (!transitions.Any()) throw new ArgumentException("List may not be empty", nameof(transitions));
            StatedEntity = statedEntity;
            Transitions = transitions.ToList();
        }

        public IStateTransitionValidator<TEntity, TState, TStateEnum> InvokeTransition(Expression<Func<TState, TState>> transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return new StateTransitionValidator<TEntity, TState, TStateEnum>(this, transition);
        }
    }
}

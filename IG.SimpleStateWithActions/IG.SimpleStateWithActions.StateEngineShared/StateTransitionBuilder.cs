using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionBuilder<TEntity, TState> : StateTransition<TEntity, TState>, IStateTransitionBuilder<TEntity, TState>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        public StateTransitionBuilder(TEntity statedEntity, List<Transition<TEntity, TState>> transitions)
        {
            if (statedEntity == null) throw new ArgumentNullException(nameof(statedEntity));
            if (transitions == null) throw new ArgumentNullException(nameof(transitions));
            if (!transitions.Any()) throw new ArgumentException("List may not be empty", nameof(transitions));
            StatedEntity = statedEntity;
            Transitions = transitions.ToList();
        }

        public IStateTransitionValidator<TEntity, TState> InvokeTransition(Expression<Func<TState, TState>> transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return new StateTransitionValidator<TEntity, TState>(this, transition);
        }
    }
}

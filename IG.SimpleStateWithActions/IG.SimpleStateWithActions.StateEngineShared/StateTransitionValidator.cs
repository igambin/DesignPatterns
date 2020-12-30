using System;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionValidator<TEntity, TState> : StateTransition<TEntity, TState>, IStateTransitionValidator<TEntity, TState>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState>
    {
        public StateTransitionValidator(StateTransitionBuilder<TEntity,TState> builder, Expression<Func<TState, TState>> transitionToInvoke)
        {
            StatedEntity = builder.StatedEntity;
            Transitions = builder.Transitions;
            TransitionToInvoke = transitionToInvoke;
        }

        public IStateTransitionRunner<TEntity, TState> WithoutPreValidation()
            => new StateTransitionRunner<TEntity, TState>(this, x => true);

        public IStateTransitionRunner<TEntity, TState> WithPreValidation(Func<TEntity, bool> preCheck)
            => new StateTransitionRunner<TEntity, TState>(this, preCheck);
    }
}
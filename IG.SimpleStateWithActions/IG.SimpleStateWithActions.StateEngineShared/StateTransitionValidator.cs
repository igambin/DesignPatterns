using System;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionValidator<TEntity, TState, TStateEnum> : StateTransition<TEntity, TState, TStateEnum>, IStateTransitionValidator<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {
        public StateTransitionValidator(StateTransitionBuilder<TEntity,TState, TStateEnum> builder, Expression<Func<TState, TState>> transitionToInvoke)
        {
            StatedEntity = builder.StatedEntity;
            Transitions = builder.Transitions;
            TransitionToInvoke = transitionToInvoke;
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> WithoutPreValidation()
            => new StateTransitionRunner<TEntity, TState, TStateEnum>(this, x => true);

        public IStateTransitionRunner<TEntity, TState, TStateEnum> WithPreValidation(Func<TEntity, bool> preCondition)
            => new StateTransitionRunner<TEntity, TState, TStateEnum>(this, preCondition);
    }
}
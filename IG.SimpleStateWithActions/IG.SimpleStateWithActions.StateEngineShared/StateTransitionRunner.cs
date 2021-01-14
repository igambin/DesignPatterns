using System;
using System.Threading.Tasks;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionRunner<TEntity, TState, TStateEnum> : StateTransition<TEntity, TState, TStateEnum>, IStateTransitionRunner<TEntity, TState, TStateEnum>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState, TStateEnum>
    {

        public StateTransitionRunner(IStateTransitionValidator<TEntity, TState, TStateEnum> validator, Func<TEntity, bool> preCondition = null)
        {
            StatedEntity = validator?.StatedEntity;
            Transitions = validator?.Transitions;
            TransitionToInvoke = validator?.TransitionToInvoke;
            PreCondition = preCondition;
        }

        public bool IsTransitionAllowed()
        {
            try
            {
                var preConditionSuccessful = PreCondition?.Invoke(StatedEntity) ?? true;
                var transitionExists = GetRequestedTransition != null;
                return transitionExists && preConditionSuccessful;
            }
            catch
            {
                return false;
            }
        } 



        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnSuccess(Action<TEntity, IState<TState, TStateEnum>> onSuccess)
        {
            ActionOnSuccess = onSuccess;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnFailed(Action<TEntity, IState<TState, TStateEnum>> onFailed)
        {
            ActionOnFailed = onFailed;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState, TStateEnum> OnError(Action<TEntity, IState<TState, TStateEnum>> onError)
        {
            ActionOnError = onError;
            return this;
        }

        private Transition<TEntity, TState, TStateEnum> GetRequestedTransition => Transitions.FindTransition(StatedEntity, TransitionToInvoke);

        public async Task<TState> Execute()
        {
            var previousState = StatedEntity.State;
            if (TransitionToInvoke == null) throw new TransitionNotSpecifiedException();

            try
            {
                var transition = GetRequestedTransition;

                if (!IsPreconditionOk)
                {
                    throw new TransitionConstraintFailedException(transition.StateTransitionOnSuccess.TransitionName<TState, TStateEnum>(),
                        $"{previousState}");
                }

                IsTransitionSuccessful = true;
                if (transition.OnTransitioning != null)
                {
                    IsTransitionSuccessful = await transition.OnTransitioning.Invoke(StatedEntity).ConfigureAwait(false);
                }

                if(IsTransitionSuccessful)
                {
                    StatedEntity.State = transition.StateTransitionOnSuccess.Compile()(StatedEntity.State);
                    ActionOnSuccess?.Invoke(StatedEntity, StatedEntity.State);
                    return StatedEntity.State;
                }

                if (transition.OnTransitionFailed == null)
                {
                    throw new TransitionFailedException(transition.StateTransitionOnSuccess.TransitionName<TState, TStateEnum>(),
                        $"{previousState}");
                }

                IsRollbackSuccessful = false;
                if (transition.OnTransitionFailed != null)
                {
                    IsRollbackSuccessful  = await transition.OnTransitionFailed.Invoke(StatedEntity).ConfigureAwait(false);
                }

                if (IsRollbackSuccessful )
                {
                    StatedEntity.State = transition.StateTransitionOnFailed.Compile()(StatedEntity.State);
                    ActionOnFailed?.Invoke(StatedEntity, StatedEntity.State);
                    return StatedEntity.State;
                }
                throw new TransitionRollbackFailedException(
                    transition.StateTransitionOnFailed?.TransitionName<TState, TStateEnum>() ?? "[rollback undefined]", $"{previousState}");
            }
            catch (Exception ex)
            {
                StatedEntity.State = StatedEntity.State.TechnicalError();
                StatedEntity.State.Init(previousState, TransitionToInvoke, ex);
                ActionOnError?.Invoke(StatedEntity, StatedEntity.State);
                return StatedEntity.State;
            }

        }

        public bool IsTransitionSuccessful { get; private set; }
        public bool IsRollbackSuccessful { get; private set; }
        public bool IsPreconditionOk => PreCondition?.Invoke(StatedEntity) ?? true;
    }
}
using System;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public class StateTransitionRunner<TEntity, TState> : StateTransition<TEntity, TState>, IStateTransitionRunner<TEntity, TState>
        where TEntity : IStatedEntity<TState>, new()
        where TState : IState<TState>
    {

        public StateTransitionRunner(IStateTransitionValidator<TEntity, TState> validator, Func<TEntity, bool> preCheck = null)
        {
            StatedEntity = validator.StatedEntity;
            Transitions = validator.Transitions;
            TransitionToInvoke = validator.TransitionToInvoke;
            PreCheck = preCheck;
        }     

        public bool IsTransitionAllowed(TState targetState) => PreCheck?.Invoke(StatedEntity) ?? false;

        public IStateTransitionRunner<TEntity, TState> OnSuccess(Action onSuccess)
        {
            ActionOnSuccess = onSuccess;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState> OnFailed(Action onFailed)
        {
            ActionOnFailed = onFailed;
            return this;
        }

        public IStateTransitionRunner<TEntity, TState> OnError(Action<Exception> onError)
        {
            ActionOnError = onError;
            return this;
        }

        public TState Execute()
        {
            var previousState = StatedEntity.State;
            if (TransitionToInvoke == null) throw new TransitionNotSpecifiedException();

            try
            {
                var transition = Transitions.FindTransition(StatedEntity, TransitionToInvoke);

                var transitionSuccessful = transition.OnTransitioning?.Invoke(StatedEntity) ?? true;

                if(transitionSuccessful)
                {
                    ActionOnSuccess?.Invoke();
                    StatedEntity.State = transition.StateTransitionOnSuccess.Compile()(StatedEntity.State);
                    return StatedEntity.State;
                } 

                var rollbackSuccessful = transition.OnTransitionFailed?.Invoke(StatedEntity) ?? false;
                if(rollbackSuccessful)
                {
                    ActionOnFailed?.Invoke();
                    StatedEntity.State = transition.StateTransitionOnFailed.Compile()(StatedEntity.State);
                    return StatedEntity.State;
                } 

                throw new TransitionRollbackFailedException(transition.StateTransitionOnFailed?.TransitionName() ?? "[rollback undefined]", $"{previousState}");
            } 
            catch (Exception ex)
            {
                ActionOnError?.Invoke(ex);
                StatedEntity.State = StatedEntity.State.T_Error(previousState, TransitionToInvoke, ex);
                return StatedEntity.State;
            }
        }
    }
}
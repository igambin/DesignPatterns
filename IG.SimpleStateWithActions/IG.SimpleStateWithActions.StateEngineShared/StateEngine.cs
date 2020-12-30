using System.Collections.Generic;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class StateEngine<TEntity, TState> : IStateEngine<TEntity, TState>
        where TEntity : class, IStatedEntity<TState>, new()
        where TState : IState<TState>
    {

        public abstract List<Transition<TEntity, TState>> Transitions { get; }

        public IStateTransitionBuilder<TEntity, TState> For(TEntity statedEntity)
            => new StateTransitionBuilder<TEntity, TState>(statedEntity, Transitions);

    }
}




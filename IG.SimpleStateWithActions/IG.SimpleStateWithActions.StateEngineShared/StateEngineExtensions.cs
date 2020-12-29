using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public static class StateEngineExtensions
    {
        public static string TransitionName<TState>(this Expression<Func<TState, TState>> transition)
            where TState : IState<TState>
        {
            var transitionMember = transition.Body as MemberExpression;
            return transitionMember.Member.Name;
        }

        public static string ActualStateName<TEntity, TState>(this TEntity entity)
            where TEntity : IStatedEntity<TState>, new()
            where TState : IState<TState> 
            => $"{entity.State}";

        public static Transition<TEntity, TState> FindTransition<TEntity, TState>(
            this List<Transition<TEntity, TState>> transitions, 
            TEntity entity, 
            Expression<Func<TState, TState>> transition
        )
            where TEntity : IStatedEntity<TState>, new()
            where TState : IState<TState>
        {
            Transition<TEntity, TState> requestedTransition = default;
            MemberExpression needleMember = null;
            try 
            {
                var needleType = entity.State.GetType();
                needleMember = transition.Body as MemberExpression;
                if (needleMember == null)
                {
                    throw new TransitionFailedException($"Transition '{transition?.Body.ToString() ?? "[null]"}' failed on {typeof(TEntity)}");
                }

                transitions.ForEach(t =>
                {
                    var transitionMember = t.StateTransitionOnSuccess.Body as MemberExpression;
                    if (needleType == t.From.GetType()
                        && needleMember.Member.Name == transitionMember.Member.Name
                        && needleMember.Member.ReflectedType == transitionMember.Member.ReflectedType)
                    {
                        requestedTransition = t;
                    }
                });
            }
            catch (Exception ex)
            {
                throw new TransitionFailedException(ex);
            }

            if (requestedTransition == default)
            {
                throw new UndefinedTransitionException(needleMember.Member.Name, entity.State.GetType().Name);
            }

            return requestedTransition;
        }
    }
}
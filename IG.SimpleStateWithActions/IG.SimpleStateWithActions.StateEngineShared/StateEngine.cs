using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class StateEngine<TEntity, TState> where TEntity : IStatedEntity<TState>, new()
    {
        protected abstract List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Expression<Func<TState, TState>> transitionOnFail)> Transitions { get; }

        public virtual void InvokeTransition(TEntity entity, Expression<Func<TState, TState>> transition)
        {
            List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Expression<Func<TState, TState>> transitionOnFail)> haystack
                = new List<(Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Expression<Func<TState, TState>> onFailedTransition)>();
            (Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Expression<Func<TState, TState>> transitionOnFail) requestedTransition = default;
            MemberExpression needleMember = null;
            try // to find requested transition 
            {
                var needleType = entity.State.GetType();
                needleMember = transition.Body as MemberExpression;
                if (needleMember == null)
                {
                    throw new TransitionFailedException($"Transition '{transition?.Body.ToString()??"[null]"}' failed on {typeof(TEntity)}");
                }

                Transitions.ForEach(t =>
                {
                    var transitionMember = t.transition.Body as MemberExpression;
                    if (needleType == t.Item1
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

            Action<TEntity> action = null;
            Expression<Func<TState, TState>> onSuccess = null;
            Expression<Func<TState, TState>> onFailure = null;
            if (requestedTransition != default)
            {
                onSuccess = requestedTransition.transition;
                action = requestedTransition.action;
                onFailure = requestedTransition.transitionOnFail;
            }
            else
            {
                throw new UndefinedTransitionException(needleMember.Member.Name, entity.State.GetType().Name);
            }

            try // to execute found action and set target-state accordingly
            {
                action?.Invoke(entity);
                entity.State = onSuccess.Compile()(entity.State);
            }
            catch (Exception ex)
            {
                if (onFailure != null)
                {
                    Console.WriteLine(ex.Messages(7));
                    Console.WriteLine($"       Invoking fallback transition '{onFailure}'");
                    InvokeTransition(entity, onFailure);
                }
                else
                {
                    throw new TransitionFailedException(ex);
                }
            }

        }
    }
}
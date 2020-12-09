using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IG.SimpleStateWithActions.StateEngineShared
{
    public abstract class StateEngine<TEntity, TState> where TEntity : IStatedEntity<TState>, new()
    {
        protected abstract List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Func<TState, TState>
            onFailedAction)> Transitions { get; }

        public virtual void InvokeTransition(TEntity entity, Expression<Func<TState, TState>> transition)
        {
            List<(Type, Expression<Func<TState, TState>> transition, Action<TEntity> action, Func<TState, TState> onFailedAction)> haystack
                = new List<(Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Func<TState, TState> onFailedAction)>();
            (Type, Expression<Func<TState, TState>> transition, System.Action<TEntity> action, Func<TState, TState> onFailedAction) RequestedTransition = default;
            MemberExpression needleMember = null;
            try // to find requested transition 
            {
                var needleType = entity.State.GetType();
                needleMember = transition.Body as MemberExpression;
                if (needleMember == null)
                {
                    Console.WriteLine("arrange failed (needle)");
                    // throw exception? or
                    return; // do nothing?
                }

                // TODO ==> invoking all transition will crash because it will also invoke on UndefinedTransitions 
                Transitions.ForEach(t =>
                {
                    var transitionMember = t.transition.Body as MemberExpression;
                    if (needleType == t.Item1
                        && needleMember.Member.Name == transitionMember.Member.Name
                        && needleMember.Member.ReflectedType == transitionMember.Member.ReflectedType)
                    {
                        RequestedTransition = t;
                    }
                });
            }
            catch (Exception ex)
            {
                // handle error on setting up transition
            }

            Action<TEntity> action = null;
            Func<TState, TState> onSuccess = null;
            Func<TState, TState> onFailure = null;
            if (RequestedTransition != default)
            {
                onSuccess = RequestedTransition.transition?.Compile();
                action = RequestedTransition.action;
                onFailure = RequestedTransition.onFailedAction;
            }
            else
            {
                var forceUndefinedTransitionException = transition.Compile()(entity.State);
            }

            try // to execute found action and set target-state accordingly
            {
                Console.WriteLine("    => handle event: " + needleMember.Member.Name);
                if (action != null) action(entity);
                entity.State = onSuccess(entity.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine("    " + ex.Message);
                Console.WriteLine("    => handle event: FAIL");
                if (onFailure != null) entity.State = onFailure(entity.State);
            }

        }
    }
}
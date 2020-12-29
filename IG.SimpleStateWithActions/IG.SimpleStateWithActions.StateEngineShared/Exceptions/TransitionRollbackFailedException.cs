using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Exceptions
{
    public class TransitionRollbackFailedException : Exception
    {
        public const string MessageTemplate = "The transition rollback '{0}' from state '{1}' failed!";

        public TransitionRollbackFailedException(string message) : base(message) { }
        public TransitionRollbackFailedException(Exception ex) : base("Transition failed!", ex) { }
        public TransitionRollbackFailedException(string message, Exception ex) : base(message, ex) { }
        public TransitionRollbackFailedException(string transition, string sourceState) : base(string.Format(MessageTemplate, transition, sourceState)) { }
        public TransitionRollbackFailedException(string transition, string sourceState, Exception ex)
            : base(string.Format(MessageTemplate, transition, sourceState), ex) { }
    }
}
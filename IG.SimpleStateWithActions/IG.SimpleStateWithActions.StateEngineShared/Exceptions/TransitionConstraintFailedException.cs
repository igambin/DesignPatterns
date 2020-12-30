using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Exceptions
{
    public class TransitionConstraintFailedException : Exception
    {
        public const string MessageTemplate = "Checking constraints for transition '{0}' from state '{1}' failed!";

        public TransitionConstraintFailedException(string message) : base(message) { }

        public TransitionConstraintFailedException(Exception ex) : base("Transition constrains failed!", ex) { }

        public TransitionConstraintFailedException(string message, Exception ex) : base(message, ex) { }

        public TransitionConstraintFailedException(string transition, string sourceState)
            : base(string.Format(MessageTemplate, transition, sourceState)) { }

        public TransitionConstraintFailedException(string transition, string sourceState, Exception ex)
            : base(string.Format(MessageTemplate, transition, sourceState), ex) { }
    }
}
using System;

namespace IG.SimpleStateWithActions.StateEngineShared.Exceptions
{
    public class TransitionFailedException : Exception
    {
        public const string MessageTemplate = "The transition '{0}' from state '{1}' failed!";

        public TransitionFailedException(string message) : base(message) { }

        public TransitionFailedException(Exception ex) : base("Transition failed!", ex) { }

        public TransitionFailedException(string message, Exception ex) : base(message, ex) { }

        public TransitionFailedException(string transition, string sourceState)
            : base(string.Format(MessageTemplate, transition, sourceState)) { }

        public TransitionFailedException(string transition, string sourceState, Exception ex)
            : base(string.Format(MessageTemplate, transition, sourceState), ex) { }
    }
}
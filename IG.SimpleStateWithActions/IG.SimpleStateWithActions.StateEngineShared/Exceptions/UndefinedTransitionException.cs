using System;
using System.Globalization;

namespace IG.SimpleStateWithActions.StateEngineShared.Exceptions
{
    public class UndefinedTransitionException : Exception
    {
        public const string MessageTemplate = "The transition '{0}' from state '{1}' is not defined!";

        public UndefinedTransitionException() { }

        public UndefinedTransitionException(string message) : base(message) { }

        public UndefinedTransitionException(string message, Exception innerException) : base(message, innerException) { }

        public UndefinedTransitionException(string transition, string sourceState)
            : base(string.Format(CultureInfo.CurrentCulture, MessageTemplate, transition, sourceState)) { }

        public UndefinedTransitionException(string transition, string sourceState, Exception ex)
            : base(string.Format(CultureInfo.CurrentCulture, MessageTemplate, transition, sourceState), ex) { }

    }
}
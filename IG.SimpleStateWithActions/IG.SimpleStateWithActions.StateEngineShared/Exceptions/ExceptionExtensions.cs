using System;
using System.Collections.Generic;
using System.Text;

namespace IG.SimpleStateWithActions.StateEngineShared.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string Messages(this Exception ex, int indents = 0) => ex.Messages($"{new string(' ',indents)}{ex.Message}");
        
        private static string Messages(this Exception ex, string message)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerException.Messages($"{message} > {ex.InnerException.Message}");
            }

            return message;
        }
    }
}

using System;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.Run.RunState;
using IG.SimpleStateWithActions.StateEngineShared;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;

namespace IG.SimpleStateWithActions.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            var r = new Run.Run
            {
                Name = "TestRun",
                State = new RunStates.Initial()
            };
            System.Console.WriteLine($"State: {r.State}");

            DoTransition(r, state => state.Finalize);
            DoTransition(r, state => state.Start);
            DoTransition(r, state => state.Finalize);
            DoTransition(r, state => state.Reset);
	}

	public static void DoTransition(Run.Run r, Expression<Func<IRunState, IRunState>> transition)
	{
            var sc = new RunStateController();
            try
            {
                sc.InvokeTransition(r, transition);
            }
            catch (UndefinedTransitionException ste)
            {
                System.Console.WriteLine("    " + ste.Message);
            }
            System.Console.WriteLine($"\nState: {r.State}");
        }

    }
}

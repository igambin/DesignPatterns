using System;
using System.Linq.Expressions;
using IG.SimpleStateWithActions.Models;
using IG.SimpleStateWithActions.StateEngines;
using IG.SimpleStateWithActions.StateEngineShared;
using IG.SimpleStateWithActions.StateEngineShared.Exceptions;

namespace IG.SimpleStateWithActions.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            var r = new Run
            {
                Name = "TestRun",
                State = new RunStates.Initial()
            };
            System.Console.WriteLine($"State: {r.State}");

            DoTransition(r, state => state.Finalize);
            DoTransition(r, state => state.Start);
            DoTransition(r, state => state.Finalize);
            DoTransition(r, state => state.Reset);

            System.Console.WriteLine();
            System.Console.WriteLine(r.Name);
	}

	public static void DoTransition(Run r, Expression<Func<IRunState, IRunState>> transition)
	{
            var sc = new RunStateEngine();
            try
            {
                sc.InvokeTransition(r, transition);
            }
            catch (Exception ste)
            {
                System.Console.WriteLine("    " + ste.Messages());
            }
            System.Console.WriteLine($"\nState: {r.State}");
        }

    }
}

using System;

namespace IG.SimpleState.Console
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
            System.Console.WriteLine();
            System.Console.WriteLine($"State: {r.State}");

			DoTransition(r, state => state.Finalize);
			DoTransition(r, state => state.Start);
			DoTransition(r, state => state.Finalize);
			DoTransition(r, state => state.Reset);
		}

		public static void DoTransition(Run r, Func<IRunState, IRunState> transition)
		{
			try
			{
				r.State = transition(r.State);
			}
			catch (UndefinedTransitionException ste)
			{
                System.Console.WriteLine("    " + ste.Message);
			}
            System.Console.WriteLine($"\nState: {r.State}");
        }

    }
}

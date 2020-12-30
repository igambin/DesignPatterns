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
            DoTransition(r, state => state.Reset);
            DoTransition(r, state => state.Start);
            DoTransition(r, state => state.Finalize);
            DoTransition(r, state => state.Reset);

            System.Console.WriteLine();
            System.Console.WriteLine(r.Name);
	    }

	    public static void DoTransition(Run run, Expression<Func<IRunState, IRunState>> transition)
	    {
            // basically you'd want to register the dependency 
            // ...Register<IStateEngine<Run, IRunState>, RunStateEngine>() per RequestLifetime
            var runStateEngine = new RunStateEngine();
            try
            {
                string tname = $"({ transition.TransitionName()} from { run.State.GetType().Name})";
                _ = runStateEngine.For(run)
                    .InvokeTransition(transition)
                    .WithoutPreValidation()
                    .OnSuccess(() =>
                    {
                        System.Console.WriteLine($"Success {tname}!");
                    })
                    .OnFailed(() =>
                    {
                        System.Console.WriteLine($"Failed {tname}!");
                    })
                    .OnError((ex) =>
                    {
                        System.Console.WriteLine($"ERROR {tname}: {ex.Message}");
                    })
                    .Execute();
            }
            catch (Exception ste)
            {
                System.Console.WriteLine("    " + ste.Messages());
            }
            System.Console.WriteLine($"\nState: {run.State}");
        }
    }
}

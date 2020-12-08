using System;
using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Run.RunState
{
    /// <summary>
    /// 	RunStateController provides Actions that are to be executed on 
    /// 	state transitions. This allows modifications and interactions in
    /// 	and of the stated object when the state is about to change.
    /// </summary>
    public class RunStateController : StateController<Run, IRunState>
    {
        public RunStateController(/* insert any dependencies required to handle the transitions, e. g. repositories, logger, etc. */)
        {
            Transitions.Add((typeof(RunStates.Initial), runState => runState.Start, run => InvokeStart(run), runState => runState.Fail));
            Transitions.Add((typeof(RunStates.InProgress), runState => runState.Cancel, run => InvokeCancel(run), runState => runState.Fail));
            Transitions.Add((typeof(RunStates.InProgress), runState => runState.Finalize, run => InvokeFinalize(run), runState => runState.Fail));
            Transitions.Add((typeof(RunStates.InProgress), runState => runState.Fail, run => InvokeFail(run), runState => runState.Fail));
            Transitions.Add((typeof(RunStates.Done), runState => runState.Reset, run => InvokeReset(run), null));
            Transitions.Add((typeof(RunStates.Cancelled), runState => runState.Reset, null, null));
            Transitions.Add((typeof(RunStates.Failed), runState => runState.Reset, null, null));
            Transitions.Add((typeof(RunStates.Initial), runState => runState.Start, run => InvokeStart(run), runState => runState.Fail));
            Transitions.Add((typeof(RunStates.Initial), runState => runState.Start, run => InvokeStart(run), runState => runState.Fail));
        }

        private void InvokeStart(Run run) { Console.WriteLine("   Starting run!");/* implement logic to handle the transition event */}
        private void InvokeFinalize(Run run) { throw new Exception("ERR: Crash on Finalizing the run!"); /*Console.WriteLine("    Finalizing run!");/* implement logic to handle the transition event */}
        private void InvokeCancel(Run run) { Console.WriteLine("    Cancelling run!"); /* implement logic to handle the transition event */}
        private void InvokeAbort(Run run) { Console.WriteLine("    Aborting run!"); /* implement logic to handle the transition event */}
        private void InvokeFail(Run run) { Console.WriteLine("    Failing run!"); /* implement logic to handle the transition event */}
        private void InvokeReset(Run run) { Console.WriteLine("    Resetting run!"); }

    }
}
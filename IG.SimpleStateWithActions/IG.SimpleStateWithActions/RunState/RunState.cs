using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Run.RunState
{
    public abstract class RunState : State<IRunState>, IRunState
    {
        public virtual IRunState Finalize => Undefined("Finalize");
        public virtual IRunState Reset => Undefined("Reset");
        public virtual IRunState Start => Undefined("Start");
        public virtual IRunState Cancel => Undefined("Cancel");
        public override IRunState Fail => new RunStates.Failed();

    }
}
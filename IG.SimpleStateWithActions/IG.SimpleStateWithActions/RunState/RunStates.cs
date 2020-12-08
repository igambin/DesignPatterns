using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Run.RunState
{
    public class RunStates
    {
        public class Initial : RunState, IState<IRunState>
        {
            public override IRunState Start => new InProgress();
        }
        public class InProgress : RunState, IState<IRunState>
        {
            public override IRunState Finalize => new Done();
            public override IRunState Cancel => new Cancelled();
        }
        public class Done : RunState, IState<IRunState>
        {
            public override IRunState Reset => new Initial();
        }
        public class Cancelled : RunState, IState<IRunState>
        {
            public override IRunState Reset => new Initial();
        }
        public class Failed : RunState, IState<IRunState>
        {
            public override IRunState Reset => new Initial();
        }
    }
}
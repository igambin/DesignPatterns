namespace IG.SimpleState
{
    public class RunStates
    {
        public class Initial : State, IRunState
        {
            public IRunState Start => new InProgress();
        }
        public class InProgress : State, IRunState
        {
            public IRunState Fail => new Failed();
            public IRunState Finalize => new Done();
            public IRunState Cancel => new Cancelled();
        }
        public class Done : State, IRunState
        {
            public IRunState Reset => new Initial();
        }
        public class Cancelled : State, IRunState
        {
            public IRunState Reset => new Initial();
        }
        public class Failed : State, IRunState
        {
            public IRunState Reset => new Initial();
        }
    }
}
namespace IG.SimpleState
{
    public interface IRunState
    {
        public IRunState Fail => Undefined("Fail");
        public IRunState Finalize => Undefined("Finalize");
        public IRunState Reset => Undefined("Reset");
        public IRunState Start => Undefined("Start");
        public IRunState Cancel => Undefined("Cancel");
        public IRunState Undefined(string transition) => throw new UndefinedTransitionException(transition, GetType().Name);
    }
}
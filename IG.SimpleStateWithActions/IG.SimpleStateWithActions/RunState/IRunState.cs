using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Run.RunState
{
    public interface IRunState : IState<IRunState>
    {
        IRunState Finalize { get; }
        IRunState Reset { get; }
        IRunState Start { get; }
        IRunState Cancel { get; }
    }
}
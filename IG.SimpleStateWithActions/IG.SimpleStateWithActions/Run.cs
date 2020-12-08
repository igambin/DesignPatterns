using IG.SimpleStateWithActions.Run.RunState;
using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Run
{
    public class Run : IStatefulEntity<IRunState>
    {
        public string Name { get; set; }
        public IRunState State { get; set; }
    }
}
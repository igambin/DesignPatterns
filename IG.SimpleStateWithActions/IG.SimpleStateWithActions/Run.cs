using IG.SimpleStateWithActions;
using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions
{
    public class Run : IStatefulEntity<IRunState>
    {
        public string Name { get; set; }
        public IRunState State { get; set; }
    }
}
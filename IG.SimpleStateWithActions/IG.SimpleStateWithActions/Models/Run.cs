using IG.SimpleStateWithActions.StateEngines;
using IG.SimpleStateWithActions.StateEngineShared;

namespace IG.SimpleStateWithActions.Models
{
    public class Run : IStatedEntity<IRunState>
    {
        public string Name { get; set; }
        public IRunState State { get; set; }
    }
}
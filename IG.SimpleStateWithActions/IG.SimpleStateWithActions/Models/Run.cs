using IG.SimpleStateWithActions.StateEngines;
using IG.SimpleStateWithActions.StateEngineShared;
using IG.SimpleStateWithActions.StateEngineShared.Interfaces;

namespace IG.SimpleStateWithActions.Models
{
    public class Run : IStatedEntity<IRunState>
    {
        public string Name { get; set; }
        public IRunState State { get; set; }
    }
}
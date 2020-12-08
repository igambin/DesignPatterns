namespace IG.SimpleStateWithActions.StateEngineShared
{
    public interface IStatefulEntity<TState>
    {
        TState State { get; set; }
    }
}
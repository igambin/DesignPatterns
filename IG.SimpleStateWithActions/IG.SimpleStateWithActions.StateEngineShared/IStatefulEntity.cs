namespace IG.SimpleStateWithActions.StateEngineShared
{
    public interface IStatedEntity<TState>
    {
        TState State { get; set; }
    }
}
namespace IG.SimpleStateWithActions.StateEngineShared.Interfaces
{
    public interface IStatedEntity<TState>
    {
        TState State { get; set; }
    }
}
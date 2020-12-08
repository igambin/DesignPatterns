namespace IG.SimpleStateWithActions.StateEngineShared
{
    public interface IState<TState>
    {
        TState Fail { get; }
        TState Undefined(string transition);

    }
}
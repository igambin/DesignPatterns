namespace IG.SimpleStateWithActions.StateEngineShared
{
    public interface IState<TState>
    {
        TState UndefinedTransition(string transition);
        TState FaíledTransition(string transition);
    }
}
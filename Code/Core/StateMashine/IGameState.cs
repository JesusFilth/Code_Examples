public interface IGameState
{
    void Execute();
}

public interface IGameState<TParam> : IGameState
{
    void SetParam(TParam param);
}
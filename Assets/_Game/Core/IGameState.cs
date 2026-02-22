using System;

public interface IGameState
{
    void Enter(Action<IGameState> onStateChange);
    void Exit();
    void Tick();
}

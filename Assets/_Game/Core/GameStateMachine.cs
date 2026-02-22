public class GameStateMachine
{
    private IGameState _currentState;

    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(ChangeState);
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}

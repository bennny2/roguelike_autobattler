public class GameStateMachine
{
    private IGameState _currentState;

    public void ChangeState(IGameState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            GameEvents.RaiseStateExited(_currentState);
        }
        _currentState = newState;
        GameEvents.RaiseStateEntered(newState);
        _currentState.Enter(ChangeState);
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}

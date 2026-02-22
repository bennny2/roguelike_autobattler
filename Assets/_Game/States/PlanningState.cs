using System;
public class PlanningState : IGameState
{
    private Action<IGameState> _onStateChange;
    public void Enter(Action<IGameState> onStateChange)
    {
        // Logic when entering the planning state
    }

    public void Tick()
    {
        // Logic for updating the planning state
    }

    public void Exit()
    {
        // Logic when exiting the planning state
    }
}
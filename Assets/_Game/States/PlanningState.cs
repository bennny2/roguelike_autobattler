using System;
using UnityEngine;
public class PlanningState : IGameState
{
    private Action<IGameState> _onStateChange;
    public void Enter(Action<IGameState> onStateChange)
    {
        Debug.Log("Entering Planning State");
        _onStateChange = onStateChange;
       
    }

    public void Tick()
    {  
     
    }

    public void Exit()
    {
        Debug.Log("Exiting Planning State");
    }
}
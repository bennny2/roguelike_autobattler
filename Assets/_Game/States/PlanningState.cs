using System;
using UnityEngine;
public class PlanningState : IGameState
{
    private Action<IGameState> OnStateChange { get; set; }
    private RunModel CurrentRun { get; set; }
    public void Enter(Action<IGameState> onStateChange)
    {
        Debug.Log("Entering Planning State");
        OnStateChange = onStateChange;

        CurrentRun = RunManager.GetRun();

        CurrentRun.AddUnitToTeam(new UnitModel 
        { 
            Name = "Warrior", 
            Health = 50, 
            Attack = 5,
            AttackDelay = 1f,
            AttackTimer = 1f
        });
        Debug.Log($"Added unit to team. Total units: {CurrentRun.PlayerUnits.Count}");

        OnStateChange(new CombatState());
    }

    public void Tick()
    {  
     
    }

    public void Exit()
    {
        Debug.Log("Exiting Planning State");
    }
}
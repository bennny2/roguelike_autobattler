using System;
using UnityEngine;
using System.Collections.Generic;
public class PlanningState : IGameState
{
    private Action<IGameState> OnStateChange { get; set; }
    private RunModel CurrentRun { get; set; }
    
    public void Enter(Action<IGameState> onStateChange)
    {
        Debug.Log("Entering Planning State");
        OnStateChange = onStateChange; //rename or refactor, events?

        CurrentRun = RunManager.GetRun();

        CurrentRun.EnemyUnits = new List<UnitModel> // should be done in seperate service, and through a method not directly
        {
            new() {
                Name = "Goblin",
                MaxHealth = 30,
                MaxMana = 0,
                AttackDamage = 3,
                AttackSpeed = 1.5f,
                AttackTimer = 1.5f,
                IsPlayerUnit = false
            },
            new()
            {
                Name = "Orc",
                MaxHealth = 50,
                MaxMana = 0,
                AttackDamage = 7,
                AttackSpeed = 2f,
                AttackTimer = 2f,
                IsPlayerUnit = false
            }
        };

        Debug.Log("Simulating adding unit to team..."); // adding units should be done in shop
        CurrentRun.AddUnitToTeam(new UnitModel 
        { 
            Name = "Warrior", 
            MaxHealth = 50, 
            MaxMana = 0,
            AttackDamage = 5,
            AttackSpeed = 1f,
            AttackTimer = 1f,
            IsPlayerUnit = true
        });
        Debug.Log($"Added unit to team. Total units: {CurrentRun.PlayerUnits.Count}");

        OnStateChange(new CombatState());
    }

    public void Tick()
    {  
      // Handle player input for planning phase (e.g., selecting units, arranging formation)
    }

    public void Exit()
    {
        Debug.Log("Exiting Planning State");
    }
}
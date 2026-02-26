using System.Collections.Generic;
using UnityEngine;

public class BattleSimulation
{
    public bool IsFinished { get; private set; } = false;
    public bool PlayerWonBattle { get; private set; } = false;
    private RunModel Run { get; set; }
    private List<UnitModel> UnitsInCombat { get; set; } = new();
    private UnitTargeting TargetingSystem { get; set; } = new();
    private AttackResolver AttackResolver { get; set; } = new();

    public BattleSimulation(RunModel run)
    {
        Run = run;
    }

    public void StartBattle()
    {
        Debug.Log("Battle started!");
        // Initialize battle state, load characters, etc.

        UnitsInCombat.AddRange(Run.PlayerUnits);
        UnitsInCombat.AddRange(Run.EnemyUnits);
    }

    public void Step(float deltaTime)
    {
        // End the fight if all player units are defeated
        if (Run.PlayerUnits.TrueForAll(u => u.Health <= 0))
        {
            Debug.Log("Combat ended - units defeated");
            IsFinished = true;
            PlayerWonBattle = false;
            return;
        }
        if (Run.EnemyUnits.TrueForAll(u => u.Health <= 0))
        {
            Debug.Log("Combat ended - enemies defeated");
            IsFinished = true;
            PlayerWonBattle = true;
            return;
        }

        // Process units actions
        foreach (var unit in UnitsInCombat)
        {
            if (unit.Health > 0)
            {
                ProcessUnitAction(unit, deltaTime);
            }
        }
    }

    private void ProcessUnitAction(UnitModel unit, float deltaTime)
    {
        unit.AttackTimer += deltaTime;

        // Has target?
        if (unit.Target == null || unit.Target.Health <= 0)
        {
            var potentialTargets = UnitsInCombat.FindAll(u => u.IsPlayerUnit != unit.IsPlayerUnit && u.Health > 0);
            unit.Target = TargetingSystem.SelectTarget(unit, potentialTargets);
        }

        // In range?

        // Can cast ability?

        // Can attack?
        if (unit.AttackTimer >= unit.AttackDelay)
        {
            AttackResolver.ResolveAttack(unit, unit.Target);
            unit.AttackTimer = 0f;
        }
    }
}
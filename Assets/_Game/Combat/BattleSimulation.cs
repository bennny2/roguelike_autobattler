using UnityEngine;

public class BattleSimulation
{
    public bool IsFinished { get; private set; } = false;
    private RunModel Run { get; set; }

    public BattleSimulation(RunModel run)
    {
        Run = run;
    }

    public void StartBattle()
    {
        Debug.Log("Battle started!");
        // Initialize battle state, load characters, etc.
    }

    public void Step(float deltaTime)
    {

        if (Run.PlayerUnits.TrueForAll(u => u.Health <= 0))
        {
            Debug.Log("Combat ended - units defeated");
            IsFinished = true;
            return;
        }
        
        DummyFight(deltaTime);
    }

    private void DummyFight(float deltaTime)
    {
        foreach (var unit in Run.PlayerUnits)
        {
            SimulateUnitAttack(unit, deltaTime);
        }
    }

    private void SimulateUnitAttack(UnitModel unit, float deltaTime)
    {
        // TODO: Implement real unit attack simulation logic
        unit.AttackTimer += deltaTime;
        if (unit.AttackTimer >= unit.AttackDelay)
        {
            unit.Health -= unit.Attack; // For testing, units just attack themselves
            unit.AttackTimer = 0f;
            Debug.Log($"{unit.Name} attacks for {unit.Attack} damage!");
        }
    }
}

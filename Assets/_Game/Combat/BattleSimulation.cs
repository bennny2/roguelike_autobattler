using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

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

        UnitsInCombat.AddRange(Run.PlayerUnits);
        UnitsInCombat.AddRange(Run.EnemyUnits);

        ResetUnits(UnitsInCombat);
        // Apply items
        // Apply trait effects
        // Apply augments
    }

    private void ResetUnits(List<UnitModel> unitsInCombat)
    {
        foreach (var unit in unitsInCombat)
        {
            unit.CurrentHealth = unit.MaxHealthForCurrentCombat = unit.MaxHealth;
            unit.CurrentMana = unit.MaxManaForCurrentCombat = unit.MaxMana;
            unit.AttackDamageForCurrentCombat = unit.AttackDamage;
            unit.AttackSpeedForCurrentCombat = unit.AttackSpeed;
            unit.CriticalStrikeChanceForCurrentCombat = unit.CriticalStrikeChance;
            unit.CriticalStrikeDamageForCurrentCombat = unit.CriticalStrikeDamage; 
            unit.AbilityPowerForCurrentCombat = unit.AbilityPower;
            unit.ArmorForCurrentCombat = unit.Armor;
            unit.MagicResistForCurrentCombat = unit.MagicResist;
            unit.AttackTimer = 0f;
            unit.Target = null;
        }
    }

    public void Step(float deltaTime)
    {
        // End the fight if all units are defeated
        if (CheckBattleOutcome())
        {
            return;
        }

        // Process trait effects

        // Process units actions
        foreach (var unit in UnitsInCombat)
        {
            if (unit.CurrentHealth > 0)
            {
                ProcessUnitAction(unit, deltaTime);
            }
        }
    }

    private bool CheckBattleOutcome()
    {
        if (Run.PlayerUnits.TrueForAll(u => u.CurrentHealth <= 0))
        {
            Debug.Log("Combat ended - units defeated");
            IsFinished = true;
            PlayerWonBattle = false;
            return true;
        }
        else if (Run.EnemyUnits.TrueForAll(u => u.CurrentHealth <= 0))
        {
            Debug.Log("Combat ended - enemies defeated");
            IsFinished = true;
            PlayerWonBattle = true;
            return true;
        }
        return false;
    }

    private void ProcessUnitAction(UnitModel unit, float deltaTime)
    {
        unit.AttackTimer += deltaTime;

        // Has target?
        if (unit.Target == null || unit.Target.CurrentHealth <= 0)
        {
            var potentialTargets = UnitsInCombat.FindAll(u => u.IsPlayerUnit != unit.IsPlayerUnit && u.CurrentHealth > 0);
            unit.Target = TargetingSystem.SelectTarget(unit, potentialTargets);
        }

        // In range?

        // Can cast ability?

        // Can attack?
        if (unit.AttackTimer >= unit.AttackSpeed)
        {
            var attack = new Attack(
                Name: "Auto Attack",
                DamageType: DamageType.Physical,
                AttackType: AttackType.InstantDamage
            ); 
            AttackResolver.ResolveAttack(unit, unit.Target, attack);
            unit.AttackTimer = 0f;
        }
    }
}
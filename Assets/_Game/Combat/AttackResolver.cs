using UnityEngine;

public class AttackResolver
{
    public void ResolveAttack(UnitModel attacker, UnitModel defender, Attack attack)
    {
        switch (attack.AttackType)
        {
            case AttackType.InstantDamage:
                ResolveInstantDamageAttack(attacker, defender, attack);
                break;
            case AttackType.Dot:
                ResolveDamageOverTimeAttack(attacker, defender, attack);
                break;
        }
    }

    public void ResolveInstantDamageAttack(UnitModel attacker, UnitModel defender, Attack attack)
    {
        // Calculate base damage with items, traits, buffs, debuffs, etc.
        var damage = CalculateBaseDamage(attacker, defender);

        // Determine if critical hit
        bool isCritical = Random.value < attacker.CriticalStrikeChanceForCurrentCombat;
        if (isCritical)
        {
            float critMultiplier = attacker.CriticalStrikeDamageForCurrentCombat > 0 
                ? attacker.CriticalStrikeDamageForCurrentCombat 
                : attacker.CriticalStrikeDamage;
            damage *= critMultiplier;
            Debug.Log("Critical hit!");
        }

        // Damage reduction from armor or magic resist based on DamageType
        var damageTaken = CalculateDamageAfterMitigation(attacker, defender, damage, attack.DamageType);

        // Apply damage to defender
        defender.TakeDamage((int)damageTaken, attack.DamageType, attacker, isCritical);

        Debug.Log($"{attacker.Name} attacks {defender.Name} for {(int)damageTaken} {attack.DamageType} damage. {defender.Name} has {defender.CurrentHealth} health left.");
    }

    public void ResolveDamageOverTimeAttack(UnitModel attacker, UnitModel defender, Attack attack)
    {
        // Apply single tick of damage over time
        var baseDamage = CalculateBaseDamage(attacker, defender) * 0.2f; // DoT ticks represent 20% of base damage
        var damageTaken = CalculateDamageAfterMitigation(attacker, defender, baseDamage, attack.DamageType);

        defender.TakeDamage((int)damageTaken, attack.DamageType, attacker, isCritical: false);

        Debug.Log($"{attacker.Name} deals {(int)damageTaken} {attack.DamageType} DoT damage to {defender.Name}. {defender.Name} has {defender.CurrentHealth} health left.");
    }

    private float CalculateBaseDamage(UnitModel attacker, UnitModel defender)
    {
        return attacker.AttackDamageForCurrentCombat;
    }

    private float CalculateDamageAfterMitigation(UnitModel attacker, UnitModel defender, float damage, DamageType damageType)
    {
        float damageMitigation = 0f;
        switch (damageType)
        {
            case DamageType.Physical:
                damageMitigation = defender.ArmorForCurrentCombat;
                break;
            case DamageType.Magic:
                damageMitigation = defender.MagicResistForCurrentCombat;
                break;
            case DamageType.True:
                return damage; // True damage completely ignores mitigation
        }

        if (damageMitigation >= 0)
        {
            return damage * 100f / (100f + damageMitigation);
        }
        else
        {
            // Amplified damage for negative armor/MR
            return damage * (2f - 100f / (100f - damageMitigation));
        }
    }
}
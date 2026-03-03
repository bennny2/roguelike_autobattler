using UnityEngine;

public class AttackResolver
{
    public void ResolveAttack(UnitModel attacker, UnitModel defender) // use interfaces
    {
        // Calculate base damage with items, traits, buffs, debuffs, etc.
        var damage = CalculateBaseDamage(attacker, defender);

        // Critical hit?
        (damage, _) = HandleCriticalHits(attacker, defender, damage);

        // Damage reduction from armor or magic resist?
        var damageTaken = CalculateDamageAfterMitigation(attacker, defender, damage);

        // Apply damage to defender
        defender.CurrentHealth -= (int)damageTaken;

        Debug.Log($"{attacker.Name} attacks {defender.Name} for {damage} damage. {defender.Name} has {defender.CurrentHealth} health left.");
    }

    private float CalculateBaseDamage(UnitModel attacker, UnitModel defender)
    {
        // if defender is tank, maybe something (trait/item) makes it do more damage
        return attacker.AttackDamageForCurrentCombat;
    }

    private (float, bool) HandleCriticalHits(UnitModel attacker, UnitModel defender, float baseDamage)
    {
        if (UnityEngine.Random.value < attacker.CriticalStrikeChanceForCurrentCombat)
        {
            Debug.Log("Critical hit!");
            return (baseDamage * attacker.CriticalStrikeDamage, true);
        }
        return (baseDamage, false);
    }

    private float CalculateDamageAfterMitigation(UnitModel attacker, UnitModel defender, float damage)
    {
        // Think about armor vs mr
        var damageMitigation = defender.ArmorForCurrentCombat; // Placeholder, should consider attack type and defender's armor/mr
        return damage * 100 / (100 + damageMitigation); // Placeholder, should consider attack type and defender's armor/mr
    }
}
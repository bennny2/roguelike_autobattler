using UnityEngine;

public class AttackResolver
{
    public void ResolveAttack(UnitModel attacker, UnitModel defender) // use interfaces
    {
        defender.CurrentHealth -= attacker.AttackDamage;
        Debug.Log($"{attacker.Name} attacks {defender.Name} for {attacker.AttackDamage} damage. {defender.Name} has {defender.CurrentHealth} health left.");
    }
}
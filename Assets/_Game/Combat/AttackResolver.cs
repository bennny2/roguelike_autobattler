using UnityEngine;

public class AttackResolver
{
    public void ResolveAttack(UnitModel attacker, UnitModel defender) // use interfaces
    {
        defender.Health -= attacker.Attack;
        Debug.Log($"{attacker.Name} attacks {defender.Name} for {attacker.Attack} damage. {defender.Name} has {defender.Health} health left.");
    }
}
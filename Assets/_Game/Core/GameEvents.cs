using System;

public static class GameEvents
{
    // State events
    public static event Action<IGameState>? OnStateEntered;
    public static event Action<IGameState>? OnStateExited;

    // Combat events
    public static event Action<UnitModel?, UnitModel, int, bool, DamageType>? OnUnitDamaged; // Attacker, Target, Damage, IsCritical, DamageType
    public static event Action<UnitModel>? OnUnitDefeated;
    public static event Action? OnCombatStarted;
    public static event Action<bool>? OnCombatEnded; // true if player won

    public static void RaiseStateEntered(IGameState state) => OnStateEntered?.Invoke(state);
    public static void RaiseStateExited(IGameState state) => OnStateExited?.Invoke(state);
    
    public static void RaiseUnitDamaged(UnitModel? attacker, UnitModel target, int damage, bool isCritical, DamageType damageType) => 
        OnUnitDamaged?.Invoke(attacker, target, damage, isCritical, damageType);
        
    public static void RaiseUnitDefeated(UnitModel defeatedUnit) => OnUnitDefeated?.Invoke(defeatedUnit);
    public static void RaiseCombatStarted() => OnCombatStarted?.Invoke();
    public static void RaiseCombatEnded(bool playerWon) => OnCombatEnded?.Invoke(playerWon);
}

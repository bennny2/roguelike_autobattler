#nullable enable

public class UnitModel
{
    public string Name { get; set; } = string.Empty;

    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            int limit = MaxHealthForCurrentCombat > 0 ? MaxHealthForCurrentCombat : MaxHealth;
            _currentHealth = value < 0 ? 0 : (value > limit ? limit : value);
        }
    }
    public int MaxHealthForCurrentCombat { get; set; }
    public int MaxHealth { get; set; }

    public int CurrentMana { get; set; }
    public int MaxManaForCurrentCombat { get; set; }
    public int MaxMana { get; set; }

    public int AttackDamage { get; set; }
    public int AttackDamageForCurrentCombat { get; set; }

    public float AttackTimer { get; set; } = 0f;
    /// <summary>
    /// Time in seconds between attacks.
    /// </summary>
    public float AttackSpeed { get; set; } = 1f;
    public float AttackSpeedForCurrentCombat { get; set; }

    public float CriticalStrikeChance { get; set; } = 0.25f;
    public float CriticalStrikeChanceForCurrentCombat { get; set; }
    public float CriticalStrikeDamage { get; set; } = 1.5f;
    public float CriticalStrikeDamageForCurrentCombat { get; set; }

    public int AbilityPower { get; set; }
    public int AbilityPowerForCurrentCombat { get; set; }

    public float AbilityModifier { get; set; } = 1f;

    public int Armor { get; set; } = 20;
    public int ArmorForCurrentCombat { get; set; }
    public int MagicResist { get; set; } = 20;
    public int MagicResistForCurrentCombat { get; set; }

    public int GoldCost { get; set; }

    public bool IsPlayerUnit { get; set; }
    public bool HasTarget { get; set; } = false;
    public UnitModel? Target { get; set; }

    public UnitModel(){}

    public void TakeDamage(int amount, DamageType damageType, UnitModel? attacker = null, bool isCritical = false)
    {
        if (amount < 0) return;

        CurrentHealth -= amount;

        // Raise gameplay damage events
        GameEvents.RaiseUnitDamaged(attacker, this, amount, isCritical, damageType);

        if (CurrentHealth <= 0)
        {
            GameEvents.RaiseUnitDefeated(this);
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0) return;
        CurrentHealth += amount;
    }
}
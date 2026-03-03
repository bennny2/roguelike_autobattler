public class Attack
{
    public string Name { get; set; } = string.Empty;
    public DamageType DamageType { get; set; }
    public AttackType AttackType { get; set; }

    public Attack(string Name, DamageType DamageType, AttackType AttackType)
    {
        this.Name = Name;
        this.DamageType = DamageType;
        this.AttackType = AttackType;
    }
}

public enum DamageType
{
    Physical,
    Magic,
    True
}

public enum AttackType
{
    InstantDamage,
    Dot
}
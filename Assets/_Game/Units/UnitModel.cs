#nullable enable

public class UnitModel
{
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int Attack { get; set; }
    public float AttackTimer { get; set; } = 0f;
    public float AttackDelay { get; set; } = 1f;
    public bool IsPlayerUnit { get; set; }
    public bool HasTarget { get; set; }
    public UnitModel? Target { get; set; }

    public UnitModel(){}
}
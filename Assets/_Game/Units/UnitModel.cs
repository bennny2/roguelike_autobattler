public class UnitModel
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public float AttackTimer { get; set; } = 0f;
    public float AttackDelay { get; set; } = 1f;

    public UnitModel(){}
}
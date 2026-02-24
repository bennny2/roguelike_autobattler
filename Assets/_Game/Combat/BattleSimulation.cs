using UnityEngine;

public class BattleSimulation
{
    private int health = 100;
    private float _attackTimer = 0f;
    private float _attackDelay = 1f; // 1 second delay between attacks
    public bool IsFinished { get; private set; } = false;

    public BattleSimulation(RunModel run)
    {}

    public void StartBattle()
    {
        Debug.Log("Battle started!");
        // Initialize battle state, load characters, etc.
    }

    public void Step(float deltaTime)
    {
        if (health <= 0)
        {
            Debug.Log("Combat ended - player defeated");
            IsFinished = true;
            return;
        }
        
        _attackTimer += deltaTime;
        
        if (_attackTimer >= _attackDelay)
        {
            Debug.Log($"Combat ticking - health = {health}");
            health -= 10;
            _attackTimer = 0f; // Reset timer
        }
    }
}

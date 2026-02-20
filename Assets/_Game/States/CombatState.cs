using UnityEngine;

public class CombatState : IGameState
{
    public int health = 100;
    private float _attackTimer = 0f;
    private float _attackDelay = 1f; // 1 second delay between attacks

    public void Enter()
    {
        Debug.Log("Combat started");
    }

    public void Exit()
    {
        Debug.Log("Combat ended");
    }

    public void Tick()
    {
        if (health <= 0)
        {
            Debug.Log("Combat ended - player defeated");
            return;
        }
        
        _attackTimer += Time.deltaTime;
        
        if (_attackTimer >= _attackDelay)
        {
            Debug.Log($"Combat ticking - health = {health}");
            health -= 10;
            _attackTimer = 0f; // Reset timer
        }
    }
}
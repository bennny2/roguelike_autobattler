using UnityEngine;

public class RunController : MonoBehaviour
{
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();

        var run = new RunModel
        {
            PlayerHealth = 100,
            PlayerMoney = 50
        };
        
        RunManager.SetRun(run);
    }

    private void Start()
    {
        _stateMachine.ChangeState(new CombatState());
    }

    private void Update()
    {
        _stateMachine.Tick();   
    }
}

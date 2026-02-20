using UnityEngine;

public class RunController : MonoBehaviour
{
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();
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

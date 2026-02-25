using System;
using UnityEngine;

public class RunController : MonoBehaviour
{
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();

        var run = new RunModel
        {
            Id = Guid.NewGuid(),
            PlayerHealth = 100,
            PlayerMoney = 50
        };
        
        RunManager.SetRun(run);
    }

    private void Start()
    {
        _stateMachine.ChangeState(new PlanningState());
    }

    private void Update()
    {
        _stateMachine.Tick();   
    }
}

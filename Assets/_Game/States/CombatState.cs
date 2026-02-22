using UnityEngine;
using System;

public class CombatState : IGameState
{
    private Action<IGameState> _onStateChange;
    private BattleSimulation _battle;

    public CombatState()
    {
    }

    public void Enter(Action<IGameState> onStateChange)
    {
        _onStateChange = onStateChange;
        _battle = new BattleSimulation();
        _battle.StartBattle();
    }

    public void Tick()
    {
        _battle.Step(Time.deltaTime);

        if (_battle.IsFinished)
        {
            _onStateChange(new ShopState());
        }
    }

    public void Exit()
    {
        // Cleanup visuals if needed
    }
}
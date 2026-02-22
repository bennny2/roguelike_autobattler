using System;
using UnityEngine;

public class ShopState : IGameState
{
    public void Enter(Action<IGameState> onStateChange)
    {
        Debug.Log("Entering Shop State");
        // Initialize shop UI and items
    }
    public void Tick()
    {
        // Handle shop interactions and purchases
    }

    public void Exit()
    {
        Debug.Log("Exiting Shop State");
        // Clean up shop state
    }

}
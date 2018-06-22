using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game states to swap.
/// </summary>
public enum GameState
{
    PAUSED,
    PLAYING
}

/// <summary>
/// Manages the current game state.
/// </summary>
public static class GameStateManager
{
    private static GameState state = GameState.PAUSED;

    /// <summary>
    /// Check if the game state is paused.
    /// </summary>
    /// <returns>Game state is paused.</returns>
    public static bool Paused()
    {
        return state == GameState.PAUSED;
    }

    /// <summary>
    /// Check if the game state is playing.
    /// </summary>
    /// <returns>Game state is playing.</returns>
    public static bool Playing()
    {
        return state == GameState.PLAYING;
    }

    /// <summary>
    /// Sets the current game state.
    /// </summary>
    /// <param name="gameState">Game state to set.</param>
    public static void SetState(GameState gameState)
    {
        state = gameState;
    }
}

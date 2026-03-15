using System;

/// <summary>
/// Manages the Game Events
/// </summary>
public static class EventManager
{
    //GAME FLOW EVENTS
    /// <summary>
    /// Triggered when Game Starts
    /// </summary>
    public static Action OnGameStart; //Start Game Event
    /// <summary>
    /// Triggered when Game Ends
    /// </summary>
    public static Action OnGameEnd; //End Game Events

    //CARD EVENTS
    /// <summary>
    /// Select Card Event
    /// Triggered when a card is selected
    /// </summary>
    public static Action OnCardSelected;
    /// <summary>
    /// Card Match/Mismatch Event
    /// True if card Matches
    /// False if Card Mismatches
    /// </summary>
    public static Action<bool> OnCardMatchingChecked;

    //GAME DATA EVENT
    /// <summary>
    /// Declares/Publishes the stored Game Data on end of the Game
    /// </summary>
    public static Action<GameData> OnUpdateGameData;

    /// <summary>
    /// Invoke On Start Game Event
    /// </summary>
    public static void StartGame()
    {
        OnGameStart?.Invoke();
    }

    /// <summary>
    /// Invoke On End Game Event
    /// </summary>
    public static void EndGame()
    {
        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// Invoke Select/Click Card Event
    /// </summary>
    public static void CardSelected()
    {
        OnCardSelected?.Invoke();
    }

    /// <summary>
    /// Invoke Card Match Result Event
    /// </summary>
    /// <param name="IsMatching">True if Card Matches and False if Cards Mismatches</param>
    public static void CardMatchResult(bool IsMatching)
    {
        OnCardMatchingChecked?.Invoke(IsMatching);
    }

    /// <summary>
    /// Invoke Game Over Event
    /// </summary>
    /// <param name="CurrentGameData">Complete Current game Session data</param>
    public static void DeclareGameData(GameData CurrentGameData)
    {
        OnUpdateGameData?.Invoke(CurrentGameData);
    }
}

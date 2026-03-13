using System;

public static class EventManager
{
    // Game Flow Events
    public static Action OnGameStart;
    public static Action OnGameEnd;

    //Card Selection Events
    public static Action OnCardSelected;
    public static Action<bool> OnCardMatchingChecked;

    // Score Change Events
    public static Action<int> OnScoreUpdate;
    public static Action<int> OnFinalScoreUpdate;

    public static void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public static void EndGame()
    {
        OnGameEnd?.Invoke();
    }

    public static void CardSelected()
    {
        OnCardSelected?.Invoke();
    }

    public static void CardMatchResult(bool IsMatching)
    {
        OnCardMatchingChecked?.Invoke(IsMatching);
    }

    public static void ScoreUpdate(int score)
    {
        OnScoreUpdate?.Invoke(score);
    }

    public static void DeclareFinalScore(int score)
    {
        OnFinalScoreUpdate?.Invoke(score);
    }
}

using UnityEngine;

/// <summary>
/// Individual Game Session Data
/// </summary>
[System.Serializable]
public class GameData
{
    /// <summary>
    /// Current Game Session Difficulty Index
    /// Defines the Row and Column Layout of the Card Grid
    /// </summary>
    public int DifficultyLevelIndex;

    /// <summary>
    /// Score for the Current Game Session
    /// </summary>
    public int Score;

    /// <summary>
    /// No of Attempts to complete the Currrent Game Session
    /// Match Check (2 card Selection) = one attempt
    /// </summary>
    public int Attempts;
}
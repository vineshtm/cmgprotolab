using System.Collections.Generic;

/// <summary>
/// Progress Data Entity
/// All time Game Session Data
/// </summary>
[System.Serializable]
public class ProgressData
{
    /// <summary>
    /// Total Number of Game Sessions Done
    /// Completion of Game Adds
    /// </summary>
    public int TotalGameSessions;

    /// <summary>
    /// Highest Score of All time
    /// </summary>
    public int HighestScore;

    /// <summary>
    /// Individual Game Session Data
    /// Can be minimised to Last 10-15 highest Score Game Data
    /// </summary>
    public List<GameData> GameDetailList;
}



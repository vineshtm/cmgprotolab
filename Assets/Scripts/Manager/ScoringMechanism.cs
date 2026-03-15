using UnityEngine;

/// <summary>
/// Scoring Mechanism - Manage the Scoring
/// </summary>
public class ScoringMechanism : MonoBehaviour
{
    /// <summary>
    /// Current Game Score
    /// </summary>
    private int m_Score;
    public int Score
    {
        get
        {
            if (m_Score < 0)
                return 0;
            else
                return m_Score;
        }
    }

    /// <summary>
    /// Reset On Game Start
    /// </summary>
    public void ResetScore()
    {
        m_Score = 0;
    }

    /// <summary>
    /// Update Score
    /// Add Score/Points
    /// </summary>
    /// <param name="value">Score</param>
    public void AddScore(int value)
    {
        m_Score += value;
    }
}

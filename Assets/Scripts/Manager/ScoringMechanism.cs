using UnityEngine;

/// <summary>
/// Scoring Mechanism - Manage the Scoring
/// </summary>
public class ScoringMechanism : MonoBehaviour
{
    /// <summary>
    /// Match Score
    /// Bonus score on Cards Match
    /// </summary>
    [SerializeField]
    private int m_MatchScore = 10; //+10 Points if Cards Match

    /// <summary>
    /// Mismatch Penalty
    /// Negative Points on Mismatches
    /// </summary>
    [SerializeField]
    private int m_MismatchPenalty = 5; // -5 points if cards does not match. Negative marking for mismatch applied

    /// <summary>
    /// No Of Continuos Matches to award Extra Bonus
    /// </summary>
    [SerializeField]
    private int m_ContinuousMatchCountForBonus = 3;

    /// <summary>
    /// Reward Bonus for Continous Matches
    /// </summary>
    [SerializeField]
    private int m_ContinuousMatchBonusPoint = 5;

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
    /// Identify Continuous matches. To keep track of Continuous matches
    /// </summary>
    private int m_ContinuousMatches = 0;

    /// <summary>
    /// Reset On Game Start
    /// </summary>
    public void ResetScoring()
    {
        m_Score = 0;
        m_ContinuousMatches = 0;
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

    /// <summary>
    /// Report the Card match Result for Scoring Update
    /// If match, add match score/bonus
    /// If mismatch, apply penalty
    /// </summary>
    /// <param name="IsMatching"></param>
    public void ReportMatchResult(bool IsMatching)
    {
        if (IsMatching)
        {
            m_Score += m_MatchScore;

            //Check for Contnuous Match Bonus
            m_ContinuousMatches++;
            if(m_ContinuousMatches >= m_ContinuousMatchCountForBonus)
            {
                //Apply Multiplied Bonus with Continuos Matching
                int Continousfactor = (m_ContinuousMatches / m_ContinuousMatchCountForBonus);
                int ExtraBonus = (Continousfactor > 1) ? (Continousfactor * m_ContinuousMatchBonusPoint) : (0);
                int ContinuousMatchScore = (m_ContinuousMatchBonusPoint + ExtraBonus);

                m_Score += ContinuousMatchScore;
            }
        }
        else
        {
            m_Score -= m_MismatchPenalty;

            m_ContinuousMatches = 0;
        }
    }

    /// <summary>
    /// Calculate the Final Score
    /// </summary>
    /// <returns></returns>
    public int GetFinalScore()
    {
        return Score;
    }
}

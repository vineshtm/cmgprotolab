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
    private int m_ContinuosMatchCountForBonus = 3;

    /// <summary>
    /// Reward Bonus for Continous Matches
    /// </summary>
    [SerializeField]
    private int m_ContinuosMatchBonusPoint = 5;

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

    private int m_ContinuosMatches = 0;

    /// <summary>
    /// Reset On Game Start
    /// </summary>
    public void ResetScoring()
    {
        m_Score = 0;
        m_ContinuosMatches = 0;
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
            m_ContinuosMatches++;
            if(m_ContinuosMatches >= m_ContinuosMatchCountForBonus)
            {
                //Apply Multiplied Bonus with Continuos Matching
                int Continousfactor = (m_ContinuosMatches / m_ContinuosMatchCountForBonus);
                int ExtraBonus = (Continousfactor > 1) ? (Continousfactor * m_ContinuosMatchCountForBonus) : (0);
                int ContinuosMatchScore = (m_ContinuosMatchCountForBonus + ExtraBonus);

                m_Score += ContinuosMatchScore;
            }
        }
        else
        {
            m_Score -= m_MismatchPenalty;

            m_ContinuosMatches = 0;
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

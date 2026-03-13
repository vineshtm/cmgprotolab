using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringMechanism : MonoBehaviour
{
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

    public void ResetScore()
    {
        m_Score = 0;
    }

    public void AddScore(int value)
    {
        m_Score += value;
    }
}

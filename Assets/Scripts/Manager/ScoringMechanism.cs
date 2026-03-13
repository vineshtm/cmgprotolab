using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringMechanism : MonoBehaviour
{
    private int m_Score;
    private int Score
    {
        get
        {
            if (m_Score < 0)
                return 0;
            else
                return m_Score;
        }
    }

    private void OnEnable()
    {
        EventManager.OnGameStart += StartGame;
        EventManager.OnScoreUpdate += AddScore;
        EventManager.OnGameEnd += GameOver;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= StartGame;
        EventManager.OnScoreUpdate -= AddScore;
        EventManager.OnGameEnd -= GameOver;
    }

    private void StartGame()
    {
        ResetScore();
    }

    private void GameOver()
    {
        EventManager.DeclareFinalScore(Score);
    }

    private void ResetScore()
    {
        m_Score = 0;
    }

    private void AddScore(int value)
    {
        m_Score += value;
    }
}

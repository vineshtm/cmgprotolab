using System;
using UnityEngine;

/// <summary>
/// Timer Component
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
	/// Timer Time
	/// </summary>
    private float m_ElapsedTime = 0f;    

    /// <summary>
    /// Timer Time in Seconds
    /// </summary>
    private int m_ElapsedSeconds = 0;

    /// <summary>
	/// Timer State
	/// </summary>
    private bool isRunning = false;

    /// <summary>
	/// Timer States
	/// </summary>
    private bool isPaused = false;

    /// <summary>
    /// Evert To be triggered every seconds - Ticks Every Second
    /// </summary>
    public event Action<int> OnSecondTick;

    /// <summary>
	/// Get Elapsed Timer Time
	/// </summary>
    public float ElapsedTime
    {
        get
        {
            return m_ElapsedTime;
        }
    }

    /// <summary>
	/// Get Elapsed Timer Time as Formatted string
	/// Format 00:00 (mm:ss)
	/// </summary>
    public string ElapsedTimeFormatted
    {
        get
        {
            int minutes = Mathf.FloorToInt(m_ElapsedTime / 60);
            int seconds = Mathf.FloorToInt(m_ElapsedTime % 60);

            return $"{minutes:00}:{seconds:00}";
        }
    }

    /// <summary>
    /// Get Elapsed Time in Seconds
    /// </summary>
    public int ElapsedTimeInSeconds
    {
        get
        {
            return m_ElapsedSeconds;
        }
    }

    void Update()
    {
        if (isRunning && !isPaused)
        {
            m_ElapsedTime += Time.deltaTime;

            int newSeconds = Mathf.FloorToInt(m_ElapsedTime);

            if (newSeconds > m_ElapsedSeconds)
            {
                m_ElapsedSeconds = newSeconds;
                OnSecondTick?.Invoke(m_ElapsedSeconds);
            }
        }
    }

    /// <summary>
    /// Start the Timer
    /// </summary>
    public void StartTimer()
    {
        m_ElapsedTime = 0f;
        m_ElapsedSeconds = 0;

        isRunning = true;
        isPaused = false;
    }

    /// <summary>
    /// Pause Timer
    /// </summary>
    public void PauseTimer()
    {
        if (!isRunning) return;
        isPaused = true;
    }

    /// <summary>
    /// Resume Timer
    /// </summary>
    public void ResumeTimer()
    {
        if (!isRunning) return;
        isPaused = false;
    }

    /// <summary>
    /// Stop Timer
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
        isPaused = false;
    }

    /// <summary>
    /// Reset Timer Time
    /// </summary>
    public void ResetTimer()
    {
        m_ElapsedTime = 0f;
        m_ElapsedSeconds = 0;
    }
}
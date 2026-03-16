using UnityEngine;

/// <summary>
/// Audio Manager managing the Audio System of the Game
/// Plays Audio clips based on Evenets
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource m_AudioSource; //Audio Source to Play Audio Clips

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip m_CardFlipClip; //Audio Clip when Card Flip

    [SerializeField]
    private AudioClip m_CardsMatchClip; //Audio Clip when Cards Match

    [SerializeField]
    private AudioClip m_CardsMismatchClip; //Audio Clip when Card Mismatch

    [SerializeField]
    private AudioClip m_GameOverClip; //Audio Clip when Game Over

    /// <summary>
    /// </summary>
    private void OnEnable()
    {
        //Register Events
        EventManager.OnCardSelected += PlayFlipSound;
        EventManager.OnCardMatchingChecked += OnCardMatchCheck;
        EventManager.OnGameEnd += PlayGameOverSound;

        //Play Background Music

    }

    /// <summary>
    /// </summary>
    private void OnDisable()
    {
        //Unregister Events
        EventManager.OnCardSelected -= PlayFlipSound;
        EventManager.OnCardMatchingChecked -= OnCardMatchCheck;
        EventManager.OnGameEnd -= PlayGameOverSound;
    }

    /// <summary>
    /// Cards Check for Match to play respective Sounds
    /// </summary>
    /// <param name="isMatching"></param>
    private void OnCardMatchCheck(bool isMatching)
    {
        if (isMatching)
        {
            PlayMatchSound();
        }
        else
        {
            PlayMismatchSound();
        }
    }

    /// <summary>
    /// Play Card flip sound
    /// </summary>
    private void PlayFlipSound()
    {
        if (m_AudioSource != null && m_CardFlipClip != null)
            m_AudioSource.PlayOneShot(m_CardFlipClip);
    }

    /// <summary>
    /// Play Cards Match Sound
    /// </summary>
    private void PlayMatchSound()
    {
        if (m_AudioSource != null && m_CardsMatchClip != null)
            m_AudioSource.PlayOneShot(m_CardsMatchClip);
    }

    /// <summary>
    /// Play Cards Mismatch Sound
    /// </summary>
    private void PlayMismatchSound()
    {
        if (m_AudioSource != null && m_CardsMismatchClip != null)
            m_AudioSource.PlayOneShot(m_CardsMismatchClip);
    }

    /// <summary>
    /// Play Game Over Sound
    /// Can also play different Clips based onb the Scores
    /// For Eg like if score = 0, Play "Better Next Time" and if good score"Excellent etc etc
    /// </summary>
    private void PlayGameOverSound()
    {
        if (m_AudioSource != null && m_GameOverClip != null)
            m_AudioSource.PlayOneShot(m_GameOverClip);
    }
}

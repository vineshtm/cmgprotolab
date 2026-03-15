using UnityEngine;

/// <summary>
/// Audio Manager managing the Audio System of the Game
/// Plays Audio clips based on Evenets
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource m_AudioSource; //Audio Source to Play Audio Clips

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip CardFlipClip; //Audio Clip when Card Flip

    [SerializeField]
    private AudioClip CardsMatchClip; //Audio Clip when Cards Match

    [SerializeField]
    private AudioClip CardsMismatchClip; //Audio Clip when Card Mismatch

    [SerializeField]
    private AudioClip GameOverClip; //Audio Clip when Game Over

    /// <summary>
    /// </summary>
    private void OnEnable()
    {
        //Register Events
        EventManager.OnCardSelected += PlayFlipSound;
        EventManager.OnCardMatchingChecked += OnCardMatchCheck;
        EventManager.OnGameEnd += PlayGameOverSound;
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
        if (m_AudioSource != null && CardFlipClip != null)
            m_AudioSource.PlayOneShot(CardFlipClip);
    }

    /// <summary>
    /// Play Cards Match Sound
    /// </summary>
    private void PlayMatchSound()
    {
        if (m_AudioSource != null && CardsMatchClip != null)
            m_AudioSource.PlayOneShot(CardsMatchClip);
    }

    /// <summary>
    /// Play Cards Mismatch Sound
    /// </summary>
    private void PlayMismatchSound()
    {
        if (m_AudioSource != null && CardsMismatchClip != null)
            m_AudioSource.PlayOneShot(CardsMismatchClip);
    }

    /// <summary>
    /// Play Game Over Sound
    /// </summary>
    private void PlayGameOverSound()
    {
        if (m_AudioSource != null && GameOverClip != null)
            m_AudioSource.PlayOneShot(GameOverClip);
    }
}

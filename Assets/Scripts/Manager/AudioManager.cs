using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip flip;
    public AudioClip match;
    public AudioClip mismatch;
    public AudioClip gameOver;

    private void OnEnable()
    {
        EventManager.OnCardSelected += PlayFlipSound;
        EventManager.OnCardMatchingChecked += OnCardMatchCheck;
        EventManager.OnGameEnd += PlayGameOverSound;
    }

    private void OnDisable()
    {
        EventManager.OnCardSelected -= PlayFlipSound;
        EventManager.OnCardMatchingChecked -= OnCardMatchCheck;
        EventManager.OnGameEnd -= PlayGameOverSound;
    }

    private void PlayFlipSound()
    {
        if (source != null && flip != null)
            source.PlayOneShot(flip);
    }

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

    private void PlayMatchSound()
    {
        if (source != null && match != null)
            source.PlayOneShot(match);
    }

    private void PlayMismatchSound()
    {
        if (source != null && mismatch != null)
            source.PlayOneShot(mismatch);
    }

    private void PlayGameOverSound()
    {
        if (source != null && gameOver != null)
            source.PlayOneShot(gameOver);
    }
}

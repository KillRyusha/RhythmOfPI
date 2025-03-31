using System;
using System.Collections;
using UnityEngine;

public class LastPlatfrom : Platform
{
    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private float _fadeOutTime;
    public event Action OnPlatformEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlatformEnter?.Invoke(); 
            StartCoroutine(FadeOut(_mainAudioSource, _fadeOutTime));
        }
    }
    public IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }
}
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelSetUpper : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private float _fadeInDuration = 2f;
    public void StartService()
    {
        _audioSource.clip = CurrentSongInfoSingleton.Instance.SongAudioClip;
        _titleText.text = CurrentSongInfoSingleton.Instance.SongTitle;
        _audioSource.volume = 0;
        _progressBar.StartProgress();
        _audioSource.Play();
        StartCoroutine(FadeInAudio());
    }

    private IEnumerator FadeInAudio()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _fadeInDuration)
        {
            _audioSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / _fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _audioSource.volume = 1f;
    }
}

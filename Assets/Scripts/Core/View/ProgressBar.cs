using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar; 
    private float duration; 
    private bool isFilling = false; 

    private void Start()
    {
    }

    public void StartProgress()
    {
        if (!isFilling)
        {
            duration = CurrentSongInfoSingleton.Instance.SongDuration;
            StartCoroutine(FillProgressBar());
        }
    }

    private IEnumerator FillProgressBar()
    {
        isFilling = true;
        float elapsedTime = 0f;
        float startValue = progressBar.value;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Lerp(startValue, 1f, elapsedTime / duration);
            yield return null;
        }

        progressBar.value = 1f;
        isFilling = false;
    }
}
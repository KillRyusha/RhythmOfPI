using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenerateSongPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private Button _submitButton;
    [SerializeField] private TMP_InputField _promptInput;
    [SerializeField] private string _hash;
    [SerializeField] private string _json; 
    private string _description;
    private string _taskId;
    private int waitTime=20;
    void Start()
    {
        _submitButton.onClick.RemoveAllListeners();
        _submitButton.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        _description = _promptInput.text;
        SongInfoJson song = JsonConvert.DeserializeObject<SongInfoJson>(_json);
       StartCoroutine(DownloadAudio(song));
        //StartCoroutine(PostRequest());
    }
    private IEnumerator PostRequest()
    {
        string postUrl = "https://rhythm-of-pi-d3f8f108a11d.herokuapp.com/api/generation/generate_audio/";
        WWWForm form = new WWWForm();
        _hash = IPHasher.GetHashedIP();
        form.AddField("description", _description);
        form.AddField("hash", _hash);
        postUrl += _description + "/" + _hash;
        Debug.Log(postUrl);
        UnityWebRequest request = UnityWebRequest.Post(postUrl, form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            _taskId = responseText.Substring(1, responseText.Length - 2);
            Debug.Log("Server response: " + responseText);
            StartCoroutine(GetsSongRequest());
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
        }

        request.Dispose();
    }
    private IEnumerator GetsSongRequest()
    {
        string postUrl = "https://rhythm-of-pi-d3f8f108a11d.herokuapp.com/api/generation/get_audio";
        WWWForm form = new WWWForm();
        _hash = IPHasher.GetHashedIP();
        form.AddField("hash", _hash);
        form.AddField("hash", _taskId);
        postUrl = postUrl + "/" + _hash + "/"+ _taskId;
        Debug.Log(postUrl);
        UnityWebRequest request = UnityWebRequest.Get(postUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;

            SongInfoJson song = JsonConvert.DeserializeObject<SongInfoJson>(responseText);
            StartCoroutine(DownloadAudio(song));
            Debug.Log("Server response: " + responseText);
        }
        else
        {
            //Debug.LogError("Request failed: " + request.error);
            yield return new WaitForSeconds(waitTime);
            waitTime = 2;
            StartCoroutine(GetsSongRequest());
        }

        request.Dispose();
    }
    private IEnumerator DownloadAudio(SongInfoJson song)
    {
        // Загружаем аудиофайл по ссылке
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(song.Url, AudioType.MPEG); // Указываем тип аудио файла (например, MPEG или WAV)

        yield return www.SendWebRequest(); // Ожидаем завершения запроса

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Загружаем аудиофайл
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);

            //CurrentSongInfoSingleton.Instance.SetCurrentSong(song.Title, song.Duration, song.Tags,1, audioClip);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("Ошибка загрузки аудиофайла: " + www.error);
        }
    }

}

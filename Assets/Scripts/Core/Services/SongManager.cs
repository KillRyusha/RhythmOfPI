using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
public class SongManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private Sprite _sprite;
    private string GetAudioFilePath(string url)
    {
        return Path.Combine(Application.persistentDataPath, MD5Hash(url) + ".mp3");
    }

    private string MD5Hash(string input)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            var hashBytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return System.BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
    public void SetSong(SongInfoJson song, Action<bool> callback)
    {
        StartCoroutine(LoadImage(song.ImageUrl));
        StartCoroutine(DownloadAndPlaySong(song, callback));
    }
    private IEnumerator DownloadAndPlaySong(SongInfoJson song, Action<bool> callback)
    {
        string filePath = GetAudioFilePath(song.Url);

        if (!File.Exists(filePath))
        {
            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(song.Url, AudioType.MPEG);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                File.WriteAllBytes(filePath, request.downloadHandler.data);
                SetSongInfo(clip, song, callback);
            }
            else
            {
                callback?.Invoke(false);
                Debug.LogError("Ошибка загрузки аудио: " + request.error);
            }
        }
        else
        {
            yield return StartCoroutine(LoadAudioFromFile(filePath, song, callback));
        }
    }

    private IEnumerator LoadAudioFromFile(string path, SongInfoJson song, Action<bool> callback)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            SetSongInfo(clip, song, callback);
        }
        else
        {
            callback?.Invoke(false);
            Debug.LogError("Ошибка загрузки локального аудио: " + request.error);
        }
    }

    private IEnumerator LoadImage(string url)
    {
        string filePath = Path.Combine(Application.persistentDataPath, MD5Hash(url) + ".png");

        if (File.Exists(filePath))
        {
            byte[] imageData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            CurrentSongInfoSingleton.Instance.SongSptite = SpriteFromTexture(texture);
            yield break;
        }

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            CurrentSongInfoSingleton.Instance.SongSptite = SpriteFromTexture(texture);
        }
        else
        {
            Debug.LogError("Ошибка загрузки изображения: " + request.error);
        }
    }

    private Sprite SpriteFromTexture(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    private void SetSongInfo(AudioClip clip, SongInfoJson song, Action<bool> callback)
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;
        CurrentSongInfoSingleton.Instance.SetCurrentSong(song.Title, song.Duration, song.Tags,song.Level,clip);
        callback?.Invoke(true);
    }

}
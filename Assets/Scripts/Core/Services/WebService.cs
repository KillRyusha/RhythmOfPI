using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class WebService
{
    private const string _getLevels = "https://rhythm-of-pi-d3f8f108a11d.herokuapp.com/api/levels/get_levels";
    private const string _getUserLevels = "https://rhythm-of-pi-d3f8f108a11d.herokuapp.com/api/registration/add_user/";
    private string _json;
    private ICoroutineRunner _runner;
    public WebService(ICoroutineRunner runner)
    {
        _runner = runner;
    }
    public void StartGame(Action<SongInfoJson> callback, string json ="")
    {
        _json = json;
        _runner.StartCoroutine(GetRequest(_getLevels, callback));
    }
    private IEnumerator GetRequest(string url, Action<SongInfoJson> onResponse)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            List<SongInfoJson> song = JsonConvert.DeserializeObject<List<SongInfoJson>>(responseText);
            _runner.StartCoroutine(GetLevel(_getUserLevels,onResponse,song));
        }
        else
        {
            SongInfoJson song = JsonConvert.DeserializeObject<SongInfoJson>(_json);
            onResponse?.Invoke(song);
        }

        request.Dispose();
    }

    private IEnumerator GetLevel(string url, Action<SongInfoJson> onResponse, List<SongInfoJson> songs)
    {
        url += IPHasher.GetUserIP() + "/" + IPHasher.GetHashedIP();
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            JsonUser json = JsonConvert.DeserializeObject<JsonUser>(responseText);
            int level = json.SelectedLevel;
            level = level < 1 ? 1 : level;
            onResponse?.Invoke(songs.FirstOrDefault(s => s.Level == level));
        }
        else
        {
            onResponse?.Invoke(songs.FirstOrDefault(s => s.Level == 1));
        }

        request.Dispose();
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;


public class JsonParser : MonoBehaviour
{
    private const string _jsonName = "numbers";
    public List<int> GetLevelNumbers(int level)
    {
        return LoadJsonFromResources()[level];
    }
    public Dictionary<int, List<int>> ConvertJsonToDict(string json)
    {
        Dictionary<string, string> rawData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

        foreach (var entry in rawData)
        {
            int level = int.Parse(entry.Key); 
            List<int> digits = new List<int>();

            foreach (char c in entry.Value)
            {
                if (char.IsDigit(c))
                {
                    digits.Add(c - '0'); 
                }
            }

            result[level] = digits;
        }

        return result;
    }

    public Dictionary<int, List<int>> LoadJsonFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(_jsonName);
        if (jsonFile == null)
        {
            Debug.LogError($"Τΰιλ {_jsonName}.json νε νΰιδεν β Resources!");
            return null;
        }

        return ConvertJsonToDict(jsonFile.text);
    }
}
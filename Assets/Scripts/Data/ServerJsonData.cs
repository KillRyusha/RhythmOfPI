using Newtonsoft.Json;

public class ServerJsonData
{
    [JsonProperty("title")]
    private string title;
    [JsonProperty("tags")]
    private string tags;
    [JsonProperty("url")]
    private string url;
    [JsonProperty("image_url")]
    private string image_url;
    [JsonProperty("duration")]
    private int duration;
    [JsonProperty("tacts")]
    private SongElements tacts;

    public string Title { get => title; }
    public string Tags { get => tags; }
    public string Url { get => url; }
    public string Image_url { get => image_url; }
    public int Duration { get => duration; }
}

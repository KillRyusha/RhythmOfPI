using Newtonsoft.Json;

public class SongInfoJson
{
    [JsonProperty("url")]
    public string Url { get; private set; }

    [JsonProperty("id")]
    public int Id { get; private set; }

    [JsonProperty("duration")]
    public int Duration { get; private set; }

    [JsonProperty("created_at")]
    public string CreatedAt { get; private set; }

    [JsonProperty("image_url")]
    public string ImageUrl { get; private set; }

    [JsonProperty("title")]
    public string Title { get; private set; }

    [JsonProperty("tags")]
    public string Tags { get; private set; }

    [JsonProperty("level")]
    public int Level { get; private set; }
}
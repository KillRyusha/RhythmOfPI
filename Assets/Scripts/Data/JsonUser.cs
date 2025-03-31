using Newtonsoft.Json;

public class JsonUser
{
    [JsonProperty("level")]
    public int Level { get; set; }
    [JsonProperty("selected_level")]
    public int SelectedLevel { get; set; }
}
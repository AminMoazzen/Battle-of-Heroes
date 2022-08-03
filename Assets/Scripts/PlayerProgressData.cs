using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProgressData
{
    private List<HeroProgressData> heroList;

    public List<HeroProgressData> HeroList => heroList;

    [JsonConstructor]
    public PlayerProgressData()
    {
        heroList = new List<HeroProgressData>();
    }

    public PlayerProgressData(string json)
    {
        var data = JsonConvert.DeserializeObject<PlayerProgressData>(json);
        heroList = data.heroList;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
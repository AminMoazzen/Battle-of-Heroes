using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProgressData
{
    private int levelsFinished;
    private List<HeroProgressData> heroList;

    public int LevelsFinished => levelsFinished;
    public List<HeroProgressData> HeroList => heroList;

    [JsonConstructor]
    public PlayerProgressData()
    {
        heroList = new List<HeroProgressData>();
    }

    public PlayerProgressData(string json)
    {
        var data = JsonConvert.DeserializeObject<PlayerProgressData>(json);
        levelsFinished = data.levelsFinished;
        heroList = data.heroList;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    public void IncreaseLevelsFinished()
    {
        levelsFinished++;
    }
}
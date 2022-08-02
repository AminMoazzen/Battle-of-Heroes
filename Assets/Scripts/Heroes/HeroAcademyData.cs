using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class HeroAcademyData
{
    public int hpMultiplier;

    public int apMulitplier;
    public List<HeroData> heroList;

    [JsonConstructor]
    public HeroAcademyData()
    {
        heroList = new List<HeroData>();
    }

    public HeroAcademyData(string json)
    {
        var data = JsonConvert.DeserializeObject<HeroAcademyData>(json);
        hpMultiplier = data.hpMultiplier;
        apMulitplier = data.apMulitplier;
        heroList = data.heroList;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
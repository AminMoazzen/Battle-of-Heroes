using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class HeroCollection
{
    public List<HeroData> heroList;

    [JsonConstructor]
    public HeroCollection()
    {
        heroList = new List<HeroData>();
    }

    public HeroCollection(string json)
    {
        var collection = JsonConvert.DeserializeObject<HeroCollection>(json);
        heroList = collection.heroList;
    }

    public void Add(HeroData hero)
    {
        heroList.Add(hero);
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(heroList, Formatting.Indented);
    }
}
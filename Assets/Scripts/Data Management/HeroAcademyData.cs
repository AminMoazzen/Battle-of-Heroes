using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class HeroAcademyData
{
    private float hpMultiplier;
    private float apMulitplier;
    private int xpToLevelUp;
    private List<HeroStaticData> heroCollection;

    public float HpMultiplier => hpMultiplier;
    public float ApMulitplier => apMulitplier;
    public int XpToLevelUp => xpToLevelUp;
    public List<HeroStaticData> HeroCollection => heroCollection;

    [JsonConstructor]
    public HeroAcademyData()
    {
        heroCollection = new List<HeroStaticData>();
    }

    public HeroAcademyData(string json)
    {
        var data = JsonConvert.DeserializeObject<HeroAcademyData>(json);
        hpMultiplier = data.HpMultiplier;
        apMulitplier = data.ApMulitplier;
        xpToLevelUp = data.xpToLevelUp;
        heroCollection = data.HeroCollection;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
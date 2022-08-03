using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class MonsterDenData
{
    private List<MonsterData> monsterCollection;

    public List<MonsterData> MonsterCollection => monsterCollection;

    [JsonConstructor]
    public MonsterDenData()
    {
        monsterCollection = new List<MonsterData>();
    }

    public MonsterDenData(string json)
    {
        var data = JsonConvert.DeserializeObject<MonsterDenData>(json);
        monsterCollection = data.monsterCollection;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
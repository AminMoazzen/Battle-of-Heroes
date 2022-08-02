using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerSaveLocal : PlayerSave
{
    public override void AddHero(HeroData hero)
    {
        heroes.Add(hero);
    }

    public override IEnumerator Fetch()
    {
        var inventoryJsonFile = GetSavePath();
        if (File.Exists(inventoryJsonFile))
        {
            var readRequest = File.ReadAllTextAsync(inventoryJsonFile);
            yield return readRequest;
            if (readRequest.IsCanceled || readRequest.IsFaulted)
            {
                heroes = new HeroCollection();
            }
            else
            {
                heroes = new HeroCollection(readRequest.Result);
            }
        }
        else
        {
            heroes = new HeroCollection();
        }
    }

    public override bool HasHero(HeroData hero)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Save()
    {
        string heroesJson = heroes.ToJson();
        var saveFile = GetSavePath();
        yield return File.WriteAllTextAsync(saveFile, heroesJson);
    }

    public override void UpdateHero(HeroData newHero)
    {
        throw new System.NotImplementedException();
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/player_save.json";
    }
}
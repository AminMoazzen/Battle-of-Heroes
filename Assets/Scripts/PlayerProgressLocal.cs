using System.Collections;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Player Progress", menuName = "Battle of Heroes / Local Player Progress")]
public class PlayerProgressLocal : PlayerProgress
{
    [SerializeField] private string fileName;
    [SerializeField] private int numInitialHeroes;

    public override void AddHero(HeroProgressData hero)
    {
        data.HeroList.Add(hero);
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
                GenerateInitialHeroes();
            }
            else
            {
                data = new PlayerProgressData(readRequest.Result);
            }
        }
        else
        {
            GenerateInitialHeroes();
        }
    }

    private void GenerateInitialHeroes()
    {
        data = new PlayerProgressData();
        for (int i = 0; i < numInitialHeroes; i++)
        {
            AddHero(new HeroProgressData(id: i, experience: 0, level: 1));
        }
    }

    public override bool HasHero(int id)
    {
        var hero = data.HeroList.Find((x) => x.Id == id);
        return hero != null;
    }

    public override IEnumerator Save()
    {
        string heroesJson = data.ToJson();
        var saveFile = GetSavePath();
        yield return File.WriteAllTextAsync(saveFile, heroesJson);
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public override void IncreaseExperience(int id, int amount, int xpToLevelUp)
    {
        var hero = data.HeroList.Find((x) => x.Id == id);

        if (hero != null)
        {
            hero.IncreaseExperience(amount, xpToLevelUp);
        }
        else
        {
            Debug.LogError($"Player does not own a hero with id number of '{id}'");
        }
    }
}
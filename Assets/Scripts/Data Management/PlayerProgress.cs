using System.Collections;
using UnityEngine;

public abstract class PlayerProgress : ScriptableObject
{
    protected PlayerProgressData data;

    public PlayerProgressData Data => data;

    public abstract IEnumerator Fetch();

    public abstract IEnumerator Save();

    public abstract void AddHero(HeroProgressData hero);

    public abstract void UnlockNextHero();

    public abstract bool HasHero(int id);

    public abstract void IncreaseExperience(int id, int amount, int xpToLevelUp);

    public abstract void IncreaseLevelsPlayed(int maxHeroCount);
}
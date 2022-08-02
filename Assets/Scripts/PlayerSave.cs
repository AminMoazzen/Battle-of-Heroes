using System.Collections;
using UnityEngine;

public abstract class PlayerSave : ScriptableObject
{
    public HeroCollection heroes;

    public abstract IEnumerator Fetch();

    public abstract IEnumerator Save();

    public abstract void AddHero(HeroData hero);

    public abstract bool HasHero(HeroData hero);

    public abstract void UpdateHero(HeroData newHero);
}
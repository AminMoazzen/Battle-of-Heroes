using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Competitors Reference", menuName = "Battle of Heroes / References / Competitorss Reference")]
public class CompetitorsReference : ScriptableObject
{
    private List<Hero> _heroes;
    private Monster _monster;

    public void Initialize()
    {
        if (_heroes == null)
        {
            _heroes = new List<Hero>();
        }
        else
        {
            _heroes.Clear();
        }

        _monster = null;
    }

    public void AddHero(Hero hero)
    {
        _heroes.Add(hero);
    }

    public void RemoveHero(Hero hero)
    {
        _heroes.Remove(hero);
    }

    public void AddMonster(Monster monster)
    {
        _monster = monster;
    }

    public Hero GetRandomHero()
    {
        return _heroes[Random.Range(0, _heroes.Count)];
    }

    public Monster GetMonster()
    {
        return _monster;
    }
}
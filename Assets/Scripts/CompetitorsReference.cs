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

    public void AddMonster(Monster monster)
    {
        _monster = monster;
    }

    public Hero GetRandomHero()
    {
        var hero = _heroes[Random.Range(0, _heroes.Count)];
        if (hero.IsDead)
        {
            _heroes.Remove(hero);
        }
        hero = _heroes[Random.Range(0, _heroes.Count)];
        return hero;
    }

    public Monster GetMonster()
    {
        return _monster;
    }
}
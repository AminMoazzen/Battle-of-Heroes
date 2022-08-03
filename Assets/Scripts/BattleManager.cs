using Nouranium;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleCoordinator coordinator;
    [SerializeField] private HeroSpawner heroSpawner;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private Vector3 heroStartPos;
    [SerializeField] private Vector3 monsterPos;

    [SerializeField] private MessageInt[] removeHeroOn;
    [SerializeField] private Message[] winBattleOn;

    [SerializeField] private UnityEvent onBattleWon;
    [SerializeField] private UnityEvent onBattleDefeated;

    private Transform _transform;
    private List<int> _battlingHeroIDs;

    private void Awake()
    {
        foreach (var msg in removeHeroOn)
        {
            msg.StartListening(RemoveHero);
        }

        foreach (var msg in winBattleOn)
        {
            msg.StartListening(WinBattle);
        }
    }

    public void SummonCompetitors()
    {
        _transform = transform;
        _battlingHeroIDs = coordinator.SelectedHeroeIDs;

        Vector3 offset = Vector3.zero;
        for (int i = 0; i < _battlingHeroIDs.Count; i++)
        {
            var randomInCircle = Random.insideUnitCircle;
            offset.Set(randomInCircle.x, 0, -i * 2 + randomInCircle.y);
            var pos = _transform.position + heroStartPos + offset;
            var heroSpanwer = Instantiate(heroSpawner, pos, heroSpawner.transform.rotation, _transform).GetComponent<HeroSpawner>();
            heroSpanwer.Spawn(_battlingHeroIDs[i]);
        }

        var monsterSpanwer = Instantiate(monsterSpawner, monsterPos, monsterSpawner.transform.rotation, _transform).GetComponent<MonsterSpawner>();
        monsterSpanwer.Spawn();
    }

    public void RemoveHero(int ID)
    {
        _battlingHeroIDs.Remove(ID);

        if (_battlingHeroIDs.Count == 0)
        {
            onBattleDefeated.Invoke();
        }
    }

    public void WinBattle()
    {
        onBattleWon.Invoke();
    }
}
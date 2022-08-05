using Nouranium;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private BattleCoordinator coordinator;
    [SerializeField] private CompetitorsReference competitorsReference;
    [SerializeField] private HeroSpawner heroSpawner;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private Vector3 heroStartPos;
    [SerializeField] private Vector3 monsterPos;
    [SerializeField][Min(1)] private float heroesDistance = 3;

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

    private void Start()
    {
        SummonCompetitors();
    }

    public void SummonCompetitors()
    {
        _transform = transform;
        _battlingHeroIDs = coordinator.SelectedHeroIDs;
        competitorsReference.Initialize();

        Vector3 offset = Vector3.zero;
        for (int i = 0; i < _battlingHeroIDs.Count; i++)
        {
            offset.Set(heroesDistance * Random.value, 0, heroesDistance * (i - _battlingHeroIDs.Count / 2));
            var pos = _transform.position + heroStartPos + offset;
            var heroSpanwer = Instantiate(heroSpawner, pos, heroSpawner.transform.rotation, null).GetComponent<HeroSpawner>();
            heroSpanwer.Spawn(_battlingHeroIDs[i]);
        }

        var monsterSpanwer = Instantiate(monsterSpawner, monsterPos, monsterSpawner.transform.rotation, null).GetComponent<MonsterSpawner>();
        monsterSpanwer.Spawn();
    }

    public void RemoveHero(int ID)
    {
        _battlingHeroIDs.Remove(ID);

        if (_battlingHeroIDs.Count == 0)
        {
            StartCoroutine(GeneratingResult(false));
        }
    }

    public void WinBattle()
    {
        StartCoroutine(GeneratingResult(true));
    }

    private IEnumerator GeneratingResult(bool hasWon)
    {
        if (hasWon)
        {
            for (int i = 0; i < _battlingHeroIDs.Count; i++)
            {
                playerProgress.IncreaseExperience(_battlingHeroIDs[i], 1, academy.Data.XpToLevelUp);
            }
        }

        playerProgress.IncreaseLevelsPlayed(academy.Data.HeroCollection.Count);

        yield return playerProgress.Save();

        if (hasWon)
        {
            onBattleWon.Invoke();
        }
        else
        {
            onBattleDefeated.Invoke();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private MonsterDen den;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UnityEvent<float> onDataLoadingProgressed;
    [SerializeField] private UnityEvent onDataLoaded;

    private IEnumerator Start()
    {
        yield return academy.Fetch();
        onDataLoadingProgressed.Invoke(0.33f);
        yield return den.Fetch();
        onDataLoadingProgressed.Invoke(0.66f);
        yield return playerProgress.Fetch();
        onDataLoadingProgressed.Invoke(1);

        onDataLoaded.Invoke();

        levelManager.LoadHeroSelection();
    }
}
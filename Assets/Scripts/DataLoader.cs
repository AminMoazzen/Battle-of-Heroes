using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private MonsterDen den;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private UnityEvent onDataLoaded;

    private IEnumerator Start()
    {
        yield return academy.Fetch();
        yield return den.Fetch();
        yield return playerProgress.Fetch();

        onDataLoaded.Invoke();
    }
}
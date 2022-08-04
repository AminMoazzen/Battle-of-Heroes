using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private CompetitorsReference competitorsReference;

    private HeroStaticData _staticData;
    private HeroProgressData _progressData;

    public void Spawn(int id)
    {
        _staticData = academy.Data.HeroCollection.Find(x => x.Id == id);
        _progressData = playerProgress.Data.HeroList.Find(x => x.Id == id);
        Addressables.InstantiateAsync(_staticData.PrefabAddress, transform).Completed += OnSpawned;
    }

    private void OnSpawned(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                var hero = obj.Result.GetComponent<Hero>();
                hero.Initialize(
                    _staticData.Id,
                    _staticData.GetScaledHealth(_progressData.Level, academy.Data.HpMultiplier),
                    _staticData.GetScaledAttackPower(_progressData.Level, academy.Data.ApMulitplier));
                competitorsReference.AddHero(hero);
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning($"Could not load the {_staticData.Name} prefab");
                break;

            default:
                break;
        }
    }
}
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterDen den;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private CompetitorsReference competitorsReference;

    private MonsterData _monsterData;

    public void Spawn()
    {
        int id = playerProgress.Data.LevelsFinished % den.Data.MonsterCollection.Count;
        _monsterData = den.Data.MonsterCollection.Find(x => x.Id == id);
        Addressables.InstantiateAsync(_monsterData.PrefabAddress, transform).Completed += OnSpawned;
    }

    private void OnSpawned(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                var monster = obj.Result.GetComponent<Monster>();
                monster.Initialize(_monsterData.Health, _monsterData.AttackPower);
                competitorsReference.AddMonster(monster);
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning($"Could not load the {_monsterData.Name} prefab");
                break;

            default:
                break;
        }
    }
}
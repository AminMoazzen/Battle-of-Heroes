using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterDen den;

    private MonsterData _monsterData;

    public void Spawn()
    {
        int id = Random.Range(0, den.Data.MonsterCollection.Count);
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
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning($"Could not load the {_monsterData.Name} prefab");
                break;

            default:
                break;
        }
    }
}
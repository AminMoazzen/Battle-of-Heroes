using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    [SerializeField] List<int> ids;
    [SerializeField] List<GameObject> gameObjects;

    Dictionary<int, GameObject> _collection;

    public void Bake()
    {
        _collection = new Dictionary<int, GameObject>();

        for (int i = 0; i < ids.Count; i++)
        {
            _collection.Add(ids[i], gameObjects[i]);
        }

    }

    public GameObject FindGameObject(int id)
    {
        return _collection[id];
    }
}

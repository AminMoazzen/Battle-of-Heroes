using System.Collections;
using UnityEngine;

public abstract class MonsterDen : ScriptableObject
{
    protected MonsterDenData data;

    public MonsterDenData Data => data;

    public abstract IEnumerator Fetch();
}
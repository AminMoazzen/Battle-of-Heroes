using System.Collections;
using UnityEngine;

public abstract class HeroAcademy : ScriptableObject
{
    protected HeroAcademyData data;

    public HeroAcademyData Data => data;

    public abstract IEnumerator Fetch();
}
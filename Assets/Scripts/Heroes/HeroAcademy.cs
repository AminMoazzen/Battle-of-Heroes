using System.Collections;
using UnityEngine;

public abstract class HeroAcademy : ScriptableObject
{
    public HeroAcademyData data;

    public abstract IEnumerator Fetch();
}
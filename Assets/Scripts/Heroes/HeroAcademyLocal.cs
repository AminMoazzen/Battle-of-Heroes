using System.Collections;
using UnityEngine;

public abstract class HeroAcademyLocal : HeroAcademy
{
    [SerializeField] private string jsonAddress;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync<TextAsset>(jsonAddress);

        yield return request;

        if (request.asset != null)
        {
            var txtAsset = (TextAsset)request.asset;
            data = new HeroAcademyData(txtAsset.text);
        }
    }
}
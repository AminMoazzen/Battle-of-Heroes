using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Hero Academy", menuName = "Battle of Heroes / Local Hero Academy")]
public class HeroAcademyLocal : HeroAcademy
{
    [SerializeField] private string jsonFileAddress;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync<TextAsset>(jsonFileAddress);

        yield return request;

        if (request.asset != null)
        {
            var txtAsset = (TextAsset)request.asset;
            data = new HeroAcademyData(txtAsset.text);
        }
    }
}
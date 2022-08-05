using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Local Monster Den", menuName = "Battle of Heroes / Local Monster Den")]
public class MonsterDenLocal : MonsterDen
{
    [SerializeField] private string jsonFileAddress;

    public override IEnumerator Fetch()
    {
        var request = Resources.LoadAsync<TextAsset>(jsonFileAddress);

        yield return request;

        if (request.asset != null)
        {
            var txtAsset = (TextAsset)request.asset;
            data = new MonsterDenData(txtAsset.text);
        }
    }
}
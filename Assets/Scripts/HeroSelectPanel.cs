using System.Collections;
using UnityEngine;

public class HeroSelectPanel : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private HeroCard cardPrefab;
    [SerializeField] private Transform grid;

    private void Start()
    {
        StartCoroutine(InitializePanel());
    }

    private IEnumerator InitializePanel()
    {
        yield return academy.Fetch();
        yield return playerProgress.Fetch();

        for (int i = 0; i < academy.Data.HeroCollection.Count; i++)
        {
            var card = Instantiate(cardPrefab, grid).GetComponent<HeroCard>();
            var staticData = academy.Data.HeroCollection[i];
            var progressData = playerProgress.Data.HeroList.Find((x) => x.Id == staticData.Id);
            card.Initialize(staticData, progressData, academy.Data.ApMulitplier);
        }
    }
}
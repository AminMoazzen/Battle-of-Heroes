using Nouranium;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectPanel : MonoBehaviour
{
    [SerializeField] private HeroAcademy academy;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private BattleCoordinator coordinator;
    [SerializeField] private HeroCard cardPrefab;
    [SerializeField] private Transform grid;
    [SerializeField] private Button startBattle;
    [SerializeField] private Message[] enableBattleButtonOn;
    [SerializeField] private Message[] disableBattleButtonOn;

    private void Awake()
    {
        foreach (var msg in enableBattleButtonOn)
        {
            msg.StartListening(() => startBattle.interactable = true);
        }

        foreach (var msg in disableBattleButtonOn)
        {
            msg.StartListening(() => startBattle.interactable = false);
        }
    }

    private void Start()
    {
        InitializePanel();
    }

    private void InitializePanel()
    {
        coordinator.Initialize();

        for (int i = 0; i < academy.Data.HeroCollection.Count; i++)
        {
            var card = Instantiate(cardPrefab, grid).GetComponent<HeroCard>();
            var staticData = academy.Data.HeroCollection[i];
            var progressData = playerProgress.Data.HeroList.Find((x) => x.Id == staticData.Id);
            card.Initialize(staticData, progressData, academy.Data.ApMulitplier);
        }
    }
}
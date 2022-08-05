using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour
{
    [SerializeField] private BattleCoordinator coordinator;
    [SerializeField] private Image thumbnail;
    [SerializeField] private GameObject locked;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroLevel;
    [SerializeField] private TextMeshProUGUI heroAttackPower;
    [SerializeField] private TextMeshProUGUI heroExperience;
    [SerializeField] private GameObject selectedFrame;
    [SerializeField] private GameObject info;

    private bool _isLocked;
    private bool _isSelected;
    private int _heroID;

    public void Initialize(HeroStaticData staticData, HeroProgressData progressData, float apMultiplier)
    {
        _heroID = staticData.Id;

        heroName.text = staticData.Name;
        int level = 1;
        int experience = 0;
        int attackPower = staticData.BaseAttackPower;

        Addressables.LoadAssetAsync<Sprite>(staticData.ThumbnailAddress).Completed += OnThumbnailLoaded;

        _isLocked = progressData == null;
        locked.SetActive(_isLocked);
        if (!_isLocked)
        {
            level = progressData.Level;
            experience = progressData.Experience;
            attackPower = staticData.GetScaledAttackPower(progressData.Level, apMultiplier);
        }

        heroLevel.text = level.ToString();
        heroExperience.text = experience.ToString();
        heroAttackPower.text = attackPower.ToString();
    }

    private void OnThumbnailLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                thumbnail.sprite = obj.Result;
                break;

            case AsyncOperationStatus.Failed:
                Debug.LogWarning($"Could not load the thumbnail for hero with ID of {_heroID}");
                break;

            default:
                break;
        }
    }

    public void OnClick()
    {
        if (!_isLocked)
        {
            if (_isSelected)
                Deselect();
            else
                Select();
        }
    }

    public void Select()
    {
        if (coordinator.CanSelectHero(_heroID))
        {
            _isSelected = true;
            selectedFrame.SetActive(true);
        }
    }

    public void Deselect()
    {
        coordinator.DeselectHero(_heroID);
        _isSelected = false;
        selectedFrame.SetActive(false);
    }

    public void OnHold()
    {
        info.SetActive(true);
    }

    public void OnRelease()
    {
        info.SetActive(false);
    }
}
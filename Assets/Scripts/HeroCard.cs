using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class HeroCard : MonoBehaviour
{
    [SerializeField] private Image thumbnail;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroLevel;
    [SerializeField] private TextMeshProUGUI heroAttackPower;
    [SerializeField] private TextMeshProUGUI heroExperience;
    [SerializeField] private GameObject selectedFrame;
    [SerializeField] private GameObject info;

    private bool _isLocked;
    private bool _isSelected;
    //private HeroBaseData _heroData;

    public void Initialize(HeroStaticData staticData, HeroProgressData progressData, float apMultiplier)
    {
        Addressables.LoadAssetAsync<Sprite>(staticData.ThumbnailAddress).Completed += OnThumbnailLoaded;

        heroName.text = staticData.Name;

        _isLocked = progressData == null;
        if (_isLocked)
        {
            heroLevel.text = "1";
            heroExperience.text = "0";
            heroAttackPower.text = staticData.BaseAttackPower.ToString();
        }
        else
        {
            heroLevel.text = progressData.Level.ToString();
            heroExperience.text = progressData.Experience.ToString();
            heroAttackPower.text = staticData.GetScaledAttackPower(progressData.Level, apMultiplier).ToString();
        }
    }

    private void OnThumbnailLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                thumbnail.sprite = obj.Result;
                break;

            case AsyncOperationStatus.Failed:
                //Debug.LogWarning($"Could not load the thumbnail for {_heroData.name}");
                break;

            default:
                break;
        }
    }

    public void OnClick()
    {
        if (_isSelected)
            Deselect();
        else
            Select();
    }

    public void Select()
    {
        if (!_isLocked)
        {
            _isSelected = true;
            selectedFrame.SetActive(true);
        }
    }

    public void Deselect()
    {
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
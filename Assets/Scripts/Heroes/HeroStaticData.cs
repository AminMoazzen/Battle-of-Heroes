using UnityEngine;

[System.Serializable]
public class HeroStaticData
{
    private int id;
    private string name;
    private int baseHealth;
    private int baseAttackPower;
    private string thumbnailAddress;
    private string prefabAddress;

    public int Id => id;
    public string Name => name;
    public int BaseHealth => baseHealth;
    public int BaseAttackPower => baseAttackPower;
    public string ThumbnailAddress => thumbnailAddress;
    public string PrefabAddress => prefabAddress;

    public HeroStaticData(int id, string name, int baseHealth, int baseAttackPower, string thumbnailAddress, string prefabAddress)
    {
        this.id = id;
        this.name = name;
        this.baseHealth = baseHealth;
        this.baseAttackPower = baseAttackPower;
        this.thumbnailAddress = thumbnailAddress;
        this.prefabAddress = prefabAddress;
    }

    public int GetScaledHealth(int level, float multiplier)
    {
        return Mathf.CeilToInt(BaseHealth * (1 + level * multiplier));
    }

    public int GetScaledAttackPower(int level, float multiplier)
    {
        return Mathf.CeilToInt(BaseAttackPower * (1 + level * multiplier));
    }
}
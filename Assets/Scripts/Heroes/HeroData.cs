using UnityEngine;

[System.Serializable]
public class HeroData
{
    public int id;
    public HeroAttributes attributes;
    public int xpToLevelUp;

    public void SetAttributes(HeroAttributes attributes)
    {
        this.attributes = attributes;
    }

    public int GetScaledHealth(float hpMultiplier)
    {
        return Mathf.CeilToInt(attributes.baseHealth * (1 + attributes.level * hpMultiplier));
    }

    public int GetScaledAttackPower(float apMultiplier)
    {
        return Mathf.CeilToInt(attributes.baseAttackPower * (1 + attributes.level * apMultiplier));
    }

    public void IncreaseExperience(int amount)
    {
        attributes.experience += amount;

        if (attributes.experience >= xpToLevelUp)
        {
            LevelUp();
            attributes.experience -= xpToLevelUp;
        }
    }

    private void LevelUp()
    {
        attributes.level++;
    }
}
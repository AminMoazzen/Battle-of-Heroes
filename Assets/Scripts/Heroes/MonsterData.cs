[System.Serializable]
public class MonsterData
{
    private int id;
    private string name;
    private int health;
    private int attackPower;
    private string thumbnailAddress;
    private string prefabAddress;

    public int Id => id;
    public string Name => name;
    public int Health => health;
    public int AttackPower => attackPower;
    public string ThumbnailAddress => thumbnailAddress;
    public string PrefabAddress => prefabAddress;
}
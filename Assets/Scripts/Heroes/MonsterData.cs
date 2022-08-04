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

    public MonsterData(int id, string name, int health, int attackPower, string thumbnailAddress, string prefabAddress)
    {
        this.id = id;
        this.name = name;
        this.health = health;
        this.attackPower = attackPower;
        this.thumbnailAddress = thumbnailAddress;
        this.prefabAddress = prefabAddress;
    }
}
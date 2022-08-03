[System.Serializable]
public class HeroProgressData
{
    private int id;
    private int experience;
    private int level;

    public int Id => id;
    public int Experience => experience;
    public int Level => level;

    public HeroProgressData(int id, int experience, int level)
    {
        this.id = id;
        this.experience = experience;
        this.level = level;
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Level Manager", menuName = "Battle of Heroes / Level Manager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] private string heroSelectionSceneName;
    [SerializeField] private string battleSceneName;

    public void LoadHeroSelection()
    {
        SceneManager.LoadScene(heroSelectionSceneName);
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene(battleSceneName);
    }
}
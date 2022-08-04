using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Battle Coordinator", menuName = "Battle of Heroes / Battle Coordinator")]
public class BattleCoordinator : ScriptableObject
{
    [Tooltip("Number of heroes required to start the battle")]
    [SerializeField][Min(1)] private int numHeroes = 3;

    [SerializeField] private UnityEvent<int> onHeroSelected;
    [SerializeField] private UnityEvent<int> onHeroDeselected;
    [SerializeField] private UnityEvent onEnoughHeroSelected;

    private List<int> _selectedHeroIDs;

    public List<int> SelectedHeroIDs => _selectedHeroIDs;

    public void Initialize()
    {
        _selectedHeroIDs = new List<int>(numHeroes);
    }

    public bool CanSelectHero(int heroID)
    {
        if (_selectedHeroIDs.Count == numHeroes)
        {
            return false;
        }

        _selectedHeroIDs.Add(heroID);
        onHeroSelected.Invoke(heroID);

        if (_selectedHeroIDs.Count == numHeroes)
        {
            onEnoughHeroSelected.Invoke();
        }

        return true;
    }

    public void DeselectHero(int heroID)
    {
        _selectedHeroIDs.Remove(heroID);
        onHeroDeselected.Invoke(heroID);
    }
}
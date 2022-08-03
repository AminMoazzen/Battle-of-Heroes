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

    private List<int> _selectedHeroeIDs;

    public List<int> SelectedHeroeIDs;

    public bool CanSelectHero(int heroID)
    {
        if (_selectedHeroeIDs == null)
            _selectedHeroeIDs = new List<int>(numHeroes);

        if (_selectedHeroeIDs.Count == numHeroes)
        {
            return false;
        }

        _selectedHeroeIDs.Add(heroID);
        onHeroSelected.Invoke(heroID);

        if (_selectedHeroeIDs.Count == numHeroes)
        {
            onEnoughHeroSelected.Invoke();
        }

        return true;
    }

    public void UnselectHero(int heroID)
    {
        var foundID = _selectedHeroeIDs.Find((id) => id == heroID);
        if (foundID != -1)
        {
            _selectedHeroeIDs.Remove(foundID);
            onHeroDeselected.Invoke(foundID);
        }
    }
}
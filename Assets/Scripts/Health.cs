using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private UnityEvent onDied;

    private int _maxHealth;
    private int _currentHealth;

    public void SetHealth(int amount)
    {
        _maxHealth = _currentHealth = amount;
        hpBar.value = hpBar.maxValue = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDied.Invoke();
    }
}
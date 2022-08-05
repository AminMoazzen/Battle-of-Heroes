using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Vector3 healthBarOffset;
    [SerializeField] private CameraReference camReference;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent onDied;

    private int _maxHealth;
    private int _currentHealth;

    public void Initialize(int healthPoint)
    {
        _maxHealth = _currentHealth = healthPoint;
        hpBar.value = hpBar.maxValue = _maxHealth;

        var screenPos = camReference.camera.WorldToScreenPoint(transform.position + healthBarOffset);
        hpBar.transform.position = screenPos;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        hpBar.value = _currentHealth;
        onHit.Invoke();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDied.Invoke();
    }
}
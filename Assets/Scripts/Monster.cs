using Nouranium;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Weapon))]
public class Monster : MonoBehaviour
{
    [SerializeField] private CompetitorsReference competitorsReference;
    [SerializeField] private float attackPreparationTime;
    [SerializeField] private Message[] attackOn;
    [SerializeField] private UnityEvent onMonsterAttacked;

    private Health _health;
    private Weapon _weapon;

    private void Awake()
    {
        foreach (var msg in attackOn)
        {
            msg.StartListening(Attack);
        }
    }

    public void Initialize(int hp, int ap)
    {
        if (_health == null)
            _health = GetComponent<Health>();
        _health.Initialize(hp);

        if (_weapon == null)
            _weapon = GetComponent<Weapon>();
        _weapon.Initialize(ap);
    }

    public void Attack()
    {
        StartCoroutine(PrepareThenShoot());
    }

    private IEnumerator PrepareThenShoot()
    {
        yield return new WaitForSeconds(attackPreparationTime);

        var hero = competitorsReference.GetRandomHero();

        _weapon.Shoot(hero.GetComponent<Health>());

        onMonsterAttacked.Invoke();
    }
}
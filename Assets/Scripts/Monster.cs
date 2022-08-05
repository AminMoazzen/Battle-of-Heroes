using Nouranium;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [SerializeField] private CompetitorsReference competitorsReference;
    [SerializeField] private Health health;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Message[] attackOn;
    [SerializeField] private UnityEvent onAttackBegan;

    public Health Health => health;

    private void Awake()
    {
        foreach (var msg in attackOn)
        {
            msg.StartListening(Attack);
        }
    }

    public void Initialize(int hp, int ap)
    {
        health.Initialize(hp);
        weapon.Initialize(ap);

        competitorsReference.AddMonster(this);
    }

    public void Attack()
    {
        onAttackBegan.Invoke();
    }

    public void Shoot()
    {
        var hero = competitorsReference.GetRandomHero();

        weapon.Shoot(hero.Health);
    }
}
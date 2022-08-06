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
    private AnimatorController _animController;

    private void Awake()
    {
        foreach (var msg in attackOn)
        {
            msg.StartListening(Attack);
        }
    }

    public void Initialize(int hp, int ap, AnimatorController animController)
    {
        health.Initialize(hp);
        weapon.Initialize(ap);
        _animController = animController;
        _animController.onShootFrame.AddListener(Shoot);

        competitorsReference.AddMonster(this);
    }

    public void Attack()
    {
        _animController.OnAttack();
        onAttackBegan.Invoke();
    }

    public void Shoot()
    {
        var hero = competitorsReference.GetRandomHero();

        weapon.Shoot(hero.Health);
    }

    public void OnHit()
    {
        _animController.OnHit();
    }

    public void OnDie()
    {
        _animController.OnDie();
    }
}
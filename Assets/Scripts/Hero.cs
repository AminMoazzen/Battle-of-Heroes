using Nouranium;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Weapon))]
public class Hero : MonoBehaviour
{
    [SerializeField] private CompetitorsReference competitorsReference;
    [SerializeField] private Message[] enableAttackOn;
    [SerializeField] private Message[] disableAttackOn;
    [SerializeField] private UnityEvent<int> onHeroAttacked;
    [SerializeField] private UnityEvent<int> onHeroDied;

    private Health _health;
    private Weapon _weapon;
    private bool _canAttack = true;
    private bool _isDead;
    private int _id;

    public bool IsDead => _isDead;

    private void Awake()
    {
        foreach (var msg in enableAttackOn)
        {
            msg.StartListening(() => SetCanAttack(true));
        }

        foreach (var msg in disableAttackOn)
        {
            msg.StartListening(() => SetCanAttack(false));
        }
    }

    public void Initialize(int id, int hp, int ap)
    {
        _id = id;

        if (_health == null)
            _health = GetComponent<Health>();
        _health.Initialize(hp);

        if (_weapon == null)
            _weapon = GetComponent<Weapon>();
        _weapon.Initialize(ap);
    }

    public void Attack()
    {
        if (_canAttack && !_isDead)
        {
            _canAttack = false;
            var monster = competitorsReference.GetMonster();

            _weapon.Shoot(monster.GetComponent<Health>());

            onHeroAttacked.Invoke(_id);
        }
    }

    public void OnHeroDied()
    {
        _isDead = true;
        onHeroDied.Invoke(_id);
    }

    public void SetCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }
}
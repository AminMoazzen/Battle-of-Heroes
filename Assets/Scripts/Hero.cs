using Nouranium;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    private enum State
    {
        ReadyToAttack,
        WaitingForTurn,
        Dead
    }

    [SerializeField] private CompetitorsReference competitorsReference;
    [SerializeField] private Health health;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Message[] enableAttackOn;
    [SerializeField] private Message[] disableAttackOn;
    [SerializeField] private UnityEvent<int> onAttackBegan;
    [SerializeField] private UnityEvent<int> onDied;

    public Health Health => health;

    private int _id;
    State _state;
    private AnimatorController _animController;

    private void Awake()
    {
        foreach (var msg in enableAttackOn)
        {
            msg.StartListening(() => _state = State.ReadyToAttack);
        }

        foreach (var msg in disableAttackOn)
        {
            msg.StartListening(() => _state = State.WaitingForTurn);
        }
    }

    public void Initialize(int id, int hp, int ap, AnimatorController animController)
    {
        _id = id;

        health.Initialize(hp);
        weapon.Initialize(ap);
        _animController = animController;
        _animController.onShootFrame.AddListener(Shoot);

        _state = State.ReadyToAttack;
        competitorsReference.AddHero(this);
    }

    public void BeginAttack()
    {
        if (_state == State.ReadyToAttack)
        {
            _animController.OnAttack();
            onAttackBegan.Invoke(_id);
        }
    }

    public void Shoot()
    {
        var monster = competitorsReference.GetMonster();
        weapon.Shoot(monster.Health);
    }

    public void OnHit()
    {
        _animController.OnHit();
    }

    public void OnDie()
    {
        _animController.OnDie();
    }

    public void OnHeroDied()
    {
        _state = State.Dead;
        competitorsReference.RemoveHero(this);
        onDied.Invoke(_id);
    }
}
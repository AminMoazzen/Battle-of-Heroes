using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    public UnityEvent onShootFrame;

    private readonly int AttackID = Animator.StringToHash("Attack");
    private readonly int HitID = Animator.StringToHash("Hit");
    private readonly int DieID = Animator.StringToHash("Die");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnAttack()
    {
        _animator.SetTrigger(AttackID);
    }

    public void OnHit()
    {
        _animator.SetTrigger(HitID);
    }

    public void OnDie()
    {
        _animator.SetTrigger(DieID);
    }

    public void OnShootFrame()
    {
        onShootFrame.Invoke();
    }
}
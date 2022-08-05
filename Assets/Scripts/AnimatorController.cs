using UnityEngine;
using UnityEngine.Events;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onShootFrame;

    private readonly int AttackID = Animator.StringToHash("Attack");
    private readonly int HitID = Animator.StringToHash("Hit");
    private readonly int DieID = Animator.StringToHash("Die");

    public void OnAttack()
    {
        animator.SetTrigger(AttackID);
    }

    public void OnHit()
    {
        animator.SetTrigger(HitID);
    }

    public void OnDie()
    {
        animator.SetTrigger(DieID);
    }

    public void OnShootFrame()
    {
        onShootFrame.Invoke();
    }
}
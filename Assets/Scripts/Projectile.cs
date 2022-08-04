using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField][Min(0.001f)] private float speed;

    private Transform _transform;

    public void Throw(int damage, Health targetHealth)
    {
        _transform = transform;
        var targetTransform = targetHealth.transform;

        var toTarget = targetTransform.position - _transform.position;
        _transform.forward = toTarget;
        _transform.DOMove(targetTransform.position, 1 / speed).onComplete += () =>
        {
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        };
    }
}
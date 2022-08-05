using DG.Tweening;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField][Min(0.001f)] private float speed;
    [SerializeField] private TextMeshPro floatingText;
    [SerializeField] private Vector3 floatingTextOffset;
    [SerializeField][Min(0.1f)] private float floatingTextFadeTime;
    [SerializeField][Min(0.1f)] private float floatingTextFlyHeight;

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
            ShowFloatingText(damage);
            Destroy(gameObject, Time.deltaTime);
        };
    }

    private void ShowFloatingText(int damage)
    {
        floatingText.gameObject.SetActive(true);
        floatingText.text = (-damage).ToString();

        var ftTransform = floatingText.rectTransform;
        ftTransform.SetParent(null);
        ftTransform.rotation = Quaternion.identity;
        ftTransform.position = _transform.position + floatingTextOffset;
        var destination = ftTransform.position + (Vector3.up * floatingTextFlyHeight);

        var endColor = floatingText.color;
        endColor.a = 0;

        ftTransform.DOMove(destination, floatingTextFadeTime);
        DOTween.To(() => floatingText.color, (c) => floatingText.color = c, endColor, floatingTextFadeTime).onComplete += () =>
        {
            Destroy(floatingText.gameObject, Time.deltaTime);
        };
    }
}
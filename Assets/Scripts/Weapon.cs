using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField][Min(1)] private int numOfProjectiles = 1;
    [SerializeField][Min(0)] private float delayBetweenShots = 0;

    private int _attackPower;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void Initialize(int attackPower)
    {
        _attackPower = attackPower;
    }

    public void Shoot(Health targetHealth)
    {
        StartCoroutine(Shooting(targetHealth));
    }

    private IEnumerator Shooting(Health targetHealth)
    {
        var wait = new WaitForSeconds(delayBetweenShots);

        for (int i = 0; i < numOfProjectiles; i++)
        {
            var proj = Instantiate(projectile, _transform.position, _transform.rotation).GetComponent<Projectile>();
            proj.Throw(_attackPower / numOfProjectiles, targetHealth);
            yield return wait;
        }
    }
}
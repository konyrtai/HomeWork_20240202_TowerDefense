using Assets.Scripts.Enemies;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    public float speed = 70f;
    public float damage = 10f;

    public void Seek(Transform target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        } 

        var dir = _target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;

        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        var enemy = _target.GetComponent<BaseEnemy>();
        enemy.Damage(damage);
        Destroy(gameObject);
    }
}
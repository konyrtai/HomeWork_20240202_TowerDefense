using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public Transform partToRotate;
    public float range = 35f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    /// <summary>
    /// Количество пуль в секунду
    /// </summary>
    public float fireRate = 5f;
    public float fireCountdown = 0f;
    //public float RotationSpeed = 10f;

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void Update()
    {
        TurnTowardToTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        if (target == null)
            return;

        var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }


    /// <summary>
    /// Наводим турель на цель
    /// </summary>
    /// <param name="rotationSpeed">Скорость поворота</param>
    void TurnTowardToTarget(float rotationSpeed = 30f)
    {
        if (target == null)
            return;

        if (partToRotate == null)
            return;

        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(TagConsts.Enemy).ToList();
        var turretPosition = (transform.position.x, transform.position.y, transform.position.z);

        enemies = enemies.Where(enemy => PositionUtils.IsPointInsideSphere(turretPosition, range, (enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z))).ToList();

        if (enemies.Any() == false)
        {
            target = null;
            return;
        }

        if (target != null)
        {
            // проверка что цель всё ещё в зоне атаки
            var isTargetInRange = PositionUtils.IsPointInsideSphere(turretPosition, range, (target.position.x, target.position.y, target.position.z));
            if (isTargetInRange == false)
                target = null;
        }

        if (target == null)
        {
            // находим ближайшую цель
            var nearestEnemyCoordinates = PositionUtils.GetClosestPoint(turretPosition, enemies.Select(enemy => (enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z)).ToList());
            var nearestEnemy = enemies.FirstOrDefault(x => x.transform.position.x == nearestEnemyCoordinates.x && x.transform.position.y == nearestEnemyCoordinates.y && x.transform.position.z == nearestEnemyCoordinates.z);
            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
                TurnTowardToTarget(10f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
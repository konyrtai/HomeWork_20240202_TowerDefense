using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed = 5f;

        private Transform target;
        private int? waypointIndex = null;

        void Start()
        {
            if (Waypoints.points.Any() == false)
                return;

            GetNextWaypoint();

            var info =  gameObject.GetComponent<BaseEnemy>();
            speed = info.Speed;
        }

        void Update()
        {
            var dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
            UpdateLookingDirection();
        }

        void UpdateLookingDirection()
        {
            if (target == null) return;

            var direction = target.position - transform.position;

            var rotation = Quaternion.LookRotation(direction);

            transform.rotation = rotation;
        }

        void GetNextWaypoint()
        {
            if (Waypoints.points.Any() == false)
                return;

            // инициализация базовой позиции
            if (waypointIndex.HasValue == false)
            {
                waypointIndex = 0;
            }
            // дошёл до финальной точки
            else if (waypointIndex.Value >= Waypoints.points.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            // следующая позиция
            else
            {
                waypointIndex++;
            }

            if (waypointIndex.HasValue == false)
                return;

            target = Waypoints.points[waypointIndex.Value];
        }
    }
}
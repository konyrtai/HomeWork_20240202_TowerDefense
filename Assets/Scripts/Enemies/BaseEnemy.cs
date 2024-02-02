using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public virtual float Health { get; set; }

        public virtual float Speed { get; set; } = 5f;

        public void Damage(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
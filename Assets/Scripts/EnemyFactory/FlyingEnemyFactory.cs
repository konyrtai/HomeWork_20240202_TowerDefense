using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory.Base;

namespace Assets.Scripts.EnemyFactory
{
    public class FlyingEnemyFactory : IEnemyFactory
    {
        public BaseEnemy Create() => new FlyingEnemy();
    }
}
using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory.Base;

namespace Assets.Scripts.EnemyFactory
{
    public class BossEnemyFactory : IEnemyFactory
    {
        public BaseEnemy Create() => new BossEnemy();
    }
}
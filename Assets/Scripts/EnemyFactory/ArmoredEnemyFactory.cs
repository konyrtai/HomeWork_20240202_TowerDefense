using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory.Base;

namespace Assets.Scripts.EnemyFactory
{
    public class ArmoredEnemyFactory : IEnemyFactory
    {
        public BaseEnemy Create() => new ArmoredEnemy();
    }
}
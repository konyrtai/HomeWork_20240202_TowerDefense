using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory.Base;

namespace Assets.Scripts.EnemyFactory
{
    public class InfantrymanEnemyFactory : IEnemyFactory
    {
        public BaseEnemy Create() => new InfantrymanEnemy();
    }
}
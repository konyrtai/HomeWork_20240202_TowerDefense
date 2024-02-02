using Assets.Scripts.Enemies;

namespace Assets.Scripts.EnemyFactory.Base
{
    public interface IEnemyFactory
    {
        BaseEnemy Create();
    }
}
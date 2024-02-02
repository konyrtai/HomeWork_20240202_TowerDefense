using System;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory.Base;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.EnemyFactory
{
    public class EnemyFactory : MonoBehaviour
    {
        public GameObject ArmoredEnemyPrefab;
        public GameObject InfantrymanEnemyPrefab;
        public GameObject FlyingEnemyPrefab;
        public GameObject BossPrefab;

        public (GameObject enemyPrefab, List<Type> logic) Create(EnemyType type)
        {
            IEnemyFactory factory = type switch
            {
                EnemyType.Infantryman => new InfantrymanEnemyFactory(),
                EnemyType.Armored => new ArmoredEnemyFactory(),
                EnemyType.Flying => new FlyingEnemyFactory(),
                EnemyType.Boss => new BossEnemyFactory(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var enemyPrefab = type switch
            {
                EnemyType.Infantryman => InfantrymanEnemyPrefab,
                EnemyType.Armored => ArmoredEnemyPrefab,
                EnemyType.Flying => FlyingEnemyPrefab,
                EnemyType.Boss => BossPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return (enemyPrefab, new List<Type>
            {
                factory.Create().GetType(),
                typeof(EnemyMovement),
            });
        }
    }
}
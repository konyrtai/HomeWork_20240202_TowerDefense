using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.EnemyFactory;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public EnemyFactory factory = new();

    #region UI

    public Text aliveEnemiesText;

    #endregion

    /// <summary>
    /// Количество волн противников
    /// </summary>
    private readonly int _totalWavesCount = 5;

    /// <summary>
    /// Номер текущей волны
    /// </summary>
    private int _currentWaveIndex = 1;

    public Transform spawnPoint;

    public Transform enemyPrefab;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;

    public event Action<int> Countdown;

    void Update()
    {
        if (CheckToSpawnNewWave())
        {
            StartCoroutine(SpawnWave());
        }

        //if (countdown <= 0f)
        //{
        //    
        //    countdown = timeBetweenWaves;
        //}


        //countdown -= Time.deltaTime;
        //waveCountdownText.text = MathF.Floor(countdown + 1).ToString();
    }

    bool CheckToSpawnNewWave()
    {
        if (_currentWaveIndex > _totalWavesCount)
        {
            return false;
        }

        var aliveEnemies = GameObject.FindGameObjectsWithTag(TagConsts.Enemy);
        if (aliveEnemies.Any())
        {
            aliveEnemiesText.text = aliveEnemies.Length.ToString();
            return false;
        }

        _currentWaveIndex++;
        return true;
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;

        if (waveIndex > 4)
            yield break;

        yield return waveIndex switch
        {
            1 => FirstWave(),
            2 => SecondWave(),
            3 => ThirdWave(),
            4 => FourthWave(),
            _ => throw new ArgumentOutOfRangeException()
        };

    }

    IEnumerator FirstWave()
    {
        var enemies = new List<(EnemyType, int)>()
        {
            (EnemyType.Infantryman, 10),
        };

        return SpawnEnemy(enemies);
    }

    IEnumerator SecondWave()
    {
        var enemies = new List<(EnemyType, int)>()
        {
            (EnemyType.Infantryman, 10),
            (EnemyType.Armored, 5),
        };

        return SpawnEnemy(enemies);
    }

    IEnumerator ThirdWave()
    {
        var enemies = new List<(EnemyType, int)>()
        {
            (EnemyType.Infantryman, 10),
            (EnemyType.Armored, 5),
            (EnemyType.Flying, 3),
        };

        return SpawnEnemy(enemies);
    }

    IEnumerator FourthWave()
    {
        var enemies = new List<(EnemyType, int)>()
        {
            (EnemyType.Boss, 1),
        };

        return SpawnEnemy(enemies);
    }

    IEnumerator SpawnEnemy(List<(EnemyType, int)> enemies)
    {
        if (enemies == null)
            yield break;

        if (enemies.Count == 0)
            yield break;

        foreach (var (enemyType, count) in enemies)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnEnemy(enemyType);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void SpawnEnemy(EnemyType enemyType)
    {
        var (prefab, logic) = factory.Create(enemyType);

        var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        enemy.gameObject.tag = TagConsts.Enemy;
        foreach (var l in logic)
        {
            enemy.gameObject.AddComponent(l);
        }
    }
}

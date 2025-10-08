using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : Manager<EnemyManager>
{
    public static List<Enemy> Enemies;

    public Enemy enemyPrefab;

    public List<Enemy> remainingEnemies => Enemies.Where(x => x.alive).ToList();
    public List<Wave> waves;
    public int currentWave { get; private set; }

    public UnityAction onWaveStarted;
    public UnityAction onWaveFinished;
    public UnityAction onAllWavesFinished;
    
    public override void Initialize()
    {
        base.Initialize();
        Register(this);
        Enemies = new List<Enemy>();
        Initialized = true;
    }

    public void NextWave()
    {
        currentWave++;
        SpawnWave();
    }
    
    public void SpawnWave()
    {
        var wave = waves[currentWave];

        foreach (var element in wave.enemies)
        {
            for (int i = 0; i < element.count; i++)
            {
                Vector3 spawnPos = GetSpawnPosition();
                var spawned = Instantiate(element.prefab, spawnPos, Quaternion.identity, this.transform).GetComponent<Enemy>();
                spawned.Initialize();
                spawned.onDeath += OnEnemyDeath;
                Enemies.Add(spawned);
            }
        }

        onWaveStarted?.Invoke();
    }

    public void FinishWave()
    {
        if ((currentWave + 1) >= waves.Count)
        {
            onAllWavesFinished?.Invoke();
        }
        else
        {
            onWaveFinished?.Invoke();
        }
        CleanupWave();
    }

    public void CleanupWave()
    {
        foreach (var enemy in Enemies.Where(x => !x.alive && x != null))
        {
            Destroy(enemy.gameObject);
        }

        Enemies.RemoveAll(x => x == null);
    }

    public List<Enemy> GetEnemiesInRange(Vector3 point, float range)
    {
        return remainingEnemies.Where(x => Vector3.Distance(x.transform.position, point) < range).ToList();
    }

    private void OnEnemyDeath(Character enemy)
    {
        enemy.onDeath -= OnEnemyDeath;
        if (remainingEnemies.Count <= 0)
        {
            FinishWave();
        }
    }

    public static Vector3 GetSpawnPosition()
    {
        float spacing = GameManager.Instance.config.enemySpacing;
        float extent = GameManager.Instance.config.arenaExtent;
        Vector2 r = Vector2.zero;
        Vector3 r3 = Vector3.zero;
        bool valid = false;
        int retries = 100;
        while (!valid)
        {
            // circle arena
            // r = Random.insideUnitCircle * radius;
            // square arena
            r = new Vector2(((Random.value * 2f) - 1f) * extent, ((Random.value * 2f) - 1f) * extent);
            r3 = new Vector3(r.x, 0, r.y);

            valid = true;
            
            foreach (var enemy in Enemies)
            {
                if (Vector3.Distance(enemy.transform.position, r3) < spacing)
                {
                    valid = false;
                    retries--;
                    break;
                }
            }

            if (retries <= 0)
            {
                return Vector3.zero;
            }
        }

        return r3;
    }

    public static Enemy GetClosestInRange(Vector3 position, float range)
    {
        float max = range;
        Enemy target = null;
        foreach (var enemy in Instance.remainingEnemies)
        {
            float dist = Vector3.Distance(position, enemy.transform.position);
            if (dist < max)
            {
                max = dist;
                target = enemy;
            }
        }

        return target;
    }

    public static List<Enemy> GetAllInRange(Vector3 position, float range)
    {
        return Instance.remainingEnemies.Where(x => Vector3.Distance(x.transform.position, position) < range).ToList();
    }
}

[System.Serializable]
public class Wave
{
    public List<WaveElement> enemies = new List<WaveElement>();
}

[System.Serializable]
public class WaveElement
{
    public GameObject prefab;
    public int count;
}

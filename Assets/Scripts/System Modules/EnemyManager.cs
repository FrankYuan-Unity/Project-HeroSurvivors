using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject RandomEnemy => enemyList.Count == 0 ? null : enemyList[Random.Range(0, enemyList.Count)];
    public int WaveNumber => waveNumber;
    public float TimeBetweenWaves => timeBetweenWaves;

    [SerializeField] bool spawnEnemy = true;
    [SerializeField] GameObject waveUI;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] int minEnemyAmount = 4;
    [SerializeField] int maxEnemyAmount = 10;

    [Header("---- Boss Settings ----")]
    [SerializeField] GameObject bossPrefab;
    [SerializeField] int bossWaveNumber;

    int waveNumber = 1;
    int enemyAmount;

    List<GameObject> enemyList;

    WaitForSeconds waitTimeBetweenSpawns;
    WaitForSeconds waitTimeBetweenWaves;

    WaitUntil waitUntilNoEnemy;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<GameObject>();
        waitTimeBetweenSpawns = new WaitForSeconds(timeBetweenSpawns);
        waitTimeBetweenWaves = new WaitForSeconds(timeBetweenWaves);
        waitUntilNoEnemy = new WaitUntil(() => enemyList.Count == 0);
    }

    IEnumerator Start()
    {
        while (spawnEnemy && GameManager.GameState != GameState.GameOver)
        {
            print("waveNumber = " + waveNumber);
            if (waveNumber == 2)
                waveUI.SetActive(true);

            yield return waitTimeBetweenWaves;

            if (waveNumber == 2)
                waveUI.SetActive(false);

            yield return StartCoroutine(nameof(RandomlySpawnCoroutine));
        }
    }

    IEnumerator RandomlySpawnCoroutine()
    {
        // if (waveNumber % bossWaveNumber == 0)
        // {
        //     var boss = PoolManage.Release(bossPrefab);
        //     enemyList.Add(boss);
        // }
        // else 
        // {
        enemyAmount = Mathf.Clamp(enemyAmount, minEnemyAmount + waveNumber / bossWaveNumber, maxEnemyAmount);

        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            enemyList.Add(PoolManage.Release(enemyPrefab, Viewport.Instance.RandomEnemyBirthPosition(0, 0)));

            yield return waitTimeBetweenSpawns;
        }
        // }

        yield return waitUntilNoEnemy;

        waveNumber++;
    }

    public void RemoveFromList(GameObject enemy) => enemyList.Remove(enemy);


}
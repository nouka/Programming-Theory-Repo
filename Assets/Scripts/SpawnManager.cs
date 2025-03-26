using UnityEngine;

// TODO: クラスのプロパティについてカプセル化を検討する。
public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject sentinelPrefab;
    public GameObject[] powerUpPrefabs;
    public int enemiesCount = 0;

    readonly float spawnRange = 9.0f;
    int waveNumber = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int index = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[index], GenerateSpawnPosition(), powerUpPrefabs[index].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemiesCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemiesCount == 0)
        {
            waveNumber++;
            if (waveNumber % 3 == 0)
            {
                SpawnBossWave();
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
            int index = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[index], GenerateSpawnPosition(), powerUpPrefabs[index].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], GenerateSpawnPosition(), enemyPrefabs[index].transform.rotation);
        }
    }

    void SpawnBossWave()
    {
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        for (int i = 0; i < 6; i++)
        {
            Instantiate(sentinelPrefab, GenerateSpawnPosition(), sentinelPrefab.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(posX, 0, posZ);
    }
}

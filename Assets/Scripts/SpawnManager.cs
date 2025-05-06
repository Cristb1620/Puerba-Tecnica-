using UnityEngine;
using TMPro;

public class SpawnManger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 5.0f;
    public int enemyCount;
    public int waveNumber;
    public TMP_Text enemyCountText;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        InvokeRepeating("SpawnEnemyWithLimit", 2f, 2f);
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        UpdateEnemyCountUI();

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }

        if (enemyCount >= 10)
        {
            CancelInvoke("SpawnEnemyWithLimit");
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            AssignRandomColorToEnemy(enemy); // Asignar color aleatorio al enemigo
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawPosX = Random.Range(-spawnRange, spawnRange);
        float spawPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawPosX, 0, spawPosZ);

        return randomPos;
    }

    void SpawnEnemyWithLimit()
    {
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount < 10)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            AssignRandomColorToEnemy(enemy); // Asignar color aleatorio al enemigo
        }
    }

    // Función para asignar un color aleatorio al material del enemigo
    void AssignRandomColorToEnemy(GameObject enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>(); // Obtenemos el Renderer del enemigo
        if (enemyRenderer != null) // Verificamos si el enemigo tiene un Renderer
        {
            enemyRenderer.material.color = Random.ColorHSV(); // Asignamos un color aleatorio
        }
    }

    void UpdateEnemyCountUI()
    {
        enemyCountText.text = "Enemigos Activos: " + enemyCount.ToString();
    }
}

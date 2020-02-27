using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public GameObject MeteorGO;
    public GameObject PointBoost50;

    float maxSpawnRateInSeconds = 5f;

    // flag para o bonus: 1 se o bonus for do 1º inimigo e esquerda, 2 se o bonus for do 2º inimigo e meio
    // 3 se o bonus for do 3º inimigo e direita
    private static int checkIfBonus = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // bottom-left point (corner) of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // top-right point (corner) of the screen

        // random entre 3 opcoes
        //int enemyRoll = Random.Range(1, 4);
        int enemyRoll = 1;
        switch (enemyRoll)
        {
            case 1:
                GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
                anEnemy.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);

                break;
            case 2:
                GameObject anMeteor = (GameObject)Instantiate(MeteorGO);
                anMeteor.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);

                break;
            case 3:
                GameObject anBoost = (GameObject)Instantiate(PointBoost50);
                anBoost.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);
                checkIfBonus = 1;

                break;
        }

        //Produzir novos inimigos em tempo aleatorio
        //ScheduleNextEnemySpawn();

        //Produzir novos inimigos de 4 em 4 segundos
        ScheduleNextEnemySpawnFourSeconds();
    }

    // produz cada inimigo entre 1 a 5s depois do inimigo anterior, de forma aleatoria
    void ScheduleNextEnemySpawnRandom()
    {
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            // o proximo inimigo vai aparecer Random.Range(1f, maxSpawnRateInSeconds) depois (>1 e <5)
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    // produz novo inimigo a cada 4 segundos
    void ScheduleNextEnemySpawnFourSeconds()
    {
        Invoke("SpawnEnemy", 4f);
    }

    // aumenta a velocidade com que faz spawn
    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        
        if(maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    // começar o enemy spawner
    public void ScheduleEnemySpawner(float initialSpawnRate)
    {
        Debug.Log("float recebido EnemySpanwer: " + initialSpawnRate);
        Invoke("SpawnEnemy", initialSpawnRate);

        //InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // parar o enemy spawner
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

    public static int GetBonus()
    {
        return checkIfBonus;
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public GameObject MeteorGO;
    public GameObject PointBoost50;

    float maxSpawnRateInSeconds = 5f;
    float inicialSpawnRate = 2f;

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
        int enemyRoll = 3;
        Debug.Log("roll: " + enemyRoll);
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

                break;
        }

        /*
        // objetos em posicoes random no x do lado esq do ecra ate 1/3 do ecra
        if (Random.value < 0.5f)
        {
            GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
            anEnemy.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);
        } else
        {
            GameObject anMeteor = (GameObject)Instantiate(MeteorGO);
            anMeteor.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);
        }
        */

        //Schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
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

    // aumenta a velocidade com que faz spawn
    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        
        if(maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    // começar o enemy spawner
    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", inicialSpawnRate);

        //InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // parar o enemy spawner
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public GameObject MeteorLeftGO;
    public GameObject MeteorGreyLeftGO;
    public GameObject Boost100Left;

    private GameObject Enemy;
    private GameObject Meteor;
    private GameObject Boost;

    float maxSpawnRateInSeconds = 10f;
    public static float speed = 1f;

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
        int enemyRoll = Random.Range(1, 4);
        //int enemyRoll = 3;
        switch (enemyRoll)
        {
            case 1:
                Enemy = (GameObject)Instantiate(EnemyGO);
                Enemy.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);

                break;
            case 2:
                if (Random.value < 0.5f)
                {
                    Meteor = (GameObject)Instantiate(MeteorLeftGO);
                } else
                {
                    Meteor = (GameObject)Instantiate(MeteorGreyLeftGO);
                }
                Meteor.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);

                break;
            case 3:

                // 80/20 entre escolher o boost ou não escolher inimigo nenhum
                if(Random.value > 0.2)
                {
                    Boost = (GameObject)Instantiate(Boost100Left);
                    Boost.transform.position = new Vector2(((min.x + max.x) / 2) - 1.2f, max.y);
                    checkIfBonus = 1;
                }

                break;
        }

        //Produzir novos inimigos em tempo aleatorio
        ScheduleNextEnemySpawnRandom();

        //Produzir novos inimigos de 4 em 4 segundos
        //ScheduleNextEnemySpawnFourSeconds();
    }

    // produz cada inimigo entre 1 a 10s depois do inimigo anterior, de forma aleatoria
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
        Invoke("SpawnEnemy", 2f);
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

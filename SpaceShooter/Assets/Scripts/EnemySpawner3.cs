using UnityEngine;

public class EnemySpawner3 : MonoBehaviour
{
    public GameObject EnemyGO2;
    public GameObject MeteorRightGO;
    public GameObject MeteorGreyRightGO;
    public GameObject Boost100Right;

    float maxSpawnRateInSeconds = 10f;

    private GameObject Enemy;
    private GameObject Meteor;
    private GameObject Boost;

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
                Enemy = (GameObject)Instantiate(EnemyGO2);
                Enemy.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);

                break;
            case 2:
                if (Random.value < 0.5f)
                {
                    Meteor = (GameObject)Instantiate(MeteorRightGO);
                }
                else
                {
                    Meteor = (GameObject)Instantiate(MeteorGreyRightGO);
                }
                Meteor.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);

                break;
            case 3:
                Boost = (GameObject)Instantiate(Boost100Right);
                Boost.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);
                checkIfBonus = 3;

                break;
        }

        //Produzir novos inimigos em tempo aleatorio
        ScheduleNextEnemySpawnRandom();

        //Produzir novos inimigos de 4 em 4segundos
        //ScheduleNextEnemySpawnFourSeconds();
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
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
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

﻿using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject EnemyGO1;
    public GameObject MeteorMidGO;
    public GameObject MeteorGreyMidGO;
    public GameObject Boost100Mid;

    private GameObject Enemy;
    private GameObject Meteor;
    private GameObject Boost;

    public static float maxSpawnRateInSeconds = 8.1f;

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
                Enemy = (GameObject)Instantiate(EnemyGO1);
                Enemy.transform.position = new Vector2((min.x + max.x) / 2, max.y);

                break;
            case 2:
                if (Random.value < 0.5f)
                {
                    Meteor = (GameObject)Instantiate(MeteorMidGO);
                }
                else
                {
                    Meteor = (GameObject)Instantiate(MeteorGreyMidGO);
                }
                Meteor.transform.position = new Vector2((min.x + max.x) / 2, max.y);

                break;
            case 3:
                // 80/20 entre escolher o boost ou não escolher inimigo nenhum
                if (Random.value > 0.7)
                {
                    Boost = (GameObject)Instantiate(Boost100Mid);
                    Boost.transform.position = new Vector2((min.x + max.x) / 2, max.y);
                    checkIfBonus = 2;
                }

                break;
        }

        //Produzir novos inimigos em tempo aleatorio
        ScheduleNextEnemySpawnRandom();

        //Produzir novos inimigos de 4 em 4 segundos
        //ScheduleNextEnemySpawnFourSeconds();
    }

    // produz cada inimigo entre 3 a 8.9s depois do inimigo anterior, de forma aleatoria
    void ScheduleNextEnemySpawnRandom()
    {
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            // o proximo inimigo vai aparecer Random.Range(1f, maxSpawnRateInSeconds) depois (>3 e <5)
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            Debug.Log("ES2 next in " + spawnInNSeconds);
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

    public static void UpdateMaxSpawnRate(float updatedSpawnRate)
    {
        maxSpawnRateInSeconds = updatedSpawnRate;
    }

    public static float GetMaxSpawnRate()
    {
        return maxSpawnRateInSeconds;
    }
}

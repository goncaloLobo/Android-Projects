﻿using UnityEngine;

public class EnemySpawner3 : MonoBehaviour
{
    public GameObject EnemyGO2;
    public GameObject MeteorRightGO;
    public GameObject MeteorGreyRightGO;
    public GameObject Boost100Right;

    public static float maxSpawnRateInSeconds = 8.1f;
    public static float minSpawnRateInSeconds = 1f;
    public float r;

    private GameObject Enemy;
    private GameObject Meteor;
    private GameObject Boost;

    // flag para o bonus: 1 se o bonus for do 1º inimigo e esquerda, 2 se o bonus for do 2º inimigo e meio
    // 3 se o bonus for do 3º inimigo e direita
    private static int checkIfBonus = 0;

    void Start()
    {

    }

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
                if (Random.value < 0.2f)
                {
                    Boost = (GameObject)Instantiate(Boost100Right);
                    Boost.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);
                    checkIfBonus = 2;
                }
                else
                {
                    r = Random.value;
                    if (r < 0.5f)
                    {
                        Enemy = (GameObject)Instantiate(EnemyGO2);
                        Enemy.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);
                    }
                    else if(r > 0.5f && r < 0.8f)
                    {
                        if (Random.value < 0.5f)
                        {
                            Meteor = (GameObject)Instantiate(MeteorRightGO);
                        }
                        else
                        {
                            Meteor = (GameObject)Instantiate(MeteorGreyRightGO);
                        }
                        Meteor.transform.position = new Vector2(((min.x + max.x) / 2) + 1.2f, max.y);
                    }
                }

                break;
        }

        //Produzir novos inimigos em tempo aleatorio
        ScheduleNextEnemySpawnRandom();
    }

    // produz cada inimigo entre 1 a 8.1s depois do inimigo anterior, de forma aleatoria
    void ScheduleNextEnemySpawnRandom()
    {
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            // o proximo inimigo vai aparecer Random.Range(1f, maxSpawnRateInSeconds)
            spawnInNSeconds = Random.Range(minSpawnRateInSeconds, maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;

        Invoke("SpawnEnemy", spawnInNSeconds);
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

    // faz update do valor maxSpawnRateInSeconds
    public static void UpdateMaxSpawnRate(float updatedSpawnRate)
    {
        maxSpawnRateInSeconds = updatedSpawnRate;
    }

    // faz update do valor minSpawnRateInSeconds
    public static void UpdateMinSpawnRate(float updatedMinSpawnRate)
    {
        if (minSpawnRateInSeconds > 0f)
        {
            minSpawnRateInSeconds = updatedMinSpawnRate;
        }
    }

    //obtem o maxSpawnRateInSeconds
    public static float GetMaxSpawnRate()
    {
        return maxSpawnRateInSeconds;
    }

    //obtem o minSpawnRateInSeconds
    public static float GetMinSpawnRate()
    {
        return minSpawnRateInSeconds;
    }
}

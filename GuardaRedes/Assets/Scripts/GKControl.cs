using UnityEngine;

public class GKControl : MonoBehaviour
{

    public float maxSpawnRateInSeconds = 10f;
    public AudioSource shotRight;
    public AudioClip impact;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void KickBall()
    {
        // 1 - esquerda, 2 - centro, 3 - direita
        //int shotRoll = Random.Range(1, 4);
        int shotRoll = 3;
        switch (shotRoll)
        {
            case 1:
                Debug.Log("esquerda");
                break;
            case 2:
                Debug.Log("centro");
                break;
            case 3:
                Debug.Log("direita");
                shotRight.loop = true;                
                shotRight.Play();

                break;
        }

        //Chutar novamente em tempo aleatório
        //ScheduleBallKicks();

        //Chutar novamente de 4 em 4 segundos
        //ScheduleNextEnemySpawnFourSeconds();
    }

    // começar os remates
    public void ScheduleKicks(float initialWaitTime)
    {
        Invoke("KickBall", initialWaitTime);
    }

    // chuta a bola entre 3 a 10s depois do chuto anterior
    void ScheduleBallKicks()
    {
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            // o proximo inimigo vai aparecer Random.Range(1f, maxSpawnRateInSeconds) depois (>1 e <5)
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;

        Invoke("KickBall", spawnInNSeconds);
    }

    // parar de chutar bolas
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}

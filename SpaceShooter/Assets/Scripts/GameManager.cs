using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject instrucoesButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject timeCounterGO;

    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float countdownForFinalState;

    private int enemiesAvoided; // enemies that reached the end of the screen
    private int finalScore; // final score

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                instrucoesButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                //Reset ao score, botao play e instrucoes
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                instrucoesButton.SetActive(false);

                // iniciar os contadores de tempo
                timeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // countdown para a velocidade
                StartCoroutine(StartCountdownSpeed());

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                /*
                //iniciar o enemy spawner
                int roll = Random.Range(1, 4);
                Debug.Log("roll: " + roll);
                switch (roll)
                {
                    case 3:
                        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                        StartCoroutine(StartCountdown());
                        if(Random.value < 0.5f)
                        {
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                        }
                        else
                        {
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                        }
                        
                        break;
                    case 2:
                        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                        StartCoroutine(StartCountdown());

                        if (Random.value < 0.5f)
                        {
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                        }
                        else
                        {
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                        }

                        break;
                    case 1:
                        enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                        StartCoroutine(StartCountdown());

                        if (Random.value < 0.5f)
                        {
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                        }
                        else
                        {
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                        }

                        break;
                }
                */

                break;
            case GameManagerState.GameOver:

                // terminar o contador de tempo
                timeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                // parar o enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().UnscheduleEnemySpawner();

                //display game over
                GameOverGO.SetActive(true);

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // botao play ira chamar esta funcao
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public IEnumerator StartCountdown(float countdownvalue = 20)
    {
        currCountdownValue = countdownvalue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }

    //countdown para aumentar a velocidade
    public IEnumerator StartCountdownSpeed(float countdownValue = 15)
    {
        float speed = EnemyControl.GetSpeed();
        increaseSpeedTimer = countdownValue;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                speed+=0.3f;
                EnemyControl.SetSpeed(speed);

                //countdown para aumentar a velocidade
                StartCoroutine(StartCountdownSpeed());
            }
        }
    }
}

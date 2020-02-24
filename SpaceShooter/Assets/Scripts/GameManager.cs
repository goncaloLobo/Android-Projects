using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;

    float currCountdownValue;

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
                break;
            case GameManagerState.Gameplay:
                //Reset ao score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                playButton.SetActive(false);
                //playerShip.GetComponent<PlayerControlSwipe>().Init();
                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                /*
                //iniciar o enemy spawner
                int roll = Random.Range(1, 4);
                switch (roll)
                {
                    case 3:
                        Debug.Log("entrei caso 3.");
                        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                        StartCoroutine(StartCountdown());
                        if(Random.value < 0.5f)
                        {
                            Debug.Log("entreiiii1");
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                        }
                        else
                        {
                            Debug.Log("entreiiii2");
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                            StartCoroutine(StartCountdown());
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                        }
                        
                        break;
                    case 2:
                        Debug.Log("entrei caso 2.");
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
                        Debug.Log("entrei caso 1.");
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
                }*/

                break;
            case GameManagerState.GameOver:

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
}

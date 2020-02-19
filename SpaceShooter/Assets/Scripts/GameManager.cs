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
                playerShip.GetComponent<PlayerControl>().Init();

                //iniciar o enemy spawner
                //enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                //enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                break;
            case GameManagerState.GameOver:
                // stop enemy spawner
                //enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                //enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
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
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject timeCounterGO;

    //imagens
    public GameObject shipImage;
    public GameObject enemyImage1;
    public GameObject enemyImage2;

    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float initialSpawnRate;

    private float highscoreStored;
    private float timeStored;
    private float enemiesStored;

    private float currentTime;
    private int finalScore; // pontuacao final (pontos)
    private int enemiesAvoided; // inimigos desviados com sucesso
        
    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions
    }

    public static GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        finalScore = 0;

        // vai buscar o highscore
        // aqui no start para quando o jogo é iniciado
        GetCurrentHighScores();
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);

                // vai buscar o highscore no opening para qdo o jogo termina e volta a este estado
                // ou seja, todas as vezes que o jogador perde
                GetCurrentHighScores();

                break;
            case GameManagerState.Gameplay:
                shipImage.SetActive(false);
                enemyImage1.SetActive(false);
                enemyImage2.SetActive(false);

                //Reset ao score, botao play e instrucoes
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                howToButton.SetActive(false);

                // iniciar os contadores de tempo
                timeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // countdown para a velocidade
                StartCoroutine(StartCountdownSpeed());

                //iniciar o enemy spawner
                int roll = Random.Range(1, 4);
                //int roll = 1;
                Debug.Log("ROLL INICIAL: " + roll);
                switch (roll)
                {
                    case 3:
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(2);
                        if(Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        
                        break;
                    case 2:
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(2);

                        if (Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                        }

                        break;
                    case 1:
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(2);

                        if (Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(2.5f, 5);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5, 8);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                        }

                        break;
                }

                break;
            case GameManagerState.GameOver:

                // terminar os contadores de tempo
                currentTime = timeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                // parar o enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().UnscheduleEnemySpawner();

                //display game over e o tempo final
                GameOverGO.SetActive(true);                

                // resultado final
                finalScore = PlayerControlSwipe.GetFinalScore();
                enemiesAvoided = EnemyControl.GetEnemiesAvoided();

                // se for novo highscore, vai regista-lo
                // assim como o tempo e o numero de inimigos desviados associado
                if (finalScore > highscoreStored)
                {
                    //new highscore
                    GameScore.SetHighScore(finalScore, currentTime, enemiesAvoided);
                }

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 1f);

                break;
            case GameManagerState.Instructions:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(false);

                shipImage.SetActive(true);
                enemyImage1.SetActive(true);
                enemyImage2.SetActive(true);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public static GameManagerState GetCurrentState()
    {
        return GMState;
    }

    // countdown para aumentar a velocidade
    // de 7 em 7s aumenta a velocidade em 0.3
    // de 7 em 7s diminui o intervalo de tempo de spawn dos inimigos [curr - 0.3,curr - 0.3]
    public IEnumerator StartCountdownSpeed(float countdownValue = 7)
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
                if (EnemySpawner.GetMaxSpawnRate() > 3.0f)
                {
                    float sp = EnemySpawner.GetMaxSpawnRate();
                    float minsp = EnemySpawner.GetMinSpawnRate();

                    float nsp = sp - 0.3f;
                    float nminsp = minsp - 0.1f;

                    EnemySpawner.UpdateMaxSpawnRate(nsp);
                    EnemySpawner.UpdateMinSpawnRate(nminsp);

                    EnemySpawner2.UpdateMaxSpawnRate(nsp);
                    EnemySpawner2.UpdateMinSpawnRate(minsp);

                    EnemySpawner3.UpdateMaxSpawnRate(nsp);
                    EnemySpawner3.UpdateMinSpawnRate(minsp);
                }

                //countdown para aumentar a velocidade
                StartCoroutine(StartCountdownSpeed());
            }
        }
    }

    // calcula float aleatorio para o 2º e 3º enemy spawner de cada caso
    // para o 2º inimigo é entre 3 e 5
    // para o 3º inimigo é entre 5 e 8
    public float CreateRandomFloat(float firstValue, float secondValue)
    {
        float froll = Random.Range(firstValue, secondValue);
        return froll;
    }

    //obtem o highscore que esteja guardado, qualquer que seja o valor, no start e no opening
    private void GetCurrentHighScores()
    {
        highscoreStored = PlayerPrefs.GetFloat("highscore", 0);
        timeStored = PlayerPrefs.GetFloat("time", 0);
        enemiesStored = PlayerPrefs.GetInt("enemies", 0);
    }
}

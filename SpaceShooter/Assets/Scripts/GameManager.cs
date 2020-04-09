using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject introducaoButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject timeCounterGO;

    public AudioSource introducao;
    private static bool started, opening, instructions, tutorialp1, tutorialp2, tutorialp3, tutorialp4, tutorialp5, pregameplay;

    public AudioSource[] sounds;
    public AudioSource instrucoespt1; // sounds[0]
    public AudioSource naveInimiga; // sounds[1]
    public AudioSource instrucoespt2; // sounds[2]
    public AudioSource asteroide; // sounds[3]
    public AudioSource instrucoespt3; // sounds[4]
    public AudioSource bonus; // sounds[5]
    public AudioSource instrucoespt4; // sounds[6]
    public AudioSource swipeSound; // sounds [7]
    public AudioSource instrucoespt5; // sounds[8]
    public AudioSource hitWallSound; // sounds[9]

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
        Opening, Gameplay, GameOver, Instructions, TutorialP1, TutorialP2, TutorialP3, TutorialP4, TutorialP5, PreGameplay
    }

    public static GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        finalScore = 0;
        started = instructions = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;

        //inicializa os sons das instrucoes
        sounds = GetComponents<AudioSource>();
        InitiateSounds(sounds);

        // vai buscar o highscore
        // aqui no start para quando o jogo é iniciado
        GetCurrentHighScores();

        if (PlayerPrefs.GetInt("introducao") == 0)
        {
            introducao.Play();
            PlayerPrefs.SetInt("introducao", 1);
        }
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);
                introducaoButton.SetActive(true);
                SetOpeningBools();

                // vai buscar o highscore no opening para qdo o jogo termina e volta a este estado
                // ou seja, todas as vezes que o jogador perde
                GetCurrentHighScores();

                break;
            case GameManagerState.Gameplay:
                //Reset ao score, botao play e instrucoes
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                howToButton.SetActive(false);
                introducaoButton.SetActive(false);
                SetGameplayBools();

                // iniciar os contadores de tempo
                timeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // countdown para a velocidade
                StartCoroutine(StartCountdownSpeed());

                //iniciar o enemy spawner
                int roll = Random.Range(1, 4);
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
                SetGameoverBools();
                
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
                Invoke("ChangeToOpeningState", 2f);

                break;
            case GameManagerState.Instructions:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);
                SetInstructionsBools();

                /*
                float delay = 0f;
                instrucoespt1.Play();
                delay += instrucoespt1.clip.length;

                naveInimiga.PlayDelayed(delay);
                delay += naveInimiga.clip.length;

                instrucoespt2.PlayDelayed(delay);
                delay += instrucoespt2.clip.length;

                asteroide.PlayDelayed(delay);
                delay += asteroide.clip.length;

                instrucoespt3.PlayDelayed(delay);
                delay += instrucoespt3.clip.length;

                bonus.PlayDelayed(delay);
                delay += bonus.clip.length;

                instrucoespt4.PlayDelayed(delay);
                delay += instrucoespt4.clip.length;

                swipeSound.PlayDelayed(delay);
                delay += swipeSound.clip.length;

                instrucoespt5.PlayDelayed(delay);
                delay += instrucoespt5.clip.length;

                hitWallSound.PlayDelayed(delay);
                */
                break;

            case GameManagerState.TutorialP1:
                SetTutorialP1Bools();
                break;
            case GameManagerState.TutorialP2:
                SetTutorialP2Bools();
                break;
            case GameManagerState.TutorialP3:
                SetTutorialP3Bools();
                break;
            case GameManagerState.TutorialP4:
                SetTutorialP4Bools();
                break;
            case GameManagerState.TutorialP5:
                SetTutorialP5Bools();
                break;
            case GameManagerState.PreGameplay:
                SetPregameplayBools();
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

    public static bool GetStarted()
    {
        return started;
    }

    public static bool GetOpening()
    {
        return opening;
    }

    public static bool GetInstructions()
    {
        return instructions;
    }

    public static bool GetTutorialP1()
    {
        return tutorialp1;
    }

    public static bool GetTutorialP2()
    {
        return tutorialp2;
    }

    public static bool GetTutorialP3()
    {
        return tutorialp3;
    }

    public static bool GetTutorialP4()
    {
        return tutorialp4;
    }

    public static bool GetTutorialP5()
    {
        return tutorialp5;
    }

    public static bool GetPregameplay()
    {
        return pregameplay;
    }

    // countdown para aumentar a velocidade
    // de 7 em 7s aumenta a velocidade em 0.3
    // de 7 em 7s diminui o intervalo de tempo de spawn dos inimigos [curr - 0.3,curr - 0.3]
    public IEnumerator StartCountdownSpeed(float countdownValue = 7)
    {
        float speed = EnemyControl.GetSpeed();
        increaseSpeedTimer = countdownValue;
        if (!(GameManager.GetCurrentState() == GameManagerState.GameOver)) {
            while (increaseSpeedTimer >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                increaseSpeedTimer--;

                if (increaseSpeedTimer == 0)
                {
                    speed += 0.3f;
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
                    yield break;
                }
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

    // inicializa os sons das instrucoes
    private void InitiateSounds(AudioSource[] sounds)
    {
        instrucoespt1 = sounds[0];
        naveInimiga = sounds[1];
        instrucoespt2 = sounds[2];
        asteroide = sounds[3];
        instrucoespt3 = sounds[4];
        bonus = sounds[5];
        instrucoespt4 = sounds[6];
        swipeSound = sounds[7];
        instrucoespt5 = sounds[8];
        hitWallSound = sounds[9];
    }

    // inicializa os bools no estado opening
    private void SetOpeningBools()
    {
        opening = true;
        started = instructions = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado gameplay
    private void SetGameplayBools()
    {
        started = true;
        opening = instructions = pregameplay = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // inicializa os bools no estado gameover
    private void SetGameoverBools()
    {
        started = instructions = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado instructions
    private void SetInstructionsBools()
    {
        instructions = true;
        opening = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado tutorialp1
    private void SetTutorialP1Bools()
    {
        tutorialp1 = true;
        opening = instructions = started = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado tutorialp2
    private void SetTutorialP2Bools()
    {
        tutorialp2 = true;
        opening = instructions = started = tutorialp1 = tutorialp3 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado tutorialp3
    private void SetTutorialP3Bools()
    {
        tutorialp3 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp4 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado tutorialp4
    private void SetTutorialP4Bools()
    {
        tutorialp4 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp5 = pregameplay = false;
    }

    // inicializa os bools no estado tutorialp5
    private void SetTutorialP5Bools()
    {
        tutorialp5 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = pregameplay = false;
    }

    // inicializa os bools no estado pregameplay
    private void SetPregameplayBools()
    {
        pregameplay = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp5 = tutorialp4 = false;
    }
}

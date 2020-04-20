using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject tutorialButton;
    public Text LivesUIText;

    // botoes instrucoes
    public GameObject instrucoesB1;
    public GameObject instrucoesB2;
    public GameObject instrucoesB3;

    public AudioSource introducao;
    public AudioSource background;
    private static bool started, opening, instructions, tutorialp1, tutorialp2, tutorialp3, tutorialp4, tutorialp5, tutorialp6;

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

    // audioSource tutorial
    public AudioSource tutorial1;
    public AudioSource tutorial2;
    public AudioSource tutorial3;
    public AudioSource tutorial4;
    public AudioSource tutorial5;
    public AudioSource tutorial2dir;
    public AudioSource tutorial3dir;
    public AudioSource tutorial6;
    public AudioSource tutorial0;

    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float initialSpawnRate;

    private float highscoreStored;
    private float timeStored;
    private float enemiesStored;
    private bool deployAsteroid;

    private float currentTime;
    private int finalScore; // pontuacao final (pontos)
    private int enemiesAvoided; // inimigos desviados com sucesso
    private static bool stopEnemySpawners;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions, TutorialP1, TutorialP2, TutorialP3, TutorialP4, TutorialP5, TutorialP6, CancelTutorial
    }

    public static GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;
        finalScore = 0;
        opening = true;
        started = instructions = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = stopEnemySpawners = false;
        deployAsteroid = false;

        //inicializa os sons das instrucoes
        sounds = GetComponents<AudioSource>();
        InitiateSounds(sounds);
        InitiateTutorialSounds(sounds);
        background.Play();
        background.loop = true;

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
                instrucoesB1.SetActive(false);
                instrucoesB2.SetActive(false);
                instrucoesB3.SetActive(false);
                tutorialButton.SetActive(false);
                playerShip.SetActive(true);

                SetOpeningBools();
                background.volume = 0.2f;

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
                instrucoesB1.SetActive(false);
                instrucoesB2.SetActive(false);
                instrucoesB3.SetActive(false);
                tutorialButton.SetActive(false);
                playerShip.SetActive(true);
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
                    case 3: // DIREITA
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(2);
                        if(Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        
                        break;
                    case 2: // CENTRO
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(2);

                        if (Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
                            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(initialSpawnRate);
                        }

                        break;
                    case 1: // ESQUERDA
                        // o primeiro inimigo aparece sempre 2s depois de iniciar o jogo
                        enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(2);

                        if (Random.value < 0.5f)
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                        }
                        else
                        {
                            initialSpawnRate = CreateRandomFloat(3.1f, 4.7f);
                            enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner(initialSpawnRate);
                            initialSpawnRate = CreateRandomFloat(5.8f, 7.5f);
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
                Invoke("ChangeToOpeningState", 1.5f);

                break;
            case GameManagerState.Instructions:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(false);
                instrucoesB1.SetActive(true);
                instrucoesB2.SetActive(true);
                instrucoesB3.SetActive(true);
                introducaoButton.SetActive(true);
                tutorialButton.SetActive(true);

                SetInstructionsBools();
                background.volume = 0.05f;

                break;
            case GameManagerState.TutorialP1:
                playButton.SetActive(false);
                howToButton.SetActive(false);
                introducaoButton.SetActive(false);
                instrucoesB1.SetActive(false);
                instrucoesB2.SetActive(false);
                instrucoesB3.SetActive(false);
                tutorialButton.SetActive(false);

                SetTutorialP1Bools();
                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // Estás numa nave espacial e o teu objetivo é desviares-te dos inimigos ou asteroides que irão vir contra ti.
                tutorial0.Play();

                // A tua nave começa o jogo no meio do ecrã. Quando ouvires o som do inimigo, tens de varrer o ecrã para um dos lados para te desviares.
                tutorial1.PlayDelayed(tutorial0.clip.length);
                
                Invoke("DeployCenterEnemyForTutorial", tutorial1.clip.length);

                break;
            case GameManagerState.TutorialP2:
                SetTutorialP2Bools();


                if (PlayerControlSwipe.CheckTutorialLeft())
                {
                    // Uma vez que foste para a esquerda, não é possível ires novamente para esse lado. Se tentares, irás ouvir um som como se batesses numa parede. 
                    // Experimenta varrer o ecrã para a esquerda.
                    tutorial2.Play();
                }
                else if (PlayerControlSwipe.CheckTutorialRight())
                {
                    // Uma vez que foste para a direita, não é possível ires novamente para esse lado. Se tentares, irás ouvir um som como se batesses numa parede. 
                    // Experimenta varrer o ecrã para a direita.
                    tutorial2dir.Play();
                }                

                break;
            case GameManagerState.TutorialP3:
                SetTutorialP3Bools();

                // se no p1 o utilizador fez swipe para a esq
                if (PlayerControlSwipe.CheckTutorialLeft())
                {
                    // Continuas à esquerda e vem um inimigo contra ti. Desvia-te varrendo o ecrã para a direita.
                    tutorial3.Play();
                    Invoke("DeployLeftEnemyForTutorial", tutorial3.clip.length);
                }
                // se no p1 o utilizador fez swipe para a dir
                else if (PlayerControlSwipe.CheckTutorialRight())
                {
                    // Continuas à direita e vem um inimigo contra ti. Desvia-te varrendo o ecrã para a esquerda.
                    tutorial3dir.Play();
                    Invoke("DeployRightEnemyForTutorial", tutorial3.clip.length);
                }
                break;
            case GameManagerState.TutorialP4:
                SetTutorialP4Bools();

                // Agora, estás novamente no centro e à tua frente encontra-se um asteroide. Desvia-te varrendo o ecrã para um dos lados.
                tutorial4.Play();

                if (!deployAsteroid)
                {
                    Invoke("DeployCenterAsteroidForTutorial", tutorial4.clip.length);
                    deployAsteroid = true;
                }

                break;
            case GameManagerState.TutorialP5:
                SetTutorialP5Bools();

                // Enquanto estiveres a jogar, podes apanhar bónus para ganhar mais pontos. Quando ouvires o som do bónus, varre o ecrã para ires de encontro ao bónus.
                tutorial5.Play();
                Invoke("DeployCenterBonusForTutorial", tutorial5.clip.length);

                break;
            case GameManagerState.TutorialP6:
                SetTutorialP6Bools();

                // Chegámos ao final do tutorial. Durante o jogo tens 3 vidas, tenta sobreviver ao máximo de inimigos possível. Podes fazer o tutorial novamente, se quiseres. Boa sorte.
                tutorial6.Play();

                Invoke("ChangeToInstructionsState", tutorial6.clip.length);
                break;
            case GameManagerState.CancelTutorial:
                if (tutorial1.isPlaying)
                    tutorial1.Stop();
                if (tutorial2.isPlaying)
                    tutorial2.Stop();
                if (tutorial2dir.isPlaying)
                    tutorial2dir.Stop();
                if (tutorial3.isPlaying)
                    tutorial3.Stop();
                if (tutorial4.isPlaying)
                    tutorial4.Stop();
                if (tutorial5.isPlaying)
                    tutorial5.Stop();
                if (tutorial6.isPlaying)
                    tutorial6.Stop();
                if (tutorial3dir.isPlaying)
                    tutorial3dir.Stop();

                Invoke("ChangeToInstructionsState", 0f);
                break;
        }

        // faz parar os inimigos qdo o utilizador volta p tras qdo esta a jogar
        if (stopEnemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
            enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
            enemySpawner3.GetComponent<EnemySpawner3>().UnscheduleEnemySpawner();
            stopEnemySpawners = false;

            LivesUIText.text = Configuration.MaxLives().ToString();
            scoreUITextGO.GetComponent<GameScore>().Score = 0;
            timeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
            timeCounterGO.GetComponent<TimeCounter>().ResetTimer();
            playerShip.SetActive(true);

            EnemyControl.TriggerExplosion();
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

    public void ChangeToP1TutorialState()
    {
        SetGameManagerState(GameManagerState.TutorialP1);
    }

    public void ChangeToInstructionsState()
    {
        SetGameManagerState(GameManagerState.Instructions);
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

    public static bool GetTutorialP6()
    {
        return tutorialp6;
    }

    public static void CheckToStopEnemySpawners()
    {
        stopEnemySpawners = true;
    }

    // faz deploy de um inimigo no centro do ecra para o tutorial
    public void DeployCenterEnemyForTutorial()
    {
        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawnerTutorial(0);
    }

    // faz deploy de um inimigo à esquerda do ecra para o tutorial
    public void DeployLeftEnemyForTutorial()
    {
        enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawnerTutorial(0);
    }

    // faz deploy de um inimigo à direita do ecra para o tutorial
    public void DeployRightEnemyForTutorial()
    {
        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawnerTutorial(0);
    }

    // faz deploy de um asteroide no centro do ecra para o tutorial
    public void DeployCenterAsteroidForTutorial()
    {
        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleAsteroidSpawnerTutorial(0);
    }

    // faz deploy de um bonus à esquerda no ecra para o tutorial
    public void DeployLeftBonusForTutorial()
    {
        enemySpawner.GetComponent<EnemySpawner>().ScheduleBonusSpawnerTutorial(0);
    }

    // faz deploy de um bonus no centro do ecra para o tutorial
    public void DeployCenterBonusForTutorial()
    {
        enemySpawner2.GetComponent<EnemySpawner2>().ScheduleBonusSpawnerTutorial(0);
    }

    // faz deploy de um bonus à direita no ecra para o tutorial
    public void DeployRightBonusForTutorial()
    {
        enemySpawner3.GetComponent<EnemySpawner3>().ScheduleBonusSpawnerTutorial(0);
    }

    // countdown para aumentar a velocidade
    // de 7 em 7s aumenta a velocidade em 0.3
    // de 7 em 7s diminui o intervalo de tempo de spawn dos inimigos [curr - 0.3, curr - 0.3]
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
    // para o 2º inimigo é entre 3.1f, 4.7f
    // para o 3º inimigo é entre 5.8f, 7.5f
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

    private void InitiateTutorialSounds(AudioSource[] sounds)
    {
        tutorial1 = sounds[10];
        tutorial2 = sounds[11];
        tutorial3 = sounds[12];
        tutorial4 = sounds[13];
        tutorial5 = sounds[14];
        tutorial2dir = sounds[15];
        tutorial3dir = sounds[16];
        tutorial6 = sounds[17];
        tutorial0 = sounds[18];
    }

    // inicializa os bools no estado opening
    private void SetOpeningBools()
    {
        opening = true;
        started = instructions = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado gameplay
    private void SetGameplayBools()
    {
        started = true;
        opening = instructions = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado gameover
    private void SetGameoverBools()
    {
        started = instructions = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado instructions
    private void SetInstructionsBools()
    {
        instructions = true;
        opening = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp1
    private void SetTutorialP1Bools()
    {
        tutorialp1 = true;
        opening = instructions = started = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp2
    private void SetTutorialP2Bools()
    {
        tutorialp2 = true;
        opening = instructions = started = tutorialp1 = tutorialp3 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp3
    private void SetTutorialP3Bools()
    {
        tutorialp3 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp4 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp4
    private void SetTutorialP4Bools()
    {
        tutorialp4 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp5 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp5
    private void SetTutorialP5Bools()
    {
        tutorialp5 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp6 = false;
    }

    // inicializa os bools no estado tutorialp5
    private void SetTutorialP6Bools()
    {
        tutorialp6 = true;
        opening = instructions = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }
}

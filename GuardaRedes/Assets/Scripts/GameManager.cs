using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject GameOverGO;
    public GameObject defenderCima;
    public GameObject defenderBaixo;
    public GameObject defenderDireita;
    public GameObject defenderEsquerda;
    public GameObject textGameModes;
    public GameObject panelHolder;
    public GameObject panelDefenderBaixo;

    public AudioSource [] sounds; // array para os varios sons
    public AudioSource apitoParaChutar; // primeiro som [0]
    public AudioSource chutoEsquerda; // segundo som [1]
    public AudioSource apito3x; // terceiro som [2]
    public AudioSource introducao;

    private static bool started, opening, instructions, gameover, swiperight, swipeleft, swipeup, swipedown, tutorialp1, tutorialp2, tutorialp3, tutorialp4, tutorialp5;
    private static int startedDirection;
    private float currCountdownValue;
    private float increaseSpeedTimer;

    private float highscoreStored;
    private int defesasStored;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions, SwipeRight, SwipeLeft, SwipeUp, SwipeDown, TutorialP1, TutorialP2, TutorialP3, TutorialP4, TutorialP5
    }

    public static GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;
        opening = true;
        started = instructions = gameover = swiperight = swipeleft = swipeup = swipedown = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        startedDirection = 0;
        sounds = GetComponents<AudioSource>();
        apitoParaChutar = sounds[0];
        chutoEsquerda = sounds[1];
        apito3x = sounds[2];

        // vai buscar o highscore
        // aqui no start para quando o jogo é iniciado
        GetCurrentHighScores();

        if(PlayerPrefs.GetInt("introducao") == 0)
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
                SetOpeningBools();
                playerShip.SetActive(false);
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);

                // mete os varios defenderes a false
                DeactivateDefenderes();

                // vai buscar o highscore no opening para qdo o jogo termina e volta a este estado
                // ou seja, todas as vezes que o jogador perde
                GetCurrentHighScores();

                break;
            case GameManagerState.Gameplay:
                SetGameplayBools();
                playButton.SetActive(false);
                howToButton.SetActive(false);
                // mete os varios defenderes a false
                DeactivateDefenderes();

                started = true;

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                //aqui ira ser feito toda a parte aleatoria de vários tipos de remates

                break;
            case GameManagerState.GameOver:
                SetGameoverBools();
                //display game over
                GameOverGO.SetActive(true);
                apito3x.Play();

                // se for novo highscore, vai regista-lo
                // assim como o tempo e o numero de inimigos desviados associado
                //if (finalScore > highscoreStored)
                //{
                    //new highscore
                //    GameScore.SetHighScore(finalScore, defesas);
                //}

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 1f);

                break;
            case GameManagerState.Instructions:
                SetInstructionsBools();
                GameOverGO.SetActive(false);
                playButton.SetActive(false);
                howToButton.SetActive(false);
                //panelHolder.SetActive(true);

                // mete os varios defenderes a true
                ActivateDefenderes();

                break;
            case GameManagerState.SwipeDown:
                SetSwipedownBools();

                // caso para ensinar remates para baixo (1)
                startedDirection = 1;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                break;
            case GameManagerState.SwipeLeft:
                SetSwipeleftBools();

                // caso para ensinar remates para esquerda (2)
                // unico caso funcional, por agora.
                startedDirection = 2;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // countdown para o apito (2s)
                //StartCoroutine(StartCountDownToApito());
                Invoke("Apitar", 2f);

                break;
            case GameManagerState.SwipeRight:
                SetSwiperightBools();

                // caso para ensinar remates para direita (3)
                startedDirection = 3;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                break;
            case GameManagerState.SwipeUp:
                SetSwipeupBools();

                // caso para ensinar remates para baixo (4)
                startedDirection = 4;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

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

    public static bool GetGameover()
    {
        return gameover;
    }

    public static bool GetSwipeUp()
    {
        return swipeup;
    }

    public static bool GetSwipeLeft()
    {
        return swipeleft;
    }

    public static bool GetSwipeRight()
    {
        return swiperight;
    }

    public static bool GetSwipeDown()
    {
        return swipedown;
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

    public void Apitar()
    {
        apitoParaChutar.Play();
        // chama repetitivamente a funcao rematar
        // 2.5s apos o inicio do apito e de 3 em 3s
        InvokeRepeating("Rematar", 2.5f, 3.0f);
    }

    public void Rematar()
    {
        // 1 - baixo
        // 2 - esquerda
        // 3 - direita
        // 4 - cima
        switch (startedDirection)
        {
            case 1:
                break;
            case 2:
                Debug.Log("vou rematar!");
                chutoEsquerda.Play();
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    //obtem o highscore que esteja guardado, qualquer que seja o valor, no start e no opening
    private void GetCurrentHighScores()
    {
        highscoreStored = PlayerPrefs.GetFloat("highscore", 0);
        defesasStored = PlayerPrefs.GetInt("defesas", 0);
    }

    private void DeactivateDefenderes()
    {
        defenderBaixo.SetActive(false);
        defenderCima.SetActive(false);
        defenderEsquerda.SetActive(false);
        defenderDireita.SetActive(false);
        textGameModes.SetActive(false);
    }

    private void ActivateDefenderes()
    {
        textGameModes.SetActive(true);
        defenderBaixo.SetActive(true);
        defenderCima.SetActive(true);
        defenderEsquerda.SetActive(true);
        defenderDireita.SetActive(true);
    }

    // Bools para o estado Opening
    private void SetOpeningBools()
    {
        opening = true;
        started = instructions = swipedown = swipeleft = swiperight = swipeup = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado Gameplay
    private void SetGameplayBools()
    {
        started = true;
        opening = instructions = swipedown = swipeleft = swiperight = swipeup = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado Instructions
    private void SetInstructionsBools()
    {
        instructions = true;
        opening = started = swipedown = swipeleft = swiperight = swiperight = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado SwipeDown
    private void SetSwipedownBools()
    {
        swipedown = true;
        instructions = opening = started = swipeleft = swiperight = swipeup = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado SwipeUp
    private void SetSwipeupBools()
    {
        swipeup = true;
        instructions = opening = started = swipeleft = swiperight = swipedown = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado SwipeLeft
    private void SetSwipeleftBools()
    {
        swipeleft = true;
        instructions = opening = started = swipeup = swiperight = swipedown = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado SwipeRight
    private void SetSwiperightBools()
    {
        swiperight = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado Gameover
    private void SetGameoverBools()
    {
        gameover = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado TutorialP1
    private void SetTutorialP1Bools()
    {
        tutorialp1 = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = gameover = false;
        tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado TutorialP2
    private void SetTutorialP2Bools()
    {
        tutorialp2 = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = gameover = false;
        tutorialp1 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado TutorialP3
    private void SetTutorialP3Bools()
    {
        tutorialp3 = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp4 = tutorialp5 = false;
    }

    // Bools para o estado TutorialP4
    private void SetTutorialP4Bools()
    {
        tutorialp4 = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp5 = false;
    }

    // Bools para o estado TutorialP5
    private void SetTutorialP5Bools()
    {
        tutorialp5 = true;
        instructions = opening = started = swipeleft = swipeup = swipedown = swiperight = gameover = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = false;
    }
}

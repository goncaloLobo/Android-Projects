using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject tutorialButton;
    public GameObject GameOverGO;
    public GameObject defenderCima;
    public GameObject defenderBaixo;
    public GameObject defenderDireita;
    public GameObject defenderEsquerda;
    public GameObject textGameModes;
    public GameObject panelHolder;
    public GameObject panelDefenderBaixo;

    private static int golos_sofridos; // numero de golos sofridos
    private static int golos_defendidos; // numero de golos defendidos

    public AudioSource [] sounds; // array para os varios sons
    public AudioSource apitoParaChutar; // primeiro som [0]
    public AudioSource chutoEsquerdaMeio; // segundo som [1]
    public AudioSource chutoDireitaMeio;
    public AudioSource apito3x; // terceiro som [2]
    public AudioSource introducao;
    public AudioSource tutorial0;
    public AudioSource tutorial1;
    public AudioSource golo;
    public AudioSource tutorial2;
    public AudioSource tutorial3;
    public AudioSource tutorial4;
    public AudioSource tutorial5;
    public AudioSource chutoCimaMeio;
    public AudioSource chutoBaixoMeio;
    public AudioSource chutoBaixoEsquerda;
    public AudioSource chutoBaixoDireita;

    private static bool started, opening, instructions, gameover, swiperight, swipeleft, swipeup, swipedown, tutorialp1, tutorialp2, tutorialp3, tutorialp4, tutorialp5;
    private static bool swipedownthenleft, swipeleftthenup, swiperightthendown, swipeleftthendown, swipedownthenright;
    private static bool resetGloves;
    private static int startedDirection;
    private float currCountdownValue, increaseSpeedTimer;

    private float highscoreStored;
    private int defesasStored;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions, SwipeRight, SwipeLeft, SwipeUp, SwipeDown, TutorialP1, TutorialP2, TutorialP3, TutorialP4, TutorialP5,
        SwipeDownThenLeft, SwipeLeftThenUp, SwipeRightThenDown, SwipeLeftThenDown, SwipeDownThenRight
    }

    public static GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;
        opening = true;
        started = instructions = gameover = swiperight = swipeleft = swipeup = swipedown = false;
        tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipedownthenleft = swipeleftthendown = swiperightthendown = swipeleftthendown = swipedownthenright = false;
        resetGloves = false;
        startedDirection = 0;
        sounds = GetComponents<AudioSource>();
        apitoParaChutar = sounds[0];
        chutoEsquerdaMeio = sounds[1];
        chutoDireitaMeio = sounds[5];
        apito3x = sounds[2];

        tutorial0 = sounds[3];
        tutorial1 = sounds[4];
        golo = sounds[6];
        tutorial2 = sounds[7];
        tutorial3 = sounds[8];
        tutorial4 = sounds[9];
        tutorial5 = sounds[10];
        chutoCimaMeio = sounds[11];
        chutoBaixoMeio = sounds[12];
        chutoBaixoEsquerda = sounds[13];
        chutoBaixoDireita = sounds[14];

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
                playerShip.SetActive(true);
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);
                tutorialButton.SetActive(false);

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

                //aqui ira ser feito toda a parte aleatoria de vários tipos de remates
                //int roll = Random.Range(1, 9);
                int roll = 3;
                switch (roll)
                {
                    case 1: // swipe left
                        startedDirection = 2;
                        Invoke("Apitar", 0f);
                        break;
                    case 2: // swipe right
                        startedDirection = 3;
                        Invoke("Apitar", 0f);
                        break;
                    case 3: // swipe up
                        startedDirection = 4;
                        Invoke("Apitar", 0f);
                        break;
                    case 4: // swipe down
                        startedDirection = 1;
                        Invoke("Apitar", 0f);
                        break;
                    case 5: // swipe down then left
                        break;
                    case 6: // swipe left then up
                        break;
                    case 7: // swipe right then down
                        break;
                    case 8: // swipe left then down
                        break;
                    case 9: // swipe down then right
                        break;
                }

                break;
            case GameManagerState.GameOver:
                SetGameoverBools();
                //display game over
                GameOverGO.SetActive(true);

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
                playButton.SetActive(true);
                howToButton.SetActive(false);
                tutorialButton.SetActive(true);
                //panelHolder.SetActive(true);

                // mete os varios defenderes a true
                ActivateDefenderes();

                break;
            case GameManagerState.SwipeDown:
                SetSwipedownBools();
                playButton.SetActive(false);
                tutorialButton.SetActive(false);

                startedDirection = 1; // caso para ensinar remates para baixo (1)
                DeactivateDefenderes(); // mete os varios defenderes a false
                Invoke("Apitar", 0f);

                break;
            case GameManagerState.SwipeLeft:
                SetSwipeleftBools();
                playButton.SetActive(false);
                tutorialButton.SetActive(false);

                startedDirection = 2; // caso para ensinar remates para esquerda (2)
                DeactivateDefenderes();  // mete os varios defenderes a false
                Invoke("Apitar", 0f);

                break;
            case GameManagerState.SwipeRight:
                SetSwiperightBools();
                playButton.SetActive(false);
                tutorialButton.SetActive(false);

                startedDirection = 3; // caso para ensinar remates para direita (3)
                DeactivateDefenderes(); // mete os varios defenderes a false
                Invoke("Apitar", 0f);

                break;
            case GameManagerState.SwipeUp:
                SetSwipeupBools();
                playButton.SetActive(false);
                tutorialButton.SetActive(false);

                startedDirection = 4; // caso para ensinar remates para cima (4)
                DeactivateDefenderes(); // mete os varios defenderes a false
                Invoke("Apitar", 0f);

                break;
            case GameManagerState.TutorialP1:
                SetTutorialP1Bools();

                // És o guarda redes da tua equipa e vais ter de defender um conjunto de remates. Vamos ver quantos consegues defender.
                tutorial0.Play();

                // Estás no centro da baliza e quando ouvires o apito significa que vão rematar para ti. Deves varrer o ecrã para o lado de onde ouves a bola.
                // O próximo remate vem para a tua direita, experimenta.
                tutorial1.PlayDelayed(tutorial0.clip.length);

                Invoke("Apitar", tutorial0.clip.length + tutorial1.clip.length);

                break;
            case GameManagerState.TutorialP2:
                SetTutorialP2Bools();

                // Bom trabalho. Depois de defenderes voltas para o meio da baliza para defender o próximo remate. Tens de prestar atenção ao remate. 
                // Rápido, o próximo remate vem para a tua esquerda.
                tutorial2.Play();
                Invoke("Apitar", tutorial2.clip.length);

                break;
            case GameManagerState.TutorialP3:
                SetTutorialP3Bools();

                //Os remates também podem ser feitos para cima de ti ou para baixo, para o meio das tuas pernas. No próximo remate tens de varrer o ecrã para baixo. Experimenta.
                tutorial3.Play();
                Invoke("Apitar", tutorial3.clip.length);

                break;
            case GameManagerState.TutorialP4:
                SetTutorialP4Bools();

                // Por fim, os remates também podem ir para os cantos da baliza. Para defenderes estes remates, tens de fazer um L no ecrã. 
                // O próximo remate vem para o canto superior direito, tens de fazer um L para lá. Experimenta.
                tutorial4.Play();
                Invoke("Apitar", tutorial4.clip.length);

                break;
            case GameManagerState.TutorialP5:
                SetTutorialP5Bools();

                // Chegámos ao final do tutorial. Durante o jogo tens 5 vidas, ou seja, só podes deixar entrar 5 golos.Mais que isso e perdes.Podes fazer o tutorial as vezes que quiseres. 
                // No menu das instruções podes ainda treinar os vários tipos de remate. Boa sorte.
                tutorial5.Play();
                Invoke("ChangeToInstructionsState", tutorial5.clip.length);

                break;
            case GameManagerState.SwipeDownThenLeft:
                SetSwipeDownThenLeftBools();

                // caso para ensinar remates para swipe down then left (5)
                startedDirection = 5;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                break;
            case GameManagerState.SwipeLeftThenUp:
                SetSwipeLeftThenUpBools();

                // caso para ensinar remates para swipe left then up (6)
                startedDirection = 6;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                break;
            case GameManagerState.SwipeRightThenDown:
                SetSwipeRightThenDownBools();

                // caso para ensinar remates para swipe right then down (7)
                startedDirection = 7;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                break;
            case GameManagerState.SwipeLeftThenDown:
                SetSwipeLeftThenDownBools();

                // caso para ensinar remates para swipe left then down (8)
                startedDirection = 8;

                // mete os varios defenderes a false
                DeactivateDefenderes();

                break;
            case GameManagerState.SwipeDownThenRight:
                SetSwipeDownThenRightBools();

                // caso para ensinar remates para swipe down then right (9)
                startedDirection = 9;

                // mete os varios defenderes a false
                DeactivateDefenderes();

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

    public void ChangeToInstructionsState()
    {
        SetGameManagerState(GameManagerState.Instructions);
    }

    public void ChangeToGameoverState()
    {
        SetGameManagerState(GameManagerState.GameOver);
    }

    public static GameManagerState GetCurrentState()
    {
        return GMState;
    }

    // verifica que o jogo ja começou
    public static bool GetStarted()
    {
        return started;
    }

    // verifica que se esta no estado inicial do jogo
    public static bool GetOpening()
    {
        return opening;
    }

    // verifica que se terminou o jogo
    public static bool GetGameover()
    {
        return gameover;
    }

    // verifica que estamos no modo de jogo de treinar swipes para cima
    public static bool GetSwipeUp()
    {
        return swipeup;
    }

    // verifica que estamos no modo de jogo de treinar swipes para esquerda
    public static bool GetSwipeLeft()
    {
        return swipeleft;
    }

    // verifica que estamos no modo de jogo de treinar swipes para direita
    public static bool GetSwipeRight()
    {
        return swiperight;
    }

    // verifica que estamos no modo de jogo de treinar swipes para baixo
    public static bool GetSwipeDown()
    {
        return swipedown;
    }

    // verifica que estamos nas instrucoes
    public static bool GetInstructions()
    {
        return instructions;
    }

    // verifica que estamos na parte 1 do tutorial
    public static bool GetTutorialP1()
    {
        return tutorialp1;
    }

    // verifica que estamos na parte 2 do tutorial
    public static bool GetTutorialP2()
    {
        return tutorialp2;
    }

    // verifica que estamos na parte 3 do tutorial
    public static bool GetTutorialP3()
    {
        return tutorialp3;
    }

    // verifica que estamos na parte 4 do tutorial
    public static bool GetTutorialP4()
    {
        return tutorialp4;
    }

    // verifica que estamos na parte 5 do tutorial
    public static bool GetTutorialP5()
    {
        return tutorialp5;
    }

    // verifica que estamos no modo de jogo de treinar swipes para baixo e para a esquerda
    public static bool GetSwipeDownThenLeft()
    {
        return swipedownthenleft;
    }

    // verifica que estamos no modo de jogo de treinar swipes para esquerda e para cima
    public static bool GetSwipeLeftThenUp()
    {
        return swipeleftthenup;
    }

    // verifica que estamos no modo de jogo de treinar swipes para direita e para baixo
    public static bool GetSwipeRightThenDown()
    {
        return swiperightthendown;
    }

    // verifica que estamos no modo de jogo de treinar swipes para esquerda e para baixo
    public static bool GetSwipeLeftThenDown()
    {
        return swipeleftthendown;
    }

    // verifica que estamos no modo de jogo de treinar swipes para baixo e para a direita
    public static bool GetSwipeDownThenRight()
    {
        return swipedownthenright;
    }

    public void Apitar()
    {
        apitoParaChutar.Play();
        if (GetTutorialP1())
        {
            Invoke("Rematar", 1.0f);
        }
        Invoke("Rematar", apitoParaChutar.clip.length);
    }

    public void Rematar()
    {
        if (GetStarted())
        {
            // 1 - baixo
            // 2 - esquerda
            // 3 - direita
            // 4 - cima
            // 5 - down then left
            // 6 - left then up
            // 7 - right then down
            // 8 - left then down
            // 9 - down then right
            switch (startedDirection)
            {
                case 1: // 1 - baixo
                    if (golos_sofridos < 5)
                    {
                        chutoBaixoMeio.Play();
                        StartCoroutine(WaitForDownShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
                case 2: // 2 - esquerda
                    if (golos_sofridos < 5)
                    {
                        chutoEsquerdaMeio.Play();
                        StartCoroutine(WaitForLeftShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
                case 3: // 3 - direita
                    if (golos_sofridos < 5)
                    {
                        chutoDireitaMeio.Play();
                        StartCoroutine(WaitForRightShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
                case 4: // 4 - cima
                    if (golos_sofridos < 5)
                    {
                        chutoCimaMeio.Play();
                        StartCoroutine(WaitForUpShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
                case 5: // 5 - down then left
                    if (golos_sofridos < 5)
                    {
                        chutoBaixoEsquerda.Play();
                        StartCoroutine(WaitForDownLeftShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
                case 9: // 9 - down then right
                    if (golos_sofridos < 5)
                    {
                        chutoBaixoDireita.Play();
                        StartCoroutine(WaitForDownRightShot(3.0f));
                    }
                    else
                    {
                        Invoke("ChangeToGameoverState", 0.5f);
                        apito3x.Play();
                    }
                    break;
            }
        }

        else if (GetTutorialP1())
        {
            chutoDireitaMeio.Play();
        }

        else if (GetTutorialP2())
        {

        }

        else if (GetTutorialP3())
        {

        }

        else if(GetTutorialP4())
        {

        }

        else if (GetTutorialP5())
        {

        }

        else if (GetSwipeLeft())
        {
            chutoEsquerdaMeio.Play();
            StartCoroutine(WaitForLeftShot(3.0f));
        }

        else if (GetSwipeRight())
        {
            chutoDireitaMeio.Play();
            StartCoroutine(WaitForRightShot(3.0f));
        }

        else if (GetSwipeDown())
        {
            chutoBaixoMeio.Play();
            StartCoroutine(WaitForDownShot(3.0f));
        }

        else if (GetSwipeUp())
        {
            chutoCimaMeio.Play();
            StartCoroutine(WaitForUpShot(3.0f));
        }        
    }

    public IEnumerator WaitForLeftShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedSwipeLeft())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a esquerda ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public IEnumerator WaitForRightShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedSwipeRight())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a direita ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public IEnumerator WaitForUpShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedSwipeUp())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a direita ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public IEnumerator WaitForDownShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedSwipeDown())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a direita ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public IEnumerator WaitForDownLeftShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedDownThenLeftSwipe())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a direita ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public IEnumerator WaitForDownRightShot(float duration)
    {
        increaseSpeedTimer = duration;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                if (PlayerControlSwipe.GetConfirmedDownThenRightSwipe())
                {
                    // defendeu
                    golos_defendidos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
                else
                { // nao se mexeu para a direita ou nao se mexeu a tempo do proximo remate, sofreu golo
                    golo.Play();
                    golos_sofridos++;
                    PlayerControlSwipe.ResetGloves();
                    Invoke("Rematar", golo.clip.length);
                    yield break;
                }
            }
        }
    }

    public static bool GetResetGloves()
    {
        return resetGloves;
    }

    public static void ResetResetGloves()
    {
        resetGloves = false;
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

    // Bools para o estado SwipeDownThenLeft
    private void SetSwipeDownThenLeftBools()
    {
        swipedownthenleft = true;
        instructions = opening = started = swipedown = swipeleft = swiperight = swipeup = gameover = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipeleftthenup = swiperightthendown = swipeleftthendown = swipedownthenright = false;
    }

    // Bools para o estado SwipeLeftThenUp
    private void SetSwipeLeftThenUpBools()
    {
        swipeleftthenup = true;
        instructions = opening = started = swipedown = swipeleft = swiperight = swipeup = gameover = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipedownthenleft = swiperightthendown = swipeleftthendown = swipedownthenright = false;
    }

    // Bools para o estado SwipeRightThenDown
    private void SetSwipeRightThenDownBools()
    {
        swiperightthendown = true;
        instructions = opening = started = swipedown = swipeleft = swiperight = swipeup = gameover = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipedownthenleft = swipeleftthenup = swipeleftthendown = swipedownthenright = false;
    }

    // Bools para o estado SwipeLeftThenDown
    private void SetSwipeLeftThenDownBools()
    {
        swipeleftthendown = true;
        instructions = opening = started = swipedown = swipeleft = swiperight = swipeup = gameover = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipedownthenleft = swipeleftthenup = swiperightthendown = swipedownthenright = false;
    }

    // Bools para o estado SwipeDownThenRight
    private void SetSwipeDownThenRightBools()
    {
        swipedownthenright = true;
        instructions = opening = started = swipedown = swipeleft = swiperight = swipeup = gameover = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        swipedownthenleft = swipeleftthenup = swiperightthendown = swipeleftthendown = false;
    }
}

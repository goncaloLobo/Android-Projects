using System.Collections;
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

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private static bool started;
    private static int startedDirection;
    private float currCountdownValue;
    private float increaseSpeedTimer;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions, SwipeRight, SwipeLeft, SwipeUp, SwipeDown
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        started = false;
        startedDirection = 0;
        sounds = GetComponents<AudioSource>();
        apitoParaChutar = sounds[0];
        chutoEsquerda = sounds[1];
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                howToButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                playButton.SetActive(false);
                howToButton.SetActive(false);
                textGameModes.SetActive(false);
                started = true;

                //aqui ira ser feito toda a parte aleatoria de vários tipos de remates

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                break;
            case GameManagerState.GameOver:
                //display game over
                GameOverGO.SetActive(true);

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 1f);

                break;
            case GameManagerState.Instructions:
                GameOverGO.SetActive(false);
                playButton.SetActive(false);
                howToButton.SetActive(false);
                textGameModes.SetActive(true);
                //panelHolder.SetActive(true);

                defenderBaixo.SetActive(true);
                defenderCima.SetActive(true);
                defenderEsquerda.SetActive(true);
                defenderDireita.SetActive(true);

                break;

            case GameManagerState.SwipeDown:
                // caso para ensinar remates para baixo (1)
                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                startedDirection = 1;

                break;

            case GameManagerState.SwipeLeft:
                // caso para ensinar remates para esquerda (2)
                // unico caso funcional, por agora.
                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                startedDirection = 2;

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                // countdown para o apito (2s)
                //StartCoroutine(StartCountDownToApito());
                Invoke("Apitar", 2f);

                break;

            case GameManagerState.SwipeRight:
                // caso para ensinar remates para direita (3)

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                startedDirection = 3;

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                break;

            case GameManagerState.SwipeUp:
                // caso para ensinar remates para baixo (4)

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                startedDirection = 4;

                // tipo de controlo
                playerShip.GetComponent<PlayerControlSwipe>().Init();

                break;
        }
    }

    public void StartGamePlay()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.Gameplay;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
    }

    public void StartGameInstructions()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.Instructions;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
    }

    //funcao que vai ser chamada qdo o botao defender para cima é carregado
    public void StartDefenderCima()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.SwipeUp;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
    }

    //funcao que vai ser chamada qdo o botao defender para baixo é carregado
    public void StartDefenderBaixo()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.SwipeDown;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
    }

    //funcao que vai ser chamada qdo o botao defender para direita é carregado
    public void StartDefenderDireita()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.SwipeRight;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
    }

    //funcao que vai ser chamada qdo o botao defender para esquerda é carregado
    public void StartDefenderEsquerda()
    {
        // Detecting double click
        clicked++;

        if (clicked == 1)
            clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            // Double click detected
            clicked = 0;
            clicktime = 0;
            GMState = GameManagerState.SwipeLeft;
            UpdateGameManagerState();
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
            clicked = 0;
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

    public static bool GetStarted()
    {
        return started;
    }

    public void Apitar()
    {
        Debug.Log("vou apitar!");
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
}

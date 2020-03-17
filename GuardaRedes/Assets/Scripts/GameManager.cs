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

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private static bool started;
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

                // countdown para a velocidade
                StartCoroutine(StartCountdownSpeed());

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

                defenderBaixo.SetActive(true);
                defenderCima.SetActive(true);
                defenderEsquerda.SetActive(true);
                defenderDireita.SetActive(true);

                break;

            case GameManagerState.SwipeDown:
                // caso para ensinar remates para baixo

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                started = true;

                break;

            case GameManagerState.SwipeLeft:
                // caso para ensinar remates para esquerda

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                started = true;

                break;

            case GameManagerState.SwipeRight:
                // caso para ensinar remates para direita

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                started = true;

                break;

            case GameManagerState.SwipeUp:
                // caso para ensinar remates para baixo

                defenderBaixo.SetActive(false);
                defenderCima.SetActive(false);
                defenderEsquerda.SetActive(false);
                defenderDireita.SetActive(false);
                textGameModes.SetActive(false);
                started = true;

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

    // aumenta ou diminui o pitch a cada 15s
    public IEnumerator StartCountdownSpeed(float countdownValue = 15)
    {
        increaseSpeedTimer = countdownValue;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
            }
        }
    }
}

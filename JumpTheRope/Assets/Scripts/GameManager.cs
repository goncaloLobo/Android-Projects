using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject howToButton;
    public GameObject playerShip;
    public GameObject GameOverGO;

    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float initialSpawnRate;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions
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
                howToButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:

                //Reset ao score, botao play e instrucoes
                playButton.SetActive(false);
                howToButton.SetActive(false);

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
                playButton.SetActive(true);
                howToButton.SetActive(false);

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

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject GameOverGO;

    public AudioSource audioData; // som da corda

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private static bool started;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instructions
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
        audioData = GetComponent<AudioSource>();
        started = false;
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

                playButton.SetActive(false);
                started = true;
                audioData.Play();
                audioData.loop = true;

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
}

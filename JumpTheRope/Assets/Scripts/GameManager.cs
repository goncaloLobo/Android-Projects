using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject GameOverGO;

    public AudioSource audioData; // som da corda

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.2f;

    private static bool started;
    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float baseCountdown = 15.0f;

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

                // countdown para alterar a velocidade do som
                StartCoroutine(StartCountdownSpeed(15));

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

    // aumenta ou diminui o pitch a cada 15s
    // vai diminuir este intervalo de 3 em 3s (até 1s)
    public IEnumerator StartCountdownSpeed(float countdownValue)
    {
        Debug.Log("entrei: " + baseCountdown);
        increaseSpeedTimer = countdownValue;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                Debug.Log("cheguei ao fim.");
                if (Random.value > 0.5f)
                {
                    //pitch inicia a 0.9
                    //aumenta o pitch
                    if ((audioData.pitch + 0.05f) > 1.1f)
                    {
                        audioData.pitch = 1.1f;
                    }
                    else
                    {
                        audioData.pitch += 0.05f;
                    }
                }
                else
                {
                    // diminui o pitch
                    if ((audioData.pitch - 0.05f) < 0.9f)
                    {
                        audioData.pitch = 0.9f;
                    }
                    else
                    {
                        audioData.pitch -= 0.05f;
                    }
                }
                Invoke("CreateNewCoroutine", 0.0f);
                yield break;
            }
        }
    }

    public void CreateNewCoroutine()
    {
        if (baseCountdown - 3.0f < 1.0f)
        {
            Debug.Log("entrei 1s");
            StartCoroutine(StartCountdownSpeed(baseCountdown =1.0f));
        }
        else
        {
            Debug.Log("entrei normalmente");
            StartCoroutine(StartCountdownSpeed(baseCountdown -= 3.0f));
        }        
    }
}

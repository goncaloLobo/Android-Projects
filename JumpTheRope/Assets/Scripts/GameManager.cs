using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject GameOverGO;

    public AudioSource audioData; // som da corda
    private static bool started, toFinish;
    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float baseCountdown = 15.0f;

    private int finalScore;
    private int n_saltos_perfeitos;
    private int n_saltos_normais;

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

        //valor inicial do pitch para a pessoa se habituar aos sons.
        audioData.pitch = 0.8f;
        started = toFinish = false;
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

                break;
            case GameManagerState.GameOver:
                //display game over
                GameOverGO.SetActive(true);

                //obtem estatísticas do final do jogo
                finalScore = DoubleClickChecker.GetPontuacao();
                n_saltos_perfeitos = DoubleClickChecker.GetSaltosPerfeitos();
                n_saltos_normais = DoubleClickChecker.GetSaltosNormais();

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 5f);

                break;
            case GameManagerState.Instructions:
                GameOverGO.SetActive(false);

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

    public void ChangeToGameOverState()
    {
        SetGameManagerState(GameManagerState.GameOver);
    }

    public static bool GetStarted()
    {
        return started;
    }

    // aumenta ou diminui o pitch a cada 15s
    // vai diminuir este intervalo de 3 em 3s (até 1s)
    public IEnumerator StartCountdownSpeed(float countdownValue)
    {
        increaseSpeedTimer = countdownValue;
        while (increaseSpeedTimer >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            increaseSpeedTimer--;

            if (increaseSpeedTimer == 0)
            {
                Debug.Log("pitch inicial: " + audioData.pitch);
                // aumenta o pitch para 0.9
                if(audioData.pitch == 0.8f)
                {
                    audioData.pitch = 0.9f;
                }

                // pitch aqui = 0.9f
                if (Random.value > 0.5f)
                {
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
                if (toFinish) // o jogo vai terminar
                {
                    Invoke("ChangeToGameOverState", 1f);
                    yield break;
                }
                else
                {
                    Invoke("CreateNewCoroutine", 0.0f);
                    //Invoke("CreateNewCoroutineRandom", 0.0f);
                    yield break;
                }                
            }
        }
    }

    public void CreateNewCoroutine()
    {
        if (baseCountdown - 2.0f < 1.0f)
        {
            StartCoroutine(StartCountdownSpeed(baseCountdown = 1.0f));
            if (IsGameOver())
            {
                // jogo vai terminar
                toFinish = true;
            }
            else
            {
                // vai chamar outra coroutine (neste caso, de 1s) para
                // verificar novamente se o jogo irá terminar ou não
                StartCoroutine(StartCountdownSpeed(baseCountdown = 1.0f));
            }
        }
        else
        {
            StartCoroutine(StartCountdownSpeed(baseCountdown -= 2.0f));
        }        
    }

    public void CreateNewCoroutineRandom()
    {
        if (Random.value > 0.3f) // diminui
        {
            Debug.Log("a diminuir");
            if (baseCountdown - 2.0f < 1.0f)
            {
                StartCoroutine(StartCountdownSpeed(baseCountdown = 1.0f));
            }
            else
            {
                StartCoroutine(StartCountdownSpeed(baseCountdown -= 2.0f));
            }
        }
        else
        { // aumentar
            Debug.Log("a aumentar");
            if (baseCountdown + 2.0f > 15.0f)
            {
                StartCoroutine(StartCountdownSpeed(baseCountdown = 1.0f));
            }
            else
            {
                StartCoroutine(StartCountdownSpeed(baseCountdown += 2.0f));
            }
        }
    }

    // se a % de saltos perfeitos no total de saltos for menor que 70% entao termina o jogo
    private bool IsGameOver()
    {
        if((float) DoubleClickChecker.GetSaltosPerfeitos() / (float)DoubleClickChecker.GetTotalSaltos() < 0.7f)
        {
            Debug.Log("GameOver");
            return true;
        }
        return false;
    }
}

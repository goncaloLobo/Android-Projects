using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject playButton;
    public GameObject GameOverGO;
    public GameObject introducaoButton;
    public GameObject instrucoesButton;
    public GameObject closeButton;

    public AudioSource[] sounds;
    public AudioSource audioData; // som da corda sounds[0]
    public AudioSource instrucoesPt1; // sounds[1]
    public AudioSource oneJump; // sounds[2]
    public AudioSource instrucoespt2; // sounds[3]
    public AudioSource correctJump; // sounds[4]
    public AudioSource backgroundLoop;
    public AudioSource pontos; // sounds [6]
    public AudioSource introducao;
    public AudioSource textToSpeech;
    public AudioSource paraIniciarJogo;
    private static bool started, toFinish, preGameplay;
    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float baseCountdown = 15.0f;

    private int finalScore;
    private int n_saltos_perfeitos;
    private int n_saltos_normais;

    private float highscoreStored;
    private float saltosPerfeitosStored;
    private float saltosNormaisStored;
    private float totalSaltosStored;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instrucoes, PreGameplay
    }

    public static GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;
        sounds = GetComponents<AudioSource>();
        audioData = sounds[0];
        instrucoesPt1 = sounds[1];
        oneJump = sounds[2];
        instrucoespt2 = sounds[3];
        correctJump = sounds[4];
        backgroundLoop = sounds[5];
        pontos = sounds[6];
        backgroundLoop.Play();
        backgroundLoop.loop = true;

        //valor inicial do pitch
        audioData.pitch = 0.8f;
        started = toFinish = preGameplay = false;

        // vai buscar o highscore no start para quando o jogo é iniciado
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
                audioData.Stop();
                introducaoButton.SetActive(true);
                instrucoesButton.SetActive(true);
                closeButton.SetActive(false);
                started = false;

                // vai buscar o highscore no opening para qdo o jogo termina e volta a este estado
                // ou seja, todas as vezes que o jogador perde
                GetCurrentHighScores();

                break;
            case GameManagerState.PreGameplay:
                System.Diagnostics.Debug.WriteLine("entrei pregameplay");

                preGameplay = true;
                paraIniciarJogo.Play();

                break;
            case GameManagerState.Gameplay:
                preGameplay = false;
                System.Diagnostics.Debug.WriteLine("entrei gameplay");
                playButton.SetActive(false);
                introducaoButton.SetActive(false);
                instrucoesButton.SetActive(false);
                closeButton.SetActive(true);
                started = true;
                audioData.Play();
                audioData.loop = true;

                // countdown para alterar a velocidade do som
                StartCoroutine(StartCountdownSpeed(15));

                break;
            case GameManagerState.GameOver:
                //display game over
                GameOverGO.SetActive(true);
                audioData.Stop();
                started = false;

                //obtem estatísticas do final do jogo
                finalScore = DoubleClickChecker.GetPontuacao();
                n_saltos_perfeitos = DoubleClickChecker.GetSaltosPerfeitos();
                n_saltos_normais = DoubleClickChecker.GetSaltosNormais();
                StartCoroutine(DownloadAudio(finalScore, n_saltos_normais, n_saltos_perfeitos));

                // se for novo highscore, vai regista-lo
                if (finalScore > highscoreStored)
                {
                    GameScore.SetHighScore(finalScore, n_saltos_perfeitos, n_saltos_normais);
                }

                //mudar o estado do gamemanagerstate
                Invoke("ChangeToOpeningState", 3f);
                break;
            case GameManagerState.Instrucoes:
                started = false;
                float delay = 0f;
                instrucoesPt1.Play();

                delay += instrucoesPt1.clip.length;
                oneJump.PlayDelayed(delay);

                delay += oneJump.clip.length;
                instrucoespt2.PlayDelayed(delay);

                delay += instrucoespt2.clip.length;
                correctJump.PlayDelayed(delay);

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

    //obtem o estado atual do jogo
    public static GameManagerState GetCurrentState()
    {
        return GMState;
    }

    public static bool GetStarted()
    {
        return started;
    }

    public static bool GetPreGameplay()
    {
        return preGameplay;
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

    //obtem o highscore que esteja guardado, qualquer que seja o valor, no start e no opening
    private void GetCurrentHighScores()
    {
        highscoreStored = PlayerPrefs.GetFloat("highscore", 0);
        saltosPerfeitosStored = PlayerPrefs.GetFloat("perfeitos", 0);
        saltosNormaisStored = PlayerPrefs.GetInt("normais", 0);
        totalSaltosStored = PlayerPrefs.GetInt("total", 0);
    }

    IEnumerator DownloadAudio(int finalScore, int saltosNormais, int saltosPerfeitos)
    {
        // "%20pontos%20"
        string googleUrl = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+" + finalScore + "&tl=pt-BR";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(googleUrl, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                textToSpeech.clip = myClip;
                AudioSource source1 = GameObject.FindGameObjectWithTag("GameOverGO").GetComponent<AudioSource>();
                textToSpeech.PlayDelayed(source1.clip.length);
                pontos.PlayDelayed(source1.clip.length + textToSpeech.clip.length);
            }
        }
    }
}

﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject GameOverGO;
    public GameObject introducaoButton;
    public GameObject instrucoesButton;

    // coisas para a pagina do tutorial
    public GameObject tutorialButton;
    public GameObject corda1;
    public GameObject descricao1;
    public GameObject corda2;
    public GameObject descricao2;
    public GameObject corda3;
    public GameObject descricao3;
    public GameObject introducaoInicial;
    public GameObject descricao4;

    // coisas para o tutorial
    public AudioSource tut0;
    public AudioSource tut1;
    public AudioSource tut2;
    public AudioSource tut3;
    public AudioSource tut4;
    public AudioSource tut5;

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
    private static bool started, toFinish, preGameplay, opening, instrucoes;
    private static bool tutorialp1, tutorialp2, tutorialp3, tutorialp4, tutorialp5;
    private float currCountdownValue;
    private float increaseSpeedTimer;
    private float baseCountdown = 15.0f;
    private float delay = 0f;

    private int finalScore;
    private int n_saltos_perfeitos;
    private int n_saltos_normais;

    private float highscoreStored;
    private float saltosPerfeitosStored;
    private float saltosNormaisStored;
    private float totalSaltosStored;

    public enum GameManagerState
    {
        Opening, Gameplay, GameOver, Instrucoes, PreGameplay, TutorialP1, TutorialP2, TutorialP3, TutorialP4, TutorialP5
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

        tut0 = sounds[7];
        tut1 = sounds[8];
        tut2 = sounds[9];
        tut3 = sounds[10];
        tut4 = sounds[11];
        tut5 = sounds[12];

        //valor inicial do pitch
        audioData.pitch = 0.8f;
        started = toFinish = preGameplay = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
        opening = true;

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
                SetOpeningBools();

                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                audioData.Stop();
                introducaoButton.SetActive(true);
                instrucoesButton.SetActive(true);

                TutorialButtonsToFalse();

                // vai buscar o highscore no opening para qdo o jogo termina e volta a este estado
                // ou seja, todas as vezes que o jogador perde
                GetCurrentHighScores();

                break;

            case GameManagerState.PreGameplay:
                SetPregameplayBools();
                break;

            case GameManagerState.Gameplay:
                SetGameplayBools();
                TutorialButtonsToFalse();

                playButton.SetActive(false);
                introducaoButton.SetActive(false);
                instrucoesButton.SetActive(false);

                audioData.Play();
                audioData.loop = true;

                // countdown para alterar a velocidade do som
                StartCoroutine(StartCountdownSpeed(15));

                break;

            case GameManagerState.TutorialP1:
                SetTutorial1Bools();

                introducaoButton.SetActive(false);
                instrucoesButton.SetActive(false);
                playButton.SetActive(false);
                TutorialButtonsToFalse();

                // "Vamos dar início ao tutorial"
                tut0.Play();
                delay += tut0.clip.length;

                // "Para levantar uma perna deve tocar uma vez no ecrã: experimente"
                tut1.PlayDelayed(delay);

                break;
            case GameManagerState.TutorialP2:
                SetTutorial2Bools();

                // "Agora, para saltar à corda, deve fazer um duplo toque no ecrã: experimente."
                tut2.Play();

                break;
            case GameManagerState.TutorialP3:
                SetTutorial3Bools();

                // "Para saltar de forma perfeita, deve fazer um duplo toque no ecrã de forma mais rápida. Experimente"
                tut3.Play();
                break;

            case GameManagerState.TutorialP4:
                SetTutorial4Bools();

                tut4.Play();
                audioData.PlayDelayed(tut4.clip.length);
                if(DoubleClickChecker.GetJ() == 3)
                {
                    audioData.Stop();
                }
                break;

            case GameManagerState.TutorialP5:
                SetTutorial5Bools();

                tut5.Play();
                audioData.Stop();

                Invoke("ChangeToInstructionsState", 0f);

                break;
            case GameManagerState.GameOver:
                SetGameoverBools();

                GameOverGO.SetActive(true);
                audioData.Stop();

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
                SetInstructionsBools();

                audioData.Stop();
                playButton.SetActive(true);
                introducaoButton.SetActive(false);
                instrucoesButton.SetActive(false);

                TutorialButtonsToTrue();
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
        SetGameManagerState(GameManagerState.Instrucoes);
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

    public static bool GetOpening()
    {
        return opening;
    }

    public static bool GetInstrucoes()
    {
        return instrucoes;
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

            // se falhar mais do que 10 saltos, então vai terminar o jogo.
            if(DoubleClickChecker.GetSaltosFalhados() > 10)
            {
                toFinish = true;
            }
            else if (IsGameOver())
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

    // desativa os botoes e os sons do tutorial
    private void TutorialButtonsToFalse()
    {
        tutorialButton.SetActive(false);
        corda1.SetActive(false);
        descricao1.SetActive(false);
        corda2.SetActive(false);
        descricao2.SetActive(false);
        corda3.SetActive(false);
        descricao3.SetActive(false);
        introducaoInicial.SetActive(false);
        descricao4.SetActive(false);
    }

    // ativa os botoes e os sons do tutorial
    private void TutorialButtonsToTrue()
    {
        tutorialButton.SetActive(true);
        corda1.SetActive(true);
        descricao1.SetActive(true);
        corda2.SetActive(true);
        descricao2.SetActive(true);
        corda3.SetActive(true);
        descricao3.SetActive(true);
        introducaoInicial.SetActive(true);
        descricao4.SetActive(true);
    }

    // inicializa os bools para o estado opening
    private void SetOpeningBools()
    {
        opening = true;
        started = instrucoes = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = preGameplay = false;
    }

    // inicializa os bools para o estado pregameplay
    private void SetPregameplayBools()
    {
        preGameplay = true;
        opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado gameplay
    private void SetGameplayBools()
    {
        started = true;
        preGameplay = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = instrucoes = false;
    }

    // inicializa os bools para o estado tutorialp1
    private void SetTutorial1Bools()
    {
        tutorialp1 = true;
        preGameplay = opening = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado tutorialp2
    private void SetTutorial2Bools()
    {
        tutorialp2 = true;
        preGameplay = opening = tutorialp1 = tutorialp3 = tutorialp4 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado tutorialp3
    private void SetTutorial3Bools()
    {
        tutorialp3 = true;
        preGameplay = opening = tutorialp1 = tutorialp2 = tutorialp4 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado tutorialp4
    private void SetTutorial4Bools()
    {
        tutorialp4 = true;
        preGameplay = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado tutorialp5
    private void SetTutorial5Bools()
    {
        tutorialp5 = true;
        preGameplay = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = started = instrucoes = false;
    }

    // inicializa os bools para o estado gameover
    private void SetGameoverBools()
    {
        preGameplay = opening = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = started = instrucoes = false;
    }

    // inicializa os bools para o estado instrucoes
    private void SetInstructionsBools()
    {
        instrucoes = true;
        preGameplay = opening = started = tutorialp1 = tutorialp2 = tutorialp3 = tutorialp4 = tutorialp5 = false;
    }
}

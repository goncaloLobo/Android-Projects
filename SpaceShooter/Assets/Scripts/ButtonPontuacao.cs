using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonPontuacao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource pontuacao;
    public AudioSource textToSpeech;
    public AudioSource avoidedEnemies;
    public AudioSource pontuacao2; // "Pontuacao deste jogo:"
    public AudioSource pontuacao3; // "Duracao do jogo:"
    public AudioSource pontuacaoError; // "Nao tem nenhum jogo feito" ou whatever
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, jogarBackToNormal;
    private static bool instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        pontuacao = sounds[0];
        avoidedEnemies = sounds[1];
        pontuacao2 = sounds[2];
        pontuacao3 = sounds[3];
        pontuacaoError = sounds[4];

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonPontuacao").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontos.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB1.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB2.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB3.PontuacaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!pontuacao.isPlaying)
            pontuacao.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                if (PlayerControlSwipe.GetPontuacaoCancelAction())
                {
                    PlayerControlSwipe.ResetPontuacaoCancelAction();
                }
                else
                {
                    float highscore = PlayerPrefs.GetFloat("highscore");
                    float time = PlayerPrefs.GetFloat("time");
                    int enemiesAvoided = PlayerPrefs.GetInt("enemies");
                    if(highscore == 0)
                    {
                        // ainda nao fez nenhum jogo
                        pontuacaoError.Play();
                    }
                    else
                    {
                        // vai dizer os resultados do melhor jogo
                        StartCoroutine(DownloadHighscore(highscore));
                        StartCoroutine(DownloadAvoidedEnemies(enemiesAvoided));
                        StartCoroutine(DownloadTime(time));
                    }
                }
            }
            else if (GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                if (PlayerControlSwipe.GetPontuacaoCancelAction())
                {
                    PlayerControlSwipe.ResetPontuacaoCancelAction();
                }
                else
                {
                    float highscore = PlayerPrefs.GetFloat("highscore");
                    float time = PlayerPrefs.GetFloat("time");
                    int enemiesAvoided = PlayerPrefs.GetInt("enemies");
                    if (highscore == 0)
                    {
                        // ainda nao fez nenhum jogo
                        pontuacaoError.Play();
                    }
                    else
                    {
                        // vai dizer os resultados do melhor jogo
                        StartCoroutine(DownloadHighscore(highscore));
                        StartCoroutine(DownloadAvoidedEnemies(enemiesAvoided));
                        StartCoroutine(DownloadTime(time));
                    }
                }
            }
        }
        lastTapTime = currentTapTime;
    }

    private bool CheckForDoubleTap(float currentTapTime, float previousTapTime)
    {
        if (currentTapTime - previousTapTime < clickdelay)
        {
            return true;
        }
        return false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (InstrucoesB1.CheckForHighlighted() == 1)
        {
            instrucoesB1BackToNormal = true;
        }

        if (InstrucoesB2.CheckForHighlighted() == 1)
        {
            instrucoesB2BackToNormal = true;
        }

        if (InstrucoesB3.CheckForHighlighted() == 1)
        {
            instrucoesB3BackToNormal = true;
        }

        if(ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!pontuacao.isPlaying)
        {
            if (InstrucoesB1.GetSoundOn() == 1)
            {
                InstrucoesB1.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (InstrucoesB1.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 1)
            {
                InstrucoesB2.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 1)
            {
                InstrucoesB3.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                pontuacao.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                pontuacao.Play();
                soundOn = 1;
            }
        }
    }

    public static int GetSoundOn()
    {
        return soundOn;
    }

    public static void SetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static bool InstrucoesB2BackToNormal()
    {
        return instrucoesB2BackToNormal;
    }

    public static bool InstrucoesB3BackToNormal()
    {
        return instrucoesB3BackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    private IEnumerator DownloadAvoidedEnemies(int enemiesAvoided)
    {
        System.Diagnostics.Debug.WriteLine("entrei coroutine download avoided enemies button pontuacao");
        // "%20pontos%20"
        string pontostxt = "pontos";
        string googleUrl = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=pt-BR&q=" + enemiesAvoided + pontostxt;
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
                avoidedEnemies.Play(); // "Inimigos desviados :"
                textToSpeech.PlayDelayed(avoidedEnemies.clip.length);
            }
        }
    }

    private IEnumerator DownloadHighscore(float highscore)
    {
        System.Diagnostics.Debug.WriteLine("entrei coroutine download highscore button pontuacao");
        // "%20pontos%20"
        string pontostxt = "pontos";
        string googleUrl = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=pt-BR&q=" + highscore + pontostxt;
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
                pontuacao2.Play(); // "Pontuação deste jogo:"
                textToSpeech.PlayDelayed(avoidedEnemies.clip.length);
            }
        }
    }

    private IEnumerator DownloadTime(float time)
    {
        // "%20pontos%20"
        System.Diagnostics.Debug.WriteLine("entrei download time coroutine button pontuacao");
        string pontostxt = "pontos";
        string googleUrl = "https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl=pt-BR&q=" + time + pontostxt;
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
                pontuacao3.Play(); // "Duração do jogo:"
                textToSpeech.PlayDelayed(avoidedEnemies.clip.length);
            }
        }
    }
}

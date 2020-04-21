using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    public AudioSource vaicomeçar321;

    void Start()
    {
        jogar = GetComponent<AudioSource>();

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = pontuacaoBackToNormal = false;
        instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonPlay").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontos.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB1.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB2.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB3.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontuacao.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                if (PlayerControlSwipe.GetJogarCancelAction())
                {
                    PlayerControlSwipe.ResetJogarCancelAction();
                }
                else
                {
                    vaicomeçar321.Play();
                    Invoke("StartGame", vaicomeçar321.clip.length);
                }
            }
            else if(GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                if (PlayerControlSwipe.GetJogarCancelAction())
                {
                    PlayerControlSwipe.ResetJogarCancelAction();
                }
                else
                {
                    vaicomeçar321.Play();
                    Invoke("StartGame", vaicomeçar321.clip.length);
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
        mImage.sprite = spriteHighlighted;
        highlighted = 1;
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

        if(ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!jogar.isPlaying)
        {
            if (InstrucoesB1.GetSoundOn() == 1)
            {
                InstrucoesB1.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (InstrucoesB1.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 1)
            {
                InstrucoesB2.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 1)
            {
                InstrucoesB3.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                jogar.Play();
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

    public static bool PontuacaoBackToNormal()
    {
        return pontuacaoBackToNormal;
    }

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }
}

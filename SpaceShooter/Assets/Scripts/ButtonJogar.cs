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
    private static bool instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal, tutorialBackToNormal;
    private static bool hasEntered;
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
        instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = tutorialBackToNormal = false;
        hasEntered = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonPlay").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonComoJogar.ResetJogarBackToNormal();
        }

        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetJogarBackToNormal();
        }

        if (ButtonTempo.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetJogarBackToNormal();
        }

        if (ButtonVidas.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetJogarBackToNormal();
        }

        if (ButtonPontos.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetJogarBackToNormal();
        }

        if (InstrucoesB1.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetJogarBackToNormal();
        }

        if (InstrucoesB2.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB2.ResetJogarBackToNormal();
        }

        if (InstrucoesB3.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB3.ResetJogarBackToNormal();
        }

        if (ButtonPontuacao.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetJogarBackToNormal();
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
                else if(!hasEntered)
                {
                    hasEntered = true;
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

        if(ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
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

    // GameManager chama esta funcao para certificar que apenas entra 1x no estado Gameplay
    public static void ResetEntered()
    {
        hasEntered = false;
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

    public static void ResetComoJogarBackToNormal()
    {
        comoJogarBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static void ResetTempoBackToNormal()
    {
        tempoBackToNormal = false;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static void ResetPontosBackToNormal()
    {
        pontosBackToNormal = false;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static void ResetVidasBackToNormal()
    {
        vidasBackToNormal = false;
    }

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static void ResetInstrucoesB1BackToNormal()
    {
        instrucoesB1BackToNormal = false;
    }

    public static bool InstrucoesB2BackToNormal()
    {
        return instrucoesB2BackToNormal;
    }

    public static void ResetInstrucoesB2BackToNormal()
    {
        instrucoesB2BackToNormal = false;
    }

    public static bool InstrucoesB3BackToNormal()
    {
        return instrucoesB3BackToNormal;
    }

    public static void ResetInstrucoesB3BackToNormal()
    {
        instrucoesB3BackToNormal = false;
    }

    public static bool PontuacaoBackToNormal()
    {
        return pontuacaoBackToNormal;
    }

    public static void ResetPontuacaoBackToNormal()
    {
        pontuacaoBackToNormal = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }
}

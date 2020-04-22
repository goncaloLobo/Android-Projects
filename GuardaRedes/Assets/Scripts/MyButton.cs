using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageJogar;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool homeBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, closeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
        mImageJogar = GameObject.FindGameObjectWithTag("ButtonPlay").GetComponent<Image>();

        highlighted = 0;
        homeBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = closeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetJogarBackToNormal();
        }

        if (ButtonHome.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonHome.ResetJogarBackToNormal();
        }

        if (ButtonClose.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetJogarBackToNormal();
        }

        if (ButtonDefenderBaixo.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetJogarBackToNormal();
        }

        if (ButtonDefenderDireita.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetJogarBackToNormal();
        }

        if (ButtonDefenderEsquerda.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetJogarBackToNormal();
        }

        if (ButtonDefenderCima.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetJogarBackToNormal();
        }

        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetJogarBackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        jogar.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonDefenderBaixo.CheckForHighlighted() == 1)
        {
            buttonDefenderBaixoBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonClose.CheckForHighlighted() == 1)
        {
            closeBackToNormal = true;
        }

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageJogar.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(!jogar.isPlaying)
            jogar.Play();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    public static int GetSoundOn()
    {
        return soundOn;
    }

    public static void ResetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void ResetInstrucoesBackToNormal()
    {
        instrucoesBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool DefenderBaixoBackToNormal()
    {
        return buttonDefenderBaixoBackToNormal;
    }

    public static void ResetDefenderBaixoBackToNormal()
    {
        buttonDefenderBaixoBackToNormal = false;
    }

    public static bool DefenderCimaBackToNormal()
    {
        return buttonDefenderCimaBackToNormal;
    }

    public static void ResetDefenderCimaBackToNormal()
    {
        buttonDefenderCimaBackToNormal = false;
    }

    public static bool DefenderDireitaBackToNormal()
    {
        return buttonDefenderDireitaBackToNormal;
    }

    public static void ResetDefenderDireitaBackToNormal()
    {
        buttonDefenderDireitaBackToNormal = false;
    }

    public static bool DefenderEsquerdaBackToNormal()
    {
        return buttonDefenderEsquerdaBackToNormal;
    }

    public static void ResetDefenderEsquerdaBackToNormal()
    {
        buttonDefenderEsquerdaBackToNormal = false;
    }

    public static bool CloseBackToNormal()
    {
        return closeBackToNormal;
    }

    public static void ResetCloseBackToNormal()
    {
        closeBackToNormal = false;
    }
}

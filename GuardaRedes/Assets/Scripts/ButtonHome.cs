using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHome : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource sair;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageHome;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, closeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sair = GetComponent<AudioSource>();
        highlighted = 0;
        mImageHome = GameObject.FindGameObjectWithTag("HomeButton").GetComponent<Image>();
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = closeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderEsquerdaBackToNormal = buttonDefenderDireitaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetHomeBackToNormal();
        }

        if (ButtonIntroducao.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetHomeBackToNormal();
        }

        if (MyButton.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetHomeBackToNormal();
        }

        if (ButtonDefenderBaixo.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetHomeBackToNormal();
        }

        if (ButtonDefenderCima.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetHomeBackToNormal();
        }

        if (ButtonDefenderDireita.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetHomeBackToNormal();
        }

        if (ButtonDefenderEsquerda.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetHomeBackToNormal();
        }

        if (ButtonClose.HomeBackToNormal())
        {
            mImageHome.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetHomeBackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        sair.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
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
        if(ButtonDefenderBaixo.CheckForHighlighted() == 1)
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

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonClose.CheckForHighlighted() == 1)
        {
            closeBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageHome.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!sair.isPlaying)
            sair.Play();
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

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
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

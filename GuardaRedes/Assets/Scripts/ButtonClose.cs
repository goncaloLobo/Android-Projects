using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClose : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource sair;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageClose;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, homeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sair = GetComponent<AudioSource>();
        mImageClose = GameObject.FindGameObjectWithTag("CloseButton").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = homeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetCloseBackToNormal();
        }

        if (ButtonIntroducao.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetCloseBackToNormal();
        }

        if (MyButton.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetCloseBackToNormal();
        }

        if (ButtonHome.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonHome.ResetCloseBackToNormal();
        }

        if (ButtonDefenderEsquerda.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetCloseBackToNormal();
        }

        if (ButtonDefenderDireita.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetCloseBackToNormal();
        }

        if (ButtonDefenderCima.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetCloseBackToNormal();
        }

        if (ButtonDefenderBaixo.CloseBackToNormal())
        {
            mImageClose.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetCloseBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (sair.isPlaying)
                sair.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!sair.isPlaying)
            sair.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            // qdo pressiona o botao de sair, muda para o estado inicial
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay || GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
            }
            else if (GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeDown || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeLeft || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeRight || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeUp)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonDefenderBaixo.CheckForHighlighted() == 1)
        {
            buttonDefenderBaixoBackToNormal = true;
        }

        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
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

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageClose.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!sair.isPlaying)
        {
            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 1)
            {
                ButtonHome.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 1)
            {
                MyButton.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 1)
            {
                ButtonInstrucoes.ResetSoundOn();
                sair.Play();
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 0)
            {
                sair.Play();
                soundOn = 1;
            }
        }
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

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
    }
}
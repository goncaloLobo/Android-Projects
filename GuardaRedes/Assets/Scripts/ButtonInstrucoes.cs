using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInstrucoes : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource instrucoes;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageInstrucoes;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, homeBackToNormal, introducaoBackToNormal, closeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        instrucoes = GetComponent<AudioSource>();
        highlighted = 0;
        mImageInstrucoes = GameObject.FindGameObjectWithTag("ButtonInstrucoes").GetComponent<Image>();
        jogarBackToNormal = homeBackToNormal = introducaoBackToNormal = closeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderEsquerdaBackToNormal = buttonDefenderDireitaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonHome.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonHome.ResetInstrucoesBackToNormal();
        }

        if (ButtonClose.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetInstrucoesBackToNormal();
        }

        if (ButtonDefenderBaixo.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetInstrucoesBackToNormal();
        }

        if (ButtonDefenderCima.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetInstrucoesBackToNormal();
        }

        if (ButtonDefenderDireita.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetInstrucoesBackToNormal();
        }

        if (ButtonDefenderEsquerda.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetInstrucoesBackToNormal();
        }

        if (ButtonIntroducao.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetInstrucoesBackToNormal();
        }

        if (MyButton.InstrucoesBackToNormal())
        {
            mImageInstrucoes.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetInstrucoesBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (instrucoes.isPlaying)
                instrucoes.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!instrucoes.isPlaying)
            instrucoes.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
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

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
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
            mImageInstrucoes.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!instrucoes.isPlaying)
        {
            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 1)
            {
                ButtonHome.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 1)
            {
                ButtonClose.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 0)
            {
                instrucoes.Play();
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 1)
            {
                MyButton.ResetSoundOn();
                instrucoes.Play();
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 0)
            {
                instrucoes.Play();
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

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
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

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTutorial : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource tutorial;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageIntroducao;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool introducaoBackToNormal;
    private static bool jogarBackToNormal, buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        tutorial = GetComponent<AudioSource>();
        mImageIntroducao = GameObject.FindGameObjectWithTag("ButtonTutorial").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonDefenderBaixo.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetTutorialBackToNormal();
        }

        if (ButtonDefenderCima.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetTutorialBackToNormal();
        }

        if (ButtonDefenderEsquerda.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetTutorialBackToNormal();
        }

        if (ButtonDefenderDireita.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetTutorialBackToNormal();
        }

        if (MyButton.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetTutorialBackToNormal();
        }

        if (ButtonIntroducao.TutorialBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetTutorialBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (tutorial.isPlaying)
                tutorial.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!tutorial.isPlaying)
            tutorial.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (PlayerControlSwipe.CancelTutorialAction())
            {
                PlayerControlSwipe.ResetTutorialAction();
            }
            else
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP1);
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

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageIntroducao.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!tutorial.isPlaying)
        {
            if (MyButton.GetSoundOn() == 1)
            {
                MyButton.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 1)
            {
                ButtonInstrucoes.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                tutorial.Play();
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }
}

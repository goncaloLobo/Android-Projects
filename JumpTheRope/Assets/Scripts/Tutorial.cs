using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource tutorial;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal;
    public GameObject GameManagerGO;

    private static bool corda4ToHighlight, jogarToHighlight;

    private float currentTapTime;
    private float lastTapTime;
    private float clickdelay = 0.5f;

    void Start()
    {
        tutorial = GetComponent<AudioSource>();
        checkToStop = false;
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Image>();

        corda4ToHighlight = jogarToHighlight = false;
    }

    void Update()
    {
        if (ButtonCorda1.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetTutorialBackToNormal();
        }

        if (ButtonCorda2.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.ResetTutorialBackToNormal();
        }

        if (ButtonCorda3.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetTutorialBackToNormal();
        }

        if (ButtonCorda4.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetTutorialBackToNormal();
        }

        if (ButtonJogar.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetTutorialBackToNormal();
        }

        if (ButtonInstrucoes.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetTutorialBackToNormal();
        }

        if (ButtonIntroducao.TutorialBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetTutorialBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (tutorial.isPlaying)
                tutorial.Stop();
        }

        //////////////////////////////
        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            corda4ToHighlight = true;
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.GetConfirmedSwipeRight())
        {
            jogarToHighlight = true;
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.GetTutorialToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.ResetTutorialToHighlight();
        }

        if (ButtonCorda4.GetCorda4ToTutorial())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda4.ResetCorda4ToTutorial();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!tutorial.isPlaying)
            tutorial.Play();
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
        if(ButtonCorda1.CheckForHighlighted() == 1)
        {
            corda1BackToNormal = true;
        }

        if (ButtonCorda2.CheckForHighlighted() == 1)
        {
            corda2BackToNormal = true;
        }

        if (ButtonCorda3.CheckForHighlighted() == 1)
        {
            corda3BackToNormal = true;
        }

        if (ButtonCorda4.CheckForHighlighted() == 1)
        {
            corda4BackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!tutorial.isPlaying)
        {
            if(ButtonCorda1.GetSoundOn() == 1)
            {
                ButtonCorda1.SetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if(ButtonCorda1.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 1)
            {
                ButtonCorda2.SetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 1)
            {
                ButtonCorda3.SetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 1)
            {
                ButtonCorda4.SetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 0)
            {
                tutorial.Play();
                soundOn = 1;
            }

            if(ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                tutorial.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
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

    public static void SetSoundOn()
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void ResetInstrucoesBackToNormal()
    {
        instrucoesBackToNormal = false;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static void ResetCorda1BackToNormal()
    {
        corda1BackToNormal = false;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
    }

    public static void ResetCorda2BackToNormal()
    {
        corda2BackToNormal = false;
    }

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static void ResetCorda3BackToNormal()
    {
        corda3BackToNormal = false;
    }

    public static bool Corda4BackToNormal()
    {
        return corda4BackToNormal;
    }

    public static void ResetCorda4BackToNormal()
    {
        corda4BackToNormal = false;
    }

    public static void SetCheckToStop()
    {
        checkToStop = true;
    }

    public void StopSounds()
    {
        if (checkToStop)
        {
            tutorial.Stop();
        }
    }

    ///////////////////////////////////
    public static bool GetJogarToHighlight()
    {
        return jogarToHighlight;
    }

    public static void ResetJogarToHighlight()
    {
        jogarToHighlight = false;
    }

    public static bool GetCorda4ToHighlight()
    {
        return corda4ToHighlight;
    }

    public static void ResetCorda4ToHighlight()
    {
        corda4ToHighlight = false;
    }
}

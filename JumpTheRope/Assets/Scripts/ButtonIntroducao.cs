using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource [] sounds;
    public AudioSource introducao;
    public AudioSource intro;
    private float currentTapTime;
    private float lastTapTime;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImage;
    private static int highlighted;
    private static bool jogarBackToNormal, buttonCorda1BackToNormal, instrucoesBackToNormal, buttonCorda2BackToNormal, buttonCorda3BackToNormal, buttonCorda4BackToNormal, tutorialBackToNormal;
    private static bool jogarToHighlight, instrucoesToHighlight;

    private Touch currentTouch;
    private Touch previousTouch;
    private float screenDPI;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private Vector2 swipeDelta;
    private static bool introducaoIsPlaying;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
        highlighted = 0;
        screenDPI = Screen.dpi;
        jogarToHighlight = instrucoesToHighlight = false;
        introducaoIsPlaying = false;
        jogarBackToNormal = buttonCorda1BackToNormal = instrucoesBackToNormal = buttonCorda2BackToNormal = buttonCorda3BackToNormal = buttonCorda4BackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("IntroductionButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        // BACK TO NORMAL
        if (ButtonJogar.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetIntroducaoBackToNormal();
        }

        if (ButtonInstrucoes.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetIntroducaoBackToNormal();
        }

        if (ButtonCorda1.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetIntroducaoBackToNormal();
        }

        if (ButtonCorda2.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.ResetIntroducaoBackToNormal();
        }

        if (ButtonCorda3.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetIntroducaoBackToNormal();
        }

        if (ButtonCorda4.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetIntroducaoBackToNormal();
        }

        if (Tutorial.IntroducaoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetIntroducaoBackToNormal();
        }

        // TO HIGHLIGHT
        if (ButtonJogar.IntroducaoToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.IntroducaoToHighlightFalse();
        }

        if (ButtonInstrucoes.IntroducaoToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonInstrucoes.ResetIntroducaoToHighlight();
        }

        // TENTATIVA DOUBLE CLICK CHECKER
        if (DoubleClickChecker.SwipeJogarToIntro() == 1)
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            intro.Play();
        }

        if(DoubleClickChecker.SwipeIntroToInstr() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.SwipeIntroToJogar() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.SwipeInstrToIntro() == 1)
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            intro.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //currentTapTime = Time.time;
        if (!intro.isPlaying)
            intro.Play();

        /*
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                if(DoubleClickChecker.GetIntroducaoCancelAction())
                {
                    DoubleClickChecker.ResetIntroducaoCancelAction();
                }
                else
                {
                    if (intro.isPlaying)
                        intro.Stop();
                    introducao.Play();
                }
            }
        }
        lastTapTime = currentTapTime;
        */
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
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonCorda1.CheckForHighlighted() == 1)
        {
            buttonCorda1BackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonCorda2.CheckForHighlighted() == 1)
        {
            buttonCorda2BackToNormal = true;
        }

        if (ButtonCorda3.CheckForHighlighted() == 1)
        {
            buttonCorda3BackToNormal = true;
        }

        if (ButtonCorda4.CheckForHighlighted() == 1)
        {
            buttonCorda4BackToNormal = true;
        }

        if (Tutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!intro.isPlaying)
            intro.Play();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

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

    public static bool JogarToHighlight()
    {
        return jogarToHighlight;
    }
    
    public static void JogarToHighlightFalse()
    {
        jogarToHighlight = false;
    }

    public static bool ButtonCorda1BackToNormal()
    {
        return buttonCorda1BackToNormal;
    }

    public static void ResetButtonCorda1BackToNormal()
    {
        buttonCorda1BackToNormal = false;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void ResetInstrucoesBackToNormal()
    {
        instrucoesBackToNormal = false;
    }

    public static bool InstrucoesToHighlight()
    {
        return instrucoesToHighlight;
    }

    public static void InstrucoesToHighlightFalse()
    {
        instrucoesToHighlight = false;
    }

    public static bool ButtonCorda2BackToNormal()
    {
        return buttonCorda2BackToNormal;
    }

    public static void ResetButtonCorda2BackToNormal()
    {
        buttonCorda2BackToNormal = false;
    }

    public static bool ButtonCorda3BackToNormal()
    {
        return buttonCorda3BackToNormal;
    }

    public static void ResetButtonCorda3BackToNormal()
    {
        buttonCorda3BackToNormal = false;
    }

    public static bool ButtonCorda4BackToNormal()
    {
        return buttonCorda4BackToNormal;
    }

    public static void ResetButtonCorda4BackToNormal()
    {
        buttonCorda4BackToNormal = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }

    // INTRODUCAO IS PLAYING
    public static bool GetIntroducaoIsPlaying()
    {
        return introducaoIsPlaying;
    }

    public static void ResetIntroducaoIsPlaying()
    {
        introducaoIsPlaying = false;
    }
}

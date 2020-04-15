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
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonJogar.IntroducaoBackToNormalFalse();
        }

        if (ButtonInstrucoes.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.IntroducaoBackToNormalFalse();
        }

        if (ButtonCorda1.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // TO HIGHLIGHT
        if (ButtonJogar.IntroducaoToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.IntroducaoToHighlightFalse();
        }

        if (ButtonInstrucoes.IntroducaoToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonInstrucoes.IntroducaoToHighlightFalse();
        }

        // TENTATIVA DOUBLE CLICK CHECKER
        if (DoubleClickChecker.SwipeJogarToIntro() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            intro.Play();
        }

        if(DoubleClickChecker.SwipeIntroToInstr() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.SwipeIntroToJogar() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.SwipeInstrToIntro() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            intro.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!intro.isPlaying)
            intro.Play();
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

    public static void JogarBackToNormalFalse()
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

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static bool InstrucoesToHighlight()
    {
        return instrucoesToHighlight;
    }

    public static void InstrucoesToHighlightFalse()
    {
        instrucoesToHighlight = false;
    }

    public static void SetInstrucoesBackToNormalFalse()
    {
        instrucoesBackToNormal = false;
    }

    public static bool ButtonCorda2BackToNormal()
    {
        return buttonCorda2BackToNormal;
    }

    public static bool ButtonCorda3BackToNormal()
    {
        return buttonCorda3BackToNormal;
    }

    public static bool ButtonCorda4BackToNormal()
    {
        return buttonCorda4BackToNormal;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
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

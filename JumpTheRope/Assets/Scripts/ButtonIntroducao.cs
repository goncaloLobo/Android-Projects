using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
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
    private bool check;
    private static bool jogarBackToNormal, buttonCorda1BackToNormal, instrucoesBackToNormal, buttonCorda2BackToNormal, buttonCorda3BackToNormal, buttonCorda4BackToNormal, tutorialBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
        highlighted = 0;
        check = false;
        jogarBackToNormal = buttonCorda1BackToNormal = instrucoesBackToNormal = buttonCorda2BackToNormal = buttonCorda3BackToNormal = buttonCorda4BackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("IntroductionButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        // BACK TO NORMAL
        if(ButtonJogar.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda1.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonCorda2.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.IntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // DOUBLE CLICK CHECKER
        if (DoubleClickChecker.ButtonIntroducaoBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.ButtonIntroducaoToHighlight() && !check)
        {
            check = true;
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        check = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!intro.isPlaying)
            intro.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                introducao.Play();
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

    public static bool ButtonCorda1BackToNormal()
    {
        return buttonCorda1BackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
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
}

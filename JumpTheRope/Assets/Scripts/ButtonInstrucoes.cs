using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInstrucoes : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource instrucoes;
    private float currentTapTime;
    private float lastTapTime;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;
    private static int highlighted;
    private static bool jogarBackToNormal, introducaoBackToNormal, tutorialBackToNormal;
    private static bool jogarToHighlight, introducaoToHighlight;
    public GameObject GameManagerGO;

    private Touch currentTouch;
    private Touch previousTouch;
    private float screenDPI;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private Vector2 swipeDelta;

    void Start()
    {
        instrucoes = GetComponent<AudioSource>();
        highlighted = 0;
        screenDPI = Screen.dpi;
        jogarToHighlight = introducaoToHighlight = false;
        jogarBackToNormal = introducaoBackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("InstructionsButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetInstrucoesBackToNormal();
        }

        if (ButtonIntroducao.InstrucoesBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetInstrucoesBackToNormal();
        }

        if (Tutorial.InstrucoesBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetInstrucoesBackToNormal();
        }

        // TO HIGHLIGHT
        if (ButtonIntroducao.InstrucoesToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonIntroducao.InstrucoesToHighlightFalse();
        }

        if (ButtonJogar.InstrucoesToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.InstrucoesToHighlightFalse();
        }

        // TENTATIVA DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeInstrucoesToJogar() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.SwipeIntroToInstr() == 1)
        {
            DoubleClickChecker.SwipeIntroToInstrReset();
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            instrucoes.Play();
        }

        if(DoubleClickChecker.SwipeJogarToInstr() == 1)
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            instrucoes.Play();
        }

        if(DoubleClickChecker.SwipeInstrToIntro() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            DoubleClickChecker.SwipeInstrToIntroReset();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!instrucoes.isPlaying)
            instrucoes.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (DoubleClickChecker.GetInstrucoesCancelAction())
            {
                DoubleClickChecker.ResetInstrucoesCancelAction();
            }
            else
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instrucoes);
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

        if(ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
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

        if (!instrucoes.isPlaying)
        {
            instrucoes.Play();
        }            
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool IntroducaoToHighlight()
    {
        return introducaoToHighlight;
    }

    public static void ResetIntroducaoToHighlight()
    {
        introducaoToHighlight = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }
}

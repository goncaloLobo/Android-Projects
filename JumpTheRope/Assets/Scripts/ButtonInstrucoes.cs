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
    private static bool jogarBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;
    private static bool jogarToHighlight, introducaoToHighlight;
    public GameObject GameManagerGO;

    private Touch currentTouch;
    private Touch previousTouch;
    private float screenDPI;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private Vector2 swipeDelta;

    private bool instrucoesToJogar, instrucoesToIntroducao;

    void Start()
    {
        instrucoes = GetComponent<AudioSource>();
        highlighted = 0;
        screenDPI = Screen.dpi;
        jogarToHighlight = introducaoToHighlight = false;
        instrucoesToJogar = instrucoesToIntroducao = false;
        jogarBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("InstructionsButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonJogar.InstrucoesBackToNormalFalse();
        }

        if (ButtonIntroducao.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.SetInstrucoesBackToNormalFalse();
        }

        if (ButtonCorda1.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // TO HIGHLIGHT
        if (ButtonIntroducao.InstrucoesToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonIntroducao.InstrucoesToHighlightFalse();
        }

        if (ButtonJogar.InstrucoesToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.InstrucoesToHighlightFalse();
        }

        // GAMEMANAGER
        if (GameManager.InstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            GameManager.InstrucoesBackToNormalFalse();
        }

        // TENTATIVA DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeInstrucoesToJogar() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.SwipeIntroToInstr() == 1)
        {
            DoubleClickChecker.SwipeIntroToInstrReset();
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(DoubleClickChecker.SwipeJogarToInstr() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(DoubleClickChecker.SwipeInstrToIntro() == 1)
        {
            mImage.sprite = normalSprite;
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
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
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
        instrucoesToIntroducao = true;
        instrucoesToJogar = true;
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void IntroducaoBackToNormalFalse()
    {
        introducaoBackToNormal = false;
    }

    public static bool IntroducaoToHighlight()
    {
        return introducaoToHighlight;
    }

    public static void IntroducaoToHighlightFalse()
    {
        introducaoToHighlight = false;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
    }

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static bool Corda4BackToNormal()
    {
        return corda4BackToNormal;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    
}

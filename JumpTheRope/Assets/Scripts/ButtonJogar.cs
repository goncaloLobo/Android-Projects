using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;
    private static bool introducaoBackToNormal, instrucoesBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;
    private static bool introducaoToHighlight, instrucoesToHighlight;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    public GameObject GameManagerGO;

    private Touch currentTouch;
    private Touch previousTouch;
    private float screenDPI;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private Vector2 swipeDelta;

    private bool jogarToInstrucoes, jogarToIntroducao;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
        screenDPI = Screen.dpi;
        introducaoToHighlight = instrucoesToHighlight = false;
        jogarToInstrucoes = jogarToIntroducao = false;
        introducaoBackToNormal = instrucoesBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("PlayButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        // BACK TO NORMAL
        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.JogarBackToNormalFalse();
        }

        if (ButtonInstrucoes.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.JogarBackToNormalFalse();
        }

        if (ButtonCorda1.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // TO HIGHLIGHT
        if (ButtonIntroducao.JogarToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonIntroducao.JogarToHighlightFalse();
        }

        if (ButtonInstrucoes.JogarToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonInstrucoes.JogarToHighlightFalse();
        }

        // DOUBLE CLICK CHECKER
        if (DoubleClickChecker.ButtonJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            DoubleClickChecker.ButtonJogarBackToNormalFalse();
        }

        // PARTE DOS SWIPES
        if (Input.touchCount > 0 && GameManager.GetOpening())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentTouch = touch;
                currentTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                previousTouch = touch;
                lastTapTime = currentTapTime;
                int deltaX = (int)previousTouch.position.x - (int)currentTouch.position.x;
                int deltaY = (int)previousTouch.position.y - (int)currentTouch.position.y;
                int distance = (deltaX * deltaX) + (deltaY * deltaY);

                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = lastTapTime - currentTapTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        // swipe!!!
                        swipeDelta = new Vector2(deltaX, deltaY);
                        swipeDelta.Normalize();

                        //swipe left
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            if (jogarToIntroducao)
                            {
                                System.Diagnostics.Debug.WriteLine("swipe left jogar -> introducao");
                                introducaoToHighlight = true;
                                mImage.sprite = normalSprite;
                                highlighted = 0;
                                jogarToIntroducao = false;
                            }
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            if (jogarToInstrucoes)
                            {
                                System.Diagnostics.Debug.WriteLine("swipe right jogar -> instrucoes");
                                instrucoesToHighlight = true;
                                mImage.sprite = normalSprite;
                                highlighted = 0;
                                jogarToInstrucoes = false;
                            }                            
                        }
                    }
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PreGameplay);
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
        jogarToIntroducao = true;
        jogarToInstrucoes = true;
        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
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

        if (!jogar.isPlaying)
            jogar.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool IntroducaoToHighlight()
    {
         return introducaoToHighlight;
    }

    public static void IntroducaoToHighlightFalse()
    {
        introducaoToHighlight = false;
    }

    public static void IntroducaoBackToNormalFalse()
    {
        introducaoBackToNormal = false;
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

    public static void InstrucoesBackToNormalFalse()
    {
        instrucoesBackToNormal = false;
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

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

    private bool introducaoToJogar, introducaoToInstrucoes;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
        highlighted = 0;
        screenDPI = Screen.dpi;
        jogarToHighlight = instrucoesToHighlight = false;
        introducaoToJogar = introducaoToInstrucoes = false;
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
                            if (introducaoToInstrucoes)
                            {
                                System.Diagnostics.Debug.WriteLine("swipe left introducao -> instrucoes");
                                instrucoesToHighlight = true;
                                mImage.sprite = normalSprite;
                                highlighted = 0;
                                introducaoToInstrucoes = false;
                            }                            
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            if (introducaoToJogar)
                            {
                                System.Diagnostics.Debug.WriteLine("swipe right introducao -> jogar");
                                jogarToHighlight = true;
                                mImage.sprite = normalSprite;
                                highlighted = 0;
                                introducaoToJogar = false;
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
        introducaoToInstrucoes = true;
        introducaoToJogar = true;
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
}

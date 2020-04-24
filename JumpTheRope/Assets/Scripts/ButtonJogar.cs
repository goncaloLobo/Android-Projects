using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource jogar;
    public AudioSource vaicomeçar321;
    public AudioSource inicioJogo;
    private float currentTapTime;
    private float lastTapTime;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool introducaoBackToNormal, instrucoesBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;
    private static bool introducaoToHighlight, instrucoesToHighlight, hasEntered;

    private static bool tutorialToHighlight, corda1ToHighlight;

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

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        jogar = sounds[0];
        vaicomeçar321 = sounds[1];

        screenDPI = Screen.dpi;
        checkToStop = false;
        introducaoToHighlight = instrucoesToHighlight = hasEntered = false;
        introducaoBackToNormal = instrucoesBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("PlayButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        // BACK TO NORMAL
        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetJogarBackToNormal();
        }

        if (ButtonInstrucoes.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetJogarBackToNormal();
        }

        if (ButtonCorda1.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetJogarBackToNormal();
        }

        if (ButtonCorda2.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.ResetJogarBackToNormal();
        }

        if (ButtonCorda3.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetJogarBackToNormal();
        }

        if (ButtonCorda4.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetJogarBackToNormal();
        }

        if (Tutorial.JogarBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetJogarBackToNormal();
        }

        // TO HIGHLIGHT
        if (ButtonIntroducao.JogarToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonIntroducao.JogarToHighlightFalse();
        }

        if (ButtonInstrucoes.JogarToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonInstrucoes.JogarToHighlightFalse();
        }

        // DOUBLE CLICK CHECKER
        if (DoubleClickChecker.SwipeJogarToIntro() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            DoubleClickChecker.SwipeJogarToIntroReset();
        }

        if(DoubleClickChecker.SwipeInstrucoesToJogar() == 1)
        {
            jogar.Play();
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            DoubleClickChecker.SwipeInstrucoesToJogarReset();
        }

        if(DoubleClickChecker.SwipeJogarToInstr() == 1)
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            DoubleClickChecker.SwipeJogarToInstrReset();
        }

        if(DoubleClickChecker.SwipeIntroToJogar() == 1)
        {
            jogar.Play();
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            DoubleClickChecker.SwipeIntroToJogarReset();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (jogar.isPlaying)
                jogar.Stop();
        }

        // GET CONFIRMED SWIPE RIGHT / LEFT
        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            corda1ToHighlight = true;
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            tutorialToHighlight = true;
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda1.GetJogarToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda1.ResetJogarToHighlight();
        }

        if (Tutorial.GetJogarToHighlight())
        {
            mImage.overrideSprite = spriteHighlighted;
            highlighted = 1;
            Tutorial.ResetJogarToHighlight();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (DoubleClickChecker.GetJogarCancelAction())
            {
                DoubleClickChecker.ResetJogarCancelAction();
            }
            else if (!hasEntered)
            {
                hasEntered = true;
                vaicomeçar321.Play();
                Invoke("StartGame", vaicomeçar321.clip.length);
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
        {
            if (ButtonCorda1.GetSoundOn() == 1)
            {
                ButtonCorda1.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda1.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 1)
            {
                ButtonCorda2.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 1)
            {
                ButtonCorda3.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 1)
            {
                ButtonCorda4.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 1)
            {
                Tutorial.SetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    // GameManager chama esta funcao para certificar que apenas entra 1x no estado Gameplay
    public static void ResetHasEntered()
    {
        hasEntered = false;
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

    public static void IntroducaoToHighlightFalse()
    {
        introducaoToHighlight = false;
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

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }

    public static void SetCheckToStop()
    {
        checkToStop = true;
    }

    public void StopSounds()
    {
        if (checkToStop)
        {
            jogar.Stop();
        }
    }

    ////////////////////////////////
    public static bool GetTutorialToHighlight()
    {
        return tutorialToHighlight;
    }

    public static void ResetTutorialToHighlight()
    {
        tutorialToHighlight = false;
    }

    public static bool GetCorda1ToHighlight()
    {
        return corda1ToHighlight;
    }

    public static void ResetCorda1ToHighlight()
    {
        corda1ToHighlight = false;
    }
}

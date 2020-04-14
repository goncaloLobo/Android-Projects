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
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal;
    public GameObject GameManagerGO;

    private float currentTapTime;
    private float lastTapTime;
    private float clickdelay = 0.5f;

    void Start()
    {
        tutorial = GetComponent<AudioSource>();

        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonCorda1.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonInstrucoes.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // GAMEMANAGER
        if (GameManager.TutorialBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            GameManager.TutorialBackToNormalFalse();
        }

        // DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeCorda4ToTutorial() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            DoubleClickChecker.SwipeCorda4ToTutorialReset();
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
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Instrucoes)
            {
                if (DoubleClickChecker.GetCancelTutorialAction())
                {
                    DoubleClickChecker.ResetCancelTutorialAction();
                }
                else
                {
                    GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP1);
                }
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

        //if (!tutorial.isPlaying)
        //    tutorial.Play();

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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
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
}

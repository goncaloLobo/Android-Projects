using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTutorial : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        mImage = GameObject.FindGameObjectWithTag("ButtonTutorial").GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!tutorial.isPlaying)
            tutorial.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                if (PlayerControlSwipe.GetTutorialCancelAction())
                {
                    PlayerControlSwipe.ResetTutorialCancelAction();
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
            if (ButtonJogar.GetSoundOn() == 1)
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

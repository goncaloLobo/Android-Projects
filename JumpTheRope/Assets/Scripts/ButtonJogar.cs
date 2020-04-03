using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;
    private static int order = 2;
    private static bool introducaoBackToNormal, check, instrucoesBackToNormal;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;

    public GameObject GameManagerGO;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
        introducaoBackToNormal = instrucoesBackToNormal = check = false;
        mImage = GameObject.FindGameObjectWithTag("PlayButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonIntroducao.JogarBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.JogarBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        check = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            System.Diagnostics.Debug.WriteLine("doubletap no jogar, yeet");
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
        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
            IntroducaoBackToNormal();
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
            InstrucoesBackToNormal();
        }

        if(highlighted == 0)
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

    public static int GetOrder()
    {
        return order;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal == true;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal == true;
    }
}

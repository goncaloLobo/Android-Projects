using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonComoJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource comoJogar;
    public GameObject GameManagerGO;
    private float currentTapTime;
    private float lastTapTime;

    private static bool jogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal;
    private static int highlighted;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        comoJogar = GetComponent<AudioSource>();

        highlighted = 0;
        jogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonComoJogar").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.ComoJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.ComoJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.ComoJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.ComoJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontos.ComoJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // PLAYER CONTROL SWIPE
        if (PlayerControlSwipe.ButtonInstrucoesBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!comoJogar.isPlaying)
            comoJogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
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

        if (ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!comoJogar.isPlaying)
            comoJogar.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }
}

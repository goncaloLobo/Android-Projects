using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal;
    private static int highlighted;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        jogar = GetComponent<AudioSource>();

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ButtonPlay").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontos.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // PLAYER CONTROL SWIPE
        if (PlayerControlSwipe.ButtonJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
            }
            else if(GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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

    public void OnPointerExit(PointerEventData eventData)
    {
        jogar.Stop();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
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

        if (!jogar.isPlaying)
            jogar.Play();
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
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

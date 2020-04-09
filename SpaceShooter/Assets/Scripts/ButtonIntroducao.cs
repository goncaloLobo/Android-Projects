using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource introducao; // sounds[0]
    public AudioSource intro; // sounds[1]
    private float currentTapTime;
    private float lastTapTime;

    private static bool jogarBackToNormal, comoJogarBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal;
    private static int highlighted;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];

        highlighted = 0;
        comoJogarBackToNormal = jogarBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("IntroducaoButton").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonComoJogar.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontos.IntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // PLAYER CONTROL SWIPE
        if (PlayerControlSwipe.ButtonIntroducaoBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!intro.isPlaying)
            intro.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if(!introducao.isPlaying)
                introducao.Play();
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

        if (ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
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

        if (!intro.isPlaying)
            intro.Play();
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
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

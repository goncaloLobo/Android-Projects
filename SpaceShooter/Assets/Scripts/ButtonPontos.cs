using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPontos : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource pontos;
    private float currentTapTime;
    private float lastTapTime;
    
    private static bool comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, jogarBackToNormal, vidasBackToNormal;
    private bool check;
    private static int highlighted;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        pontos = GetComponent<AudioSource>();

        highlighted = 0;
        check = false;
        comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = jogarBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ScoreTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.PontosBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.PontosBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonTempo.PontosBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonVidas.PontosBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.PontosBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        check = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!pontos.isPlaying)
            pontos.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pontos.Stop();
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

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!pontos.isPlaying)
            pontos.Play();
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

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }
}

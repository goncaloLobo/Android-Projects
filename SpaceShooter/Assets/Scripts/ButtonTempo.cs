using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTempo : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource tempo;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, jogarBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool tutorialBackToNormal, instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        tempo = GetComponent<AudioSource>();

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = jogarBackToNormal = pontosBackToNormal = vidasBackToNormal = pontuacaoBackToNormal = false;
        tutorialBackToNormal = instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("TimeTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonComoJogar.ResetTempoBackToNormal();
        }

        if (ButtonJogar.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetTempoBackToNormal();
        }

        if (ButtonIntroducao.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetTempoBackToNormal();
        }

        if (ButtonVidas.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetTempoBackToNormal();
        }

        if (ButtonPontos.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetTempoBackToNormal();
        }

        if (ButtonPontuacao.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetTempoBackToNormal();
        }

        if (ButtonTutorial.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetTempoBackToNormal();
        }

        if (InstrucoesB1.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetTempoBackToNormal();
        }

        if (InstrucoesB2.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB2.ResetTempoBackToNormal();
        }

        if (InstrucoesB3.TempoBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB3.ResetTempoBackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!tempo.isPlaying)
            tempo.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

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

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if(ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if(InstrucoesB1.CheckForHighlighted() == 1)
        {
            instrucoesB1BackToNormal = true;
        }

        if (InstrucoesB2.CheckForHighlighted() == 1)
        {
            instrucoesB2BackToNormal = true;
        }

        if (InstrucoesB3.CheckForHighlighted() == 1)
        {
            instrucoesB3BackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!tempo.isPlaying)
            tempo.Play();
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

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static void ResetComoJogarBackToNormal()
    {
        comoJogarBackToNormal = false;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static void ResetVidasBackToNormal()
    {
        vidasBackToNormal = false;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static void ResetPontosBackToNormal()
    {
        pontosBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool PontuacaoBackToNormal()
    {
        return pontuacaoBackToNormal;
    }

    public static void ResetPontuacaoBackToNormal()
    {
        pontuacaoBackToNormal = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static void ResetInstrucoesB1BackToNormal()
    {
        instrucoesB1BackToNormal = false;
    }

    public static bool InstrucoesB2BackToNormal()
    {
        return instrucoesB2BackToNormal;
    }

    public static void ResetInstrucoesB2BackToNormal()
    {
        instrucoesB2BackToNormal = false;
    }

    public static bool InstrucoesB3BackToNormal()
    {
        return instrucoesB3BackToNormal;
    }

    public static void ResetInstrucoesB3BackToNormal()
    {
        instrucoesB3BackToNormal = false;
    }
}

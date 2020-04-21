using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPontos : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource pontos;
    private float currentTapTime;
    private float lastTapTime;
    
    private static bool comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, jogarBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool tutorialBackToNormal, instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        pontos = GetComponent<AudioSource>();

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = jogarBackToNormal = vidasBackToNormal = pontuacaoBackToNormal = false;
        tutorialBackToNormal = instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("ScoreTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonComoJogar.ResetPontosBackToNormal();
        }

        if (ButtonIntroducao.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetPontosBackToNormal();
        }

        if (ButtonTempo.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetPontosBackToNormal();
        }

        if (ButtonVidas.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetPontosBackToNormal();
        }

        if (ButtonJogar.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetPontosBackToNormal();
        }

        if (ButtonPontuacao.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetPontosBackToNormal();
        }

        if (ButtonTutorial.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetPontosBackToNormal();
        }

        if (InstrucoesB1.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetPontosBackToNormal();
        }

        if (InstrucoesB2.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB2.ResetPontosBackToNormal();
        }

        if (InstrucoesB3.PontosBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB3.ResetPontosBackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!pontos.isPlaying)
            pontos.Play();
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

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if(ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (InstrucoesB1.CheckForHighlighted() == 1)
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

        if (!pontos.isPlaying)
        {
            if (InstrucoesB1.GetSoundOn() == 1)
            {
                InstrucoesB1.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (InstrucoesB1.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 1)
            {
                InstrucoesB2.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 1)
            {
                InstrucoesB3.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonTutorial.SetSoundOn();
                pontos.Play();
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                pontos.Play();
                soundOn = 1;
            }
        }
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static void ResetTempoBackToNormal()
    {
        tempoBackToNormal = false;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static void ResetVidasBackToNormal()
    {
        vidasBackToNormal = false;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
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

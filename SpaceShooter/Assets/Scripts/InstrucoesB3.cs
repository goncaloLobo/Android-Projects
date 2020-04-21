using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstrucoesB3 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource[] sounds;
    public AudioSource asteroide; // sounds[0]
    public AudioSource asteroideDescricao; // sounds[1]

    private static bool jogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool instrucoesB1BackToNormal, instrucoesB2BackToNormal, tutorialBackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        asteroide = sounds[0];
        asteroideDescricao = sounds[1];
        jogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = pontuacaoBackToNormal = tutorialBackToNormal = false;
        highlighted = 0;
        mImage = GameObject.FindGameObjectWithTag("InstrucoesB3").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonIntroducao.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetInstrucoesB3BackToNormal();
        }

        if (InstrucoesB1.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetInstrucoesB3BackToNormal();
        }

        if (InstrucoesB2.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB2.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonPontuacao.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonTutorial.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonPontos.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonVidas.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonTempo.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetInstrucoesB3BackToNormal();
        }

        if (ButtonPontuacao.InstrucoesB3BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetInstrucoesB3BackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!asteroide.isPlaying)
        {
            asteroide.Play();
            asteroideDescricao.PlayDelayed(asteroide.clip.length);
        }
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

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if (InstrucoesB2.CheckForHighlighted() == 1)
        {
            instrucoesB2BackToNormal = true;
        }

        if (InstrucoesB1.CheckForHighlighted() == 1)
        {
            instrucoesB1BackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if(ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!asteroide.isPlaying)
        {
            if (InstrucoesB1.GetSoundOn() == 1)
            {
                InstrucoesB1.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB1.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 1)
            {
                InstrucoesB2.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }
        }
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
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

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static void ResetPontosBackToNormal()
    {
        pontosBackToNormal = false;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static void ResetVidasBackToNormal()
    {
        vidasBackToNormal = false;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static void ResetTempoBackToNormal()
    {
        tempoBackToNormal = false;
    }

    public static bool InstrucoesB2BackToNormal()
    {
        return instrucoesB2BackToNormal;
    }

    public static void ResetInstrucoesB2BackToNormal()
    {
        instrucoesB2BackToNormal = false;
    }

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static void ResetInstrucoesB1BackToNormal()
    {
        instrucoesB1BackToNormal = false;
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
}

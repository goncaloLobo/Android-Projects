using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstrucoesB2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource[] sounds;
    public AudioSource naveJogador; // sounds[0]
    public AudioSource naveJogadorDescricao; // sounds[1]

    private static bool jogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool instrucoesB1BackToNormal, instrucoesB3BackToNormal, tutorialBackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        naveJogador = sounds[0];
        naveJogadorDescricao = sounds[1];
        pontosBackToNormal = jogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontuacaoBackToNormal = vidasBackToNormal = tutorialBackToNormal = false;
        highlighted = 0;
        mImage = GameObject.FindGameObjectWithTag("InstrucoesB2").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonIntroducao.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetInstrucoesB2BackToNormal();
        }

        if (InstrucoesB1.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetInstrucoesB2BackToNormal();
        }

        if (InstrucoesB3.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB3.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonPontuacao.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonTempo.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonPontos.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonVidas.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonVidas.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonPontuacao.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetInstrucoesB2BackToNormal();
        }

        if (ButtonTutorial.InstrucoesB2BackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetInstrucoesB2BackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (naveJogador.isPlaying)
                naveJogador.Stop();
            if (naveJogadorDescricao.isPlaying)
                naveJogadorDescricao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!naveJogador.isPlaying)
        {
            naveJogador.Play();
            naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
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

        if (InstrucoesB1.CheckForHighlighted() == 1)
        {
            instrucoesB1BackToNormal = true;
        }

        if (InstrucoesB3.CheckForHighlighted() == 1)
        {
            instrucoesB3BackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if (ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!naveJogador.isPlaying)
        {
            if (InstrucoesB1.GetSoundOn() == 1)
            {
                InstrucoesB1.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB1.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 1)
            {
                InstrucoesB3.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                naveJogador.Play();
                naveJogadorDescricao.PlayDelayed(naveJogador.clip.length);
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

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static void ResetInstrucoesB1BackToNormal()
    {
        instrucoesB1BackToNormal = false;
    }

    public static bool InstrucoesB3BackToNormal()
    {
        return instrucoesB3BackToNormal;
    }

    public static void ResetInstrucoesB3BackToNormal()
    {
        instrucoesB3BackToNormal = false;
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

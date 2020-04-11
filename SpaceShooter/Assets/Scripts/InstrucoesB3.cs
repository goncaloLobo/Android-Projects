using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstrucoesB3 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource[] sounds;
    public AudioSource asteroide; // sounds[0]
    public AudioSource asteroideDescricao; // sounds[1]

    private static bool jogarBackToNormal, comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal;
    private static bool instrucoesB1BackToNormal, instrucoesB2BackToNormal;
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

        highlighted = 0;
        mImage = GameObject.FindGameObjectWithTag("InstrucoesB3").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesB3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonComoJogar.InstrucoesB3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.InstrucoesB3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB1.InstrucoesB3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB2.InstrucoesB3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
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

        if (ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
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

            if (ButtonComoJogar.GetSoundOn() == 1)
            {
                ButtonComoJogar.SetSoundOn();
                asteroide.Play();
                asteroideDescricao.PlayDelayed(asteroide.clip.length);
                soundOn = 1;
            }

            if (ButtonComoJogar.GetSoundOn() == 0)
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

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static bool VidasBackToNormal()
    {
        return vidasBackToNormal;
    }

    public static bool TempoBackToNormal()
    {
        return tempoBackToNormal;
    }

    public static bool InstrucoesB2BackToNormal()
    {
        return instrucoesB2BackToNormal;
    }

    public static bool InstrucoesB1BackToNormal()
    {
        return instrucoesB1BackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstrucoesB1 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource[] sounds;
    public AudioSource inimigo; // sounds[0]
    public AudioSource inimigoDescricao; // sounds[1]

    private static bool jogarBackToNormal, comoJogarBackToNormal, introducaoBackToNormal, tempoBackToNormal, pontosBackToNormal, vidasBackToNormal, pontuacaoBackToNormal;
    private static bool instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        inimigo = sounds[0];
        inimigoDescricao = sounds[1];

        highlighted = 0;
        pontuacaoBackToNormal = jogarBackToNormal = comoJogarBackToNormal = introducaoBackToNormal = tempoBackToNormal = pontosBackToNormal = vidasBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("InstrucoesB1").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonJogar.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonComoJogar.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB3.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (InstrucoesB2.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonPontuacao.InstrucoesB1BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!inimigo.isPlaying)
        {
            inimigo.Play();
            inimigoDescricao.PlayDelayed(inimigo.clip.length);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonComoJogar.CheckForHighlighted() == 1)
        {
            comoJogarBackToNormal = true;
        }

        if(ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if(ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if(ButtonVidas.CheckForHighlighted() == 1)
        {
            vidasBackToNormal = true;
        }

        if(InstrucoesB2.CheckForHighlighted() == 1)
        {
            instrucoesB2BackToNormal = true;
        }

        if(InstrucoesB3.CheckForHighlighted() == 1)
        {
            instrucoesB3BackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!inimigo.isPlaying)
        {
            if (InstrucoesB2.GetSoundOn() == 1)
            {
                InstrucoesB2.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB2.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 1)
            {
                InstrucoesB3.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (InstrucoesB3.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonComoJogar.GetSoundOn() == 1)
            {
                ButtonComoJogar.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonComoJogar.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 1)
            {
                ButtonTempo.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonTempo.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 1)
            {
                ButtonPontos.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonPontos.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 1)
            {
                ButtonVidas.SetSoundOn();
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
                soundOn = 1;
            }

            if (ButtonVidas.GetSoundOn() == 0)
            {
                inimigo.Play();
                inimigoDescricao.PlayDelayed(inimigo.clip.length);
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

    public static bool InstrucoesB3BackToNormal()
    {
        return instrucoesB3BackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool PontuacaoBackToNormal()
    {
        return pontuacaoBackToNormal;
    }
}

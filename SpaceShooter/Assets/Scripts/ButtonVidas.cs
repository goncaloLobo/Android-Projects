using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonVidas : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource [] sounds;
    public AudioSource vidas; //sounds[0]
    public AudioSource vidas1; //sounds[1]
    public AudioSource vidas2; //sounds[2]
    public AudioSource vidas3; //sounds[3]

    public GameObject GameManagerGO;
    private static bool comoJogarBackToNormal, introducaoBackToNormal, jogarBackToNormal, pontosBackToNormal, tempoBackToNormal, pontuacaoBackToNormal;
    private static bool tutorialBackToNormal, instrucoesB1BackToNormal, instrucoesB2BackToNormal, instrucoesB3BackToNormal;
    private static int highlighted;
    private static int soundOn = 0;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        vidas = sounds[0];
        vidas1 = sounds[1];
        vidas2 = sounds[2];
        vidas3 = sounds[3];

        highlighted = 0;
        comoJogarBackToNormal = introducaoBackToNormal = jogarBackToNormal = pontosBackToNormal = tempoBackToNormal = pontuacaoBackToNormal = false;
        tutorialBackToNormal = instrucoesB1BackToNormal = instrucoesB2BackToNormal = instrucoesB3BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("LivesTag").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonComoJogar.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonComoJogar.ResetVidasBackToNormal();
        }

        if (ButtonIntroducao.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetVidasBackToNormal();
        }

        if (ButtonJogar.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetVidasBackToNormal();
        }

        if (ButtonTempo.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTempo.ResetVidasBackToNormal();
        }

        if (ButtonPontuacao.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontuacao.ResetVidasBackToNormal();
        }

        if (ButtonTutorial.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetVidasBackToNormal();
        }

        if (InstrucoesB1.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB1.ResetVidasBackToNormal();
        }

        if (InstrucoesB2.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB2.ResetVidasBackToNormal();
        }

        if (InstrucoesB3.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            InstrucoesB3.ResetVidasBackToNormal();
        }

        if (ButtonPontos.VidasBackToNormal())
        {
            mImage.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonPontos.ResetVidasBackToNormal();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
        {
            if (!vidas.isPlaying)
                vidas.Play();
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
        {
            switch (PlayerControlSwipe.GetCurrentNumberOfLives())
            {
                case 3:
                    if (!vidas3.isPlaying)
                        vidas3.Play();
                    break;
                case 2:
                    if (!vidas2.isPlaying)
                        vidas2.Play();
                    break;
                case 1:
                    if (!vidas1.isPlaying)
                        vidas1.Play();
                    break;
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

        if(ButtonTempo.CheckForHighlighted() == 1)
        {
            tempoBackToNormal = true;
        }

        if (ButtonPontuacao.CheckForHighlighted() == 1)
        {
            pontuacaoBackToNormal = true;
        }

        if (ButtonPontos.CheckForHighlighted() == 1)
        {
            pontosBackToNormal = true;
        }

        if (ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if(InstrucoesB1.CheckForHighlighted() == 1)
        {
            instrucoesB1BackToNormal = true;
        }

        if(InstrucoesB2.CheckForHighlighted() == 1)
        {
            instrucoesB2BackToNormal = true;
        }

        if(InstrucoesB3.CheckForHighlighted() == 1)
        {
            instrucoesB3BackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
        {
            if (!vidas.isPlaying)
                vidas.Play();
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
        {
            if (!vidas.isPlaying)
            {
                if (InstrucoesB1.GetSoundOn() == 1)
                {
                    InstrucoesB1.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (InstrucoesB1.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (InstrucoesB2.GetSoundOn() == 1)
                {
                    InstrucoesB2.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (InstrucoesB2.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (InstrucoesB3.GetSoundOn() == 1)
                {
                    InstrucoesB3.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (InstrucoesB3.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonJogar.GetSoundOn() == 1)
                {
                    ButtonJogar.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonJogar.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonTempo.GetSoundOn() == 1)
                {
                    ButtonTempo.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonTempo.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonPontos.GetSoundOn() == 1)
                {
                    ButtonPontos.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonPontos.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonTutorial.GetSoundOn() == 1)
                {
                    ButtonTutorial.SetSoundOn();
                    vidas.Play();
                    soundOn = 1;
                }

                if (ButtonTutorial.GetSoundOn() == 0)
                {
                    vidas.Play();
                    soundOn = 1;
                }
            }
        }

        if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
        {
            switch (PlayerControlSwipe.GetCurrentNumberOfLives())
            {
                case 3:
                    if (!vidas3.isPlaying)
                        vidas3.Play();
                    break;
                case 2:
                    if (!vidas2.isPlaying)
                        vidas2.Play();
                    break;
                case 1:
                    if (!vidas1.isPlaying)
                        vidas1.Play();
                    break;
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

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool ComoJogarBackToNormal()
    {
        return comoJogarBackToNormal;
    }

    public static void ResetComoJogarBackToNormal()
    {
        comoJogarBackToNormal = false;
    }

    public static bool PontosBackToNormal()
    {
        return pontosBackToNormal;
    }

    public static void ResetPontosBackToNormal()
    {
        pontosBackToNormal = false;
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

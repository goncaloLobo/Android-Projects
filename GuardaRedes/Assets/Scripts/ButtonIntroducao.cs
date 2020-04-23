using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource introducao;
    public AudioSource intro;
    private float currentTapTime;
    private float lastTapTime;
    
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageIntroducao;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, homeBackToNormal, closeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
        mImageIntroducao = GameObject.FindGameObjectWithTag("ButtonIntroducao").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = homeBackToNormal = closeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetIntroducaoBackToNormal();
        }

        if (ButtonHome.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonHome.ResetIntroducaoBackToNormal();
        }

        if (ButtonDefenderBaixo.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetIntroducaoBackToNormal();
        }

        if (ButtonDefenderCima.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetIntroducaoBackToNormal();
        }

        if (ButtonDefenderEsquerda.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetIntroducaoBackToNormal();
        }

        if (ButtonDefenderDireita.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetIntroducaoBackToNormal();
        }

        if (MyButton.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetIntroducaoBackToNormal();
        }

        if (ButtonClose.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetIntroducaoBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (intro.isPlaying)
                intro.Stop();
            if (introducao.isPlaying)
                introducao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!intro.isPlaying)
            intro.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
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
        if (ButtonDefenderBaixo.CheckForHighlighted() == 1)
        {
            buttonDefenderBaixoBackToNormal = true;
        }

        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (ButtonClose.CheckForHighlighted() == 1)
        {
            closeBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageIntroducao.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!intro.isPlaying)
        {
            if (ButtonClose.GetSoundOn() == 1)
            {
                ButtonClose.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 1)
            {
                MyButton.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (MyButton.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 1)
            {
                ButtonHome.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 1)
            {
                ButtonInstrucoes.ResetSoundOn();
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 0)
            {
                intro.Play();
                introducao.PlayDelayed(intro.clip.length);
                soundOn = 1;
            }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    public static int GetSoundOn()
    {
        return soundOn;
    }

    public static void ResetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void ResetInstrucoesBackToNormal()
    {
        instrucoesBackToNormal = false;
    }

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
    }

    public static bool DefenderBaixoBackToNormal()
    {
        return buttonDefenderBaixoBackToNormal;
    }

    public static void ResetDefenderBaixoBackToNormal()
    {
        buttonDefenderBaixoBackToNormal = false;
    }

    public static bool DefenderCimaBackToNormal()
    {
        return buttonDefenderCimaBackToNormal;
    }

    public static void ResetDefenderCimaBackToNormal()
    {
        buttonDefenderCimaBackToNormal = false;
    }

    public static bool DefenderDireitaBackToNormal()
    {
        return buttonDefenderDireitaBackToNormal;
    }

    public static void ResetDefenderDireitaBackToNormal()
    {
        buttonDefenderDireitaBackToNormal = false;
    }

    public static bool DefenderEsquerdaBackToNormal()
    {
        return buttonDefenderEsquerdaBackToNormal;
    }

    public static void ResetDefenderEsquerdaBackToNormal()
    {
        buttonDefenderEsquerdaBackToNormal = false;
    }

    public static bool CloseBackToNormal()
    {
        return closeBackToNormal;
    }

    public static void ResetCloseBackToNormal()
    {
        closeBackToNormal = false;
    }
}

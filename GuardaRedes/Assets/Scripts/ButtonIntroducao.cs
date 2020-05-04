using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonIntroducao : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource introducao;
    public AudioSource intro;
    public AudioSource menuInstrucoes;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageIntroducao;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, tutorialBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        intro = sounds[1];
        menuInstrucoes = sounds[2];

        mImageIntroducao = GameObject.FindGameObjectWithTag("ButtonIntroducao").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = tutorialBackToNormal = false;
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

        if (ButtonTutorial.IntroducaoBackToNormal())
        {
            mImageIntroducao.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetIntroducaoBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (intro.isPlaying)
                intro.Stop();
            if (introducao.isPlaying)
                introducao.Stop();
            if (menuInstrucoes.isPlaying)
                menuInstrucoes.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (GameManager.GetOpening())
        {
            if (!intro.isPlaying)
                intro.Play();

            if (CheckForDoubleTap(currentTapTime, lastTapTime))
            {
                introducao.Play();
            }
        }

        if (GameManager.GetInstructions())
        {
            if (!menuInstrucoes.isPlaying)
                menuInstrucoes.Play();

            if (CheckForDoubleTap(currentTapTime, lastTapTime))
            {
                menuInstrucoes.Play();
            }
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

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageIntroducao.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (GameManager.GetOpening())
        {
            if (!intro.isPlaying)
            {
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

                if (ButtonTutorial.GetSoundOn() == 1)
                {
                    ButtonTutorial.ResetSoundOn();
                    intro.Play();
                    introducao.PlayDelayed(intro.clip.length);
                    soundOn = 1;
                }

                if (ButtonTutorial.GetSoundOn() == 0)
                {
                    intro.Play();
                    introducao.PlayDelayed(intro.clip.length);
                    soundOn = 1;
                }
            }
        }

        if (GameManager.GetInstructions())
        {
            if (!menuInstrucoes.isPlaying)
            {
                if (MyButton.GetSoundOn() == 1)
                {
                    MyButton.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (MyButton.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderCima.GetSoundOn() == 1)
                {
                    ButtonDefenderCima.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderCima.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderBaixo.GetSoundOn() == 1)
                {
                    ButtonDefenderBaixo.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderBaixo.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderEsquerda.GetSoundOn() == 1)
                {
                    ButtonDefenderEsquerda.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderEsquerda.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderDireita.GetSoundOn() == 1)
                {
                    ButtonDefenderDireita.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonDefenderDireita.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonInstrucoes.GetSoundOn() == 1)
                {
                    ButtonInstrucoes.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonInstrucoes.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonTutorial.GetSoundOn() == 1)
                {
                    ButtonTutorial.ResetSoundOn();
                    menuInstrucoes.Play();
                    soundOn = 1;
                }

                if (ButtonTutorial.GetSoundOn() == 0)
                {
                    menuInstrucoes.Play();
                    soundOn = 1;
                }
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

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }
}

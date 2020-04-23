using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDefenderBaixo : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource defenderParaBaixo;
    public AudioSource defenderDescricao;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageDefenderBaixo;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, closeBackToNormal, homeBackToNormal;
    private static bool buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        defenderParaBaixo = sounds[0];
        defenderDescricao = sounds[1];

        mImageDefenderBaixo = GameObject.FindGameObjectWithTag("DefenderBaixo").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = closeBackToNormal = homeBackToNormal = false;
        buttonDefenderEsquerdaBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetDefenderBaixoBackToNormal();
        }

        if (ButtonClose.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetDefenderBaixoBackToNormal();
        }

        if (MyButton.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetDefenderBaixoBackToNormal();
        }

        if (ButtonHome.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonHome.ResetDefenderBaixoBackToNormal();
        }

        if (ButtonDefenderCima.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetDefenderBaixoBackToNormal();
        }

        if (ButtonDefenderEsquerda.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetDefenderBaixoBackToNormal();
        }

        if (ButtonDefenderDireita.DefenderBaixoBackToNormal())
        {
            mImageDefenderBaixo.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetDefenderBaixoBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (defenderParaBaixo.isPlaying)
                defenderParaBaixo.Stop();
            if (defenderDescricao.isPlaying)
                defenderDescricao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!defenderParaBaixo.isPlaying)
            defenderParaBaixo.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeDown);
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
        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonClose.CheckForHighlighted() == 1)
        {
            closeBackToNormal = true;
        }

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageDefenderBaixo.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!defenderParaBaixo.isPlaying)
        {
            if(ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 1)
            {
                ButtonClose.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 1)
            {
                ButtonHome.ResetSoundOn();
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 0)
            {
                defenderParaBaixo.Play();
                defenderDescricao.PlayDelayed(defenderParaBaixo.clip.length);
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

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
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
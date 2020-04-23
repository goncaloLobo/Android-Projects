using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDefenderEsquerda : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource defenderParaEsquerda;
    public AudioSource defenderDescricao;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageDefenderEsquerda;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, closeBackToNormal, homeBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        defenderParaEsquerda = sounds[0];
        defenderDescricao = sounds[1];

        mImageDefenderEsquerda = GameObject.FindGameObjectWithTag("DefenderEsquerda").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = closeBackToNormal = homeBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetDefenderEsquerdaBackToNormal();
        }

        if (ButtonIntroducao.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetDefenderEsquerdaBackToNormal();
        }

        if (ButtonClose.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonClose.ResetDefenderEsquerdaBackToNormal();
        }

        if (MyButton.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetDefenderEsquerdaBackToNormal();
        }

        if (ButtonDefenderDireita.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetDefenderEsquerdaBackToNormal();
        }

        if (ButtonDefenderCima.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetDefenderEsquerdaBackToNormal();
        }

        if (ButtonDefenderBaixo.DefenderEsquerdaBackToNormal())
        {
            mImageDefenderEsquerda.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetDefenderEsquerdaBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (defenderParaEsquerda.isPlaying)
                defenderParaEsquerda.Stop();
            if (defenderDescricao.isPlaying)
                defenderDescricao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!defenderParaEsquerda.isPlaying)
            defenderParaEsquerda.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeLeft);
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

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (ButtonClose.CheckForHighlighted() == 1)
        {
            closeBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonHome.CheckForHighlighted() == 1)
        {
            homeBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageDefenderEsquerda.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!defenderParaEsquerda.isPlaying)
        {
            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 1)
            {
                ButtonHome.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonHome.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 1)
            {
                ButtonClose.ResetSoundOn();
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
                soundOn = 1;
            }

            if (ButtonClose.GetSoundOn() == 0)
            {
                defenderParaEsquerda.Play();
                defenderDescricao.PlayDelayed(defenderParaEsquerda.clip.length);
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

    public static bool HomeBackToNormal()
    {
        return homeBackToNormal;
    }

    public static void ResetHomeBackToNormal()
    {
        homeBackToNormal = false;
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
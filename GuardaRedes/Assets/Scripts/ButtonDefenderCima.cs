using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDefenderCima : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource defenderParaCima;
    public AudioSource defenderDescricao;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageDefenderCima;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, tutorialBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        defenderParaCima = sounds[0];
        defenderDescricao = sounds[1];

        mImageDefenderCima = GameObject.FindGameObjectWithTag("DefenderCima").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = tutorialBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetDefenderCimaBackToNormal();
        }

        if (MyButton.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetDefenderCimaBackToNormal();
        }

        if (ButtonDefenderBaixo.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetDefenderCimaBackToNormal();
        }

        if (ButtonDefenderEsquerda.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetDefenderCimaBackToNormal();
        }

        if (ButtonDefenderDireita.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetDefenderCimaBackToNormal();
        }

        if (ButtonIntroducao.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetDefenderCimaBackToNormal();
        }

        if (ButtonTutorial.DefenderCimaBackToNormal())
        {
            mImageDefenderCima.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetDefenderCimaBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (defenderParaCima.isPlaying)
                defenderParaCima.Stop();
            if (defenderDescricao.isPlaying)
                defenderDescricao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!defenderParaCima.isPlaying)
            defenderParaCima.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeUp);
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

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (MyButton.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageDefenderCima.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!defenderParaCima.isPlaying)
        {
            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonTutorial.ResetSoundOn();
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                defenderParaCima.Play();
                defenderDescricao.PlayDelayed(defenderParaCima.clip.length);
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

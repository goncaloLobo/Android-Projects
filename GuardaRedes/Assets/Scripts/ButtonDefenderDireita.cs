using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDefenderDireita : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource[] sounds;
    public AudioSource defenderParaDireita;
    public AudioSource defenderDescricao;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageDefenderDireita;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, tutorialBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        defenderParaDireita = sounds[0];
        defenderDescricao = sounds[1];

        mImageDefenderDireita = GameObject.FindGameObjectWithTag("DefenderDireita").GetComponent<Image>();
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = tutorialBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetDefenderDireitaBackToNormal();
        }

        if (ButtonIntroducao.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetDefenderDireitaBackToNormal();
        }

        if (MyButton.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            MyButton.ResetDefenderDireitaBackToNormal();
        }

        if (ButtonDefenderCima.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetDefenderDireitaBackToNormal();
        }

        if (ButtonDefenderEsquerda.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetDefenderDireitaBackToNormal();
        }

        if (ButtonDefenderBaixo.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetDefenderDireitaBackToNormal();
        }

        if (ButtonTutorial.DefenderDireitaBackToNormal())
        {
            mImageDefenderDireita.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetDefenderDireitaBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (defenderParaDireita.isPlaying)
                defenderParaDireita.Stop();
            if (defenderDescricao.isPlaying)
                defenderDescricao.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!defenderParaDireita.isPlaying)
            defenderParaDireita.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeRight);
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

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
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
            mImageDefenderDireita.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!defenderParaDireita.isPlaying)
        {
            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonTutorial.ResetSoundOn();
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                defenderParaDireita.Play();
                defenderDescricao.PlayDelayed(defenderParaDireita.clip.length);
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
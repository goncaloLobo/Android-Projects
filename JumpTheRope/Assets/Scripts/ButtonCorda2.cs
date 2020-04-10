using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCorda2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource saltar1perna;
    public AudioSource descricao;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    private static int soundOn = 0;
    private static bool corda1BackToNormal, introducaoBackToNormal, jogarBackToNormal, instrucoesBackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    void Start()
    {
        highlighted = 0;
        corda1BackToNormal = introducaoBackToNormal = jogarBackToNormal = instrucoesBackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("CordaButton2").GetComponent<Image>();
    }

    void Update()
    {
        if(ButtonCorda1.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.ButtonCorda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonInstrucoes.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(Tutorial.Corda2BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (descricao.isPlaying)
                descricao.Stop();
            if (saltar1perna.isPlaying)
                saltar1perna.Stop();
        }

        // DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
            DoubleClickChecker.SwipeCorda1ToCorda2Reset();
        }

        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            DoubleClickChecker.SwipeCorda2ToCorda3();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!descricao.isPlaying)
        {
            descricao.Play();
            saltar1perna.PlayDelayed(descricao.clip.length);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ButtonCorda1.CheckForHighlighted() == 1)
        {
            corda1BackToNormal = true;
        }

        if (ButtonCorda3.CheckForHighlighted() == 1)
        {
            corda3BackToNormal = true;
        }

        if (ButtonCorda4.CheckForHighlighted() == 1)
        {
            corda4BackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (Tutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!descricao.isPlaying)
        {
            if (ButtonCorda1.GetSoundOn() == 1)
            {
                ButtonCorda1.SetSoundOn();
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonCorda1.GetSoundOn() == 0)
            {
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 1)
            {
                ButtonCorda3.SetSoundOn();
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 0)
            {
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 1)
            {
                ButtonCorda4.SetSoundOn();
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 0)
            {
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 1)
            {
                Tutorial.SetSoundOn();
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 0)
            {
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                descricao.Play();
                saltar1perna.PlayDelayed(descricao.clip.length);
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

    public static void SetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static bool Corda4BackToNormal()
    {
        return corda4BackToNormal;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }
}

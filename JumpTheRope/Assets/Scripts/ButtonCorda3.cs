using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCorda3 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource salto1perna;
    public AudioSource salto2pernas;
    public AudioSource descricao;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    private static int soundOn = 0;
    private static bool jogarBackToNormal, instrucoesBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    void Start()
    {
        highlighted = 0;
        jogarBackToNormal = instrucoesBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda4BackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("CordaButton3").GetComponent<Image>();
    }

    void Update()
    {
        if (ButtonCorda1.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonJogar.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonIntroducao.ButtonCorda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.Corda3BackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // PARTE RELACIONADA COM OS SONS
        if(soundOn == 0)
        {
            if (descricao.isPlaying)
                descricao.Stop();
            if (salto1perna.isPlaying)
                salto1perna.Stop();
            if (salto2pernas.isPlaying)
                salto2pernas.Stop();
        }

        // DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda4() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.SwipeCorda4ToCorda3() == 1)
        {
            DoubleClickChecker.SwipeCorda4ToCorda3Reset();
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda2() == 1)
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!descricao.isPlaying)
        {
            descricao.Play();
            salto2pernas.PlayDelayed(descricao.clip.length);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if(ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonCorda1.CheckForHighlighted() == 1)
        {
            corda1BackToNormal = true;
        }

        if (ButtonCorda2.CheckForHighlighted() == 1)
        {
            corda2BackToNormal = true;
        }

        if (ButtonCorda4.CheckForHighlighted() == 1)
        {
            corda4BackToNormal = true;
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
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonCorda1.GetSoundOn() == 0)
            {
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 1)
            {
                ButtonCorda2.SetSoundOn();
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 0)
            {
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 1)
            {
                ButtonCorda4.SetSoundOn();
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 0)
            {
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 1)
            {
                Tutorial.SetSoundOn();
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 0)
            {
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                descricao.Play();
                salto1perna.PlayDelayed(descricao.clip.length);
                salto2pernas.PlayDelayed(descricao.clip.length + 0.3f);
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

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
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

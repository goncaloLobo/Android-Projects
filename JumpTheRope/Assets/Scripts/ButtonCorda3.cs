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
    private Image mImageBC3;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool jogarBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda4BackToNormal, tutorialBackToNormal;
    private static bool corda4ToHighlight, corda2ToHighlight;

    void Start()
    {
        highlighted = 0;
        checkToStop = false;
        jogarBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda4BackToNormal = tutorialBackToNormal = false;
        mImageBC3 = GameObject.FindGameObjectWithTag("CordaButton3").GetComponent<Image>();

        corda4ToHighlight = corda2ToHighlight = false;
    }

    void Update()
    {
        if (ButtonCorda1.Corda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetCorda3BackToNormal();
        }

        if (ButtonCorda2.Corda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.ResetCorda3BackToNormal();
        }

        if (ButtonCorda4.Corda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetCorda3BackToNormal();
        }

        if (ButtonJogar.Corda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetCorda3BackToNormal();
        }

        if(ButtonIntroducao.ButtonCorda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetButtonCorda3BackToNormal();
        }

        if (Tutorial.Corda3BackToNormal())
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetCorda3BackToNormal();
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
        /*
        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImageBC3.overrideSprite = spriteHighlighted;
            highlighted = 1;
            descricao.Play();
            salto1perna.PlayDelayed(descricao.clip.length);
            salto2pernas.PlayDelayed(descricao.clip.length + salto1perna.clip.length);
            soundOn = 1;
            DoubleClickChecker.SwipeCorda2ToCorda3Reset();
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda4() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }
        */

        if(DoubleClickChecker.SwipeCorda4ToCorda3() == 1)
        {
            mImageBC3.overrideSprite = spriteHighlighted;
            highlighted = 1;
            descricao.Play();
            salto1perna.PlayDelayed(descricao.clip.length);
            salto2pernas.PlayDelayed(descricao.clip.length + salto1perna.clip.length);
            soundOn = 1;
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda2() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
            DoubleClickChecker.SwipeCorda3ToCorda2Reset();
        }

        /////////////////////////////////////////////////////
        /*
        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda4ToTutorial() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeTutorialToJogar() == 1)
        {
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }
        */

        // TENTATIVA DESESPERADA BLA BLA BLA
        if (DoubleClickChecker.GetConfirmedSwipeRight())
        {
            corda4ToHighlight = true;
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            corda2ToHighlight = true;
            mImageBC3.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.GetCorda3ToHighlight())
        {
            mImageBC3.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda2.ResetCorda3ToHighlight();
        }

        if (ButtonCorda4.GetCorda3ToHighlight())
        {
            mImageBC3.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda4.ResetCorda3ToHighlight();
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
            mImageBC3.sprite = spriteHighlighted;
            highlighted = 1;
        }

        /*
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
        */
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

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static void ResetCorda1BackToNormal()
    {
        corda1BackToNormal = false;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
    }

    public static void ResetCorda2BackToNormal()
    {
        corda2BackToNormal = false;
    }

    public static bool Corda4BackToNormal()
    {
        return corda4BackToNormal;
    }

    public static void ResetCorda4BackToNormal()
    {
        corda4BackToNormal = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }

    public static void SetCheckToStop()
    {
        checkToStop = true;
    }

    public void StopSounds()
    {
        if (checkToStop)
        {
            salto1perna.Stop();
            salto2pernas.Stop();
            descricao.Stop();
        }
    }

    //////////////////////
    public static bool GetCorda4ToHighlight()
    {
        return corda4ToHighlight;
    }

    public static void ResetCorda4ToHighlight()
    {
        corda4ToHighlight = false;
    }

    public static bool GetCorda2ToHighlight()
    {
        return corda2ToHighlight;
    }

    public static void ResetCorda2ToHighlight()
    {
        corda2ToHighlight = false;
    }
}

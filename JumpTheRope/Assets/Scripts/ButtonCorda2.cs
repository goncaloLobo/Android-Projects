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
    private Image mImageBC2;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool corda1BackToNormal, introducaoBackToNormal, jogarBackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    private static bool corda3ToHighlight, corda1ToHighlight;

    void Start()
    {
        highlighted = 0;
        checkToStop = false;
        corda1BackToNormal = introducaoBackToNormal = jogarBackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImageBC2 = GameObject.FindGameObjectWithTag("CordaButton2").GetComponent<Image>();
        corda3ToHighlight = corda1ToHighlight = false;
    }

    void Update()
    {
        if(ButtonCorda1.Corda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetCorda2BackToNormal();
        }

        if (ButtonCorda3.Corda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetCorda2BackToNormal();
        }

        if (ButtonCorda4.Corda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetCorda2BackToNormal();
        }

        if (ButtonIntroducao.ButtonCorda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetButtonCorda2BackToNormal();
        }

        if (ButtonJogar.Corda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetCorda2BackToNormal();
        }

        if(Tutorial.Corda2BackToNormal())
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetCorda2BackToNormal();
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
        /*
        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            descricao.Play();
            saltar1perna.PlayDelayed(descricao.clip.length);
            soundOn = 1;
            mImageBC2.overrideSprite = spriteHighlighted;
            highlighted = 1;
            DoubleClickChecker.SwipeCorda1ToCorda2Reset();
        }

        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }
        */
        if(DoubleClickChecker.SwipeCorda3ToCorda2() == 1)
        {
            mImageBC2.overrideSprite = spriteHighlighted;
            highlighted = 1;
            descricao.Play();
            saltar1perna.PlayDelayed(descricao.clip.length);
            soundOn = 1;
        }
        
        if (DoubleClickChecker.SwipeCorda2ToCorda1() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
            DoubleClickChecker.SwipeCorda2ToCorda1Reset();
        }

        /////////////////////////////////
        /*
        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda4() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda4ToTutorial() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeTutorialToJogar() == 1)
        {
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }
        */

        // AQUELA TENTATIVA MESMO DESESPERADA
        if (DoubleClickChecker.GetConfirmedSwipeRight())
        {
            corda3ToHighlight = true;
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda1.GetCorda2ToHighlight())
        {
            mImageBC2.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda1.ResetCorda2ToHighlight();
        }

        if (ButtonCorda3.GetCorda2ToHighlight())
        {
            mImageBC2.sprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda3.ResetCorda2ToHighlight();
        }

        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            corda1ToHighlight = true;
            mImageBC2.overrideSprite = normalSprite;
            highlighted = 0;
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

        if (Tutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if(highlighted == 0)
        {
            mImageBC2.sprite = spriteHighlighted;
            highlighted = 1;
        }
        
        /*
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

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
    }

    public static void Corda1BackToNormalReset()
    {
        corda1BackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static void ResetJogarBackToNormal()
    {
        jogarBackToNormal = false;
    }

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static void ResetCorda3BackToNormal()
    {
        corda3BackToNormal = false;
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
            saltar1perna.Stop();
            descricao.Stop();
        }
    }

    ////////////////////
    public static bool GetCorda3ToHighlight()
    {
        return corda3ToHighlight;
    }

    public static void ResetCorda3ToHighlight()
    {
        corda3ToHighlight = false;
    }

    public static bool GetCorda1ToHighlight()
    {
        return corda1ToHighlight;
    }

    public static void ResetCorda1ToHighlight()
    {
        corda1ToHighlight = false;
    }
}

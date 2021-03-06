﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCorda4 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource intro;
    public AudioSource descricao;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImageBC4;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool jogarBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, tutorialBackToNormal;
    private static bool corda4ToTutorial, corda3ToHighlight;

    void Start()
    {
        intro = GetComponent<AudioSource>();
        checkToStop = false;
        highlighted = 0;
        jogarBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = false;
        mImageBC4 = GameObject.FindGameObjectWithTag("CordaButton4").GetComponent<Image>();

        corda4ToTutorial = corda3ToHighlight = false;
    }

    void Update()
    {
        if (ButtonCorda1.Corda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda1.ResetCorda4BackToNormal();
        }

        if (ButtonCorda2.Corda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.ResetCorda4BackToNormal();
        }

        if (ButtonCorda3.Corda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetCorda4BackToNormal();
        }

        if (ButtonJogar.Corda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetCorda4BackToNormal();
        }

        if (ButtonIntroducao.ButtonCorda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetButtonCorda4BackToNormal();
        }

        if (Tutorial.Corda4BackToNormal())
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetCorda4BackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if(soundOn == 0){
            if (intro.isPlaying)
                intro.Stop();
            if (descricao.isPlaying)
                descricao.Stop();
        }

        // DOUBLE CLICK CHECKER
        /*
        if(DoubleClickChecker.SwipeCorda3ToCorda4() == 1)
        {
            mImageBC4.overrideSprite = spriteHighlighted;
            highlighted = 1;
            descricao.Play();
            intro.PlayDelayed(descricao.clip.length);
            soundOn = 1;
            DoubleClickChecker.SwipeCorda3ToCorda4Reset();
        }

        if(DoubleClickChecker.SwipeCorda4ToCorda3() == 1)
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
            DoubleClickChecker.SwipeCorda4ToCorda3Reset();
        }

        if (DoubleClickChecker.SwipeCorda4ToTutorial() == 1)
        {
            System.Diagnostics.Debug.WriteLine("corda4: entrei corda4 -> tutorial");
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
            DoubleClickChecker.SwipeCorda4ToTutorialReset();
        }

        if(DoubleClickChecker.SwipeTutorialToCorda4() == 1)
        {
            mImageBC4.overrideSprite = spriteHighlighted;
            highlighted = 1;
            descricao.Play();
            intro.PlayDelayed(descricao.clip.length);
            soundOn = 1;
            DoubleClickChecker.SwipeTutorialToCorda4Reset();
        }
        */
        /////////////////////////////////////////////////////
        /*
        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda2ToCorda3() == 1)
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeCorda3ToCorda4() == 1)
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }

        if(DoubleClickChecker.SwipeTutorialToJogar() == 1)
        {
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
        }
        */

        /////////////////////////////////////
        if (DoubleClickChecker.GetConfirmedSwipeRight())
        {
            corda4ToTutorial = true;
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            corda3ToHighlight = true;
            mImageBC4.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.GetCorda4ToHighlight())
        {
            mImageBC4.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda3.ResetCorda4ToHighlight();
        }

        if (Tutorial.GetCorda4ToHighlight())
        {
            mImageBC4.overrideSprite = spriteHighlighted;
            highlighted = 1;
            Tutorial.ResetCorda4ToHighlight();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!intro.isPlaying)
        {
            intro.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonCorda1.CheckForHighlighted() == 1)
        {
            corda1BackToNormal = true;
        }

        if (ButtonCorda2.CheckForHighlighted() == 1)
        {
            corda2BackToNormal = true;
        }

        if (ButtonCorda3.CheckForHighlighted() == 1)
        {
            corda3BackToNormal = true;
        }

        if (Tutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageBC4.sprite = spriteHighlighted;
            highlighted = 1;
        }

        /*
        if (!intro.isPlaying)
        {
            if (ButtonCorda1.GetSoundOn() == 1)
            {
                ButtonCorda1.SetSoundOn();
                intro.Play();
                soundOn = 1;
            }

            if (ButtonCorda1.GetSoundOn() == 0)
            {
                intro.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 1)
            {
                ButtonCorda2.SetSoundOn();
                intro.Play();
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 0)
            {
                intro.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 1)
            {
                ButtonCorda3.SetSoundOn();
                intro.Play();
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 0)
            {
                intro.Play();
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 1)
            {
                Tutorial.SetSoundOn();
                intro.Play();
                soundOn = 1;
            }

            if (Tutorial.GetSoundOn() == 0)
            {
                intro.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                intro.Play();
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                intro.Play();
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

    public static bool Corda3BackToNormal()
    {
        return corda3BackToNormal;
    }

    public static void ResetCorda3BackToNormal()
    {
        corda3BackToNormal = false;
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
            intro.Stop();
            descricao.Stop();
        }
    }

    ///////////////////////////////
    public static bool GetCorda4ToTutorial()
    {
        return corda4ToTutorial;
    }

    public static void ResetCorda4ToTutorial()
    {
        corda4ToTutorial = false;
    }

    public static bool GetCorda3ToHighlight()
    {
        return corda3ToHighlight;
    }

    public static void ResetCorda3ToHighlight()
    {
        corda3ToHighlight = false;
    }
}

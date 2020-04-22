using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class ButtonCorda1 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource corda;
    public AudioSource descricao;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImageBC1;
    private static int soundOn = 0;
    private static bool checkToStop;
    private static bool introducaoBackToNormal, jogarBackToNormal;
    private static bool corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    private static bool corda2ToHighlight, jogarToHighlight;

    void Start()
    {
        highlighted = 0;
        checkToStop = false;
        introducaoBackToNormal = jogarBackToNormal = corda2BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImageBC1 = GameObject.FindGameObjectWithTag("CordaButton1").GetComponent<Image>();
        corda2ToHighlight = jogarToHighlight = false;
    }

    void Update()
    {
        if(ButtonIntroducao.ButtonCorda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetButtonCorda1BackToNormal();
        }

        if(ButtonJogar.Corda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonJogar.ResetCorda1BackToNormal();
        }

        if(ButtonCorda2.Corda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda2.Corda1BackToNormalReset();
        }

        if (ButtonCorda3.Corda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda3.ResetCorda1BackToNormal();
        }

        if (ButtonCorda4.Corda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonCorda4.ResetCorda1BackToNormal();
        }

        if (Tutorial.Corda1BackToNormal())
        {
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
            Tutorial.ResetCorda1BackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (corda.isPlaying)
                corda.Stop();
            if (descricao.isPlaying)
                descricao.Stop();
        }

        // GET CONFIRMED SWIPE RIGHT / LEFT
        if (DoubleClickChecker.GetConfirmedSwipeRight())
        {
            corda2ToHighlight = true;
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (DoubleClickChecker.GetConfirmedSwipeLeft())
        {
            jogarToHighlight = true;
            mImageBC1.overrideSprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.GetCorda1ToHighlight())
        {
            mImageBC1.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonCorda2.ResetCorda1ToHighlight();
        }

        if (ButtonJogar.GetCorda1ToHighlight())
        {
            mImageBC1.overrideSprite = spriteHighlighted;
            highlighted = 1;
            ButtonJogar.ResetCorda1ToHighlight();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!descricao.isPlaying)
        {
            descricao.Play();
            corda.PlayDelayed(descricao.clip.length);
            StartCoroutine(WaitForTouch(8, DoAfter));
        }            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonCorda2.CheckForHighlighted() == 1)
        {
            corda2BackToNormal = true;
        }

        if (ButtonCorda3.CheckForHighlighted() == 1)
        {
            corda3BackToNormal = true;
        }

        if (ButtonCorda4.CheckForHighlighted() == 1)
        {
            corda4BackToNormal = true;
        }

        if (Tutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        /*
        if (!descricao.isPlaying)
        {
            if (ButtonCorda2.GetSoundOn() == 1)
            {
                ButtonCorda2.SetSoundOn();
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonCorda2.GetSoundOn() == 0)
            {
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 1)
            {
                ButtonCorda3.SetSoundOn();
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonCorda3.GetSoundOn() == 0)
            {
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 1)
            {
                ButtonCorda4.SetSoundOn();
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonCorda4.GetSoundOn() == 0)
            {
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 1)
            {
                ButtonJogar.SetSoundOn();
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }

            if (ButtonJogar.GetSoundOn() == 0)
            {
                descricao.Play();
                //corda.PlayDelayed(descricao.clip.length);
                //StartCoroutine(WaitForTouch(8, DoAfter));
                soundOn = 1;
            }
        }
        */

        if (highlighted == 0)
        {
            mImageBC1.overrideSprite = spriteHighlighted;
            highlighted = 1;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    // da sinal para terminar o som de saltar à corda passado algum tempo
    public IEnumerator WaitForTouch(float duration, Action DoAfter)
    {
        yield return new WaitForSeconds(duration);
        DoAfter();
    }

    // termina o som de saltar à corda
    public void DoAfter()
    {
        corda.Stop();
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static int GetSoundOn()
    {
        return soundOn;
    }

    public static void SetSoundOn()
    {
        soundOn = 0;
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
            corda.Stop();
            descricao.Stop();
        }    
    }

    public static void StayHighlighted()
    {
        highlighted = 1;
    }

    private void StayHighlightedPrivate()
    {
        if(highlighted == 1)
        {
            mImageBC1.overrideSprite = spriteHighlighted;
            highlighted = 1;
        }
    }

    // AQUELA TENTATIVA DESESPERADA
    public static bool GetCorda2ToHighlight()
    {
        return corda2ToHighlight;
    }

    public static void ResetCorda2ToHighlight()
    {
        corda2ToHighlight = false;
    }

    public static bool GetJogarToHighlight()
    {
        return jogarToHighlight;
    }

    public static void ResetJogarToHighlight()
    {
        jogarToHighlight = false;
    }
}

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
    private static bool introducaoBackToNormal, jogarBackToNormal, instrucoesBackToNormal;
    private static bool corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    void Start()
    {
        highlighted = 0;
        checkToStop = false;
        introducaoBackToNormal = jogarBackToNormal = instrucoesBackToNormal = corda2BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImageBC1 = GameObject.FindGameObjectWithTag("CordaButton1").GetComponent<Image>();
    }

    void Update()
    {
        if(ButtonIntroducao.ButtonCorda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonJogar.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonCorda2.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.Corda1BackToNormal())
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (corda.isPlaying)
                corda.Stop();
            if (descricao.isPlaying)
                descricao.Stop();
        }

        // DOUBLE CLICK CHECKER
        if(DoubleClickChecker.SwipeJogarToCorda1() == 1)
        {
            descricao.Play();
            mImageBC1.sprite = spriteHighlighted;
            highlighted = 1;
            soundOn = 1;
            DoubleClickChecker.SwipeJogarToCorda1Reset();
        }

        if(DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            soundOn = 0;
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
            System.Diagnostics.Debug.WriteLine("hey1");
        }

        if(DoubleClickChecker.SwipeCorda2ToCorda1() == 1)
        {
            descricao.Play();
            soundOn = 1;
            mImageBC1.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(DoubleClickChecker.SwipeCorda1ToJogar() == 1)
        {
            mImageBC1.sprite = normalSprite;
            highlighted = 0;
            soundOn = 0;
            DoubleClickChecker.SwipeCorda1ToJogarReset();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!descricao.isPlaying)
        {
            descricao.Play();
            //corda.PlayDelayed(descricao.clip.length);
            //StartCoroutine(WaitForTouch(8, DoAfter));
        }            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        System.Diagnostics.Debug.WriteLine("ahoy sir");
        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonJogar.CheckForHighlighted() == 1)
        {
            jogarBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
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
            System.Diagnostics.Debug.WriteLine("hey jude");
            mImageBC1.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if(highlighted == 1 && DoubleClickChecker.SwipeCorda1ToCorda2() == 1)
        {
            System.Diagnostics.Debug.WriteLine("aqui estou eu");
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

    public static bool JogarBackToNormal()
    {
        return jogarBackToNormal;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static bool Corda2BackToNormal()
    {
        return corda2BackToNormal;
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
}

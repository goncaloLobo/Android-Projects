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
    private Image mImage;
    private bool check;
    private static bool introducaoBackToNormal, jogarBackToNormal, instrucoesBackToNormal;
    private static bool corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    void Start()
    {
        highlighted = 0;
        check = false;
        introducaoBackToNormal = jogarBackToNormal = instrucoesBackToNormal = corda2BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("CordaButton1").GetComponent<Image>();
    }

    void Update()
    {
        if(ButtonIntroducao.ButtonCorda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonJogar.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonInstrucoes.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(ButtonCorda2.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // DOUBLE CLICK CHECKER
        if(DoubleClickChecker.Corda1BackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        check = false;
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

        if (!descricao.isPlaying)
        {
            descricao.Play();
            corda.PlayDelayed(descricao.clip.length);
            StartCoroutine(WaitForTouch(8, DoAfter));
        }

        if (highlighted == 0)
        {
            mImage.sprite = spriteHighlighted;
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
}

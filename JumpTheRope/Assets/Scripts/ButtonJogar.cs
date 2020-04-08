﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJogar : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;
    private static bool introducaoBackToNormal, instrucoesBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal, tutorialBackToNormal;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static int highlighted;
    private Image mImage;
    private static bool spriteBool;

    public GameObject GameManagerGO;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
        introducaoBackToNormal = instrucoesBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = corda4BackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("PlayButtonTag").GetComponent<Image>();
    }

    void Update()
    {
        // BACK TO NORMAL
        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.JogarBackToNormalFalse();
        }

        if(ButtonInstrucoes.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.JogarBackToNormalFalse();
        }

        if(ButtonCorda1.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda4.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.JogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        //DOUBLE CLICK CHECKER
        if (DoubleClickChecker.ButtonJogarBackToNormal())
        {
            mImage.sprite = normalSprite;
            highlighted = 0;
        }
 
        if(DoubleClickChecker.ButtonJogarToHighlight())
        {
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if (!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PreGameplay);
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
        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if(ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if(ButtonCorda1.CheckForHighlighted() == 1)
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

        if (!jogar.isPlaying)
            jogar.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void IntroducaoBackToNormalFalse()
    {
        introducaoBackToNormal = false;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void InstrucoesBackToNormalFalse()
    {
        instrucoesBackToNormal = false;
    }

    public static bool Corda1BackToNormal()
    {
        return corda1BackToNormal;
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

﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInstrucoes : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private float clickdelay = 0.5f;
    public AudioSource instrucoes;
    private float currentTapTime;
    private float lastTapTime;

    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private Image mImage;
    private static int highlighted;
    private static bool check, jogarBackToNormal, introducaoBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, tutorialBackToNormal;

    public GameObject GameManagerGO;

    void Start()
    {
        instrucoes = GetComponent<AudioSource>();
        highlighted = 0;
        check = false;
        jogarBackToNormal = introducaoBackToNormal = corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = tutorialBackToNormal = false;
        mImage = GameObject.FindGameObjectWithTag("InstructionsButtonTag").GetComponent<Image>();
    }

    void Update()
    {

        if(ButtonJogar.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonIntroducao.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda1.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda2.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (ButtonCorda3.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if (Tutorial.InstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        // DOUBLE CLICK CHECKER
        if (DoubleClickChecker.ButtonInstrucoesBackToNormal() && !check)
        {
            check = true;
            mImage.sprite = normalSprite;
            highlighted = 0;
        }

        if(DoubleClickChecker.ButtonInstrucoesToHighlight() && !check)
        {
            check = true;
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        check = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!instrucoes.isPlaying)
            instrucoes.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instrucoes);
            }
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
            mImage.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!instrucoes.isPlaying)
            instrucoes.Play();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

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

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }
}

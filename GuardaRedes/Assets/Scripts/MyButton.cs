﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;
    public Sprite normalSprite;
    public Sprite spriteHighlighted;
    private static Image mImageJogar;
    private static int highlighted;
    private static int soundOn = 0;
    private static bool instrucoesBackToNormal, introducaoBackToNormal, tutorialBackToNormal;
    private static bool buttonDefenderBaixoBackToNormal, buttonDefenderCimaBackToNormal, buttonDefenderEsquerdaBackToNormal, buttonDefenderDireitaBackToNormal;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
        mImageJogar = GameObject.FindGameObjectWithTag("ButtonPlay").GetComponent<Image>();

        highlighted = 0;
        instrucoesBackToNormal = introducaoBackToNormal = tutorialBackToNormal = false;
        buttonDefenderBaixoBackToNormal = buttonDefenderCimaBackToNormal = buttonDefenderDireitaBackToNormal = buttonDefenderEsquerdaBackToNormal = false;
    }

    void Update()
    {
        if (ButtonInstrucoes.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonInstrucoes.ResetJogarBackToNormal();
        }

        if (ButtonDefenderBaixo.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderBaixo.ResetJogarBackToNormal();
        }

        if (ButtonDefenderDireita.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderDireita.ResetJogarBackToNormal();
        }

        if (ButtonDefenderEsquerda.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderEsquerda.ResetJogarBackToNormal();
        }

        if (ButtonDefenderCima.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonDefenderCima.ResetJogarBackToNormal();
        }

        if (ButtonIntroducao.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonIntroducao.ResetJogarBackToNormal();
        }

        if (ButtonTutorial.JogarBackToNormal())
        {
            mImageJogar.overrideSprite = normalSprite;
            highlighted = 0;
            ButtonTutorial.ResetJogarBackToNormal();
        }

        // PARTE RELACIONADA COM OS SONS
        if (soundOn == 0)
        {
            if (jogar.isPlaying)
                jogar.Stop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        if(!jogar.isPlaying)
            jogar.Play();

        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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
        if (ButtonDefenderBaixo.CheckForHighlighted() == 1)
        {
            buttonDefenderBaixoBackToNormal = true;
        }

        if (ButtonDefenderCima.CheckForHighlighted() == 1)
        {
            buttonDefenderCimaBackToNormal = true;
        }

        if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
        {
            buttonDefenderEsquerdaBackToNormal = true;
        }

        if (ButtonDefenderDireita.CheckForHighlighted() == 1)
        {
            buttonDefenderDireitaBackToNormal = true;
        }

        if (ButtonIntroducao.CheckForHighlighted() == 1)
        {
            introducaoBackToNormal = true;
        }

        if (ButtonInstrucoes.CheckForHighlighted() == 1)
        {
            instrucoesBackToNormal = true;
        }

        if (ButtonTutorial.CheckForHighlighted() == 1)
        {
            tutorialBackToNormal = true;
        }

        if (highlighted == 0)
        {
            mImageJogar.sprite = spriteHighlighted;
            highlighted = 1;
        }

        if (!jogar.isPlaying)
        {
            if (ButtonInstrucoes.GetSoundOn() == 1)
            {
                ButtonInstrucoes.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonInstrucoes.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 1)
            {
                ButtonIntroducao.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonIntroducao.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 1)
            {
                ButtonDefenderDireita.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderDireita.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 1)
            {
                ButtonDefenderBaixo.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderBaixo.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 1)
            {
                ButtonDefenderCima.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderCima.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 1)
            {
                ButtonDefenderEsquerda.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonDefenderEsquerda.GetSoundOn() == 0)
            {
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 1)
            {
                ButtonTutorial.ResetSoundOn();
                jogar.Play();
                soundOn = 1;
            }

            if (ButtonTutorial.GetSoundOn() == 0)
            {
                jogar.Play();
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

    public static void ResetSoundOn()
    {
        soundOn = 0;
    }

    public static int CheckForHighlighted()
    {
        return highlighted;
    }

    public static bool InstrucoesBackToNormal()
    {
        return instrucoesBackToNormal;
    }

    public static void ResetInstrucoesBackToNormal()
    {
        instrucoesBackToNormal = false;
    }

    public static bool IntroducaoBackToNormal()
    {
        return introducaoBackToNormal;
    }

    public static void ResetIntroducaoBackToNormal()
    {
        introducaoBackToNormal = false;
    }

    public static bool DefenderBaixoBackToNormal()
    {
        return buttonDefenderBaixoBackToNormal;
    }

    public static void ResetDefenderBaixoBackToNormal()
    {
        buttonDefenderBaixoBackToNormal = false;
    }

    public static bool DefenderCimaBackToNormal()
    {
        return buttonDefenderCimaBackToNormal;
    }

    public static void ResetDefenderCimaBackToNormal()
    {
        buttonDefenderCimaBackToNormal = false;
    }

    public static bool DefenderDireitaBackToNormal()
    {
        return buttonDefenderDireitaBackToNormal;
    }

    public static void ResetDefenderDireitaBackToNormal()
    {
        buttonDefenderDireitaBackToNormal = false;
    }

    public static bool DefenderEsquerdaBackToNormal()
    {
        return buttonDefenderEsquerdaBackToNormal;
    }

    public static void ResetDefenderEsquerdaBackToNormal()
    {
        buttonDefenderEsquerdaBackToNormal = false;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
    }

    public static void ResetTutorialBackToNormal()
    {
        tutorialBackToNormal = false;
    }
}

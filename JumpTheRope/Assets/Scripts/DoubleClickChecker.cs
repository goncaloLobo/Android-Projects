﻿using UnityEngine;
using System.Collections;
using System;

public class DoubleClickChecker : MonoBehaviour
{
    private static int n_saltos_perfeitos;
    private static int n_saltos_normais;
    private static int pontuacaoTotal;

    private float doubleTapDeltaBigger = Configuration.DoubleTapDeltaBigger();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int perfectJump = Configuration.PerfectJump();
    private int normalJump = Configuration.NormalJump();
    private int doubleTapRadius = Configuration.DoubleTapRadius();
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();

    private Touch currentTouch;
    private Touch previousTouch;
    private float currentTapTime;
    private float lastTapTime;
    private int doubleTapCircle;

    public AudioSource manJumping; // ManJumping
    public AudioSource oneFootJumping; // tap
    public AudioSource saltoPerfeito; // salto perfeito
    public AudioSource inicioJogo;
    public GameManager GameManagerGO;
    private int i = 0;

    private bool isDoubleTap, isGoingToTouch;
    private float screenDPI;
    private float increaseSpeedTimer;

    private Vector2 swipeDelta;

    private static bool buttonJogarBackToNormal, buttonIntroducaoToHighlight, buttonIntroducaoBackToNormal, buttonInstrucoesToHighlight, buttonInstrucoesBackToNormal;
    private static bool buttonJogarToHighlight;
    public GameObject buttonJogar;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Touch CurrentTouch { get { return currentTouch; } }
    public Touch PreviousTouch { get { return previousTouch; } }
    public float CurrentTapTime { get { return currentTapTime; } }
    public float LastTapTime { get { return lastTapTime; } }
    public int DoubleTapCircle { get { return doubleTapCircle; } }

    void Start()
    {
        n_saltos_perfeitos = n_saltos_normais = pontuacaoTotal = 0;
        doubleTapCircle = doubleTapRadius * doubleTapRadius;
        isGoingToTouch = isDoubleTap = buttonJogarBackToNormal = false;
        buttonIntroducaoToHighlight = buttonIntroducaoBackToNormal = buttonInstrucoesToHighlight = buttonInstrucoesBackToNormal = false;
        buttonJogarToHighlight = false;
        screenDPI = Screen.dpi;
    }

    void Update()
    {
        if (Input.touchCount > 0 && GameManager.GetStarted())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oneFootJumping.Play();
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    manJumping.Play();
                    n_saltos_perfeitos++;
                    pontuacaoTotal += perfectJump;
                    saltoPerfeito.PlayDelayed(manJumping.clip.length);
                }
                else if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 1)
                {
                    n_saltos_normais++;
                    pontuacaoTotal += normalJump;
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                previousTouch = currentTouch;
                lastTapTime = currentTapTime;
            }
        }

        if(GameManager.GetPreGameplay() && i == 0)
        {
            i++;
            inicioJogo.Play();
            StartCoroutine(WaitForTouch(inicioJogo.clip.length, DoAfter)); 
        }

        if (Input.touchCount > 0 && GameManager.GetOpening())
        {
            System.Diagnostics.Debug.WriteLine("aqui");
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    isDoubleTap = true;
                    // se for doubletap e o botao jogar highlighted, entao faz jogar.
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                previousTouch = touch;
                lastTapTime = currentTapTime;
                int deltaX = (int)previousTouch.position.x - (int)currentTouch.position.x;
                int deltaY = (int)previousTouch.position.y - (int)currentTouch.position.y;
                int distance = (deltaX * deltaX) + (deltaY * deltaY);

                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = lastTapTime - currentTapTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        // swipe!!!
                        swipeDelta = new Vector2(deltaX, deltaY);
                        swipeDelta.Normalize();

                        //swipe left
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            if (ButtonIntroducao.CheckForHighlighted() == 1)
                            {
                                System.Diagnostics.Debug.WriteLine("introducao highlighted");
                                /*
                                buttonIntroducaoBackToNormal = true;
                                ButtonIntroducaoBackToNormal();

                                buttonInstrucoesToHighlight = true;
                                ButtonInstrucoesToHighlight();
                                */
                            }

                            if (ButtonJogar.CheckForHighlighted() == 1)
                            {
                                buttonJogarBackToNormal = true;
                                buttonIntroducaoToHighlight = true;
                            }

                            if(ButtonInstrucoes.CheckForHighlighted() == 1)
                            {
                                System.Diagnostics.Debug.WriteLine("instrucoes highlighted");
                                //buttonInstrucoesBackToNormal = true;
                                //ButtonInstrucoesBackToNormal();

                                //buttonJogarToHighlight = true;
                                //ButtonJogarToHighlight();
                            }
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            if (ButtonIntroducao.CheckForHighlighted() == 1)
                            {
                                buttonIntroducaoBackToNormal = true;
                                buttonJogarToHighlight = true;
                            }
                        }
                    }
                    isDoubleTap = false;
                }
            }
        }
            /*
            if (Input.touchCount > 0 && GameManager.GetCurrentState() == GameManager.GameManagerState.Opening)
            {
                if (ButtonJogar.HasEntered())
                {
                    System.Diagnostics.Debug.WriteLine("entrei buttonjogar.hasEntered");
                    System.Diagnostics.Debug.WriteLine("Input.touchCount: " + Input.touchCount);
                    System.Diagnostics.Debug.WriteLine("entrei no toque seguinte");
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        currentTouch = touch;
                        currentTapTime = Time.time;
                        if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                        {
                            isDoubleTap = true;
                            // se for doubletap e o botao jogar highlighted, entao faz jogar.
                        }
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {

                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        previousTouch = currentTouch;
                        lastTapTime = currentTapTime;

                        int deltaX = (int)previousTouch.position.x - (int)currentTouch.position.x;
                        int deltaY = (int)previousTouch.position.y - (int)currentTouch.position.y;

                        int distance = (deltaX * deltaX) + (deltaY * deltaY);
                        if (distance > (16.0f * screenDPI + 0.5f) && !isDoubleTap)
                        {
                            float difference = lastTapTime - currentTapTime;
                            if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                            {
                                // swipe!!!
                                swipeDelta = new Vector2(deltaX, deltaY);
                                swipeDelta.Normalize();

                                //swipe left
                                if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                                {
                                    System.Diagnostics.Debug.WriteLine("entrei swipe esquerda");
                                }

                                //swipe right
                                if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                                {
                                    System.Diagnostics.Debug.WriteLine("entrei swipe direita");
                                }
                        }
                        isDoubleTap = false;
                        }
                    }
                    // se fizer swipe para a direita -> mudo para o botao de order 3 (instrucoes)
                    // se fizer swipe para a esquerda -> mudo para o botao de order 1 (introducao)
                }

                if (ButtonIntroducao.HasEntered())
                {
                    // se fizer swipe para a direita -> mudo para o botao de order 2 (jogar)
                    // se fizer swipe para a esquerda -> mudo para o botao de order 3 (instrucoes)
                }

                if (ButtonInstrucoes.HasEntered())
                {
                    // se fizer swipe para a direita -> mudo para o botao de order 1 (introducao)
                    // se fizer swipe para a esquerda -> mudo para o botao de order 2 (jogar)
                }
            }
            */
    }

    public void DoAfter()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }

    private int CheckForDoubleTap(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
    {
        int deltaX = (int)currentTouch.position.x - (int)previousTouch.position.x;
        int deltaY = (int)currentTouch.position.y - (int)previousTouch.position.y;

        // diferença entre os toques superior a 1s
        if (currentTapTime - previousTapTime > doubleTapDeltaBigger)
        {
            return -1;
        }

        // se a diferença entre os toques for menor que 1s e maior que 300ms então é salto normal
        if (currentTapTime - previousTapTime < doubleTapDeltaBigger && currentTapTime - previousTapTime > doubleTapDelta)
        {
            // se o duplo toque "normal" estiver dentro do circulo aceitavel, entao retorna 1
            if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
                return 1;
            else
                return -1;
        }

        // se o duplo toque "perfeito" estiver dentro do circulo aceitavel, entao retorna 0
        if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
            return 0;
        return -1;
    }

    private int CheckForDoubleTapOpening(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
    {
        int deltaX = (int)currentTouch.position.x - (int)previousTouch.position.x;
        int deltaY = (int)currentTouch.position.y - (int)previousTouch.position.y;

        // diferença entre os toques superior a 1s
        if (currentTapTime - previousTapTime > doubleTapDelta)
        {
            return -1;
        }

        // se o duplo toque "perfeito" estiver dentro do circulo aceitavel, entao retorna 0
        if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
            return 0;
        return -1;
    }

    public IEnumerator WaitForTouch(float duration, Action DoAfter)
    {
        yield return new WaitForSeconds(duration);
        DoAfter();
    }

        //numero de saltos perfeitos no final
        public static int GetSaltosPerfeitos()
    {
        return n_saltos_perfeitos;
    }

    //numero de saltos normais no final
    public static int GetSaltosNormais()
    {
        return n_saltos_normais;
    }

    //obtem a pontuacao no final (pontuacao final pretty much)
    public static int GetPontuacao()
    {
        return pontuacaoTotal;
    }

    //obtem o total de saltos feitos pelo utilizador
    public static int GetTotalSaltos()
    {
        return n_saltos_normais + n_saltos_perfeitos;
    }

    public static bool ButtonJogarBackToNormal()
    {
        return buttonJogarBackToNormal;
    }

    public static bool ButtonIntroducaoToHighlight()
    {
        return buttonIntroducaoToHighlight;
    }

    public static bool ButtonIntroducaoBackToNormal()
    {
        return buttonIntroducaoBackToNormal;
    }

    public static bool ButtonInstrucoesToHighlight()
    {
        return buttonInstrucoesToHighlight;
    }

    public static bool ButtonInstrucoesBackToNormal()
    {
        return buttonInstrucoesBackToNormal;
    }

    public static bool ButtonJogarToHighlight()
    {
        return buttonJogarToHighlight;
    }
}
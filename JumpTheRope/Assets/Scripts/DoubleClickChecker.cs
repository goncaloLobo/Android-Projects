using UnityEngine;
using System.Collections;
using System;

public class DoubleClickChecker : MonoBehaviour
{
    private static int n_saltos_perfeitos;
    private static int n_saltos_normais;
    private static int pontuacaoTotal;
    private static int n_saltos_falhados;

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

    public AudioSource introducaoSound; // som do botao da introducao
    public AudioSource cordaSound; // som da corda (usado no ButtonCorda1)
    public AudioSource descricaoCordaSound; // descricao da corda (usado no ButtonCorda1)
    public AudioSource umaPernaSound; // som de uma perna (usado no ButtonCorda2)
    public AudioSource descricaoUmaPerna; // descricao do som (usado no ButtonCorda2)
    public AudioSource duasPernasSound; // som de um salto com 2 pernas (usado no ButtonCorda3)
    public AudioSource descricaoDuasPernas; // descricao do som (usado no ButtonCorda3)

    private float screenDPI;
    private float increaseSpeedTimer;

    private Vector2 swipeDelta;

    private static bool buttonJogarBackToNormal, buttonIntroducaoToHighlight, buttonIntroducaoBackToNormal, buttonInstrucoesToHighlight, buttonInstrucoesBackToNormal;
    private static bool buttonJogarToHighlight, tutorialBackToNormal, corda1BackToNormal, corda2BackToNormal, corda3BackToNormal, corda4BackToNormal;
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
        buttonJogarBackToNormal = false;
        buttonIntroducaoToHighlight = buttonIntroducaoBackToNormal = buttonInstrucoesToHighlight = buttonInstrucoesBackToNormal = false;
        corda1BackToNormal = corda2BackToNormal = corda3BackToNormal = corda4BackToNormal = false;
        buttonJogarToHighlight = tutorialBackToNormal = false;
        screenDPI = Screen.dpi;
    }

    void Update()
    {
        // accoes para o botao de voltar para trás do android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.GetOpening())
            {
                if (introducaoSound.isPlaying)
                    introducaoSound.Stop();
                else
                    Application.Quit();
            }                

            if(GameManager.GetStarted())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if (GameManager.GetInstrucoes())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if(GameManager.GetTutorialP1() || GameManager.GetTutorialP2() || GameManager.GetTutorialP3() || GameManager.GetTutorialP4() || GameManager.GetTutorialP5())
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instrucoes);
            }

            if(GameManager.GetPreGameplay())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
        }

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
                else
                    n_saltos_falhados++;

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

        if (GameManager.GetPreGameplay() && i == 0)
        {
            i++;
            inicioJogo.Play();
            StartCoroutine(WaitForTouch(inicioJogo.clip.length, DoAfter));
        }

        if (Input.touchCount > 0 && GameManager.GetOpening())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    // se o botao jogar estiver highlighted
                    if (ButtonJogar.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PreGameplay);
                        buttonJogarBackToNormal = true;
                    }

                    else if (ButtonIntroducao.CheckForHighlighted() == 1)
                    {
                        if (!introducaoSound.isPlaying)
                            introducaoSound.Play();
                        buttonIntroducaoBackToNormal = true;
                    }

                    else if (ButtonInstrucoes.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instrucoes);
                        buttonInstrucoesBackToNormal = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                previousTouch = touch;
                lastTapTime = currentTapTime;
            }
        }

        if (Input.touchCount > 0)
        {
            if (GameManager.GetInstrucoes())
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    currentTouch = touch;
                    currentTapTime = Time.time;
                    if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                    {
                        // se o botao jogar estiver highlighted
                        if (Tutorial.CheckForHighlighted() == 1)
                        {
                            tutorialBackToNormal = true;
                            Invoke("ChangeToTutorialP1State", 0.5f);
                        }

                        else if (ButtonJogar.CheckForHighlighted() == 1)
                        {
                            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PreGameplay);
                            buttonJogarBackToNormal = true;
                        }

                        else if (ButtonCorda1.CheckForHighlighted() == 1)
                        {
                            if (!descricaoCordaSound.isPlaying)
                            {
                                descricaoCordaSound.Play();
                                cordaSound.PlayDelayed(descricaoCordaSound.clip.length);
                                StartCoroutine(WaitForEndSound(8, DoAfterDescricao));
                            }
                        }

                        else if (ButtonCorda2.CheckForHighlighted() == 1)
                        {
                            if (!descricaoUmaPerna.isPlaying)
                            {
                                descricaoUmaPerna.Play();
                                umaPernaSound.PlayDelayed(descricaoUmaPerna.clip.length);
                            }
                        }

                        else if (ButtonCorda3.CheckForHighlighted() == 1)
                        {
                            if (!descricaoDuasPernas.isPlaying)
                            {
                                descricaoDuasPernas.Play();
                                duasPernasSound.PlayDelayed(descricaoDuasPernas.clip.length);
                            }
                        }
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
                            swipeDelta = new Vector2(deltaX, deltaY);
                            swipeDelta.Normalize();

                            //swipe left
                            if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                            {

                            }

                            //swipe right
                            if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                            {

                            }
                        }
                    }

                }
            }

            if (GameManager.GetTutorialP1())
            {
                Touch touchp1 = Input.GetTouch(0);
                if (touchp1.phase == TouchPhase.Began)
                {
                    currentTouch = touchp1;
                    currentTapTime = Time.time;
                    oneFootJumping.Play();
                    Invoke("ChangeToTutorialP2State", 0.5f);
                }
                else if (touchp1.phase == TouchPhase.Ended)
                {
                    previousTouch = touchp1;
                    lastTapTime = currentTapTime;
                }
            }

            if (GameManager.GetTutorialP2())
            {
                Touch touchp2 = Input.GetTouch(0);
                if (touchp2.phase == TouchPhase.Began)
                {
                    currentTouch = touchp2;
                    currentTapTime = Time.time;
                    oneFootJumping.Play();
                    if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                    {
                        manJumping.Play();
                        Invoke("ChangeToTutorialP3State", 0.5f);
                    }

                }
                else if (touchp2.phase == TouchPhase.Ended)
                {
                    previousTouch = touchp2;
                    lastTapTime = currentTapTime;
                }
            }

            if (GameManager.GetTutorialP3())
            {
                Touch touchp3 = Input.GetTouch(0);
                if (touchp3.phase == TouchPhase.Began)
                {
                    currentTouch = touchp3;
                    currentTapTime = Time.time;
                    oneFootJumping.Play();
                    if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                    {
                        manJumping.Play();
                        saltoPerfeito.PlayDelayed(manJumping.clip.length);
                        Invoke("ChangeToTutorialP4State", 1.5f);
                    }

                }
                else if (touchp3.phase == TouchPhase.Ended)
                {
                    previousTouch = touchp3;
                    lastTapTime = currentTapTime;
                }
            }

            if (GameManager.GetTutorialP4())
            {
                Touch touchp4 = Input.GetTouch(0);
                if (touchp4.phase == TouchPhase.Began)
                {
                    currentTouch = touchp4;
                    currentTapTime = Time.time;
                    oneFootJumping.Play();
                    if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                    {
                        manJumping.Play();
                        saltoPerfeito.PlayDelayed(manJumping.clip.length);

                        Invoke("ChangeToTutorialP5State", 1.5f);
                    }

                }
                else if (touchp4.phase == TouchPhase.Ended)
                {
                    previousTouch = touchp4;
                    lastTapTime = currentTapTime;
                }
            }
        }
    }

    public IEnumerator WaitForTouch(float duration, Action DoAfter)
    {
        yield return new WaitForSeconds(duration);
        DoAfter();
    }

    private void DoAfter()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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

    //obtem o numero de saltos falhados
    public static int GetSaltosFalhados()
    {
        return n_saltos_falhados;
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

    public static void ButtonIntroducaoToHighlightFalse()
    {
        buttonIntroducaoToHighlight = false;
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

    public static void ButtonInstrucoesBackToNormalFalse()
    {
        buttonInstrucoesBackToNormal = false;
    }

    public static bool ButtonJogarToHighlight()
    {
        return buttonJogarToHighlight;
    }

    public static bool TutorialBackToNormal()
    {
        return tutorialBackToNormal;
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

    public IEnumerator WaitForEndSound(float duration, Action DoAfterDescricao)
    {
        yield return new WaitForSeconds(duration);
        DoAfterDescricao();
    }

    public void DoAfterDescricao()
    {
        cordaSound.Stop();
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

    private void ChangeToTutorialP1State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP1);
    }

    private void ChangeToTutorialP2State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP2);
    }

    private void ChangeToTutorialP3State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP3);
    }

    private void ChangeToTutorialP4State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP4);
    }

    private void ChangeToTutorialP5State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP5);
    }
}
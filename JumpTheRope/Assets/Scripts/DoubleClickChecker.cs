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
    private float timeBetweenTouches; // tempo entre os toques, para quanto maior mais alto salta (para qdo for implementar isto)
    private float height; // altura a saltar que depende do tempo entre os toques (qto menor tempo, mais baixo o salto)
    private float lastTapTime;
    private int doubleTapCircle;

    public AudioSource manJumping; // ManJumping
    public AudioSource oneFootJumping; // tap
    public AudioSource saltoPerfeito; // salto perfeito
    public AudioSource inicioJogo;
    public GameManager GameManagerGO;
    private static int j = 0; // para verificar que faz duplo salto no tutorial 3x

    public AudioSource introducaoSound; // som do botao da introducao
    public AudioSource cordaSound; // som da corda (usado no ButtonCorda1)
    public AudioSource descricaoCordaSound; // descricao da corda (usado no ButtonCorda1)
    public AudioSource umaPernaSound; // som de uma perna (usado no ButtonCorda2)
    public AudioSource descricaoUmaPerna; // descricao do som (usado no ButtonCorda2)
    public AudioSource duasPernasSound; // som de um salto com 2 pernas (usado no ButtonCorda3)
    public AudioSource descricaoDuasPernas; // descricao do som (usado no ButtonCorda3)

    private static int swipeJogarToIntroducao, swipeInstrucoesToJogar, swipeIntroToInstr; // swipe left opening
    private static int swipeJogarToInstr, swipeIntroToJogar, swipeInstrToIntro; // swipe right opening
    private static int swipeJogarToCorda1, swipeCorda1ToCorda2, swipeCorda2ToCorda3, swipeCorda3ToCorda4, swipeCorda4ToTutorial; // swipe left instrucoes
    private static int stopSounds;
    private static bool cancelJogarAction, cancelTutorialAction;

    private float screenDPI;
    private float increaseSpeedTimer;

    private Vector2 swipeDelta;
    public GameObject buttonJogar;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Touch CurrentTouch { get { return currentTouch; } }
    public Touch PreviousTouch { get { return previousTouch; } }
    public float CurrentTapTime { get { return currentTapTime; } }
    public float TimeBetweenTouches { get { return timeBetweenTouches; } }
    public float LastTapTime { get { return lastTapTime; } }
    public float Height { get { return height; } }
    public int DoubleTapCircle { get { return doubleTapCircle; } }

    void Start()
    {
        n_saltos_perfeitos = n_saltos_normais = pontuacaoTotal = 0;
        doubleTapCircle = doubleTapRadius * doubleTapRadius;

        screenDPI = Screen.dpi;
        height = 5; // altura padrão

        swipeJogarToIntroducao = swipeInstrucoesToJogar = swipeIntroToInstr = 0;
        swipeJogarToInstr = swipeIntroToJogar = swipeInstrToIntro = 0;
        swipeJogarToCorda1 = swipeCorda1ToCorda2 = swipeCorda2ToCorda3 = swipeCorda3ToCorda4 = swipeCorda4ToTutorial = 0;
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

            if (GameManager.GetStarted() || GameManager.GetInstrucoes())
            {
                ButtonCorda1.SetCheckToStop();
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
            }

            if(GameManager.GetTutorialP1() || GameManager.GetTutorialP2() || GameManager.GetTutorialP3() || GameManager.GetTutorialP4() || GameManager.GetTutorialP5())
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.CancelTutorial);
            }
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
                    // salta sempre 5m x tempo entre os toques (mais tempo, mais alto)
                    height *= timeBetweenTouches;
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
                        inicioJogo.Play();
                        Invoke("StartGame", inicioJogo.clip.length);
                    }

                    else if (ButtonIntroducao.CheckForHighlighted() == 1)
                    {
                        if (!introducaoSound.isPlaying)
                            introducaoSound.Play();
                    }

                    else if (ButtonInstrucoes.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instrucoes);
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

                        //swipe left no menu opening
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            System.Diagnostics.Debug.WriteLine("ENTREI SWIPE LEFT DOUBLE CLICK CHECKER");
                            // se o botao jogar estiver highlighted
                            if (ButtonJogar.CheckForHighlighted() == 1)
                                swipeJogarToIntroducao = 1;

                            // se o botao instrucoes estiver highlighted
                            if (ButtonInstrucoes.CheckForHighlighted() == 1)
                                swipeInstrucoesToJogar = 1;

                            // se o botao introducao estiver highlighted
                            if (ButtonIntroducao.CheckForHighlighted() == 1)
                                swipeIntroToInstr = 1;
                        }

                        //swipe right no menu opening
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            // se o botao jogar estiver highlighted
                            if (ButtonJogar.CheckForHighlighted() == 1)
                                swipeJogarToInstr = 1;

                            // se o botao introducao estiver highlighted
                            if (ButtonIntroducao.CheckForHighlighted() == 1)
                                swipeIntroToJogar = 1;

                            // se o botao instrucoes estiver highlighted
                            if (ButtonInstrucoes.CheckForHighlighted() == 1)
                                swipeInstrToIntro = 1;
                        }
                    }
                }
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
                        if (Tutorial.CheckForHighlighted() == 1)
                        {
                            Invoke("ChangeToTutorialP1State", 0.5f);
                        }

                        else if (ButtonJogar.CheckForHighlighted() == 1)
                        {
                            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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

                            //swipe left no menu instrucoes
                            if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                            {

                            }

                            //swipe right no menu instrucoes
                            if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                            {
                                if (ButtonJogar.CheckForHighlighted() == 1)
                                    swipeJogarToCorda1 = 1;
                                if (ButtonCorda1.CheckForHighlighted() == 1)
                                    swipeCorda1ToCorda2 = 1;
                                if (ButtonCorda2.CheckForHighlighted() == 1)
                                    swipeCorda2ToCorda3 = 1;
                                if (ButtonCorda3.CheckForHighlighted() == 1)
                                    swipeCorda3ToCorda4 = 1;                                
                                if (ButtonCorda4.CheckForHighlighted() == 1)
                                    swipeCorda4ToTutorial = 1;
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
                        j++;

                        if(j == 3)
                        {
                            Invoke("ChangeToTutorialP5State", 1.5f);
                        }                        
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

    public static int GetJ()
    {
        return j;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO JOGAR PARA O BOTAO INTRODUCAO
    public static int SwipeJogarToIntro()
    {
        return swipeJogarToIntroducao;
    }

    public static void SwipeJogarToIntroReset()
    {
        swipeJogarToIntroducao = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO INSTRUCOES PARA O BOTAO JOGAR
    public static int SwipeInstrucoesToJogar()
    {
        return swipeInstrucoesToJogar;
    }

    public static void SwipeInstrucoesToJogarReset()
    {
        swipeInstrucoesToJogar = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO INTRODUCAO PARA O BOTAO INSTRUCOES
    public static int SwipeIntroToInstr()
    {
        return swipeIntroToInstr;
    }

    public static void SwipeIntroToInstrReset()
    {
        swipeIntroToInstr = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO JOGAR PARA O BOTAO INSTRUCOES
    public static int SwipeJogarToInstr()
    {
        return swipeJogarToInstr;
    }

    public static void SwipeJogarToInstrReset()
    {
        swipeJogarToInstr = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO INTRODUCAO PARA O BOTAO JOGAR
    public static int SwipeIntroToJogar()
    {
        return swipeIntroToJogar;
    }

    public static void SwipeIntroToJogarReset()
    {
        swipeIntroToJogar = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO INSTRUCAO PARA O BOTAO INTRODUCAO
    public static int SwipeInstrToIntro()
    {
        return swipeInstrToIntro;
    }

    public static void SwipeInstrToIntroReset()
    {
        swipeInstrToIntro = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO JOGAR PARA O BOTAO CORDA1
    public static int SwipeJogarToCorda1()
    {
        return swipeJogarToCorda1;
    }

    public static void SwipeJogarToCorda1Reset()
    {
        swipeJogarToCorda1 = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO CORDA1 PARA O BOTAO CORDA2
    public static int SwipeCorda1ToCorda2()
    {
        return swipeCorda1ToCorda2;
    }

    public static void SwipeCorda1ToCorda2Reset()
    {
        swipeCorda1ToCorda2 = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO CORDA2 PARA O BOTAO CORDA3
    public static int SwipeCorda2ToCorda3()
    {
        return swipeCorda2ToCorda3;
    }

    public static void SwipeCorda2ToCorda3Reset()
    {
        swipeCorda2ToCorda3 = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO CORDA3 PARA O BOTAO CORDA4
    public static int SwipeCorda3ToCorda4()
    {
        return swipeCorda3ToCorda4;
    }

    public static void SwipeCorda3ToCorda4Reset()
    {
        swipeCorda3ToCorda4 = 0;
    }

    // FUNCOES PARA VER SE HOUVE SWIPE DO BOTAO CORDA4 PARA O BOTAO TUTORIAL
    public static int SwipeCorda4ToTutorial()
    {
        return swipeCorda4ToTutorial;
    }

    public static void SwipeCorda4ToTutorialReset()
    {
        swipeCorda4ToTutorial = 0;
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

    public IEnumerator WaitForEndSound(float duration, Action DoAfterDescricao)
    {
        yield return new WaitForSeconds(duration);
        DoAfterDescricao();
    }

    public void DoAfterDescricao()
    {
        cordaSound.Stop();
    }

    // FAZER COM QUE DUPLO TOQUE EM CIMA DO BOTAO NAO FAÇA A MESMA COISA 2X
    public static bool GetCancelJogarAction()
    {
        return cancelJogarAction;
    }

    public static void ResetCancelJogarAction()
    {
        cancelJogarAction = false;
    }

    public static bool GetCancelTutorialAction()
    {
        return cancelTutorialAction;
    }

    public static void ResetCancelTutorialAction()
    {
        cancelTutorialAction = false;
    }

    private int CheckForDoubleTap(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
    {
        int deltaX = (int)currentTouch.position.x - (int)previousTouch.position.x;
        int deltaY = (int)currentTouch.position.y - (int)previousTouch.position.y;
        timeBetweenTouches = currentTapTime - previousTapTime;
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

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }
}
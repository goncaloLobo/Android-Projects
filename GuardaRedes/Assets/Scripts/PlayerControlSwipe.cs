using UnityEngine;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    public AudioSource hitCenterUp; // som de bater no topo
    public AudioSource hitCenterDown; // som de bater em baixo

    public AudioSource[] sounds;
    public AudioSource introducao;
    public AudioSource inicioJogo;

    // CONSTANTES PARA OS VALORES
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int doubleTapRadius = Configuration.DoubleTapRadius();

    private Vector2 startGlovePosition, endGlovePosition, swipeDelta, stTouch, sndTouch, turningPoint;
    private Vector3 rotationEuler;
    private Touch currentTouch, previousTouch;
    private Touch startTouch, endTouch;
    private float startTouchTime, endTouchTime;
    private float currentTapTime, lastTapTime, flytime;
    private float flightDuration = 0.1f;
    private float screenDPI;
    private static bool jogarCancelAction, hasEntered, resetGloves, tutorialCancelAction;
    private static bool confirmedSwipeLeft, confirmedSwipeRight, confirmedSwipeUp, confirmedSwipeDown;
    private bool entreiResetGloves;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartGlovePosition { get { return startGlovePosition; } }
    public Vector2 EndGlovePosition { get { return endGlovePosition; } }
    public Vector2 StartTouch { get { return stTouch; } }
    public Vector2 SecondTouch { get { return sndTouch; } }

    public void Init()
    {
        //mostra as luvas do gr no ecra
        gameObject.SetActive(true);
        screenDPI = Screen.dpi;

        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
        jogarCancelAction = hasEntered = false;
    }

    void Update()
    {
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        // accoes para o botao de voltar para trás do android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.GetOpening())
            {
                Application.Quit();
            }

            if (GameManager.GetStarted())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if (GameManager.GetInstructions())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
        }

        // DETETA O INPUT PARA O ESTADO INICIAL (DUPLO TOQUE NOS BOTOES QUE É SUPOSTO)
        if (Input.touchCount > 0 && GameManager.GetOpening())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    if (MyButton.CheckForHighlighted() == 1 && !hasEntered)
                    {
                        hasEntered = true;
                        inicioJogo.Play();
                        Invoke("StartGame", inicioJogo.clip.length);
                    }

                    if (ButtonInstrucoes.CheckForHighlighted() == 1)
                    {
                        Invoke("ChangeToInstructionsState", 0f);
                    }

                    if (ButtonIntroducao.CheckForHighlighted() == 1)
                    {
                        if (!introducao.isPlaying)
                            introducao.Play();
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

        if (Input.touchCount > 0 && GameManager.GetInstructions())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    if (ButtonDefenderBaixo.CheckForHighlighted() == 1)
                    {
                        inicioJogo.Play();
                        Invoke("ChangeToSwipeDownState", inicioJogo.clip.length);
                    }

                    if (ButtonDefenderCima.CheckForHighlighted() == 1)
                    {
                        inicioJogo.Play();
                        Invoke("ChangeToSwipeUpState", inicioJogo.clip.length);
                    }

                    if (ButtonDefenderEsquerda.CheckForHighlighted() == 1)
                    {
                        inicioJogo.Play();
                        Invoke("ChangeToSwipeLeftState", inicioJogo.clip.length);
                    }

                    if (ButtonDefenderDireita.CheckForHighlighted() == 1)
                    {
                        inicioJogo.Play();
                        Invoke("ChangeToSwipeRightState", inicioJogo.clip.length);
                    }

                    if(ButtonTutorial.CheckForHighlighted() == 1)
                    {
                        tutorialCancelAction = true;
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

                // DAQUI PARA BAIXO É SWIPES, JÁ VOLTAMOS CÁ
            }
        }

        // DETETA O INPUT PARA QUANDO ESTÁ A JOGAR
        if (Input.touchCount > 0 && GameManager.GetStarted())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch;
                startTouchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouch = touch;
                endTouchTime = Time.time;
                int deltaX = (int)endTouch.position.x - (int)startTouch.position.x;
                int deltaY = (int)endTouch.position.y - (int)startTouch.position.y;

                int distance = (deltaX * deltaX) + (deltaY * deltaY);
                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = endTouchTime - startTouchTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        // swipe!!!
                        swipeDelta = new Vector2(deltaX, deltaY);

                        //normalize the 2d vector
                        swipeDelta.Normalize();

                        //swipe left
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                confirmedSwipeLeft = true;
                            }

                            // roda as luvas do gr para a esquerda
                            rotationEuler += Vector3.forward * 30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                confirmedSwipeRight = true;
                            }

                            // roda as luvas do gr para a direita
                            rotationEuler += Vector3.forward * -30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }
                    }
                }
            }
        }

        if (Input.touchCount > 0 && GameManager.GetSwipeDown())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetSwipeLeft())
        {
            Touch touchSwipeLeft = Input.GetTouch(0);
            if (touchSwipeLeft.phase == TouchPhase.Began)
            {
                startTouch = touchSwipeLeft;
                startTouchTime = Time.time;
            }
            else if (touchSwipeLeft.phase == TouchPhase.Moved)
            {

            }
            else if (touchSwipeLeft.phase == TouchPhase.Ended)
            {
                endTouch = touchSwipeLeft;
                endTouchTime = Time.time;
                int deltaX = (int)endTouch.position.x - (int)startTouch.position.x;
                int deltaY = (int)endTouch.position.y - (int)startTouch.position.y;

                int distance = (deltaX * deltaX) + (deltaY * deltaY);
                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = endTouchTime - startTouchTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        swipeDelta = new Vector2(deltaX, deltaY);
                        swipeDelta.Normalize();
                        //swipe left
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                confirmedSwipeLeft = true;
                            }

                            // roda as luvas do gr para a esquerda
                            rotationEuler += Vector3.forward * 30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }
                    }
                }
            }
        }

        if (Input.touchCount > 0 && GameManager.GetSwipeRight())
        {
            Touch touchSwipeRight = Input.GetTouch(0);
            if (touchSwipeRight.phase == TouchPhase.Began)
            {
                startTouch = touchSwipeRight;
                startTouchTime = Time.time;
            }
            else if (touchSwipeRight.phase == TouchPhase.Moved)
            {

            }
            else if (touchSwipeRight.phase == TouchPhase.Ended)
            {
                endTouch = touchSwipeRight;
                endTouchTime = Time.time;
                int deltaX = (int)endTouch.position.x - (int)startTouch.position.x;
                int deltaY = (int)endTouch.position.y - (int)startTouch.position.y;

                int distance = (deltaX * deltaX) + (deltaY * deltaY);
                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = endTouchTime - startTouchTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        // swipe!!!
                        swipeDelta = new Vector2(deltaX, deltaY);
                        swipeDelta.Normalize();

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                confirmedSwipeRight = true;
                                //ResetGlovesPosition(); // volta a por as luvas no meio do ecrã
                            }

                            // roda as luvas do gr para a direita
                            rotationEuler += Vector3.forward * -30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }
                    }
                }
            }
        }

        if (Input.touchCount > 0 && GameManager.GetSwipeUp())
        {

        }

        // AQUI FAZ A PRIMEIRA DEFESA PARA A DIREITA
        if (Input.touchCount > 0 && GameManager.GetTutorialP1())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch;
                startTouchTime = Time.time;
                if (CheckForDoubleTap(startTouchTime, endTouchTime, startTouch, endTouch) == 0)
                {

                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouch = touch;
                endTouchTime = Time.time;
                int deltaX = (int)endTouch.position.x - (int)startTouch.position.x;
                int deltaY = (int)endTouch.position.y - (int)startTouch.position.y;

                int distance = (deltaX * deltaX) + (deltaY * deltaY);
                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = endTouchTime - startTouchTime;
                    if ((Mathf.Abs(deltaX / difference) > minimumFlingVelocity) | (Mathf.Abs(deltaY / difference) > minimumFlingVelocity))
                    {
                        // swipe!!!
                        swipeDelta = new Vector2(deltaX, deltaY);

                        //normalize the 2d vector
                        swipeDelta.Normalize();

                        //swipe left
                        if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                            }

                            // roda as luvas do gr para a esquerda
                            rotationEuler += Vector3.forward * 30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                            }

                            // roda as luvas do gr para a direita
                            rotationEuler += Vector3.forward * -30;
                            transform.rotation = Quaternion.Euler(rotationEuler);
                        }
                    }
                }
            }
        }

        if (Input.touchCount > 0 && GameManager.GetTutorialP2())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetTutorialP3())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetTutorialP4())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetTutorialP5())
        {

        }

        // depois de uma defesa ou golo, volta a por as luvas no centro do ecra
        if (resetGloves && !entreiResetGloves)
        {
            entreiResetGloves = true;
            flytime = 0f;
            startGlovePosition = transform.position;
            if (confirmedSwipeLeft)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
            if (confirmedSwipeRight)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);

            while (flytime < flightDuration)
            {
                flytime += Time.deltaTime;
                transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
            }

            rotationEuler = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(rotationEuler);

            ResetResetGloves();
            ResetConfirmedSwipeLeft();
            ResetConfirmedSwipeRight();
        }
    }

    // passa o bool resetGloves a true, chamado no GameManager depois de uma defesa ou de um golo
    public static void ResetGloves()
    {
        resetGloves = true;
    }

    public void ResetResetGloves()
    {
        resetGloves = false;
        entreiResetGloves = false;
    }

    public static bool GetConfirmedSwipeLeft()
    {
        return confirmedSwipeLeft;
    }

    public static void ResetConfirmedSwipeLeft()
    {
        confirmedSwipeLeft = false;
    }

    public static bool GetConfirmedSwipeRight()
    {
        return confirmedSwipeRight;
    }

    public static void ResetConfirmedSwipeRight()
    {
        confirmedSwipeRight = false;
    }

    public static bool GetConfirmedSwipeUp()
    {
        return confirmedSwipeUp;
    }

    public static void ResetConfirmedSwipeUp()
    {
        confirmedSwipeUp = false;
    }

    public static bool GetConfirmedSwipeDown()
    {
        return confirmedSwipeDown;
    }

    public static void ResetConfirmedSwipeDown()
    {
        confirmedSwipeDown = false;
    }

    public static bool CancelTutorialAction()
    {
        return tutorialCancelAction;
    }

    public static void ResetTutorialAction()
    {
        tutorialCancelAction = false;
    }

    private int CheckForDoubleTap(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
    {
        int deltaX = (int)currentTouch.position.x - (int)previousTouch.position.x;
        int deltaY = (int)currentTouch.position.y - (int)previousTouch.position.y;

        // diferença entre os toques superior a 1s
        if (currentTapTime - previousTapTime > doubleTapDelta)
        {
            return -1;
        }

        // se o duplo toque "perfeito" estiver dentro do circulo aceitavel, entao retorna 0
        if (deltaX * deltaX + deltaY * deltaY < (doubleTapRadius * doubleTapRadius))
            return 0;
        return -1;
    }

    private void ChangeToInstructionsState()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
    }

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
    }

    private void ChangeToSwipeDownState()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeDown);
    }

    private void ChangeToSwipeUpState()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeUp);
    }

    private void ChangeToSwipeLeftState()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeLeft);
    }

    private void ChangeToSwipeRightState()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeRight);
    }
}

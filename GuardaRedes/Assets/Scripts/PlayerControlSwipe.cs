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
    private Touch startTouch, endTouch, currentTouchMove, previousTouchMove, currentLMove, turningPointTouch;
    private float startTouchTime, endTouchTime;
    private float currentTapTime, lastTapTime, flytime;
    private float flightDuration = 0.1f;
    private float screenDPI;
    private static bool jogarCancelAction, hasEntered, resetGloves, tutorialCancelAction;
    private static bool confirmedSwipeLeft, confirmedSwipeRight, confirmedSwipeUp, confirmedSwipeDown;
    private bool leftThenUpSwipe, leftThenDownSwipe, rightThenDownSwipe, upThenLeftSwipe, upThenRightSwipe, downThenRightSwipe, downThenLeftSwipe;
    private bool entreiResetGloves, rightThenUpSwipe, firstTime = true;
    private int deltaXMoved = 0, deltaYMoved = 0;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartGlovePosition { get { return startGlovePosition; } }
    public Vector2 EndGlovePosition { get { return endGlovePosition; } }
    public Vector2 StartTouch { get { return stTouch; } }
    public Vector2 SecondTouch { get { return sndTouch; } }

    public void Init()
    {
        //mostra as luvas do gr no ecra
        gameObject.SetActive(true);
        sounds = GetComponents<AudioSource>();
        introducao = sounds[0];
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
                currentTouchMove = touch;

                // swipe para a esquerda
                if (currentTouchMove.position.x - startTouch.position.x < 0)
                {
                    if (currentTouchMove.position.y - previousTouchMove.position.y < 5f && currentTouchMove.position.y - previousTouchMove.position.y > -5f && !leftThenUpSwipe && !leftThenDownSwipe)
                    {
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            System.Diagnostics.Debug.WriteLine("confirmedSwipeLeft");
                            confirmedSwipeLeft = true;
                        }
                    }

                    // leftThenUpSwipe
                    if (currentTouchMove.position.y - previousTouchMove.position.y > 5f && currentTouchMove.position.x - previousTouchMove.position.x > -5f && previousTouchMove.position.x != 0 && previousTouchMove.position.y != 0 && confirmedSwipeLeft)
                    {
                        if (firstTime)
                        {
                            turningPointTouch = currentTouchMove;
                            firstTime = false;
                        }

                        deltaXMoved = (int)currentTouchMove.position.x - (int)turningPointTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)turningPointTouch.position.y;
                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            leftThenUpSwipe = true;
                        }
                    }

                    // leftThenDownSwipe
                    if (currentTouchMove.position.y - previousTouchMove.position.y < -5f && currentTouchMove.position.x - previousTouchMove.position.x < 5f && currentTouchMove.position.x - previousTouchMove.position.x > -5f && previousTouchMove.position.x != 0 && previousTouchMove.position.y != 0 && confirmedSwipeLeft)
                    {
                        if (firstTime)
                        {
                            turningPointTouch = currentTouchMove;
                            firstTime = false;
                        }

                        deltaXMoved = (int)currentTouchMove.position.x - (int)turningPointTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)turningPointTouch.position.y;
                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            leftThenDownSwipe = true;
                        }
                    } 
                }
                // swipe para a direita
                if (currentTouchMove.position.x - startTouch.position.x > 0)
                {
                    if (currentTouchMove.position.y - previousTouchMove.position.y < 5f && currentTouchMove.position.y - previousTouchMove.position.y > -5f && !rightThenDownSwipe)
                    {
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            System.Diagnostics.Debug.WriteLine("confirmedSwipeRight");
                            confirmedSwipeRight = true;
                        }
                    }

                    // rightThenDownSwipe
                    if (currentTouchMove.position.y - previousTouchMove.position.y < -5f && currentTouchMove.position.x - previousTouchMove.position.x < 5f && currentTouchMove.position.x - previousTouchMove.position.x > -5f && previousTouchMove.position.x != 0 && previousTouchMove.position.y != 0 && confirmedSwipeRight)
                    {
                        if (firstTime)
                        {
                            turningPointTouch = currentTouchMove;
                            firstTime = false;
                        }

                        deltaXMoved = (int)currentTouchMove.position.x - (int)turningPointTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)turningPointTouch.position.y;
                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            rightThenDownSwipe = true;
                        }
                    }
                }
                
                // swipe para baixo
                if (currentTouchMove.position.y - startTouch.position.y < 0 && !confirmedSwipeLeft && !confirmedSwipeRight)
                {
                    if (currentTouchMove.position.x - previousTouchMove.position.x < 5f && currentTouchMove.position.x - previousTouchMove.position.x > -5f && !downThenRightSwipe && !downThenLeftSwipe)
                    {                        
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            System.Diagnostics.Debug.WriteLine("confirmedSwipeDown");
                            confirmedSwipeDown = true;
                        }
                    }
                    /*
                    // downThenRightSwipe
                    if (currentTouchMove.position.x - previousTouchMove.position.x > 5f && currentTouchMove.position.y - previousTouchMove.position.y < 5f && currentTouchMove.position.y - previousTouchMove.position.y > -5f && previousTouchMove.position.x != 0 && previousTouchMove.position.y != 0 && confirmedSwipeDown)
                    {
                        System.Diagnostics.Debug.WriteLine("entrei downThenRightSwipe");
                        if (firstTime)
                        {
                            turningPointTouch = currentTouchMove;
                            firstTime = false;
                        }

                        deltaXMoved = (int)currentTouchMove.position.x - (int)turningPointTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)turningPointTouch.position.y;
                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            System.Diagnostics.Debug.WriteLine("downThenRightSwipe");
                            downThenRightSwipe = true;
                        }
                    }
                    */

                    // downThenLeftSwipe
                    if (currentTouchMove.position.x - previousTouchMove.position.x < -5f && currentTouchMove.position.y - previousTouchMove.position.y < 5f && currentTouchMove.position.y - previousTouchMove.position.y > -5f && previousTouchMove.position.x != 0 && previousTouchMove.position.y != 0 && confirmedSwipeDown)
                    {
                        if (firstTime)
                        {
                            turningPointTouch = currentTouchMove;
                            firstTime = false;
                        }

                        deltaXMoved = (int)currentTouchMove.position.x - (int)turningPointTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)turningPointTouch.position.y;
                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            System.Diagnostics.Debug.WriteLine("downThenLeftSwipe");
                            downThenLeftSwipe = true;
                        }
                    }
                }

                previousTouchMove = currentTouchMove;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (leftThenUpSwipe && confirmedSwipeLeft)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y + 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a esquerda
                    rotationEuler += Vector3.forward * 30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                if (!leftThenUpSwipe && confirmedSwipeLeft && !leftThenDownSwipe)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a esquerda
                    rotationEuler += Vector3.forward * 30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                if(leftThenDownSwipe && confirmedSwipeLeft && !leftThenUpSwipe)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a esquerda
                    rotationEuler += Vector3.forward * 30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                if(rightThenDownSwipe && confirmedSwipeRight)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, startGlovePosition.y - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a direita
                    rotationEuler += Vector3.forward * -30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                if(!rightThenDownSwipe && confirmedSwipeRight)
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

                if(downThenRightSwipe && confirmedSwipeDown)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, startGlovePosition.y - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a direita
                    rotationEuler += Vector3.forward * -30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                if(!downThenRightSwipe && confirmedSwipeDown && !downThenLeftSwipe)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x, startGlovePosition.y - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }
                }

                if (!downThenRightSwipe && confirmedSwipeDown && downThenLeftSwipe)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a esquerda
                    rotationEuler += Vector3.forward * 30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
                }

                    /*
                    if (confirmedSwipeDown)
                    {
                        flytime = 0f;
                        startGlovePosition = transform.position;
                        endGlovePosition = new Vector2(startGlovePosition.x, startGlovePosition.y - 2f);
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }
                    }

                    if (rightThenDownSwipe)
                    {
                        flytime = 0f;
                        startGlovePosition = transform.position;
                        endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, startGlovePosition.y - 2f);
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }

                        // roda as luvas do gr para a direita
                        rotationEuler += Vector3.forward * -30;
                        transform.rotation = Quaternion.Euler(rotationEuler);
                    }

                    if (downThenRightSwipe)
                    {
                        flytime = 0f;
                        startGlovePosition = transform.position;
                        endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, startGlovePosition.y - 2f);
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }

                        // roda as luvas do gr para a direita
                        rotationEuler += Vector3.forward * -30;
                        transform.rotation = Quaternion.Euler(rotationEuler);
                    }

                    if (downThenLeftSwipe)
                    {
                        flytime = 0f;
                        startGlovePosition = transform.position;
                        endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y - 2f);
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }

                        // roda as luvas do gr para a esquerda
                        rotationEuler += Vector3.forward * 30;
                        transform.rotation = Quaternion.Euler(rotationEuler);
                    }
                    */
                }
        }

        // TREINAR SWIPES PARA BAIXO
        if (Input.touchCount > 0 && GameManager.GetSwipeDown())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch;
                startTouchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchMove = touch;

                // swipe para baixo
                if (currentTouchMove.position.y - startTouch.position.y < 0)
                {
                    if (currentTouchMove.position.x - previousTouchMove.position.x < -5f)
                    {
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            confirmedSwipeDown = true;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (confirmedSwipeDown)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x, startGlovePosition.x - 2f);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }
                }
            }
        }

        // TREINAR SWIPES PARA A ESQUERDA
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
                currentTouchMove = touchSwipeLeft;

                // swipe para a esquerda
                if (currentTouchMove.position.x - startTouch.position.x < 0)
                {
                    if (currentTouchMove.position.y - previousTouchMove.position.y < 5f)
                    {
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            confirmedSwipeLeft = true;
                        }
                    }
                }
            }
            else if (touchSwipeLeft.phase == TouchPhase.Ended)
            {
                if (confirmedSwipeLeft)
                {
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, startGlovePosition.y);
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas do gr para a esquerda
                    rotationEuler += Vector3.forward * 30;
                    transform.rotation = Quaternion.Euler(rotationEuler);
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
                currentTouchMove = touchSwipeRight;

                // swipe para a direita
                if (currentTouchMove.position.x - startTouch.position.x > 0)
                {
                    if (currentTouchMove.position.y - previousTouchMove.position.y < 5f)
                    {
                        deltaXMoved = (int)currentTouchMove.position.x - (int)startTouch.position.x;
                        deltaYMoved = (int)currentTouchMove.position.y - (int)startTouch.position.y;

                        int distance = (deltaXMoved * deltaXMoved) + (deltaYMoved * deltaYMoved);
                        screenDPI = Screen.dpi;
                        if (distance > (16.0f * screenDPI + 0.5f))
                        {
                            confirmedSwipeRight = true;
                        }
                    }
                }
            }
            else if (touchSwipeRight.phase == TouchPhase.Ended)
            {
                if (confirmedSwipeRight)
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

        if (Input.touchCount > 0 && GameManager.GetSwipeDownThenLeft())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetSwipeDownThenRight())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetSwipeLeftThenDown())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetSwipeLeftThenUp())
        {

        }

        if (Input.touchCount > 0 && GameManager.GetSwipeRightThenDown())
        {

        }

        // depois de uma defesa ou golo, volta a por as luvas no centro do ecra
        if (resetGloves && !entreiResetGloves)
        {
            entreiResetGloves = true;
            flytime = 0f;
            startGlovePosition = transform.position;

            if (leftThenUpSwipe && confirmedSwipeLeft)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y - 2f);
            if(!leftThenUpSwipe && confirmedSwipeLeft)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
            if(leftThenDownSwipe && confirmedSwipeLeft)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y + 2f);
            if(rightThenDownSwipe && confirmedSwipeRight)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y + 2f);
            if(!rightThenDownSwipe && confirmedSwipeRight)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
            if(downThenRightSwipe && confirmedSwipeDown)
            {
                System.Diagnostics.Debug.WriteLine("olha eu aqui");
                System.Diagnostics.Debug.WriteLine("startGlovePosition.x: " + startGlovePosition.x);
                System.Diagnostics.Debug.WriteLine("transform.position.y: " + transform.position.y);
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y + 2f);
            }

            if(!downThenRightSwipe && confirmedSwipeDown && !downThenLeftSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x, transform.position.y + 2f);
            if(!downThenRightSwipe && confirmedSwipeDown && downThenLeftSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y + 2f);
            /*
            if (confirmedSwipeLeft)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
            if (confirmedSwipeRight)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
            if (leftThenUpSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y - 2f);
            if (leftThenDownSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y + 2f);
            if(rightThenDownSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y + 2f);
            if(rightThenUpSwipe)
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y - 2f);
            */

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
            ResetLeftThenUpSwipe();
            ResetLeftThenDownSwipe();
            ResetRightThenDownSwipe();
            ResetUpThenRightSwipe();
            ResetDownThenRightSwipe();
            ResetDownThenLeftSwipe();
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

    public void ResetLeftThenUpSwipe()
    {
        leftThenUpSwipe = false;
    }

    public void ResetLeftThenDownSwipe()
    {
        leftThenDownSwipe = false;
    }
    
    public void ResetRightThenDownSwipe()
    {
        rightThenDownSwipe = false;
    }

    public void ResetUpThenLeftSwipe()
    {
        upThenLeftSwipe = false;
    }

    public void ResetUpThenRightSwipe()
    {
        upThenRightSwipe = false;
    }

    public void ResetDownThenRightSwipe()
    {
        downThenRightSwipe = false;
    }

    public void ResetDownThenLeftSwipe()
    {
        downThenLeftSwipe = false;
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

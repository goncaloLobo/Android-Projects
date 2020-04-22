using UnityEngine;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource hitCenterUp; // som de bater no topo
    public AudioSource hitCenterDown; // som de bater em baixo

    // CONSTANTES PARA OS VALORES
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int doubleTapRadius = Configuration.DoubleTapRadius();

    private Vector2 startGlovePosition, endGlovePosition, swipeDelta, stTouch, sndTouch, turningPoint;
    private Touch currentTouch, previousTouch;
    private Touch startTouch, endTouch;
    private float startTouchTime, endTouchTime;
    private float currentTapTime, lastTapTime, flytime;
    private float flightDuration = 0.1f;
    private float screenDPI;

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
    }
    
    void Start()
    {

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
                    // VERIFICA SE ESTA HIGHLIGHTED E FAZ A ACAO DO DUPLO TOQUE
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
                            if (endGlovePosition.x > border.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                }
                            }
                            else
                            {
                                // som de bater na parede no lado esquerdo
                                hitWallSoundLeft.Play();
                            }
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startGlovePosition = transform.position;
                            endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                            if (endGlovePosition.x < border2.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                                }
                            }
                            else
                            {
                                // som de bater na parede no lado direito
                                hitWallSoundRight.Play();
                            }
                        }
                    }
                }
            }
        }
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
}

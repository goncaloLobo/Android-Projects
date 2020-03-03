using UnityEngine;
using UnityEngine.UI;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager

    private Vector3 startRocketPosition, endRocketPosition;
    private float flytime;
    private float flightDuration = 0.1f;

    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    public GameObject ExplosionGO;
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score

    public AudioSource swipeSound;
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource bonusLeft; // som bonus esq
    public AudioSource bonusMid; // som bonus esq
    public AudioSource bonusRight; // som bonus esq

    public Text LivesUIText;
    const int MaxLives = 3;
    int lives = 0;

    public float speed = 1f;

    private bool tap, doubleTap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, stTouch;
    private float lastTap;
    private float sqrDeadzone;
    private float deadzone = 100.0f;
    private float doubleTapDelta = 0.5f;

    public bool Tap { get { return tap;  } }
    public bool DoubleTap { get { return doubleTap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");

        //mostra a nave do jogador no ecra
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        sqrDeadzone = deadzone * deadzone;
    }

    // Update is called once per frame
    void Update()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

#if UNITY_EDITOR
        UpdateStandalone();
#else
        UpdateMobile();
#endif

    }

    // controlador para fazer swipes no pc
    private void UpdateStandalone()
    {
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            stTouch = Input.mousePosition;
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;

        if (stTouch != Vector2.zero && Input.GetMouseButton(0))
            swipeDelta = (Vector2)Input.mousePosition - stTouch;

        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector3(startRocketPosition.x - 1.3f, transform.position.y, transform.position.z);
                    if (endRocketPosition.x > border.x)
                    {
                        while (flytime < flightDuration)
                        {
                            swipeSound.Play();
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }
                    }
                    else
                    {
                        hitWallSoundLeft.Play();
                    }
                }
                else{
                    swipeRight = true;
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector3(startRocketPosition.x + 1.3f, transform.position.y, transform.position.z);
                    if (endRocketPosition.x < border2.x)
                    {
                        while (flytime < flightDuration)
                        {
                            swipeSound.Play();
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }
                    }
                    else
                        hitWallSoundRight.Play();
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else{
                    swipeUp = true;
                }
                    
            }
            stTouch = swipeDelta = Vector2.zero;
        }
    }

    // controlador para fazer swipes no telemóvel
    private void UpdateMobile()
    {
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                stTouch = Input.touches[0].position;
                doubleTap = Time.time - lastTap < doubleTapDelta;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                stTouch = swipeDelta = Vector2.zero;
            }

            swipeDelta = Vector2.zero;

            if(stTouch != Vector2.zero && Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - stTouch;
            }

            if (swipeDelta.sqrMagnitude > sqrDeadzone)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        swipeLeft = true;
                        flytime = 0f;
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector3(startRocketPosition.x - 1.3f, transform.position.y, transform.position.z);
                        if (endRocketPosition.x > border.x)
                        {
                            while (flytime < flightDuration)
                            {
                                swipeSound.Play();
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                            }
                        }
                        else
                        {
                            hitWallSoundLeft.Play();
                        }
                    }
                    else{
                        swipeRight = true;
                        flytime = 0f;
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector3(startRocketPosition.x + 1.3f, transform.position.y, transform.position.z);
                        if (endRocketPosition.x < border2.x)
                        {
                            while (flytime < flightDuration)
                            {
                                swipeSound.Play();
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                            }
                        }
                        else
                            hitWallSoundRight.Play();
                    }
                }
                else
                {
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
                stTouch = swipeDelta = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag") || (collision.tag == "MeteorTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            if (lives == 0)
            {
                // mudar o estado para gameover
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false); // esconder o objecto
            }
        }

        // o jogador bate com a nave no boost de 100 pontos e recebe-os
        if (collision.tag == "Boost100PointsLeft" || collision.tag == "Boost100PointsMid" || collision.tag == "Boost100PointsRight")
        {
            if (EnemySpawner.GetBonus() == 1)
            {
                bonusLeft.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner2.GetBonus() == 2)
            {
                bonusMid.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner3.GetBonus() == 3)
            {
                bonusRight.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}

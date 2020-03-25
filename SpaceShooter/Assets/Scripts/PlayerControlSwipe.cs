using UnityEngine;
using UnityEngine.UI;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    private float flytime;
    private float flightDuration = 0.1f;

    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    public GameObject ExplosionGO;
    public GameObject AsteroidExplosion;
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score

    public AudioSource swipeSound;
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource bonusLeft; // som bonus esq
    public AudioSource bonusMid; // som bonus mid
    public AudioSource bonusRight; // som bonus dir
    public AudioSource bonus100; // som a dizer "100 pontos"

    public Text LivesUIText;
    const int MaxLives = 3;
    private static int lives = 0;
    private static float finalScore = 0f;

    private Vector2 swipeDelta, stTouch, startRocketPosition, endRocketPosition;
    private float sqrDeadzone;
    private float deadzone = 100.0f;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartRocketPosition { get { return startRocketPosition; } }
    public Vector2 EndRocketPosition { get { return endRocketPosition; } }

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
            stTouch = Input.mousePosition;
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
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
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
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
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
                stTouch = Input.touches[0].position;
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
                        flytime = 0f;
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
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
                        flytime = 0f;
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
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
                gameObject.SetActive(false); // esconder o objecto
                finalScore = scoreUITextGO.GetComponent<GameScore>().Score;

                // o jogo passa para o estado de gameover
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
            }
        }

        // o jogador bate com a nave no boost de 100 pontos e recebe-os
        if (collision.tag == "Boost100PointsLeft" || collision.tag == "Boost100PointsMid" || collision.tag == "Boost100PointsRight")
        {
            if (EnemySpawner.GetBonus() == 1)
            {
                bonusLeft.Play();
                bonus100.PlayDelayed(bonusLeft.clip.length);
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner2.GetBonus() == 2)
            {
                bonusMid.Play();
                bonus100.PlayDelayed(bonusMid.clip.length);
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner3.GetBonus() == 3)
            {
                bonusRight.Play();
                bonus100.PlayDelayed(bonusRight.clip.length);
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
        }
    }

    public void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    public void PlayAsteroidExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(AsteroidExplosion);
        explosion.transform.position = transform.position;
    }

    public static int GetFinalScore()
    {
        return (int) finalScore;
    }

    public static int GetCurrentNumberOfLives()
    {
        return lives;
    }
}

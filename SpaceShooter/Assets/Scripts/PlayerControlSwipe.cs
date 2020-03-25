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

    private Vector2 startRocketPosition, endRocketPosition, swipeDelta, stTouch, sndTouch;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartRocketPosition { get { return startRocketPosition; } }
    public Vector2 EndRocketPosition { get { return endRocketPosition; } }
    public Vector2 StartTouch { get { return stTouch; } }
    public Vector2 SecondTouch { get { return sndTouch; } }

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
            stTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            sndTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            swipeDelta = new Vector2(sndTouch.x - stTouch.x, sndTouch.y - stTouch.y);

            //normalize the 2d vector
            swipeDelta.Normalize();

            //swipe left
            if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
            {
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                if (endRocketPosition.x > border.x)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    }

                    // roda as luvas para a esquerda

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
                startRocketPosition = transform.position;
                endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
                if (endRocketPosition.x < border2.x)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    }

                    // roda as luvas para a direita
                }
                else
                {
                    // som de bater na parede no lado direito
                    hitWallSoundRight.Play();
                }
            }
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
                stTouch = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                sndTouch = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                swipeDelta = new Vector2(sndTouch.x - stTouch.x, sndTouch.y - stTouch.y);

                //normalize the 2d vector
                swipeDelta.Normalize();

                //swipe left
                if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                {
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                    if (endRocketPosition.x > border.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }

                        // roda as luvas para a esquerda

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
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
                    if (endRocketPosition.x < border2.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }

                        // roda as luvas para a direita
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

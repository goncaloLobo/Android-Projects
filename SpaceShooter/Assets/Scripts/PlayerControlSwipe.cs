using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    private float flytime;
    private float flightDuration = 0.1f;
    public GameObject ExplosionGO;
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score

    public AudioSource swipeSound;
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource bonusLeft; // som bonus esq
    public AudioSource bonusMid; // som bonus mid
    public AudioSource bonusRight; // som bonus dir
    public AudioSource bonus100; // som a dizer "100 pontos"
    public AudioSource introducao; // som do button introducao
    public AudioSource vaiComeçar321;

    public Text LivesUIText;
    private static int lives;
    private static float finalScore = 0f;
    private int i = 0;

    private Touch startTouch, endTouch;
    private float startTouchTime, endTouchTime;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int doubleTapRadius = Configuration.DoubleTapRadius();
    private int doubleTapCircle;
    private bool isDoubleTap;
    private float screenDPI;

    private static bool buttonJogarBackToNormal, buttonIntroducaoBackToNormal, buttonInstrucoesBackToNormal;

    private Vector2 startRocketPosition, endRocketPosition, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartRocketPosition { get { return startRocketPosition; } }
    public Vector2 EndRocketPosition { get { return endRocketPosition; } }
    public Touch StartTouch { get { return startTouch; } }
    public Touch EndTouch { get { return endTouch; } }
    public float StartTouchTime { get { return startTouchTime; } }
    public float EndTouchTime { get { return endTouchTime; } }

    public void Init()
    {
        lives = Configuration.MaxLives();
        LivesUIText.text = lives.ToString();
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");

        //mostra a nave do jogador no ecra
        gameObject.SetActive(true);
        doubleTapCircle = doubleTapRadius * doubleTapRadius;
        isDoubleTap = false;
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
                if (introducao.isPlaying)
                    introducao.Stop();
                else
                    Application.Quit();
            }

            if (GameManager.GetStarted())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if (GameManager.GetInstructions())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if (GameManager.GetTutorialP1() || GameManager.GetTutorialP2() || GameManager.GetTutorialP3() || GameManager.GetTutorialP4() || GameManager.GetTutorialP5())
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
            }

            if (GameManager.GetPregameplay())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
        }

        if (GameManager.GetPregameplay() && i == 0)
        {
            i++;
            vaiComeçar321.Play();
            StartCoroutine(WaitForTouch(vaiComeçar321.clip.length, DoAfter));
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
                    isDoubleTap = true;
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
                if (distance > (16.0f * screenDPI + 0.5f) && !isDoubleTap)
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                            if (endRocketPosition.x > border.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    swipeSound.Play();
                                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
                            if (endRocketPosition.x < border2.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    swipeSound.Play();
                                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
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
                isDoubleTap = false;
            }
        }

        // DETETA O INPUT PARA O ESTADO INICIAL (DUPLO TOQUE NOS BOTOES QUE É SUPOSTO)
        if (Input.touchCount > 0 && GameManager.GetOpening())
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch;
                startTouchTime = Time.time;

                if (CheckForDoubleTap(startTouchTime, endTouchTime, startTouch, endTouch) == 0)
                {
                    if (ButtonJogar.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PreGameplay);
                        buttonJogarBackToNormal = true;
                    }

                    else if (ButtonComoJogar.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
                        buttonInstrucoesBackToNormal = true;
                    }

                    else if (ButtonIntroducao.CheckForHighlighted() == 1)
                    {
                        if (!introducao.isPlaying)
                            introducao.Play();
                        buttonIntroducaoBackToNormal = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouch = touch;
                endTouchTime = Time.time;
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

    public static int GetFinalScore()
    {
        return (int) finalScore;
    }

    public static int GetCurrentNumberOfLives()
    {
        return lives;
    }

    public static bool ButtonJogarBackToNormal()
    {
        return buttonJogarBackToNormal;
    }

    public static bool ButtonIntroducaoBackToNormal()
    {
        return buttonIntroducaoBackToNormal;
    }

    public static bool ButtonInstrucoesBackToNormal()
    {
        return buttonInstrucoesBackToNormal;
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

        // se o duplo toque estiver dentro do circulo aceitavel, entao retorna 0
        if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
            return 0;
        return -1;
    }
}

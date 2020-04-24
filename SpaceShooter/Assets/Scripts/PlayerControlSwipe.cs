using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Networking;

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
    public AudioSource introducao; // som do button introducao

    public AudioSource[] sounds;
    public AudioSource pontuacaoError;
    public AudioSource pontuacao2;
    public AudioSource pontuacao3;
    public AudioSource avoidedEnemies;
    public AudioSource playerEngine;

    public AudioSource vaiComeçar321;
    public AudioSource introducaoSound;
    public AudioSource textToSpeech;

    public Text LivesUIText;
    private static int lives;
    private static float finalScore = 0f;

    private Touch currentTouch;
    private Touch previousTouch;
    private float currentTapTime;
    private float lastTapTime;

    private Touch startTouch, endTouch;
    private float startTouchTime, endTouchTime;
    private int minimumFlingVelocity = Configuration.MinimumFlingVelocity();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int doubleTapRadius = Configuration.DoubleTapRadius();
    private bool isDoubleTap;
    private float screenDPI, increaseSpeedTimer;
    private static bool tutorialLeft, tutorialRight, hasEntered;
    private static bool tutorialCancelAction, jogarCancelAction, instrucoesCancelAction, pontuacaoCancelAction;

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

        sounds = GetComponents<AudioSource>();
        pontuacaoError = sounds[0];
        pontuacao2 = sounds[1];
        pontuacao3 = sounds[2];
        avoidedEnemies = sounds[3];
        playerEngine = sounds[4];

        //mostra a nave do jogador no ecra
        gameObject.SetActive(true);
        playerEngine.Play();

        isDoubleTap = false;
        tutorialLeft = tutorialRight = hasEntered = false;
        tutorialCancelAction = jogarCancelAction = instrucoesCancelAction = pontuacaoCancelAction = false;
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
            {
                GameManager.CheckToStopEnemySpawners();
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
            }

            if (GameManager.GetInstructions())
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);

            if (GameManager.GetTutorialP1() || GameManager.GetTutorialP2() || GameManager.GetTutorialP3() || GameManager.GetTutorialP4() || GameManager.GetTutorialP5())
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
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
                if (CheckForDoubleTapOpening(startTouchTime, endTouchTime, startTouch, endTouch) == 0)
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
                                    if (swipeSound.isPlaying)
                                    {
                                        swipeSound.panStereo -= 0.1f;
                                    }

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
                                    if (swipeSound.isPlaying)
                                    {
                                        swipeSound.panStereo += 0.1f;
                                    }

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
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTapOpening(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    // se o botao jogar estiver highlighted
                    if (ButtonJogar.CheckForHighlighted() == 1 && !hasEntered)
                    {
                        hasEntered = true;
                        vaiComeçar321.Play();
                        Invoke("StartGame", vaiComeçar321.clip.length);
                    }

                    if (ButtonComoJogar.CheckForHighlighted() == 1)
                    {
                        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
                    }

                    if(ButtonPontuacao.CheckForHighlighted() == 1)
                    {
                        float highscore = PlayerPrefs.GetFloat("highscore");
                        float time = PlayerPrefs.GetFloat("time");
                        int enemiesAvoided = PlayerPrefs.GetInt("enemies");
                        if (highscore == 0)
                        {
                            // ainda nao fez nenhum jogo
                            pontuacaoError.Play();
                        }
                        else
                        {
                            // vai dizer os resultados do melhor jogo
                            StartCoroutine(DownloadHighscore(highscore));
                            StartCoroutine(DownloadAvoidedEnemies(enemiesAvoided));
                            StartCoroutine(DownloadTime(time));
                        }
                    }

                    if(ButtonIntroducao.CheckForHighlighted() == 1)
                    {
                        if (!introducaoSound.isPlaying)
                            introducaoSound.Play();
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
                if (CheckForDoubleTapInstructions(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    if (ButtonTutorial.CheckForHighlighted() == 1)
                    {
                        tutorialCancelAction = true;
                        Invoke("ChangeToTutorialP1State", 0.5f);
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

        if (Input.touchCount > 0 && GameManager.GetTutorialP1())
        {
            Touch touchp1 = Input.GetTouch(0);
            if (touchp1.phase == TouchPhase.Began)
            {
                startTouch = touchp1;
                startTouchTime = Time.time;
            }
            else if (touchp1.phase == TouchPhase.Ended)
            {
                endTouch = touchp1;
                endTouchTime = Time.time;

                int deltaX = (int)endTouch.position.x - (int)startTouch.position.x;
                int deltaY = (int)endTouch.position.y - (int)startTouch.position.y;
                int distance = (deltaX * deltaX) + (deltaY * deltaY);
                if (distance > (16.0f * screenDPI + 0.5f))
                {
                    float difference = lastTapTime - currentTapTime;
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
                                    Invoke("ChangeToTutorialP2StateLeft", 1f);
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
                                    Invoke("ChangeToTutorialP2StateRight", 1f);
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

        // PARTE 2 DO TUTORIAL
        if (GameManager.GetTutorialP2())
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                            if (endRocketPosition.x < border.x)
                            {
                                hitWallSoundLeft.Play();
                                Invoke("ChangeToTutorialP3State", 1f);
                            }
                        }

                        //swipe right
                        if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                        {
                            flytime = 0f;
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x + 1.3f, transform.position.y);
                            if (endRocketPosition.x > border2.x)
                            {
                                hitWallSoundRight.Play();
                                Invoke("ChangeToTutorialP3State", 1f);
                            }
                        }
                    }
                }
            }
        }

        if (GameManager.GetTutorialP3())
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                            if (endRocketPosition.x > border.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    swipeSound.Play();
                                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                                    Invoke("ChangeToTutorialP4StateLeft", 1f);
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
                                    Invoke("ChangeToTutorialP4StateRight", 1f);
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

        if (GameManager.GetTutorialP4())
        {
            tutorialLeft = tutorialRight = false;
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                            if (endRocketPosition.x > border.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    swipeSound.Play();
                                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                                    Invoke("ChangeToTutorialP5StateLeft", 1f);
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
                                    Invoke("ChangeToTutorialP5StateRight", 1f);
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

        if (GameManager.GetTutorialP5())
        {
            Touch touchp5 = Input.GetTouch(0);
            if (touchp5.phase == TouchPhase.Began)
            {
                startTouch = touchp5;
                startTouchTime = Time.time;
            }
            else if (touchp5.phase == TouchPhase.Moved)
            {

            }
            else if (touchp5.phase == TouchPhase.Ended)
            {
                endTouch = touchp5;
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
                            startRocketPosition = transform.position;
                            endRocketPosition = new Vector2(startRocketPosition.x - 1.3f, transform.position.y);
                            if (endRocketPosition.x > border.x)
                            {
                                while (flytime < flightDuration)
                                {
                                    flytime += Time.deltaTime;
                                    swipeSound.Play();
                                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                                    Invoke("ChangeToTutorialP6State", 1f);
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
                                    Invoke("ChangeToTutorialP6State", 1f);
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
            //lives--;
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

    public void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    public static void ResetHasEntered()
    {
        hasEntered = false;
    }

    public static int GetFinalScore()
    {
        return (int) finalScore;
    }

    public static int GetCurrentNumberOfLives()
    {
        return lives;
    }

    public static bool CheckTutorialLeft()
    {
        return tutorialLeft;
    }

    public static bool CheckTutorialRight()
    {
        return tutorialRight;
    }

    // FAZER COM QUE NAO FAÇA A AÇAO DO DUPLO TOQUE E DO BOTAO AO MESMO TEMPO
    public static bool GetTutorialCancelAction()
    {
        return tutorialCancelAction;
    }

    public static void ResetTutorialCancelAction()
    {
        tutorialCancelAction = false;
    }

    public static bool GetJogarCancelAction()
    {
        return jogarCancelAction;
    }

    public static void ResetJogarCancelAction()
    {
        jogarCancelAction = false;
    }

    public static bool GetInstrucoesCancelAction()
    {
        return instrucoesCancelAction;
    }

    public static void ResetInstrucoesCancelAction()
    {
        instrucoesCancelAction = false;
    }

    public static bool GetPontuacaoCancelAction()
    {
        return pontuacaoCancelAction;
    }

    public static void ResetPontuacaoCancelAction()
    {
        pontuacaoCancelAction = false;
    }

    private void StartGame()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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
        if (deltaX * deltaX + deltaY * deltaY < (doubleTapRadius*doubleTapRadius))
            return 0;
        return -1;
    }

    private int CheckForDoubleTapInstructions(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
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

    private void ChangeToTutorialP1State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP1);
    }

    private void ChangeToTutorialP2StateLeft()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP2);
        tutorialLeft = true;
    }

    private void ChangeToTutorialP2StateRight()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP2);
        tutorialRight = true;
    }

    private void ChangeToTutorialP3State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP3);
    }

    private void ChangeToTutorialP4StateLeft()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP4);
        tutorialLeft = true;
    }

    private void ChangeToTutorialP4StateRight()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP4);
        tutorialRight = true;
    }

    private void ChangeToTutorialP5StateLeft()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP5);
        tutorialLeft = true;
    }

    private void ChangeToTutorialP5StateRight()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP5);
        tutorialRight = true;
    }

    private void ChangeToTutorialP6State()
    {
        GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.TutorialP6);
    }

    private IEnumerator DownloadAvoidedEnemies(int enemiesAvoided)
    {
        // "%20pontos%20"
        string pontostxt = "pontos";
        string googleUrl = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+" + enemiesAvoided + pontostxt + "&tl=pt-BR";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(googleUrl, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                textToSpeech.clip = myClip;
                avoidedEnemies.Play(); // "Inimigos desviados :"
                textToSpeech.PlayDelayed(avoidedEnemies.clip.length);
            }
        }
    }

    private IEnumerator DownloadHighscore(float highscore)
    {
        System.Diagnostics.Debug.WriteLine("entrei crl");
        // "%20pontos%20"
        string pontostxt = "pontos";
        string googleUrl = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+" + highscore + pontostxt + "&tl=pt-BR";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(googleUrl, AudioType.MPEG))
        {
            System.Diagnostics.Debug.WriteLine("entrei crl 2");
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                System.Diagnostics.Debug.WriteLine("entrei crl erro");
                Debug.Log(www.error);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("entrei crl 3");
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                textToSpeech.clip = myClip;
                pontuacao2.Play(); // "Pontuação deste jogo:"
                textToSpeech.PlayDelayed(pontuacao2.clip.length);
            }
        }
    }

    private IEnumerator DownloadTime(float time)
    {
        // "%20pontos%20"
        string pontostxt = "pontos";
        string googleUrl = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+" + time + pontostxt + "&tl=pt-BR";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(googleUrl, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                textToSpeech.clip = myClip;
                pontuacao3.Play(); // "Duração do jogo:"
                textToSpeech.PlayDelayed(avoidedEnemies.clip.length);
            }
        }
    }
}

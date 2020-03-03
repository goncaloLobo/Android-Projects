using UnityEngine;
using UnityEngine.UI;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager

    private Vector2 startTouchPosition, endTouchPosition;
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
    Vector2 shipsPosition;

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
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }            

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if ((endTouchPosition.x < startTouchPosition.x) && transform.position.x > -1.75f)
            {
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x - 1.2f, transform.position.y, transform.position.z);
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
                    hitWallSoundLeft.Play();
            }

            if ((endTouchPosition.x > startTouchPosition.x) && transform.position.x < 1.75f)
            {
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x + 1.2f, transform.position.y, transform.position.z);
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
                Debug.Log("Bati boost 100 pts esquerda");
                bonusLeft.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner2.GetBonus() == 2)
            {
                Debug.Log("Bati boost 100 pts meio");
                bonusMid.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner3.GetBonus() == 3)
            {
                Debug.Log("Bati boost 100 pts direita");
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

    /*
    //A PARTIR DAQUI É O CONTROLO POR TECLAS DO PC
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }

        // the value will be -1, 0 or 1 (left, no input or right)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f; // subtrai metade da largura da sprite
        min.x = min.x + 0.225f; // adiciona metade da largura da sprite

        max.y = max.y - 0.285f; // subtrai metade da altura da sprite
        min.y = min.y + 0.285f; // adiciona metade da altura da sprite

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    // colisão entre a nave do jogador e a nave inimiga ou a bala inimiga.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
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
                Debug.Log("Bati boost 100 pts esquerda");
                bonusLeft.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner2.GetBonus() == 2)
            {
                Debug.Log("Bati boost 100 pts meio");
                bonusMid.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
            else if (EnemySpawner3.GetBonus() == 3)
            {
                Debug.Log("Bati boost 100 pts direita");
                bonusRight.Play();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }
        }

        if(collision.tag == "MeteorTag")
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
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    void UpdateShipsPosition()
    {

    }
    */
}

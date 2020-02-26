﻿using UnityEngine;
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

    public AudioSource swipeSound;
    public AudioSource hitWallSoundRight;
    public AudioSource hitWallSoundLeft;

    public Text LivesUIText;
    const int MaxLives = 3;
    int lives = 0;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();

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
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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

    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if ((endTouchPosition.x < startTouchPosition.x) && transform.position.x > -1.75f)
            {
                //transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
                StartCoroutine(Fly("left"));
            }

            if ((endTouchPosition.x > startTouchPosition.x) && transform.position.x < 1.75f)
            {
                //transform.position = new Vector2(transform.position.x + 1.75f, transform.position.y);
                StartCoroutine(Fly("right"));
            }

            if ((endTouchPosition.y > startTouchPosition.y) && transform.position.y > -1.75f)
            {
                //transform.position = new Vector2(transform.position.x - 1.75f, transform.position.y);
                StartCoroutine(Fly("top"));
            }
        }
    }

    private IEnumerator Fly(string wheretofly)
    {
        switch (wheretofly)
        {
            case "left":
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x - 1.75f, transform.position.y, transform.position.z);

                while(flytime < flightDuration)
                {
                    flytime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    yield return null;
                }
                break;
            case "right":
                flytime = 0f;
                startRocketPosition = transform.position;
                endRocketPosition = new Vector3(startRocketPosition.x + 1.75f, transform.position.y, transform.position.z);

                while(flytime < flightDuration)
                {
                    flytime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                    yield return null;
                }
                break;
            case "top":
                GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
                bullet01.transform.position = bulletPosition01.transform.position;

                GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
                bullet02.transform.position = bulletPosition02.transform.position;

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            if(lives == 0)
            {
                // mudar o estado para gameover
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false); // esconder o objecto
            }
            //Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}

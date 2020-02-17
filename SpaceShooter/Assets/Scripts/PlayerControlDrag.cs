using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlDrag : MonoBehaviour
{

    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    public float moveSpeed;

    float MaxTimeWait = 1;
    float VariancePosition = 1;

    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }

            // duplo toque para disparar
            /*
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                float DeltaTime = Input.GetTouch(0).deltaTime;
                float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

                if (DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
                {
                    GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
                    bullet01.transform.position = bulletPosition01.transform.position;

                    GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
                    bullet02.transform.position = bulletPosition02.transform.position;
                }
            }
            */
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
        {
            Destroy(gameObject);
        }
    }
}

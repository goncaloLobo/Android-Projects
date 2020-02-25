using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score

    public static float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        Debug.Log("Speed no update: " + speed);
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }

    // Deteta colisao entre bala do jogador que vem da nave do jogador
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "PlayerShipTag") || (collision.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    public static float getSpeed()
    {
        return speed;
    }

    public static void setSpeed(float sp)
    {
        speed = sp;
    }

}

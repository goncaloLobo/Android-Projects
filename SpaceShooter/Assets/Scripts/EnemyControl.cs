using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score
    public GameObject Boost50Points;
    public GameObject EnemyGO;
    public GameObject Meteor;

    public static float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        Boost50Points = GameObject.FindGameObjectWithTag("Boost50Pts");
        EnemyGO = GameObject.FindGameObjectWithTag("EnemyShipTag");
        Meteor = GameObject.FindGameObjectWithTag("MeteorTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // destruir os objetos que passem o limite inferior do ecra
        if (transform.position.y < min.y)
        {
            if (gameObject.CompareTag("Boost50Pts"))
            {
                Debug.Log("VOU APENAS DESTRUIR");
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("MeteorTag"))
            {
                Debug.Log("VOU DAR 100 PONTOS -> ASTEROIDE");
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("EnemyShipTag"))
            {
                Debug.Log("VOU DAR 100 PONTOS -> NAVE");
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // jogador bater com a nave no inimigo
        if (collision.tag == "PlayerShipTag")
        {
            // destroi o bónus/nave inimiga/asteroid quando bate na nave do jogador
            Destroy(gameObject);
        }

        // disparar sobre o inimigo -> + 100 pontos (200 na realidade)
        else if (collision.tag == "PlayerBulletTag")
        {
            Debug.Log("DISPAREI SOBRE O INIMIGO, 100 PTS");
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    // gets the speed
    public static float GetSpeed()
    {
        return speed;
    }

    // sets the speed
    public static void SetSpeed(float sp)
    {
        speed = sp;
    }
}

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

        // dar 100 pontos a uma nave ou asteroide que atravesse o ecra
        if (transform.position.y < min.y && (EnemyGO || Meteor))
        {
            Debug.Log("Vou dar 100 pts");
            scoreUITextGO.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }

        // apenas destroi o objeto boost 50pts
        else if (transform.position.y < min.y && Boost50Points)
        {
            Debug.Log("VOU DAR PONTOS DO BOOOOOOOST. NAO DEVIA.");
            Destroy(gameObject);
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

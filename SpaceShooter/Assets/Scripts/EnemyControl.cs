using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO; // referencia para o objeto de explosao
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score
    public GameObject EnemyGO; // referencia para o objeto de inimigo
    public GameObject Meteor; // referencia para o objeto de meteorito
    public GameObject Boost100PointsLeft; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsMid; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsRight; // referencia para o objeto de som a anunciar um boost à esquerda
    public static float speed = 2f;
    private static bool triggerExplosion;

    private static int enemiesAvoided; // inimigos que chegam ao final do ecra (que o utilizador se desviou)

    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        Boost100PointsLeft = GameObject.FindGameObjectWithTag("Boost100PointsLeft");
        Boost100PointsMid = GameObject.FindGameObjectWithTag("Boost100PointsMid");
        Boost100PointsRight = GameObject.FindGameObjectWithTag("Boost100PointsRight");
        EnemyGO = GameObject.FindGameObjectWithTag("EnemyShipTag");
        Meteor = GameObject.FindGameObjectWithTag("MeteorTag");
        triggerExplosion = false;
        enemiesAvoided = 0;
    }

    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // assim que aparecem no ecrã
        if (transform.position.y > min.y)
        {
            if (gameObject.CompareTag("EnemyShipTag"))
                gameObject.GetComponent<AudioSource>().volume += 0.01f;

            if (gameObject.CompareTag("MeteorTag"))
                gameObject.GetComponent<AudioSource>().volume += 0.01f;

            if (gameObject.CompareTag("Boost100PointsLeft"))
                gameObject.GetComponent<AudioSource>().volume += 0.01f;

            if (gameObject.CompareTag("Boost100PointsMid"))
                gameObject.GetComponent<AudioSource>().volume += 0.01f;

            if (gameObject.CompareTag("Boost100PointsRight"))
                gameObject.GetComponent<AudioSource>().volume += 0.01f;
        }

        // destruir os objetos que passem o limite inferior do ecra
        if (transform.position.y < min.y)
        {
            enemiesAvoided++;
            if (gameObject.CompareTag("Boost100PointsLeft"))
            {
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Boost100PointsRight"))
            {
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Boost100PointsMid"))
            {
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("MeteorTag"))
            {
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("EnemyShipTag"))
            {
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
                Destroy(gameObject);
            }
        }

        if (triggerExplosion)
            Destroy(gameObject);
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

    // obtem os inimigos que o utilizador se desviou, no final do jogo
    public static int GetEnemiesAvoided()
    {
        return enemiesAvoided;
    }

    public static void TriggerExplosion()
    {
        triggerExplosion = true;
    }
}

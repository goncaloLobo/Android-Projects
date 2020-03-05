using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject EnemyGO; // referencia para o objeto de inimigo
    public GameObject Meteor; // referencia para o objeto de meteorito
    public GameObject Boost100PointsLeft; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsMid; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsRight; // referencia para o objeto de som a anunciar um boost à esquerda

    public static float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Boost100PointsLeft = GameObject.FindGameObjectWithTag("Boost100PointsLeft");
        Boost100PointsMid = GameObject.FindGameObjectWithTag("Boost100PointsMid");
        Boost100PointsRight = GameObject.FindGameObjectWithTag("Boost100PointsRight");
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
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("EnemyShipTag"))
            {
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
        }
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

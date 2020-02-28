using UnityEngine;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGO; // referencia para o objeto de explosao
    public GameObject scoreUITextGO; // referencia para o objeto do jogo UI do score
    public GameObject EnemyGO; // referencia para o objeto de inimigo
    public GameObject Meteor; // referencia para o objeto de meteorito
    public GameObject Boost100PointsLeft; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsMid; // referencia para o objeto de som a anunciar um boost à esquerda
    public GameObject Boost100PointsRight; // referencia para o objeto de som a anunciar um boost à esquerda

    public static float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
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
        
        /*
        Dictionary<int, GameObject> teste = EnemySpawner.GetDictionary();
        GameObject EnemyGO;

        if (teste.TryGetValue(1, out EnemyGO) && GameObject.FindGameObjectWithTag("Boost100PointsLeft").transform.position.y > min.y)
        {
            Debug.Log("entrei no if");
            EnemySpawner.SetSliderValue();
        }
        // detetar um objeto em particular no ecra pela tag
        if (GameObject.FindGameObjectWithTag("Boost100PointsLeft").transform.position.y < min.y)
        {
            Debug.Log("FINAAAAAL");
        }
        */

        // destruir os objetos que passem o limite inferior do ecra
        if (transform.position.y < min.y)
        {
            if (gameObject.CompareTag("Boost100PointsLeft"))
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
}

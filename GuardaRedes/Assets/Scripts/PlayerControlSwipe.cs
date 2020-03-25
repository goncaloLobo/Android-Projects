using UnityEngine;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource hitCenterUp; // som de bater no topo
    public AudioSource hitCenterDown; // som de bater em baixo

    private Vector2 startGlovePosition, endGlovePosition, swipeDelta, stTouch, sndTouch;
    private float flytime;
    private float flightDuration = 0.1f;

    private bool swipeLeft, swipeRight, swipeUp, swipeDown;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartGlovePosition { get { return startGlovePosition; } }
    public Vector2 EndGlovePosition { get { return endGlovePosition; } }
    public Vector2 StartTouch { get { return stTouch; } }
    public Vector2 SecondTouch { get { return sndTouch; } }

    public void Init()
    {
        //mostra as luvas do gr no ecra
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        swipeLeft = swipeRight = swipeDown = swipeUp = false;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        UpdateStandalone();
#else
        UpdateMobile();
#endif

    }

    // controlador para fazer swipes no pc
    private void UpdateStandalone()
    {
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        if (Input.GetMouseButtonDown(0))
        {
            stTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            sndTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            swipeDelta = new Vector2(sndTouch.x - stTouch.x, sndTouch.y - stTouch.y);

            //normalize the 2d vector
            swipeDelta.Normalize();

            //swipe up
            if (swipeDelta.y > 0 && swipeDelta.x > -0.5f && swipeDelta.x < 0.5f)
            {
                swipeUp = true;
                flytime = 0f;
                startGlovePosition = transform.position;
                endGlovePosition = new Vector2(transform.position.x, startGlovePosition.y + 2.3f);
                if (endGlovePosition.y < border2.y)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }
                }
                else
                {
                    // som de bater na parede em cima
                    hitCenterUp.Play();
                }
            }

            //swipe down
            if (swipeDelta.y < 0 && swipeDelta.x > -0.5f && swipeDelta.x < 0.5f)
            {
                swipeDown = true;
                flytime = 0f;
                startGlovePosition = transform.position;
                endGlovePosition = new Vector2(transform.position.x, startGlovePosition.y - 2.3f);
                if (endGlovePosition.y > border.y)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }
                }
                else
                {
                    // som de bater na parede em baixo
                    hitCenterDown.Play();
                }
            }

            //swipe left
            if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
            {
                swipeLeft = true;
                flytime = 0f;
                startGlovePosition = transform.position;
                endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
                if (endGlovePosition.x > border.x)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas para a esquerda

                }
                else
                {
                    // som de bater na parede no lado esquerdo
                    hitWallSoundLeft.Play();
                }
            }

            //swipe right
            if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
            {
                swipeRight = true;
                flytime = 0f;
                startGlovePosition = transform.position;
                endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                if (endGlovePosition.x < border2.x)
                {
                    while (flytime < flightDuration)
                    {
                        flytime += Time.deltaTime;
                        transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                    }

                    // roda as luvas para a direita
                }
                else
                {
                    // som de bater na parede no lado direito
                    hitWallSoundRight.Play();
                }
            }         
        }
    }

    // controlador para fazer swipes no telemóvel
    private void UpdateMobile()
    {
        Vector2 border = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // para limite esq
        Vector2 border2 = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // para limite dir

        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                stTouch = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                sndTouch = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                swipeDelta = new Vector2(sndTouch.x - stTouch.x, sndTouch.y - stTouch.y);

                //normalize the 2d vector
                swipeDelta.Normalize();

                //swipe up
                if (swipeDelta.y > 0 && swipeDelta.x > -0.5f && swipeDelta.x < 0.5f)
                {
                    swipeUp = true;
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(transform.position.x, startGlovePosition.y + 2.3f);
                    if (endGlovePosition.y < border2.y)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }
                    }
                    else
                    {
                        // som de bater na parede em cima
                        hitCenterUp.Play();
                    }
                }

                //swipe down
                if (swipeDelta.y < 0 && swipeDelta.x > -0.5f && swipeDelta.x < 0.5f)
                {
                    swipeDown = true;
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(transform.position.x, startGlovePosition.y - 2.3f);
                    if (endGlovePosition.y > border.y)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }
                    }
                    else
                    {
                        // som de bater na parede em baixo
                        hitCenterDown.Play();
                    }
                }

                //swipe left
                if (swipeDelta.x < 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                {
                    swipeLeft = true;
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x - 1.3f, transform.position.y);
                    if (endGlovePosition.x > border.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }

                        // roda as luvas para a esquerda

                    }
                    else
                    {
                        // som de bater na parede no lado esquerdo
                        hitWallSoundLeft.Play();
                    }
                }

                //swipe right
                if (swipeDelta.x > 0 && swipeDelta.y > -0.5f && swipeDelta.y < 0.5f)
                {
                    swipeRight = true;
                    flytime = 0f;
                    startGlovePosition = transform.position;
                    endGlovePosition = new Vector2(startGlovePosition.x + 1.3f, transform.position.y);
                    if (endGlovePosition.x < border2.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startGlovePosition, endGlovePosition, flytime / flightDuration);
                        }

                        // roda as luvas para a direita
                    }
                    else
                    {
                        // som de bater na parede no lado direito
                        hitWallSoundRight.Play();
                    }
                }
            }
        }
    }
}

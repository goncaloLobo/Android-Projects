using UnityEngine;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager
    public AudioSource hitWallSoundRight; // som de bater no limite dir do ecra
    public AudioSource hitWallSoundLeft; // som de bater no limite esq do ecra
    public AudioSource hitCenterUp; // som de bater no topo
    public AudioSource hitCenterDown; // som de bater em baixo

    private Vector2 startGlovePosition, endGlovePosition, swipeDelta, stTouch;
    private float flytime;
    private float flightDuration = 0.1f;
    private float sqrDeadzone;
    private float deadzone = 100.0f;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartGlovePosition { get { return startGlovePosition; } }
    public Vector2 EndGlovePosition { get { return endGlovePosition; } }
    public Vector2 StartTouch { get { return stTouch; } }

    public void Init()
    {
        //mostra as luvas do gr no ecra
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        sqrDeadzone = deadzone * deadzone;
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
            stTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stTouch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;

        if (stTouch != Vector2.zero && Input.GetMouseButton(0))
            swipeDelta = (Vector2)Input.mousePosition - stTouch;

        if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
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
                else{
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
            else
            {
                if (y < 0)
                {
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
                else{
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
                    
            }
            stTouch = swipeDelta = Vector2.zero;
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
                stTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                stTouch = swipeDelta = Vector2.zero;
            }

            swipeDelta = Vector2.zero;

            if (stTouch != Vector2.zero && Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - stTouch;
            }

            if (swipeDelta.sqrMagnitude > sqrDeadzone)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
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
                    else
                    {
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
                else
                {
                    if (y < 0)
                    {
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
                    else
                    {
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
                }
                stTouch = swipeDelta = Vector2.zero;
            }
        }
    }
}

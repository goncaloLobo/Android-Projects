using UnityEngine;
using UnityEngine.UI;

public class PlayerControlSwipe : MonoBehaviour
{
    public GameObject GameManagerGO; // game manager

    private Vector3 startRocketPosition, endRocketPosition;
    private float flytime;
    private float flightDuration = 0.1f;

    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;

    public float speed = 1f;

    private bool tap;
    private Vector2 swipeDelta, stTouch;
    private float lastTap;
    private float sqrDeadzone;
    private float deadzone = 100.0f;

    public bool Tap { get { return tap;  } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }

    public void Init()
    {

        //mostra a nave do jogador no ecra
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
        tap = false;

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
            tap = true;
            stTouch = Input.mousePosition;
            lastTap = Time.time;
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
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector3(startRocketPosition.x - 1.3f, transform.position.y, transform.position.z);
                    if (endRocketPosition.x > border.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }
                    }
                    else
                    {
                    }
                }
                else{
                    flytime = 0f;
                    startRocketPosition = transform.position;
                    endRocketPosition = new Vector3(startRocketPosition.x + 1.3f, transform.position.y, transform.position.z);
                    if (endRocketPosition.x < border2.x)
                    {
                        while (flytime < flightDuration)
                        {
                            flytime += Time.deltaTime;
                            transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                if (y < 0)
                {
                    Debug.Log("swipe down");
                }
                else{
                    Debug.Log("swipe up");
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
                tap = true;
                stTouch = Input.touches[0].position;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                stTouch = swipeDelta = Vector2.zero;
            }

            swipeDelta = Vector2.zero;

            if(stTouch != Vector2.zero && Input.touches.Length != 0)
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
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector3(startRocketPosition.x - 1.3f, transform.position.y, transform.position.z);
                        if (endRocketPosition.x > border.x)
                        {
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                            }
                        }
                        else
                        {
                        }
                    }
                    else{
                        flytime = 0f;
                        startRocketPosition = transform.position;
                        endRocketPosition = new Vector3(startRocketPosition.x + 1.3f, transform.position.y, transform.position.z);
                        if (endRocketPosition.x < border2.x)
                        {
                            while (flytime < flightDuration)
                            {
                                flytime += Time.deltaTime;
                                transform.position = Vector2.Lerp(startRocketPosition, endRocketPosition, flytime / flightDuration);
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        Debug.Log("swipe down");
                    }
                    else
                    {
                        Debug.Log("swipe up");
                    }
                }
                stTouch = swipeDelta = Vector2.zero;
            }
        }
    }
}

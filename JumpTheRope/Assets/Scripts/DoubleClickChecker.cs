using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private bool tap, doubleTap;
    private Vector2 startTouch;
    private float doubleTapDelta = 0.5f;
    private float lastTap;
    float touchDuration;
    Touch touch;
    private int n_saltos = 0;

    private float maxTime = 0.5f;
    private float lastTimeClicked;

    public AudioSource[] sounds;
    public AudioSource manJumping; // ManJumping
    public AudioSource buzzer; // Buzzer

    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        manJumping = sounds[0];
        buzzer = sounds[1];
    }

    void Update()
    {
        tap = doubleTap = false;

#if UNITY_EDITOR
        Update_Standalone();
#else
        Update_Mobile();
#endif
    }

    private void Update_Standalone()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
        }

        // se for duplo toque então o homem salta.
        if (doubleTap && GameManager.GetStarted())
        {
            n_saltos++;
            manJumping.Play();
        }

        if(!doubleTap && GameManager.GetStarted() && tap)
        {
            Debug.Log("doubletap: " + doubleTap + " tap: " + tap);
        }

    }

    private void Update_Mobile()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                doubleTap = Time.time - lastTap < doubleTapDelta;
                lastTap = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

            }

            if (doubleTap && GameManager.GetStarted())
            {
                n_saltos++;
                manJumping.Play();
            }
        }
    }

    public int getNSaltos()
    {
        return n_saltos;
    }
}
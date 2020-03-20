using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private bool tap, doubleTap, biggerDoubleTap;
    private Vector2 startTouch;
    private float doubleTapDelta = 0.5f;
    private float doubleTapDeltaBigger = 1.0f;
    private float lastTap;
    private int n_saltos = 0;

    private float lastTimeClicked;

    public AudioSource[] sounds;
    public AudioSource manJumping; // ManJumping
    public AudioSource buzzer; // Buzzer
    public AudioSource oneFootJumping; // tap

    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }
    public bool BiggerDoubleTap { get { return biggerDoubleTap; } }

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        manJumping = sounds[0];
        buzzer = sounds[1];
        oneFootJumping = sounds[2];
    }

    void Update()
    {
        tap = doubleTap = biggerDoubleTap = false;

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
            startTouch = Input.mousePosition;
            // true qdo a diferença entre o ultimo e o primeiro toque < doubletapDelta
            doubleTap = Time.time - lastTap < doubleTapDelta;
            lastTap = Time.time;
            oneFootJumping.Play();
        }

        // se for duplo toque então o homem salta.
        if (doubleTap && GameManager.GetStarted())
        {
            n_saltos++;
            manJumping.Play();
        }

        if(!doubleTap && GameManager.GetStarted() && tap)
        {
            Debug.Log("entrei");
        }

    }

    private void Update_Mobile()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.touches[0].position;
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
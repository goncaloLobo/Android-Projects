using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private bool tap, doubleTap, biggerDoubleTap;
    private Vector2 startTouch;
    private float doubleTapDelta = 0.5f;
    private float doubleTapDeltaBigger = 1.0f;
    private float lastTap;

    private static int n_saltos_perfeitos;
    private static int n_saltos_normais;
    private static int pontuacaoTotal;

    private const int perfectJump = 100;
    private const int normalJump = 25;

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

        n_saltos_perfeitos = n_saltos_normais = pontuacaoTotal = 0;
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
        if (Input.GetMouseButtonDown(0) && GameManager.GetStarted())
        {
            tap = true;
            startTouch = Input.mousePosition;

            // doubletap = true qdo a diferença entre os taps < 0.5f
            doubleTap = Time.time - lastTap < doubleTapDelta;

            //biggerDoubleTap = true qdo a diferença entre os taps > 0.5f e < 1.0f
            biggerDoubleTap = (Time.time - lastTap > doubleTapDelta) && (Time.time - lastTap < doubleTapDeltaBigger);
            lastTap = Time.time;
            oneFootJumping.Play();
        }

        // se for duplo toque então o homem salta (SALTO PERFEITO).
        if (doubleTap && GameManager.GetStarted())
        {
            n_saltos_perfeitos++;
            manJumping.Play();
            pontuacaoTotal += perfectJump;
        }

        if(!doubleTap && GameManager.GetStarted() && tap)
        {
            //Debug.Log("entrei");
        }

        // duplo toque "mal feito", não deve fazer nada (SALTO NÃO PERFEITO).
        if (biggerDoubleTap && GameManager.GetStarted())
        {
            n_saltos_normais++;
            pontuacaoTotal += normalJump;
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
                // doubletap = true qdo a diferença entre os taps < 0.5f
                doubleTap = Time.time - lastTap < doubleTapDelta;

                //biggerDoubleTap = true qdo a diferença entre os taps > 0.5f e < 1.0f
                biggerDoubleTap = (Time.time - lastTap > doubleTapDelta) && (Time.time - lastTap < doubleTapDeltaBigger);
                lastTap = Time.time;
                oneFootJumping.Play();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {

            }

            if (doubleTap && GameManager.GetStarted())
            {
                n_saltos_perfeitos++;
                manJumping.Play();
                pontuacaoTotal += perfectJump;
            }

            // duplo toque "mal feito", não deve fazer nada (SALTO NÃO PERFEITO).
            if (biggerDoubleTap && GameManager.GetStarted())
            {
                n_saltos_normais++;
                pontuacaoTotal += normalJump;
            }
        }
    }

    //numero de saltos perfeitos no final
    public static int GetSaltosPerfeitos()
    {
        return n_saltos_perfeitos;
    }

    //numero de saltos normais no final
    public static int GetSaltosNormais()
    {
        return n_saltos_normais;
    }

    //obtem a pontuacao no final (pontuacao final pretty much)
    public static int GetPontuacao()
    {
        return pontuacaoTotal;
    }
}
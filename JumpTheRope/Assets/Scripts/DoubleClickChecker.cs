using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private float doubleTapDelta = 0.3f;
    private float doubleTapDeltaBigger = 1.0f;
    private static int n_saltos_total;
    private static int n_saltos_perfeitos;
    private static int n_saltos_normais;
    private static int pontuacaoTotal;

    private const int perfectJump = 100;
    private const int normalJump = 25;
    private const int doubleTapRadius = 100;

    private Touch currentTouch;
    private Touch previousTouch;
    private float currentTapTime;
    private float lastTapTime;
    private int doubleTapCircle;
    public static bool mainScreen;

    public AudioSource[] sounds;
    public AudioSource manJumping; // ManJumping
    public AudioSource oneFootJumping; // tap

    public Touch CurrentTouch { get { return currentTouch; } }
    public Touch PreviousTouch { get { return previousTouch; } }
    public float CurrentTapTime { get { return currentTapTime; } }
    public float LastTapTime { get { return lastTapTime; } }
    public int DoubleTapCircle { get { return doubleTapCircle; } }

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        manJumping = sounds[0];
        oneFootJumping = sounds[1];
        mainScreen = true;

        n_saltos_perfeitos = n_saltos_normais = pontuacaoTotal = n_saltos_total = 0;
        doubleTapCircle = doubleTapRadius * doubleTapRadius;
    }

    void Update()
    {
        if (Input.touchCount > 0 && GameManager.GetStarted())
        {
            mainScreen = false;
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oneFootJumping.Play();
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    manJumping.Play();
                    n_saltos_total++;
                    n_saltos_perfeitos++;
                    pontuacaoTotal += perfectJump;
                }
                else if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 1)
                {
                    n_saltos_total++;
                    n_saltos_normais++;
                    pontuacaoTotal += normalJump;
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                previousTouch = currentTouch;
                lastTapTime = currentTapTime;
            }
        }
    }

    private int CheckForDoubleTap(float currentTapTime, float previousTapTime, Touch currentTouch, Touch previousTouch)
    {
        int deltaX = (int)currentTouch.position.x - (int)previousTouch.position.x;
        int deltaY = (int)currentTouch.position.y - (int)previousTouch.position.y;

        // diferença entre os toques superior a 1s
        if (currentTapTime - previousTapTime > doubleTapDeltaBigger)
        {
            return -1;
        }

        // se a diferença entre os toques for menor que 1s e maior que 300ms então é salto normal
        if (currentTapTime - previousTapTime < doubleTapDeltaBigger && currentTapTime - previousTapTime > doubleTapDelta)
        {
            // se o duplo toque "normal" estiver dentro do circulo aceitavel, entao retorna 1
            if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
                return 1;
            else
                return -1;
        }

        // se o duplo toque "perfeito" estiver dentro do circulo aceitavel, entao retorna 0
        if (deltaX * deltaX + deltaY * deltaY < doubleTapCircle)
            return 0;
        return -1;
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

    //obtem o total de saltos feitos pelo utilizador
    public static int GetTotalSaltos()
    {
        return n_saltos_total;
    }

    public static void SetMainScreen(bool value)
    {
        mainScreen = value;
    }
}
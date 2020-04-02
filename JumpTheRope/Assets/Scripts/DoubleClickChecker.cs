using UnityEngine;

public class DoubleClickChecker : MonoBehaviour
{
    private static int n_saltos_perfeitos;
    private static int n_saltos_normais;
    private static int pontuacaoTotal;

    private float doubleTapDeltaBigger = Configuration.DoubleTapDeltaBigger();
    private float doubleTapDelta = Configuration.DoubleTapDelta();
    private int perfectJump = Configuration.PerfectJump();
    private int normalJump = Configuration.NormalJump();
    private int doubleTapRadius = Configuration.DoubleTapRadius();

    private Touch currentTouch;
    private Touch previousTouch;
    private float currentTapTime;
    private float lastTapTime;
    private int doubleTapCircle;

    public AudioSource manJumping; // ManJumping
    public AudioSource oneFootJumping; // tap
    public AudioSource saltoPerfeito; // salto perfeito
    public GameManager GameManagerGO;

    public bool isGoingToTouch;

    public Touch CurrentTouch { get { return currentTouch; } }
    public Touch PreviousTouch { get { return previousTouch; } }
    public float CurrentTapTime { get { return currentTapTime; } }
    public float LastTapTime { get { return lastTapTime; } }
    public int DoubleTapCircle { get { return doubleTapCircle; } }

    void Start()
    {
        n_saltos_perfeitos = n_saltos_normais = pontuacaoTotal = 0;
        doubleTapCircle = doubleTapRadius * doubleTapRadius;
        isGoingToTouch = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && GameManager.GetStarted())
        {
            System.Diagnostics.Debug.WriteLine("entrei aqui, o jogo já começou.");
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oneFootJumping.Play();
                currentTouch = touch;
                currentTapTime = Time.time;
                if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 0)
                {
                    manJumping.Play();
                    n_saltos_perfeitos++;
                    pontuacaoTotal += perfectJump;
                    saltoPerfeito.PlayDelayed(manJumping.clip.length);
                }
                else if (CheckForDoubleTap(currentTapTime, lastTapTime, currentTouch, previousTouch) == 1)
                {
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

        if(Input.touchCount > 0 && GameManager.GetPreGameplay())
        {
            System.Diagnostics.Debug.WriteLine("pregameplay -> agora vou receber mais toques");
            System.Diagnostics.Debug.WriteLine("entrei aqui, o jogo ainda nao comecou. Nº toques: " + Input.touchCount);
            isGoingToTouch = true;
            if (isGoingToTouch)
            {
                System.Diagnostics.Debug.WriteLine("yo");
            }
            isGoingToTouch = false;
            // nao faz nada, usado apenas para não fazer sons
            // no menu principal depois de perder.
            // poderá ser usado para verificar quais os botões a ser pressionados?
        }

        if(Input.touchCount > 0 && !GameManager.GetStarted() && !GameManager.GetPreGameplay())
        {
            System.Diagnostics.Debug.WriteLine("toques aqui? " + Input.touchCount);
        }

        /*
        if (GameManager.GetPreGameplay())
        {
            if(Input.touchCount > 1)
            {
                System.Diagnostics.Debug.WriteLine("ja entrei eheheheh");
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
            }
        }
        */
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
        return n_saltos_normais + n_saltos_perfeitos;
    }
}
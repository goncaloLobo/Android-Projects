using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClose : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource sair;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;

    void Start()
    {
        sair = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        sair.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            // qdo pressiona o botao de sair, muda para o estado inicial
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay || GameManager.GetCurrentState() == GameManager.GameManagerState.Instructions)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
            }
            else if (GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeDown || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeLeft || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeRight || GameManager.GetCurrentState() == GameManager.GameManagerState.SwipeUp)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
            }
        }
        lastTapTime = currentTapTime;
    }

    private bool CheckForDoubleTap(float currentTapTime, float previousTapTime)
    {
        if (currentTapTime - previousTapTime < clickdelay)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sair.Play();
    }
}
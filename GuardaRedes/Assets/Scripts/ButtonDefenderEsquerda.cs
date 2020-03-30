using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDefenderEsquerda : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource defenderParaEsquerda;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;

    void Start()
    {
        defenderParaEsquerda = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        defenderParaEsquerda.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeLeft);
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
        defenderParaEsquerda.Play();
    }
}
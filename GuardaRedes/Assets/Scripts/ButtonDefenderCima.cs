using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDefenderCima : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource defenderParaCima;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;

    void Start()
    {
        defenderParaCima = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        defenderParaCima.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeUp);
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
        defenderParaCima.Play();
    }
}

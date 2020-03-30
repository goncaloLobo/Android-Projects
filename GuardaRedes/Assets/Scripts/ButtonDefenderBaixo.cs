using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDefenderBaixo : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource defenderParaBaixo;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;

    void Start()
    {
        defenderParaBaixo = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        defenderParaBaixo.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeDown);
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
        defenderParaBaixo.Play();
    }
}
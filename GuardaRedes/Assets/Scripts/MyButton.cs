using UnityEngine;
using UnityEngine.EventSystems;
 
public class MyButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    float clickdelay = 0.5f;
    public AudioSource jogar;
    private float currentTapTime;
    private float lastTapTime;

    public GameObject GameManagerGO;

    void Start()
    {
        jogar = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        jogar.Play();
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameplay);
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
        jogar.Play();
    }
}

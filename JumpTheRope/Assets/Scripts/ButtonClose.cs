using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClose : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private float clickdelay = 0.5f;
    public AudioSource sair;
    private float currentTapTime;
    private float lastTapTime;

    private float delay = 0f;

    public GameObject GameManagerGO;

    void Start()
    {
        sair = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentTapTime = Time.time;
        sair.PlayDelayed(delay);
        delay += sair.clip.length;
        if (CheckForDoubleTap(currentTapTime, lastTapTime))
        {
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
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
        sair.PlayDelayed(delay);
        delay += sair.clip.length;       
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDefenderCima : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public AudioSource defenderParaCima;

    public GameObject GameManagerGO;

    void Start()
    {
        defenderParaCima = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            buttonPressed = true;
            defenderParaCima.Play();
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeUp);
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}

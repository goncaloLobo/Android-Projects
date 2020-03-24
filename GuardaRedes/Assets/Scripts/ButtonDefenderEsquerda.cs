using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDefenderEsquerda : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public AudioSource defenderParaEsquerda;

    public GameObject GameManagerGO;

    void Start()
    {
        defenderParaEsquerda = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            defenderParaEsquerda.Play();
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.SwipeLeft);
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        defenderParaEsquerda.Play();
    }
}
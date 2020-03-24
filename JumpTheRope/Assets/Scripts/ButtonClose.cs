using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClose : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public AudioSource sair;

    public GameObject GameManagerGO;

    void Start()
    {
        sair = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            sair.Play();
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            // qdo pressiona o botao de sair, muda para o estado inicial.
            if (GameManager.GetCurrentState() == GameManager.GameManagerState.Gameplay)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Opening);
            }
            
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sair.Play();
    }
}

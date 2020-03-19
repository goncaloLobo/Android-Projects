using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInstrucoes : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public AudioSource instrucoes;

    public GameObject GameManagerGO;

    void Start()
    {
        instrucoes = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            buttonPressed = true;
            instrucoes.Play();
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Instructions);
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTempo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public bool buttonPressed;
    float clicked = 0;
    public AudioSource tempo;

    public GameObject GameManagerGO;

    void Start()
    {
        tempo = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1)
        {
            buttonPressed = true;
            tempo.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tempo.Play();
    }
}
